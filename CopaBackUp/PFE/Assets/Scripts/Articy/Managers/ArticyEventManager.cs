using Articy.Unity;
using Articy.Copacetic;
using Articy.Unity.Interfaces;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using TMPro;

public class ArticyEventManager : MonoBehaviour
{
    public static ArticyEventManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    private Dictionary<string, List<UnityEvent>> _events = new Dictionary<string, List<UnityEvent>>();


    public void AddEvent(string key, UnityEvent unityEvent)
    {
        if(_events.TryGetValue(key, out var list))
        {
            list.Add(unityEvent);
        }
        else
        {
            _events.Add(key, new List<UnityEvent>() { unityEvent });
        }
    }

    private void DoEvent(string key)
    {
        if (_events.TryGetValue(key, out var list))
        {
            foreach (var unityEvent in list)
            {
                unityEvent?.Invoke();
            }
        }
        else
        {
            Debug.LogWarning($"Cannot DoEvent({key}) : Event '{key}' does not exist");
        }
    }
    public void RemoveEvent(string key, UnityEvent unityEvent)
    {
        if (_events.TryGetValue(key, out var list))
        {
            list.Remove(unityEvent);
        }
        else
        {
            Debug.LogWarning($"Cannot RemoveEvent() : Event '{key}' does not exist");
        }
    }
}
