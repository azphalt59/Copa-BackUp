using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PlayerCrackleZone : MonoBehaviour
{
    [SerializeField] private float radius = 1f;
    [SerializeField] private Vector3 offset;
    [SerializeField] List<Collider> crackleCol = new List<Collider>();
    private float crackleDelay;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position + offset, radius);
        foreach (Collider collider in hitColliders)
        {
            if(collider.gameObject.GetComponent<Crackable>() != null && !crackleCol.Contains(collider))
                crackleCol.Add(collider);
        }
        
        foreach (var item in crackleCol)
        {
            item.gameObject.GetComponent<Crackable>().SetInCrackleZone(true);
        }

        foreach (var item in crackleCol)
        {
            if (!hitColliders.Contains(item))
            {
                item.gameObject.GetComponent<Crackable>().SetInCrackleZone(false);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position + offset, radius);
    }
}
