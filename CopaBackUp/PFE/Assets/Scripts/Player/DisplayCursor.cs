using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayCursor : MonoBehaviour
{
    [SerializeField] private GameObject cursor;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        if (PossManager.Instance.PossState == PossManager.PossessionState.InPossession)
            cursor.SetActive(false);
        else
            cursor.SetActive(true);
    }
}
