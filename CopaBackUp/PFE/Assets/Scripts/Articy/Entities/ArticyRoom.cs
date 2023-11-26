using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArticyRoom : MonoBehaviour
{
    public GameObject RoomBlocker;

    public void Unlock()
    {
        RoomBlocker?.SetActive(false);
    }
}
