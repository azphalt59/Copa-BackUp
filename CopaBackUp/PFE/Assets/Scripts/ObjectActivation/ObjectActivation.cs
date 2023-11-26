using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActivation : MonoBehaviour
{
    [SerializeField] public bool isActive;
    [SerializeField] public bool isPossessed;

    public virtual void Activation()
    {
        

    }

    public void InverseIsActive()
    {
        isActive = !isActive;
    }

    public void InverseIsPossessed() 
    {  
        isPossessed = !isPossessed; 
    }

    public bool GetIsPossessed()
    { return isPossessed; }

    public bool GetIsActive()
    { return isActive; }
}
