using Articy.Copacetic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Copacetic;
using Articy.Unity;

public abstract class CustomMethodProvider : MonoBehaviour, IScriptMethodProvider
{
    public bool IsCalledInForecast { get; set; }

    public void PathTo(string aParam0, string aParam1)
    {
        if(!IsCalledInForecast)
        {
            ArticySceneFlow.Current.Instructions++;

            Entity self   = ArticyDatabase.GetObject<Entity>(aParam0);
            Entity target = ArticyDatabase.GetObject<Entity>(aParam1);

            if(self != null && target != null)
            {
                PathTo(self, target);
            }
            else
            {
                Debug.LogError("invalid params in method");
                ArticySceneFlow.Current.Instructions--;
            }
        }
    }
    protected abstract void PathTo(Entity self, Entity target);

    public void Teleport(string aParam0, string aParam1)
    {
        if (!IsCalledInForecast)
        {
            Entity self   = ArticyDatabase.GetObject<Entity>(aParam0);
            Entity target = ArticyDatabase.GetObject<Entity>(aParam1);

            if (self != null && target != null)
            {
                Teleport(self, target);
            }
            else
            {
                Debug.LogError("invalid params in method");
            }
        }
    }
    protected abstract void Teleport(Entity self, Entity target);

    public bool ProximityCheck(string aParam0, string aParam1, int aParam2)
    {
        if (!IsCalledInForecast)
        {
            Entity self = ArticyDatabase.GetObject<Entity>(aParam0);
            Entity target = ArticyDatabase.GetObject<Entity>(aParam1);

            if (self != null && target != null)
            {
                bool value = ProximityCheck(self, target, aParam2);
                Debug.Log($"ProximityCheck : {value}");
                return value;
            }
            else
            {
                Debug.LogError("invalid params in method");
            }
        }
        return false;
    }

    protected abstract bool ProximityCheck(Entity self, Entity target, int distance);

    public void Unlock(string aParam0)
    {
        if (!IsCalledInForecast)
        {
            Entity unlock = ArticyDatabase.GetObject<Entity>(aParam0);

            if (unlock != null)
            {
                Unlock(unlock);
            }
            else
            {
                Debug.LogError("invalid params in method");
            }
        }
    }
    protected abstract void Unlock(Entity unlock);
}