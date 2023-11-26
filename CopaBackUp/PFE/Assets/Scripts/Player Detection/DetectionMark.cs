using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DetectionMark : MonoBehaviour
{
    private float minX;
    private float maxX;

    private float minY;
    private float maxY;

    [SerializeField] private Image imageUiMark;
    [SerializeField] private DetectorFov detector;

    [SerializeField] private Sprite suspiciousSprite;
    [SerializeField] private Sprite detectedSprite;

    // Update is called once per frame
    void Update()
    {
        // set clamping values
        minX = imageUiMark.GetPixelAdjustedRect().width / 1.5f;
        maxX = Screen.width - minX;
        minY = imageUiMark.GetPixelAdjustedRect().height / 1.5f;
        maxY = Screen.height - minY;

        Vector2 position = Camera.main.WorldToScreenPoint(detector.GetDetectionMark().transform.position);
       
        if (Vector3.Dot((detector.GetDetectionMark().transform.position - PlayerMovement.Instance.transform.position), Camera.main.transform.forward) < 0)
        {
            //if (position.y < Screen.height / 2)
            //{
            //    position.y = maxY/2;
            //}
            //else
            //{
            //    position.y = minY;
            //}
            position.y = minY;
        }
        // clamp to border
        position.x = Mathf.Clamp(position.x, minX, maxX);
        position.y = Mathf.Clamp(position.y, minY, maxY);
       
        imageUiMark.transform.position = Vector3.Lerp(imageUiMark.transform.position, position, 5 * Time.deltaTime);
    }

    public Image GetImageUiMark()
    {
        return imageUiMark;
    }
    public void SetDetectedMarkSprite(bool suspicious)
    {
        if (suspicious)
            imageUiMark.sprite = suspiciousSprite;
        else
            imageUiMark.sprite = detectedSprite;
    }
}
