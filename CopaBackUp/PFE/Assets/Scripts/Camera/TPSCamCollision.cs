using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TPSCamCollision : MonoBehaviour
{
    [SerializeField] private CinemachineCollider camCollider;
    [SerializeField] private bool collisionRender = true;
    public float radius;
    public List<Collider> colliders;
    public Camera Cam;
    public GameObject player;
    public float raySize;
    public LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (collisionRender == false)
        {
            if(colliders.Count != 0)
            {
                colliders.Clear();
            }
            return;
        }
           

        Vector3 playerScreenPos = Cam.WorldToScreenPoint(player.transform.position);
        Ray ray = Cam.ScreenPointToRay(playerScreenPos);
        RaycastHit raycastHit;
        if(Physics.SphereCast(ray, raySize,out raycastHit, raySize, layerMask))
        {
            if(raycastHit.collider != null)
            {
                Debug.Log("Collide avec " + raycastHit.collider.gameObject.name);
                if(colliders.Contains(raycastHit.collider) == false)
                {
                    colliders.Add(raycastHit.collider);
                }
            }
        }
        else
        {
            Debug.Log("Aucune Collision");
            foreach (var item in colliders)
            {
                RenderObject(item.gameObject.GetComponent<MeshRenderer>());
            }
            colliders.Clear();
        }

        foreach (var item in colliders)
        {
            UnRenderObject(item.gameObject.GetComponent<MeshRenderer>());
        }
    }

    private void UnRenderObject(MeshRenderer meshRenderer)
    {
        meshRenderer.renderingLayerMask = 0;
    }
    private void RenderObject(MeshRenderer meshRenderer)
    {
        meshRenderer.renderingLayerMask = 1;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
        
        Vector3 playerScreenPos = Cam.WorldToScreenPoint(player.transform.position);
        Ray ray = Cam.ScreenPointToRay(playerScreenPos);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(Cam.transform.position, raySize);
    }
}
