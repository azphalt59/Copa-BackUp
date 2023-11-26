using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DetectionManager : MonoBehaviour
{
    public static DetectionManager Instance;

    [Tooltip("Nombre de dégat que le joueur peut subir au début d'une partie")] public int DetectionLife = 10;
    [Tooltip("Nombre de dégat de détection que le joueur a subi")] public int DetectionCount = 0;
    [SerializeField] private bool hasBeenDetected = false;

    [Header("Invicbility")]
    [SerializeField] private float invicibilityTime = 2f;
    private float invicibilityTimer;
    [Tooltip("Nombres de secondes après lesquelles les symboles de détection disparaissent après avoir apeuré un personnage")] [SerializeField] private float disableTime = 1f;

    [Header("UI Detection")]
    [SerializeField] private Slider detectionSlider;
    [Tooltip("Nombres de secondes après lesquelles les symboles de détection disparaissent après avoir apeuré un personnage")] public float SliderValueDurationChangement;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        detectionSlider.maxValue = DetectionLife;
    }

    // Update is called once per frame
    void Update()
    {
        if(hasBeenDetected)
        {
            invicibilityTimer += Time.deltaTime;
            if(invicibilityTimer >= invicibilityTime)
            {
                hasBeenDetected = false;
                invicibilityTimer = 0;
            }
        }
    }

    public void AddDetectionCount(int amount, DetectorFov detector)
    {
        if (hasBeenDetected == true)
            return;
        if (DetectionCount >= DetectionLife)
            return;

        hasBeenDetected = true;
        detector.detectedTime = 0f;
        StartCoroutine(DisableExclamation(detector.GetDetectionMark().GetComponent<DetectionMark>().GetImageUiMark().gameObject, detector));

        int oldValue = DetectionCount;
        detectionSlider.DOValue(oldValue + amount, SliderValueDurationChangement);
        DetectionCount += amount;

        // Si le joueur a été détecté trop de fois
        if(DetectionCount >= DetectionLife)
        {
            detector.GetAnimator().SetBool("RunAway", true);
            detector.gameObject.GetComponent<DetectorNavMesh>(). ResetIndex();
            detector.gameObject.GetComponent<DetectorNavMesh>().DetectorEscape();
           
        }
    }

    private IEnumerator DisableExclamation(GameObject gameObject, DetectorFov detector)
    {
        yield return new WaitForSeconds(disableTime);
        gameObject.SetActive(false);
        detector.playerIsDetected = false;
    }

    public bool PlayerHasBeenDetected()
    {
        return hasBeenDetected;
    }


}
