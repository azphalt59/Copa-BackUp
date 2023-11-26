using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DetectorFov))]
public class FovDetectionEditor : Editor
{
    private void OnSceneGUI()
    {
        DetectorFov fov = (DetectorFov)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.radius);

        Vector3 angle1 = DirFromAngle(fov.transform.eulerAngles.y, -fov.angle / 2);
        Vector3 angle2 = DirFromAngle(fov.transform.eulerAngles.y, fov.angle / 2);

        Handles.color = Color.red;
        Handles.DrawLine(fov.transform.position, fov.transform.position + angle1 * fov.radius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + angle2 * fov.radius);

        if(fov.canSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, PlayerMovement.Instance.gameObject.transform.position);
        }
    }

    private Vector3 DirFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
