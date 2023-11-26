using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MarmiteTest : ObjectActivation
{
    // Start is called before the first frame update
    [SerializeField] private GameObject couvercle;
    [SerializeField] private float verticalOffset = 0.4f;
    [SerializeField] private float duration = 0.3f;
    bool isWaiting = false;

    void Start()
    {
        if (transform.GetChild(0).transform.childCount == 0)
            return;

        couvercle = transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            if (isWaiting)
                return;

            Activation();
        }
    }

    public override void Activation()
    {
        base.Activation();
        if (!isPossessed)
            return;
        isWaiting = true;
        InverseIsActive();
        couvercle.transform.DOLocalMoveY (transform.position.y + verticalOffset, duration).OnComplete(Desactivation);
    }

    public void Desactivation()
    {
        couvercle.transform.DOLocalMoveY(0, duration).OnComplete(ActivationIsDone);
    }

    public void ActivationIsDone()
    {
        isWaiting = false;
        InverseIsActive();
    }
}
