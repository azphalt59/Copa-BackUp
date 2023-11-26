using Articy.Unity;
using Articy.Copacetic;
using Articy.Unity.Interfaces;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using Unity.VisualScripting;

public class ArticyMethodProvider : CustomMethodProvider
{
    public static ArticyMethodProvider Instance { get; private set; }


    private void Awake()
    {
        Instance = this;
        ArticyDatabase.DefaultMethodProvider = this;
    }
    
    protected override void PathTo(Entity self, Entity target)
    {
        if (ArticyEntityManager.EntityToGameObject.TryGetValue(target, out GameObject targetGO) && ArticyEntityManager.EntityToGameObject.TryGetValue(self, out GameObject selfGO) && selfGO.TryGetComponent(out DetectorNavMesh agent))
        {
            Debug.LogWarning("PathTo");
            agent.SetDestinationWayPoints(new List<Transform>() { targetGO.transform });

            ArticyFlowCallback scene = ArticyFlowCallback.Current;
            agent.OnDestinationReached = () => { scene.PlayNext(); };
        }
        else
        {
            Debug.LogError("entity representations not found");
            ArticySceneFlow.Current.Instructions--;
        }
    }

    protected override void Teleport(Entity self, Entity target)
    {
        Debug.Log($"Teleport {self.DisplayName} in scene : {ArticySceneFlow.Current.name}");

        if (ArticyEntityManager.EntityToGameObject.TryGetValue(target, out GameObject targetGO) && ArticyEntityManager.EntityToGameObject.TryGetValue(self, out GameObject selfGO))
        {
            selfGO.transform.position = targetGO.transform.position;
        }
        else
        {
            Debug.LogError("entity representations not found");
        }
    }

    protected override bool ProximityCheck(Entity self, Entity target, int distance)
    {
        if (ArticyEntityManager.EntityToGameObject.TryGetValue(target, out GameObject targetGO) && ArticyEntityManager.EntityToGameObject.TryGetValue(self, out GameObject selfGO))
        {
            return Vector2.Distance(new Vector2(selfGO.transform.position.x, selfGO.transform.position.z), new Vector2(targetGO.transform.position.x, targetGO.transform.position.z)) < distance;
        }
        return false;
    }

    protected override void Unlock(Entity unlock)
    {
        if(ArticyEntityManager.EntityToGameObject.TryGetValue(unlock, out GameObject unlockGO) && unlockGO.TryGetComponent(out ArticyRoom room))
        {
            room.Unlock();
        }
    }
}
