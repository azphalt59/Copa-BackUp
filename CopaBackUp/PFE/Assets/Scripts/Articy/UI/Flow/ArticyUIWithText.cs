using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Articy.Unity;
using Articy.Copacetic;
using Articy.Unity.Interfaces;
using System;
using UnityEngine.UI;

public abstract class ArticyUIWithText : ArticyUI
{
    public TextMeshProUGUI Text;

    public override void ConstructFrom(ArticyObject aObject)
    {
        base.ConstructFrom(aObject);

        MakeText();
	}
	protected virtual void MakeText()
	{
        Text.text = "";

		var txtObj = AObject as IObjectWithText;
		if (txtObj != null)
		{
            Text.text = txtObj.Text;
		}
	}
}
