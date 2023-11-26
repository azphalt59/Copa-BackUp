using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessableParent : MonoBehaviour
{
    public GameObject PossMainGameObject;
    public GameObject MeshPrefab;
    public ObjectActivation objectActivation;

    public void CreateMesh()
    {
        PlayerMovement.Instance.CreateMesh(MeshPrefab, objectActivation);
    }
   

    public void LayerChangement(int layerIndex)
    {
        gameObject.layer = layerIndex;
        foreach (Transform item in transform)
        {
            item.gameObject.layer = layerIndex;
            if (item.childCount > 0)
            {
                for (int i = 0; i < item.childCount; i++)
                {
                    item.GetChild(i).gameObject.layer = layerIndex;
                    if(item.GetChild(i).childCount > 0)
                    {
                        for (int j = 0; j < item.GetChild(i).childCount; j++)
                        {
                            item.GetChild(i).GetChild(j).gameObject.layer = layerIndex;
                            if (item.GetChild(i).GetChild(j).childCount > 0)
                            {
                                for (int k = 0; k < item.GetChild(i).childCount; k++)
                                {
                                    item.GetChild(i).GetChild(j).GetChild(k).gameObject.layer = layerIndex;
                                }
                            }
                        }
                    }

                }
            }

        }
    }

    public void ResetPosition(Vector3 playerPos)
    {
        gameObject.transform.position = playerPos;
        foreach (Transform item in transform)
        {
            item.gameObject.transform.localPosition = Vector3.zero;
            if (item.childCount > 0)
            {
                for (int i = 0; i < item.childCount; i++)
                {
                    item.GetChild(i).gameObject.transform.localPosition = Vector3.zero;
                    if (item.GetChild(i).childCount > 0)
                    {
                        for (int j = 0; j < item.GetChild(i).childCount; j++)
                        {
                            item.GetChild(i).GetChild(j).gameObject.transform.localPosition = Vector3.zero;
                            if (item.GetChild(i).GetChild(j).childCount > 0)
                            {
                                for (int k = 0; k < item.GetChild(i).childCount; k++)
                                {
                                    item.GetChild(i).GetChild(j).GetChild(k).gameObject.transform.localPosition = Vector3.zero;
                                }
                            }
                        }
                    }

                }
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerMovement>() != null)
        {
            Debug.Log(other.name);
            LayerChangement(0);
        }
    }
}
