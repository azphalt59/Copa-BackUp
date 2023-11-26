using Articy.Copacetic;
using Articy.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArticyFlowManager : MonoBehaviour
{
    public static ArticyFlowManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    [Header("UI Prefabs")]
    public ArticyUIScene SceneUIPrefab;
    public ArticyUIDialogueFragment DialogueFragmentUIPrefab;
    public ArticyUICondition ConditionUIPrefab;
    public ArticyUIInstruction InstructionUIPrefab;

    [Header("Flow Prefabs")]
    public ArticySceneFlow ArticySceneFlowPrefab;

    [Header("Refs")]
    public RectTransform FlowContent;

    private List<ArticyFlowCallback> _allFlows = new List<ArticyFlowCallback>();

    public void Add(ArticyFlowCallback flow)
    {
        _allFlows.Add(flow);
    }
    public void Remove(ArticyFlowCallback flow)
    {
        _allFlows.Remove(flow);
    }
    public void CreateFrom(ArticyReference articyReference)
    {
        var scene = ((ArticyObject)articyReference.reference) as SubScene;

        if (scene != null)
        {/*
            ArticyRoom room = GetRoomFromName(scene.GetFeatureFromRoom().EnumValue.GetDisplayName());

            Transform parent = room.UI.transform;

            AddSubScene(scene, parent, false);*/
        }
    }
}
