using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArticyEventListener : MonoBehaviour
{
    [SerializeField] private string _eventName;
    [SerializeField] private UnityEvent _event;


    void Start()
    {
        ArticyEventManager.Instance.AddEvent(_eventName, _event);
    }
    private void OnDestroy()
    {
        ArticyEventManager.Instance?.RemoveEvent(_eventName, _event);
    }
}
