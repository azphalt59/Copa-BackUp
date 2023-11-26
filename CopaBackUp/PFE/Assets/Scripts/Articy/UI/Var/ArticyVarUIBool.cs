using Articy.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArticyVarUIBool : ArticyVarUI<StoredBaseVariable, bool>
{
    public Toggle Toggle;

    public override void InitialiseVariable(StoredBaseVariable variable)
    {
        base.InitialiseVariable(variable);

        Toggle.isOn = GetVariable();
    }


    public override void OnVariableChanged(string aVariableName, object aValue)
    {
        //Debug.Log($"Variable {aVariableName} = {aValue}");
        Toggle.isOn = GetVariable();
    }
    public override void OnInputChanged()
    {
        SetVariable(Toggle.isOn);
    }
}
