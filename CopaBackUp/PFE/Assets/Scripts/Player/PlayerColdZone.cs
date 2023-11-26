using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColdZone : MonoBehaviour
{
    [SerializeField] private bool triggerCharacter;
    [SerializeField] private DetectorFov characterDetector;
    [SerializeField] private LayerMask characterNpcLayer;
    [SerializeField] private float triggerLegth = 1f;
    [SerializeField] private Vector3 offset;

    private void Update()
    {
        triggerCharacter = Physics.Raycast(transform.position - offset, Vector3.up * triggerLegth, characterNpcLayer);

        if(triggerCharacter)
        {
            if (DetectionManager.Instance.PlayerHasBeenDetected())
                return;

            RaycastHit hit;
            if(Physics.Raycast(transform.position - offset, Vector3.up * triggerLegth, out hit, characterNpcLayer))
            {
                if (hit.collider.gameObject.GetComponent<DetectorFov>() == null)
                    return;

                characterDetector = hit.collider.gameObject.GetComponent<DetectorFov>();
                // suspected or detected
                //characterDetector.Suspected();
                characterDetector.Detected();
                //Debug.Log(characterDetector.gameObject + " is hit by cold zone");
                characterDetector = null;
               
            }
            
        }
        
    }
    private void OnDrawGizmosSelected()
    {
        if (triggerCharacter)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }

        Gizmos.DrawLine(transform.position - offset, transform.position - offset + Vector3.up * triggerLegth);
    }
}
