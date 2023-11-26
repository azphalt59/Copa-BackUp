using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PossessionController : MonoBehaviour
{
    [Header("Refs")]
    public PhysicPossessable Possession;
    public CinemachineFreeLook Camera;

    [Header("GD")]
    [Range(0f, 1f)]
    public float MoveSmooth = 0.2f;


    Vector3 _velocity;

    public bool IsPossessing { get => Possession != null; }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float camAngle = Mathf.Atan2(Camera.transform.position.x - transform.position.x, Camera.transform.position.z - transform.position.z) * Mathf.Rad2Deg;
            Vector3 localDirection = Quaternion.AngleAxis(camAngle, Vector3.up) * -direction;

            //Possession.Body.velocity = new Vector3(localDirection.x * Possession.Speed, Possession.Body.velocity.y, localDirection.z * Possession.Speed);

            Vector3 targetVelocity = new Vector3(localDirection.x * Possession.Speed, Possession.Body.velocity.y, localDirection.z * Possession.Speed);

            Possession.Body.AddForceAtPosition((targetVelocity - Possession.Body.velocity) / Time.fixedDeltaTime, Possession.Pivot.position);
        }
    }

    private void LateUpdate()
    {
        // Follow Possessed object and smooth the movement
        // We don't set transform to parent so as not to have unwanted rotations
        transform.position = Vector3.SmoothDamp(transform.position, Possession.CameraPivot.position, ref _velocity, MoveSmooth);
    }


    private void OnDrawGizmos()
    {
        if(Possession != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(Possession.CameraPivot.position, Possession.CameraPivot.position + new Vector3(0, 1, 0));
            Gizmos.DrawSphere(Possession.Pivot.position, 0.05f);

            if (!Application.isPlaying)
            {
                transform.position = Possession.CameraPivot.position;
            }
        }
    }
}
