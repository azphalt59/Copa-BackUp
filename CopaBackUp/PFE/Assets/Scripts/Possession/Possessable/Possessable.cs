using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Possessable : PhantomaticObject, IPossessable
{
    public enum ObjectType
    {
        Small, Medium, Big
    }
    public ObjectType objectType = ObjectType.Small;

    [SerializeField] private Material glowMat;
    private Material normalMat;
    [SerializeField] private MeshRenderer meshReference;
    [SerializeField] private List<MeshRenderer> meshes;

    [SerializeField] private Vector3 camPositionOffset;
    CameraController camController;

    [Header("Ref")]
    PlayerMovement playerMovement;
    PossManager possManager;

    private void Start()
    {
        normalMat = meshReference.sharedMaterial;
        camController = CameraController.Instance;

        playerMovement = PlayerMovement.Instance;
        possManager = PossManager.Instance;
    }
    private void Update()
    {
        PlayerDistance = Vector3.Distance(playerMovement.transform.position, transform.position);
        if (possManager.PossState != PossManager.PossessionState.Free)
            return;

        if (CheckDistance())
        {
            Glow();
        }
        else if(!CheckDistance())
        {
            UnGlow();
        }
    }
    private void OnMouseEnter()
    {
        if (possManager.PossState != PossManager.PossessionState.Free)
            return;

        if (CheckDistance())
        {
            // apply effect when mouse enter
        }
    }
    private void OnMouseExit()
    {
        // apply effect when mouse exit
    }
    public void Glow()
    {
        foreach (MeshRenderer meshR in meshes)
        {
            meshR.material = glowMat;
        }
    }  
    public void UnGlow()
    {
        if (meshes[0].material == normalMat)
            return;

            foreach (MeshRenderer meshR in meshes)
            {
                meshR.material = normalMat;
            }
    }
    public void BeingPossessed()
    {
        playerMovement.canRelease = false;
        playerMovement.gameObject.GetComponent<Collider>().enabled = false;
        possManager.PossState = PossManager.PossessionState.InPossession;

        playerMovement.SetMeshColl(GetComponent<MeshCollider>());

        if (gameObject.transform.parent.gameObject.GetComponent<PossessableParent>() != null)
        {
            possManager.SetPossItem(gameObject.transform.parent.gameObject);
        }
        else
            possManager.SetPossItem(gameObject.transform.parent.gameObject.transform.parent.gameObject);

        if(possManager.GetLastPossItem() != null)
            possManager.GetLastPossItem().GetComponent<PossessableParent>().LayerChangement(0);

        possManager.GetPossItem().GetComponent<PossessableParent>().LayerChangement(6);
        playerMovement.gameObject.GetComponent<Rigidbody>().useGravity = false;
        playerMovement.gameObject.GetComponent<Collider>().isTrigger = true;

        possManager.GetPossItem().GetComponent<PossessableParent>().PossMainGameObject.GetComponent<Rigidbody>().useGravity = true;
        
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<MeshCollider>().isTrigger = true;

        CameraController.CameraSetting camSettings = new CameraController.CameraSetting(); 
        if (objectType == ObjectType.Small)
        {
            camSettings = camController.SmallObjects;
        }
        if (objectType == ObjectType.Medium)
        {
            camSettings = camController.MediumObjects;
        }
        if (objectType == ObjectType.Big)
        {
            camSettings = camController.BigObjects;
        }
        camController.SetCameraSetting(camSettings);

        playerMovement.MovementPlayerToPossItem(transform.position);


    }
    private void OnMouseDown()
    {
        if (InPossessionRange)
        {
            if (possManager.PossState != PossManager.PossessionState.Free)
                return;
            BeingPossessed();
        }
        else
        {
            Debug.Log(gameObject.name + " is too far");
            Debug.Log(PlayerDistance);
        }
          
    }

    public Vector3 GetCamOffset()
    {
        return camPositionOffset;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, PossessionRange);
    }

}
