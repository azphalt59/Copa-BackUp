using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Articy.Unity;
using Articy.Unity.Interfaces;
using UnityEngine.UI;

public abstract class ArticyUI : MonoBehaviour
{
	[HideInInspector]
	public ArticyObject AObject;
	[Header("Refs")]
	public TextMeshProUGUI Title;

    public virtual void ConstructFrom(ArticyObject aObject)
    {
        AObject = aObject;
		MakeTitle();

		NameAfterTitle();
	}

	protected virtual void NameAfterTitle()
	{
		name = Title.text;
	}

	protected virtual void MakeTitle()
    {
		Title.text = "";

		// now we figure out which text our button should have, and we just try to cast our target into different types, 
		// creating some sort of priority naming  MenuText -> DisplayName -> TechnicalName -> ClassName/Null

		var obj = AObject as IObjectWithMenuText;
		if (obj != null)
		{
			Title.text = obj.MenuText;

			// Empty? Usually it would have a menu text, but it was deliberately left empty, in a normal game this could mean a single branch to just continue the dialog, if the protagonist is talking for
			// example, how you handle this is up to you, for this we just use the text normal text to show.
			if (Title.text == "")
			{
				var txtObj = obj as IObjectWithText;
				if (txtObj != null)
					Title.text = txtObj.Text;
				else
					Title.text = "...";
			}
		}

		// if the text is still empty, we can show the displayname of the target
		if (Title.text == "")
		{
			var dspObj = AObject as IObjectWithDisplayName;
			if (dspObj != null)
				Title.text = dspObj.DisplayName;
			else
			{
				// if it is still empty, we just show the technical name
				var articyObject = AObject as IArticyObject;
				if (articyObject != null)
					Title.text = articyObject.TechnicalName;
				else
				{
					// if for some reason the object cannot be cast to a basic articy type, we show its class name or null.
					Title.text = AObject == null ? "null" : AObject.GetType().Name;
				}
			}
		}
	}
}
