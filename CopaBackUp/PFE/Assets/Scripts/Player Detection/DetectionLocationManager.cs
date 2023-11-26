using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectionLocationManager : MonoBehaviour
{
    public static DetectionLocationManager Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }

  
    public Color SetColorSprite(Color color1, Color color2, float detectedTime, float detectionTime)
    {
        return Color.Lerp(color1, color2, detectedTime / detectionTime);
    }
  
}
