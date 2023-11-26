using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DetectorNavMesh : MonoBehaviour
{
    [Header("Nav Mesh")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform currDestination;
    [SerializeField] private List<Transform> destinationWaypoints;
    [SerializeField] public DummyAnimController animController;
    [SerializeField] private int waypointsIndex = 0;
    [SerializeField] private float dist = 0f;

    public Action OnDestinationReached;

    public void DetectorEscape()
    {
        //if(waypointsIndex == destinationWaypoints.Count -1)
        //{
        //    animController.StopRunAway();
        //}
        //if(waypointsIndex >= destinationWaypoints.Count)
        //{
        //    return;
        //}
        if (waypointsIndex >= destinationWaypoints.Count)
            return;
        agent.destination = destinationWaypoints[waypointsIndex].position;
    }

    private void Update()
    {
        if(currDestination != null) 
        {
            dist = Vector3.Distance(transform.position, currDestination.position);
            animController.Walk();
        }
        else
        {
            animController.StopRunAway();
        }

        if (currDestination == null)
            return;

        //if(waypointsIndex >= destinationWaypoints.Count -1) 
        //{
        //    currDestination = null;
        //    animController.StopRunAway();

        //    return;
        //}

        float testDistance = 0.5f;
        if(currDestination.TryGetComponent(out NavigationTarget target))
        {
            testDistance = target.Distance;
        }

        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(currDestination.position.x, currDestination.position.z)) < testDistance)
        {
            if(waypointsIndex >= destinationWaypoints.Count)
            {
                currDestination = null;

                OnDestinationReached?.Invoke();
                OnDestinationReached = null;
                return;
            }
            waypointsIndex++;
            
        }
        DetectorEscape();
        if (waypointsIndex >= destinationWaypoints.Count)
            return;
        currDestination = destinationWaypoints[waypointsIndex];
    }
    public void ResetIndex()
    {
        waypointsIndex = 0;
    }

    public void SetDestinationWayPoints(List<Transform> movementList)
    {
        destinationWaypoints.Clear();
        destinationWaypoints = movementList;
        if(destinationWaypoints.Count > 0)
        {
            waypointsIndex = 0;
            currDestination = destinationWaypoints[0];
        }
        
        
    }
}
