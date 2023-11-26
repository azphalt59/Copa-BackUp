using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Larry : MonoBehaviour
{
    [Header("Larry Bottle  Detection")]
    [SerializeField] private bool nearBottle = false;
    [SerializeField] private bool showBottleGizmos = false;
    [SerializeField] private float radius = 2f;
    [SerializeField] private Vector3 verticalOffset;

    [SerializeField] List<Collider> col;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position + verticalOffset, radius);
        foreach (Collider collider in hitColliders)
        {
            if (!collider.gameObject.activeSelf)
                return;
            if (collider.gameObject.GetComponent<LarryBottle>() == null)
                return;

            col.Add(collider);
            if (collider.gameObject.GetComponent<LarryBottle>() != null && collider.gameObject.activeSelf)
            {
                nearBottle = true;
                break;
            }
            nearBottle = false;
        }

        if(nearBottle == false)
        {
            
        }
    }
    public bool GetNearBottle()
    {
        return nearBottle;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position + verticalOffset, radius);
    }
}
