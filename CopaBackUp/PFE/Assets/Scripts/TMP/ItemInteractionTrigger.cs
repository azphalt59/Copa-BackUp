using Articy.Copacetic;
using Articy.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractionTrigger : MonoBehaviour
{
    public ArticyRef Interaction;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerMovement player))
        {
            ArticySceneFlow sceneFlow = GameObject.Instantiate(ArticyFlowManager.Instance.ArticySceneFlowPrefab, ArticyFlowManager.Instance.transform);
            
            sceneFlow.Scene = Interaction.GetObject<FlowFragment>();


            Destroy(this);
        }
    }
}
