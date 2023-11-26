using Articy.Unity;
using Articy.Unity.Interfaces;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Copacetic;
using Articy.Copacetic;
using UnityEngine.UI;

public class ArticyUICondition : ArticyUIWithText
{
	public override void ConstructFrom(ArticyObject aObject)
	{
		base.ConstructFrom(aObject);

		Condition condition = aObject as Condition;
        if (condition != null && TryGetComponent(out Image image))
        {
            bool value = condition.Evaluate();
            
            if (value)
            {
                image.color = new Color(0.1f, 0.8f, 0.1f);
            }
            else
            {
                image.color = new Color(0.8f, 0.1f, 0.1f);
            }
        }
    }
}