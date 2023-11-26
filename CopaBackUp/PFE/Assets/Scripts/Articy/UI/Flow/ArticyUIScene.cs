using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Articy.Unity;
using Articy.Copacetic;
using Articy.Unity.Interfaces;
using System;
using UnityEngine.UI;

public class ArticyUIScene : ArticyUIWithText
{
    public void CloseScene()
    {
        if(TryGetComponent(out Image image))
		{
            image.color = Color.black;
		}
    }
}
