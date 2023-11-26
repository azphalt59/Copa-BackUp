using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomaticObject : MonoBehaviour
{
    public float PossessionRange;
    public bool InPossessionRange;
    public float PlayerDistance;

    private void Update()
    {
        CheckDistance();
    }

    public bool CheckDistance()
    {
        PlayerDistance = Vector3.Distance(PlayerMovement.Instance.transform.position, transform.position);
        if (PossessionRange >= PlayerDistance)
        {
            InPossessionRange = true;
            return true;
        }
        else
        {
            InPossessionRange= false;
            return false;
        }
    }
}
