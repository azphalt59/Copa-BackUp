using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PossManager : MonoBehaviour
{
    public static PossManager Instance;
    public enum PossessionState
    {
        Free, InPossession
    }
    public PossessionState PossState;
    [SerializeField] private CinemachineBrain brain;
  
    private GameObject possItem;
    private GameObject lastPossItem;


    [Tooltip("Si true, les objets possédables changent de texture en étant proche d'eux, sinon seulement en les regardant")] public bool glowWithRange = true;

    [SerializeField] public ObjectActivation activation;
    


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }
    void Start()
    {
        PossState = PossessionState.Free;
    }
    public void SetState(PossessionState state)
    {
        PossState = state;
    }

    public void PossessionEntry()
    {
        PossState = PossessionState.InPossession;
    }

    public void SetPossItem(GameObject item)
    {
        possItem = item;
    }
    
    public GameObject GetPossItem()
    {
        return possItem;
    }
   
    public void SetLastPossItem(GameObject item)
    {
        lastPossItem = item;
    }
    public GameObject GetLastPossItem()
    {
        return lastPossItem;
    }
}
