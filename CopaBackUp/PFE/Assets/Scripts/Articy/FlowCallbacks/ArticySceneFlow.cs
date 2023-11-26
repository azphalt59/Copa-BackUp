using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Articy.Unity;
using Articy.Unity.Interfaces;
using Articy.Copacetic;
using System;
using UnityEditor.PackageManager;

[RequireComponent(typeof(ArticyFlowPlayer))]
public class ArticySceneFlow : ArticyFlowCallback
{
    public ArticyRef SubScene;
    public FlowFragment Scene;
    public ArticyUIScene UIScene;
    public ArticySceneFlow Parent;
    
    private void Start()
    {
        if (Scene == null)
        {
            Scene = SubScene.GetObject<FlowFragment>();
            if (Scene == null)
            {
                Destroy(gameObject);
                return;
            }
        }

        // add a UI representation
        Transform parentTransform;
        if (Parent == null)
        {
            parentTransform = ArticyFlowManager.Instance.FlowContent;
        }
        else
        {
            parentTransform = Parent.UIScene.transform;
        }

        UIScene = GameObject.Instantiate(ArticyFlowManager.Instance.SceneUIPrefab, parentTransform);
        UIScene.ConstructFrom(Scene);

        // name it so i'm not lost
        name = UIScene.name;


        ArticyFlowManager.Instance.Add(this);


        _flowPlayer = GetComponent<ArticyFlowPlayer>();
        _flowPlayer.StartOn = Scene;
    }

    public override void OnFlowPlayerPaused(IFlowObject aObject)
    {
        string displayName = (aObject as IObjectWithDisplayName)?.DisplayName;
        string text = (aObject as IObjectWithText)?.Text;
        
        Debug.Log(name + " >\n" + displayName + " : " + text);

        // if it pause on itself, it need to ignore it
        if (aObject == Scene)
        {
            Debug.Log("same scene, we skip");
            PlayNext();
            return;
        }


        // All differents cases depending on type.

        #region DialogueFragment
        var dialogueFragment = aObject as DialogueFragment;
        if (dialogueFragment != null)
        {
            OnDialogueFragment(dialogueFragment);
            return;
        }
        #endregion
        #region Conditions
        var condition = aObject as Condition;
        if (condition != null)
        {
            OnCondition(condition);
            return;
        }
        #endregion
        #region Instrictions
        var instruction = aObject as Instruction;
        if (instruction != null)
        {
            OnInstruction(instruction);
            return;
        }
        #endregion

        #region SubScene
        // if it's a SubScene we launch it separatly
        var subScene = aObject as SubScene;
        if (subScene != null)
        {
            //Debug.Log("SubScene");
            UIScene.CloseScene();
            ArticySceneFlow sceneFlow = GameObject.Instantiate(ArticyFlowManager.Instance.ArticySceneFlowPrefab, ArticyFlowManager.Instance.transform);
            sceneFlow.Scene = subScene;
            sceneFlow.Parent = this;
            return;
        }
        #endregion

        //if it's none of them play
        //PlayNext();
    }

    private void OnDialogueFragment(DialogueFragment dialogue)
    {
        ArticyUIDialogueFragment ui = GameObject.Instantiate(ArticyFlowManager.Instance.DialogueFragmentUIPrefab, UIScene.transform);
        ui.ConstructFrom(dialogue);

        float waitTime = 5;
        WaitAfterDialogue waitDialogue = dialogue as WaitAfterDialogue;
        if (waitDialogue != null)
        {
            waitTime = waitDialogue.GetFeatureWaitTime().Time;
        }
        StartCoroutine(PlayAfter(waitTime));

        ArticyDialogueManager.Instance.Dialogue(dialogue);
    }

    private void OnCondition(Condition condition)
    {
        ArticyUICondition ui = GameObject.Instantiate(ArticyFlowManager.Instance.ConditionUIPrefab, UIScene.transform);
        ui.ConstructFrom(condition);

        PlayNext();
    }

    private void OnInstruction(Instruction instruction)
    {
        ArticyUIInstruction ui = GameObject.Instantiate(ArticyFlowManager.Instance.InstructionUIPrefab, UIScene.transform);
        ui.ConstructFrom(instruction);

        if (Instructions == 0)
            PlayNext();
    }

    public override void OnBranchesUpdated(IList<Branch> aBranches)
    {
        // destroy this if the branch is empty
        if (aBranches == null || aBranches.Count == 0)
        {
            UIScene.CloseScene();
            ArticyFlowManager.Instance.Remove(this);
            Destroy(this.gameObject);
            //Debug.LogWarning("End of scene : " + name);
        }
    }
    IEnumerator PlayAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        //Debug.Log("playing");
        PlayNext();
    }


}

