//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Rendering;
//using UnityEngine.Rendering.Universal;


//public class PhantomState : MonoBehaviour
//{
//    [SerializeField] private Volume volume;
//    [SerializeField] private Vignette vignette;
//    [SerializeField] private List<PhantomaticWall> phantomaticWalls;

//    [SerializeField] private bool InPhantomaticWall = false;
//    [SerializeField] private bool HaveToUntrigger = false;


//    // Start is called before the first frame update
//    void Start()
//    {
//        if(volume.profile.TryGet<Vignette>(out vignette))
//        {}
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        PhantomStateChangement();   
//    }
//    public void PhantomStateChangement()
//    {
//        if (PossManager.Instance.possessionState == PossManager.PossessionState.Free)
//            PhantomObjectTracking();
//        else
//            SetNrmMat();
//    }
   
//    public void AddPhantomaticWall(PhantomaticWall phantomaticWall)
//    {
//        phantomaticWalls.Add(phantomaticWall);
//    }
//    private void PhantomaticCol(bool condition)
//    {
//        foreach (PhantomaticWall phantomaticWall in phantomaticWalls)
//        {
//            phantomaticWall.gameObject.GetComponent<Collider>().isTrigger = condition;
//        }
//    }
//    private void OnTriggerEnter(Collider other)
//    {
//        if(other.gameObject.GetComponent<PhantomaticWall>() != null)
//        {
//            InPhantomaticWall = true;
//        }
//    }
//    private void OnTriggerExit(Collider other)
//    {
//        if (other.gameObject.GetComponent<PhantomaticWall>() != null)
//        {
//            if(HaveToUntrigger)
//            {
//                PhantomaticCol(false);
//                HaveToUntrigger = false;
//            }
//            InPhantomaticWall = false;
//        }
//    }
//    public void PhantomObjectTracking()
//    {
//        foreach (PhantomaticWall phantomaticWall in phantomaticWalls)
//        {
//            phantomaticWall.SetEmissiveMat();
//        }
//    }
//    public void SetNrmMat()
//    {
//        foreach (PhantomaticWall phantomaticWall in phantomaticWalls)
//        {
//            phantomaticWall.SetNormalMat();
//        }
//    }

//}
