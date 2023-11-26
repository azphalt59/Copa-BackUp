using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSRotation : MonoBehaviour
{
    [SerializeField] private GameObject player;
    CameraController cameraController;

    [Header("First Person Settings")]
    [SerializeField] private float mouseSensitivity = 1000f;
    private float xRotation, yRotation;

    // Start is called before the first frame update
    void Start()
    {
        cameraController = CameraController.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (PossManager.Instance.PossState == PossManager.PossessionState.Free)
        {
            // First Person Settings
            RotationFirstPerson();
        }
    }

    private void RotationFirstPerson()
    {
        // Récupère les mouvements de la souris
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Tourne la caméra en fonction des mouvements de la souris
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        // Fait tourner le joueur pour qu'il soit face à la vue de la caméra
        player.transform.Rotate(Vector3.up * mouseX);
        transform.parent.rotation = Quaternion.Euler(0f, player.transform.rotation.eulerAngles.y, 0f);
    }
}
