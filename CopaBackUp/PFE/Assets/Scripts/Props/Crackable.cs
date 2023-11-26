using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crackable : MonoBehaviour
{
    [SerializeField] private bool inCrackeZone;
    private bool isCrackle = false;
    [SerializeField] private float crackleTime;
    private float timer = 0f;
    public float crackeTimer;

    private Vector3 startingPos;
    [SerializeField] private float crackleIntensity = 0.1f;
    [SerializeField] private float crackleSpeed = 10f;


    public bool GetInCrackleZone()
    {
        return inCrackeZone;
    }
    public void SetInCrackleZone(bool state)
    {
        inCrackeZone = state; 
    }

    private void Start()
    {
        startingPos = transform.position;
    }

    private void Update()
    {
        if(inCrackeZone)
        {
            crackeTimer = 2f;
        }
        else
        {
            crackeTimer -= Time.deltaTime;
        }
        if (crackeTimer > 0) 
        {
            //if (isCrackle)
            //    return;

            timer += Time.deltaTime;
            CrackleMovement();
        }
    }

    public void CrackleMovement()
    {
        float xOffset = Mathf.PerlinNoise(timer * crackleSpeed, 0) * 2 - 1;
        float yOffset = Mathf.PerlinNoise(0, timer * crackleSpeed) * 2 - 1;
        float zOffset = Mathf.PerlinNoise(timer * crackleSpeed, timer * crackleSpeed) * 2 - 1;

        Vector3 offset = new Vector3(xOffset, yOffset, zOffset) * crackleIntensity;
        transform.position = startingPos + offset;
    }

    public bool GetInCracke()
    {
        return inCrackeZone;
    }
}
