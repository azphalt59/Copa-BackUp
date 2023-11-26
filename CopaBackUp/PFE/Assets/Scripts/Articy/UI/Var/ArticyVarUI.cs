using Articy.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Articy.Unity.Interfaces;
using UnityEditor;

public abstract class ArticyVarUI<T, TValue> : MonoBehaviour where T : StoredBaseVariable
{
    protected T _variable;

    public TextMeshProUGUI Name;
    public TextMeshProUGUI Description;

    public virtual void InitialiseVariable(T variable)
    {
        Name.text = variable.VariableName;

        var withText = variable as IObjectWithText;
        if(withText != null)
        {
            Description.text = withText.Text;
        }

        _variable = variable;

        ArticyDatabase.DefaultGlobalVariables.Notifications.AddListener(_variable.FullQualifiedName, OnVariableChanged);
    }
    public void SetVariable(TValue value)
    {
        ArticyDatabase.DefaultGlobalVariables.SetVariableByString(_variable.FullQualifiedName, value);
    }
    public TValue GetVariable()
    {
        return ArticyDatabase.DefaultGlobalVariables.GetVariableByString<TValue>(_variable.FullQualifiedName);
    }

    public abstract void OnVariableChanged(string aVariableName, object aValue);
    public abstract void OnInputChanged();
}
