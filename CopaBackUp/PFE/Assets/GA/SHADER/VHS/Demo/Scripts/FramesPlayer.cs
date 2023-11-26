using UnityEngine;

namespace FronkonGames.Retro.VHS
{
  /// <summary> Frames player. </summary>
  /// <remarks> This code is designed for a simple demo, not for production environments. </remarks>
  public sealed class FramesPlayer : MonoBehaviour
  {
    [SerializeField]
    private Texture2D[] frames;
    
    [SerializeField]
    private float fps = 10.0f;

    [SerializeField]
    private Material target;

    private void Update() => target.mainTexture = frames[(int)(Time.time * fps) % frames.Length];
  }  
}
