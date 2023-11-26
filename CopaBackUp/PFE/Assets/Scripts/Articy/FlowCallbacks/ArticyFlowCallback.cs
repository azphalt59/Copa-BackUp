using Articy.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ArticyFlowCallback : MonoBehaviour, IArticyFlowPlayerCallbacks
{
    public static ArticyFlowCallback Current;

    public int Instructions;
    protected ArticyFlowPlayer _flowPlayer;
    public abstract void OnBranchesUpdated(IList<Branch> aBranches);

    public abstract void OnFlowPlayerPaused(IFlowObject aObject);

    public void PlayNext()
    {
        Current = this;
        Instructions = 0;

        _flowPlayer.Play();
    }
}