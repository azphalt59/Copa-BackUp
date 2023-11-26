using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyAnimController : MonoBehaviour
{
    [SerializeField] private Animator dummyAnim;

    
    private void Start()
    {
        dummyAnim = GetComponent<Animator>();
    }
    public void Unscared()
    {
        dummyAnim.SetBool("isScared", false);
    }
    public void StopRunAway()
    {
        dummyAnim.SetBool("RunAway", false);
    }
    public void Walk()
    {
        dummyAnim.SetBool("RunAway", true);
    }
    public void Sleep()
    {
        dummyAnim.SetBool("IsSleeping", true);
    }
    public void UnSleep()
    {
        dummyAnim.SetBool("IsSleeping", false);
    }



}
