using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevToolArticy : MonoBehaviour
{
    [Header("Keybinds")]
    [SerializeField]
    private KeyCode _toggleFlow;
    [SerializeField]
    private KeyCode _toggleVariables;


    [Header("Refs")]
    [SerializeField]
    private GameObject _UIFlow;
    [SerializeField]
    private GameObject _UIVariables;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(_toggleFlow))
        {
            _UIFlow.SetActive(!_UIFlow.activeSelf);
        }

        if (Input.GetKeyDown(_toggleVariables))
        {
            _UIVariables.SetActive(!_UIVariables.activeSelf);
        }
    }
}
