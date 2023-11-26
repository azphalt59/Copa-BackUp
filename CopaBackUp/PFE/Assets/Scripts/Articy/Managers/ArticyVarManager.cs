using Articy.Unity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArticyVarManager : MonoBehaviour
{
    public static ArticyVarManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    [Header("UI Prefabs")]
    public GameObject NamespacePrefab;
    public ArticyVarUIBool BoolUIPrefab;

    [Header("Refs")]
    public RectTransform VarContent;

    private void Start()
    {
        Dictionary<string, List<IStoredVariable>> map = ArticyDatabase.DefaultGlobalVariables.NamespaceVariableMap;

        foreach (string key in map.Keys)
        {
            List<IStoredVariable> list = map[key];
            GameObject nameSpace = GameObject.Instantiate(NamespacePrefab, VarContent);

            TextMeshProUGUI text = nameSpace.GetComponentInChildren<TextMeshProUGUI>();
            if (text != null)
            {
                text.text = key;
            }


            for (int i = 0; i < list.Count; i++)
            {
                Debug.Log($"{list[i].Namespace} - {list[i].VariableName} - {list[i].FullQualifiedName}");

                if (TryCastVar(list[i], out StoredBoolVariable boolVar))
                {
                    ArticyVarUIBool boolUI = GameObject.Instantiate(BoolUIPrefab, nameSpace.transform);
                    boolUI.InitialiseVariable(boolVar);
                }

            }
        }
    }
    private bool TryCastVar<T>(IStoredVariable cast, out T casted) where T : StoredBaseVariable
    {
        casted = cast as T;
        return casted != null;
    }
}