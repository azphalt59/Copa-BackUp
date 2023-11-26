using Articy.Unity;
using Articy.Copacetic;
using Articy.Unity.Interfaces;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class ArticyDialogueManager : MonoBehaviour
{
    public static ArticyDialogueManager Instance;
    


    private void Awake()
    {
        Instance = this;
    }

    [Header("Refs")]
    public Transform DialogueUIContainer;
    public ArticyDialogueText DialoguePrefab;


    public void Dialogue(DialogueFragment dialogue)
    {
        //Debug.LogWarning("is it working ?");

        Vector3 position = Vector3.zero;
        Color color = Color.white;
        string title = dialogue.name;
        string text = dialogue.Text;
        float waitTime = 5;
        Transform parent = null;

        var speakerEntity = dialogue.Speaker as Entity;
        if(speakerEntity != null)
        {
            title = speakerEntity.DisplayName;
            color = speakerEntity.Color;

            if(ArticyEntityManager.EntityToGameObject.TryGetValue(speakerEntity, out GameObject gameObject))
            {
                position = gameObject.transform.position + Vector3.down;
                parent = gameObject.transform;
            }
        }
        
        var waitTimeObject = dialogue as WaitAfterDialogue;
        if(waitTimeObject != null)
        {
            waitTime = waitTimeObject.GetFeatureWaitTime().Time;
        }

        //SpeakAt(position, color, title, text, waitTime, parent);
        SpeakUI(color, title, text, waitTime);
    }

    public void SpeakUI(Color color, string title, string text, float waitTime)
    {
        ArticyDialogueText dialogueText = GameObject.Instantiate(DialoguePrefab, DialogueUIContainer);

        dialogueText.Color = color;
        dialogueText.Text = text;
        dialogueText.Title = title;
        dialogueText.WaitTime = waitTime;
    }


    public void SpeakAt(Vector3 position, Color color, string title, string text, float waitTime, Transform parent)
    {
        ArticyDialogueText dialogueText = GameObject.Instantiate(DialoguePrefab, position, Quaternion.identity);

        dialogueText.Color = color;
        dialogueText.Text = text;
        dialogueText.Title = title;
        dialogueText.WaitTime = waitTime;
        dialogueText.transform.SetParent(parent);
    }
}
