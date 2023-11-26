using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Articy.Unity;
using Articy.Copacetic;
using Articy.Unity.Interfaces;
using System;
using UnityEngine.UI;

public class ArticyUIInstruction : ArticyUIWithText
{
    protected override void MakeText()
    {
        Text.text = "";

        Instruction instruction = AObject as Instruction;
        if (instruction != null)
        {
            Text.text = instruction.Expression.RawScript;
        }
    }
}
