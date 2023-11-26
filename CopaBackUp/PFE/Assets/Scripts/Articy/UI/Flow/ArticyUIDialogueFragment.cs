using Articy.Unity;
using Articy.Unity.Interfaces;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArticyUIDialogueFragment : ArticyUIWithText
{
    protected override void MakeTitle()
	{
		// get the speaker
		var withSpeaker = AObject as IObjectWithSpeaker;
		if (withSpeaker != null)
		{
			// if we have a speaker, we extract it, because now we have to check if it has a preview image.
			var withDisplayName = withSpeaker.Speaker as IObjectWithDisplayName;
			if (withDisplayName != null)
			{
				Title.text = withDisplayName.DisplayName;
			}
		}
	}
}