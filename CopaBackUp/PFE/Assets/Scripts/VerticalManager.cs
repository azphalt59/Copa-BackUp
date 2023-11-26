using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalManager : MonoBehaviour
{
    [System.Serializable]
    public struct VerticalStep
    {
        public float stepHistoryValue;
        public int stepIndex;
        public string stepName;
        public bool nextStepWithTime;
        public float nextStepDelay;

        public List<Transform> MovementsLarry;
        public List<Transform> MovementsMargaret;
    }
    public List<VerticalStep> steps;
    public int currentStep;
    public string currentStepDescription;
    private float timer;

    [SerializeField] DetectorNavMesh LarryNavMesh;
    [SerializeField] DetectorNavMesh MargaretNavMesh;
    [SerializeField] Crackable Tv;
    [SerializeField] BoxCollider GraceBedRoomCol;

    // Start is called before the first frame update
    void Start()
    {
        currentStep = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentStepDescription = steps[currentStep].stepName;

        // si le joueur déclenche la télé
        if(currentStep == 0)
        {
            ChangeStepBranch(Tv.GetInCracke(), 0, 4);
        }
        // si le joueur state 2 il dort
        if(currentStep == 2 || currentStep == 3)
        {
            LarryNavMesh.animController.Sleep();
        }
        else
        {
            LarryNavMesh.animController.UnSleep();
        }

        // si le joueur cache la bouteille
        if (currentStep == 6)
        {
            timer += Time.deltaTime;
            ChangeStepBranch(LarryNavMesh.gameObject.GetComponent<Larry>().GetNearBottle(), 5, 2);
            
            //  si bouteille, on repart sur le scénario d'avant
            if (timer > steps[currentStep].nextStepDelay)
            {
                timer = 0;
                currentStep = 7;
            }
        }
        // si step 9 on débloque le col quand on récup la photo
        if(currentStep == 8)
        {
            GraceBedRoomCol.gameObject.SetActive(false);
        }
        

        // si la current step à un temps de delay pour déclencher la next step
        if (steps[currentStep].nextStepWithTime)
        {
            timer += Time.deltaTime;
            if(timer > steps[currentStep].nextStepDelay)
            {
                timer = 0;
                currentStep++;
                SetCharacterMovement(LarryNavMesh, steps[currentStep].MovementsLarry);
                SetCharacterMovement(MargaretNavMesh, steps[currentStep].MovementsMargaret);
            }
        }
    }

    public void SetCharacterMovement(DetectorNavMesh character, List<Transform> movements)
    {
        character.SetDestinationWayPoints(movements);
    }

    public void ChangeStepBranch(bool condition, int previousStep, int newStep)
    {
        if(condition)
        {
            timer = 0f;
            currentStep = newStep;
            SetCharacterMovement(LarryNavMesh, steps[currentStep].MovementsLarry);
            SetCharacterMovement(MargaretNavMesh, steps[currentStep].MovementsMargaret);
        }
    }
}
