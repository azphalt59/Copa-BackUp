using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;

    CameraController cameraController;
    PossManager possManager;

    [SerializeField] private CinemachineBrain brain;
    [SerializeField] private CinemachineFreeLook freeLook;
    private float freelookXMaxSpeed;
    private float freelookYMaxSpeed;

    [SerializeField] private GameObject playerParent;
    [SerializeField] private GameObject cam;
    [SerializeField] private MeshCollider PossItemColliderShape;
    [SerializeField] private GameObject Mesh;

    [SerializeField] public bool canRelease = false;
    public float PlayerToPoss;
    [Header("First Person Mvt")]
    [SerializeField] private float firstPersonSpeed = 2f;

    [Header("Third Person Mvt")]
    [SerializeField] private float thirdPersonSpeed = 2f;

    [SerializeField] public bool isMoving = false;
    [HideInInspector] public bool playerMvtTransition = false;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        cameraController = CameraController.Instance;
        possManager = PossManager.Instance;

        freelookYMaxSpeed = freeLook.m_YAxis.m_MaxSpeed;
        freelookXMaxSpeed = freeLook.m_XAxis.m_MaxSpeed;
    }
    public bool GetIsMoving()
    {
        return isMoving;
    }
    void Update()
    {

        DisableRotationWhenBlending();
        CheckIfMoving();
       

        if(possManager.PossState == PossManager.PossessionState.Free)
        {
            FirstPersonMvt();
            PossItemColliderShape.enabled = false;
        }
           
        if (possManager.PossState == PossManager.PossessionState.InPossession)
        {
            if(Input.GetMouseButtonDown(1))
            {
                if (canRelease == false)
                    return;
                ReleaseFromPossession();
                return;
            }
            PossItemColliderShape.enabled = true;
            ThirdPersonMvt();
        }
    }
    private void DisableRotationWhenBlending()
    {
        if (brain.IsBlending)
        {
            freeLook.m_YAxis.m_MaxSpeed = 0;
            freeLook.m_XAxis.m_MaxSpeed = 0;
        }
        if (brain.IsBlending == false)
        {
            freeLook.m_YAxis.m_MaxSpeed = freelookYMaxSpeed;
            freeLook.m_XAxis.m_MaxSpeed = freelookXMaxSpeed;
        }
    }
    private void CheckIfMoving()
    {
        // check if moving
        if (Mathf.Approximately(GetComponent<Rigidbody>().velocity.magnitude, 0))
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
    }
    public void FirstPersonMvt()
    {
        Vector3 inputVector;
        inputVector.x = Input.GetAxisRaw("Horizontal");
        inputVector.z = Input.GetAxisRaw("Vertical");

        Vector3 vertMvt = transform.TransformDirection(Vector3.forward) * Input.GetAxis("Vertical") * firstPersonSpeed;
        Vector3 horiMvt = transform.TransformDirection(Vector3.right) * Input.GetAxis("Horizontal") * firstPersonSpeed;
        transform.position += (vertMvt * Time.deltaTime) + (horiMvt * Time.deltaTime);
    }

    public void ThirdPersonMvt()
    {
        if (brain.IsBlending)
            return;

        Vector3 inputVector;
        inputVector.x = Input.GetAxisRaw("Horizontal");
        inputVector.z = Input.GetAxisRaw("Vertical");

        Vector3 camDir = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 movement = inputVector.z * camDir + inputVector.x * Camera.main.transform.right;

        if (Mesh != null)
            Mesh.transform.LookAt(Mesh.transform.position + movement);

        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + movement.normalized * thirdPersonSpeed * Time.deltaTime);
    }
    public void MovementPlayerToPossItem(Vector3 PosPossItem)
    {
        playerMvtTransition = true;
        transform.DOMove(PosPossItem, PlayerToPoss).OnComplete(CameraSwitch);
    }
    public void CameraSwitch()
    {
        playerMvtTransition = false;
        possManager.GetPossItem().transform.parent = playerParent.transform;
        CameraController.Instance.SetCameraPriorities(CameraController.Instance.FirstPerson, CameraController.Instance.ThirdPerson);
        canRelease = true;
        
        GetComponent<Rigidbody>().useGravity = true;

        possManager.GetPossItem().GetComponent<PossessableParent>().PossMainGameObject.GetComponent<MeshCollider>().isTrigger = false;
        possManager.GetPossItem().GetComponent<PossessableParent>().PossMainGameObject.GetComponent<Rigidbody>().useGravity = true;
        possManager.GetPossItem().SetActive(false);

        CreateMesh(possManager.GetPossItem().GetComponent<PossessableParent>().MeshPrefab, possManager.GetPossItem().GetComponent<PossessableParent>().objectActivation);
    }

    public void ReleaseFromPossession()
    {
        if (Mesh == null)
            return;

        Quaternion meshRotation = Mesh.transform.rotation;
        //SetMeshColl(null);

        PossManager.Instance.activation = null;
        Destroy(Mesh);

        transform.rotation = Quaternion.Euler(-90, 0, 0);

        GetComponent<Collider>().enabled = true;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<MeshCollider>().enabled = false;
        GetComponent<Collider>().isTrigger = false;

        possManager.GetPossItem().GetComponent<PossessableParent>().PossMainGameObject.GetComponent<MeshCollider>().isTrigger = false;
        possManager.GetPossItem().GetComponent<PossessableParent>().ResetPosition(new Vector3(transform.position.x, transform.position.y, transform.position.z));
        possManager.GetPossItem().transform.rotation = meshRotation;
        possManager.GetPossItem().GetComponent<PossessableParent>().gameObject.SetActive(true);


        possManager.PossState = PossManager.PossessionState.Free;
        possManager.GetPossItem().transform.parent = null;
        possManager.SetLastPossItem(PossManager.Instance.GetPossItem());
        possManager.SetPossItem(null);
        cam.transform.parent = transform;
        
        cameraController.SetCameraPriorities(cameraController.ThirdPerson, cameraController.FirstPerson);
        
    }

    public void SetMeshColl(MeshCollider meshCol)
    {
        PossItemColliderShape.sharedMesh = meshCol.sharedMesh;
    }

   
    public void CreateMesh(GameObject mesh, ObjectActivation objActivation)
    {
        GameObject possededMesh = Instantiate(mesh, transform.position, possManager.GetPossItem().transform.rotation, transform);
        ObjectActivation copy =  CopyComponent(objActivation, possededMesh);
        possManager.activation = copy;
        copy.InverseIsPossessed();
        possededMesh.name = mesh.name;
        Mesh = possededMesh;
        possededMesh.SetActive(true);
        possededMesh.name = "created " + possededMesh.name;
        possManager.GetPossItem().transform.parent = possededMesh.transform;
    }

    public static T CopyComponent<T>(T original, GameObject destination) where T : Component
    {
        var type = original.GetType();
        var copy = destination.AddComponent(type);
        var fields = type.GetFields();
        foreach (var field in fields) field.SetValue(copy, field.GetValue(original));


        if (copy.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            Destroy(rb);
        }
        if (copy.TryGetComponent<MeshCollider>(out MeshCollider col))
        {
            Destroy(col);
        }
        foreach (Transform child in copy.transform)
        {
            if (child.TryGetComponent<Rigidbody>(out Rigidbody rbChild))
            {
                Destroy(rbChild);
            }
            MeshCollider[] childCollider = child.GetComponentsInChildren<MeshCollider>();
            foreach(MeshCollider childCol in childCollider)
            {
                Destroy(childCol);
            }
        }

       

        return copy as T;
    }
}
