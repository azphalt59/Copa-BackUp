using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.AI;
using UnityEngine.UI;

public class DetectorFov : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject headObject;
    [SerializeField] private Animator animator;
    [SerializeField] private Image uiDetector;
    private DetectorNavMesh detectorNavMesh;

    [Header("Fov")]
    [SerializeField] public float radius;
    [Range(0, 360)][SerializeField] public float angle;
    [SerializeField] private float delay;
    
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask ObstructionLayer;

    

    [Header("Detection")]
    [Tooltip("Dégats infligés dans la barre de détection du joueur")] [SerializeField] private int detectionDmg = 2;
    [Tooltip("Temps de suspicion du détecteur avant de repèrer le joueur")] [SerializeField] private float detectionTime = 2f;
    public float detectedTime = 0f;

    [Tooltip("Le joueur est vu par le détecteur, le detecteur est sur ses gardes")] [SerializeField] public bool canSeePlayer = false;
    [Tooltip("Le joueur peut être vu même quand il ne se déplace pas")] [SerializeField] public bool canSeePlayerWhileIdle = false;
    [Tooltip("Le joueur est resté trop longtemps dans le FOV du détecteur, il est repèré")] [SerializeField] public bool playerIsDetected = false;
    [Tooltip("Le détecteur s'enfuit, le joueur l'a effrayé")] [SerializeField] public bool isRunningAway = false;

    [SerializeField] private GameObject detectionMark;


    // Start is called before the first frame update
    void Start()
    {
        detectorNavMesh = GetComponent<DetectorNavMesh>();
        StartCoroutine(Fov());
    }

    private IEnumerator Fov()
    {
        WaitForSeconds wait = new WaitForSeconds(delay);

        while(true)
        {
            yield return wait;
            FovCheck();
        }
    }
    private void FovCheck()
    {
        if (DetectionManager.Instance.PlayerHasBeenDetected())
        {
            canSeePlayer = false;
            return;
        }
        if (PossManager.Instance.PossState == PossManager.PossessionState.Free)
        {
            canSeePlayer = false;
            return;
        }
        if (PlayerMovement.Instance.isMoving == false && canSeePlayerWhileIdle == false)
        {
            canSeePlayer = false;
            if(PossManager.Instance.activation != null)
            {
                if (PossManager.Instance.activation.GetIsActive())
                {
                    canSeePlayer = true;
                }
            }
            
            return;
        }
        if(isRunningAway)
        {
            return;
        }

        FovCalcul();
        
    }
    private void Update()
    {
        InterrogativePointColorBlend();
        Detection();
    }
    private void FovCalcul()
    {
        Collider[] cols = Physics.OverlapSphere(headObject.transform.position, radius, playerLayer);

        if (cols.Length != 0)
        {
            Transform col = cols[0].transform;
            Vector3 dirToCol = (col.position - headObject.transform.position).normalized;

            if (Vector3.Angle(transform.forward, dirToCol) < angle / 2)
            {
                float distanceToPlayer = Vector3.Distance(headObject.transform.position, col.position);

                // if hit smt isnt player
                if (!Physics.Raycast(headObject.transform.position, dirToCol, distanceToPlayer, ObstructionLayer))
                {
                    canSeePlayer = true;
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }
    }
    public void Suspected()
    {
        if(detectedTime >= 0 && detectedTime <= detectionTime)
        {
            if (DetectionManager.Instance.PlayerHasBeenDetected())
                return;
            detectionMark.GetComponent<DetectionMark>().SetDetectedMarkSprite(true);
            detectionMark.GetComponent<DetectionMark>().GetImageUiMark().gameObject.SetActive(true);
        }
        else
        {
            detectionMark.GetComponent<DetectionMark>().GetImageUiMark().gameObject.SetActive(false);
        }
        if(detectedTime <= detectionTime)
        {
            detectedTime += Time.deltaTime;
        }
        if(detectedTime >= detectionTime)
        {
            Detected();
        }
       
    }
    private void InterrogativePointColorBlend()
    {
        if (canSeePlayer == false)
        {
            detectionMark.GetComponent<DetectionMark>().GetImageUiMark().gameObject.SetActive(false);
        }

        uiDetector.color = Color.Lerp(Color.yellow, Color.red, detectedTime / detectionTime);
    }
    public void Detected()
    {
        playerIsDetected = true;
        detectionMark.GetComponent<DetectionMark>().SetDetectedMarkSprite(true);
        animator.SetBool("isScared", true);
        DetectionManager.Instance.AddDetectionCount(detectionDmg, this);
    }
    private void Escaped()
    {
        if(0 <= detectedTime)
        {
            detectedTime -= Time.deltaTime;
        }
    }
    private void Detection()
    {
        if (canSeePlayer)
        {
            if (PossManager.Instance.activation != null)
            {
                if (PossManager.Instance.activation.GetIsActive())
                {
                    Detected();
                }
            }
            
            Suspected();
        }
        if (canSeePlayer == false)
        {
            Escaped();
        }
    }

    public Animator GetAnimator()
    {
        return animator;
    }
    
    public GameObject GetDetectionMark()
    {
        return detectionMark;
    }
}
