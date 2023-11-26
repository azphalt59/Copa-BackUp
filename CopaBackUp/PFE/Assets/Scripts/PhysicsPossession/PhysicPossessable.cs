using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicPossessable : MonoBehaviour
{
    [Header("Refs")]
    public Rigidbody Body;
    [SerializeField]
    private Transform _pivot;
    [SerializeField]
    private Transform _cameraPivot;


    // Getters
    public Transform Pivot { get => _pivot != null ? _pivot : transform; }
    public Transform CameraPivot { get => _cameraPivot != null ? _cameraPivot : Pivot; }

    [Header("GD")]
    public float Speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        if(Body == null)
        {
            Body = GetComponent<Rigidbody>();
        }
    }
}
