using UnityEngine;
using UnityEngine.Rendering;
using FronkonGames.Retro.VHS;

/// <summary>
/// Retro: VHS demo.
/// </summary>
public class RetroVHSDemo : MonoBehaviour
{
  [SerializeField]
  private RenderPipelineAsset overrideRenderPipelineAsset;

  [Space]

  [SerializeField]
  private Transform floor;

  [SerializeField, Range(0.0f, 10.0f)]
  private float angularVelocity;

  private RenderPipelineAsset defaultRenderPipelineAsset;
  private VHS.Settings settings;

  private GUIStyle styleFont;
  private GUIStyle styleButton;
  private GUIStyle styleLogo;
  private Vector2 scrollView;

  private const float BoxWidth = 700.0f;
  private const float Margin = 20.0f;
  private const float LabelSize = 250.0f;
  private const float OriginalScreenWidth = 1920.0f;

  private int Slider(string label, int value, int left, int right)
  {
    GUILayout.BeginHorizontal();
    {
      GUILayout.Space(Margin);

      GUILayout.Label(label, styleFont, GUILayout.Width(LabelSize));

      value = (int)GUILayout.HorizontalSlider(value, left, right, GUILayout.ExpandWidth(true));

      GUILayout.Space(Margin);
    }
    GUILayout.EndHorizontal();

    return value;
  }

  private float Slider(string label, float value, float left, float right)
  {
    GUILayout.BeginHorizontal();
    {
      GUILayout.Space(Margin);

      GUILayout.Label(label, styleFont, GUILayout.Width(LabelSize));

      value = GUILayout.HorizontalSlider(value, left, right, GUILayout.ExpandWidth(true));

      GUILayout.Space(Margin);
    }
    GUILayout.EndHorizontal();

    return value;
  }

  private bool Toggle(string label, bool value)
  {
    GUILayout.BeginHorizontal();
    {
      GUILayout.Space(Margin);

      GUILayout.Label(label, styleFont, GUILayout.Width(LabelSize));

      value = GUILayout.Toggle(value, string.Empty);

      GUILayout.Space(Margin);
    }
    GUILayout.EndHorizontal();

    return value;
  }

  private void Start()
  {
    defaultRenderPipelineAsset = GraphicsSettings.currentRenderPipeline;

    GraphicsSettings.defaultRenderPipeline = overrideRenderPipelineAsset;
    QualitySettings.renderPipeline = overrideRenderPipelineAsset;

    if (VHS.IsInRenderFeatures() == false)
      VHS.AddRenderFeature();

    settings = VHS.GetSettings();
    settings.ResetDefaultValues();
  }

  private void Update()
  {
    if (floor != null && angularVelocity > 0.0f)
      floor.rotation = Quaternion.Euler(0.0f, floor.rotation.eulerAngles.y + Time.deltaTime * angularVelocity * 10.0f, 0.0f);
  }

  private void OnDestroy()
  {
    GraphicsSettings.defaultRenderPipeline = defaultRenderPipelineAsset;
    QualitySettings.renderPipeline = defaultRenderPipelineAsset;
  }

  private void OnGUI()
  {
    Matrix4x4 guiMatrix = GUI.matrix;
    GUI.matrix = Matrix4x4.Scale(Vector3.one * (Screen.width / OriginalScreenWidth));

    if (styleFont == null)
      styleFont = new GUIStyle(GUI.skin.label)
      {
        alignment = TextAnchor.UpperLeft,
        fontStyle = FontStyle.Bold,
        fontSize = 28
      };

    if (styleButton == null)
      styleButton = new GUIStyle(GUI.skin.button)
      {
        fontStyle = FontStyle.Bold,
        fontSize = 28
      };

    if (styleLogo == null)
      styleLogo = new GUIStyle(GUI.skin.label)
      {
        alignment = TextAnchor.MiddleCenter,
        fontStyle = FontStyle.Bold,
        fontSize = 32
      };

    if (settings != null)
    {
      GUILayout.BeginVertical("box", GUILayout.Width(BoxWidth), GUILayout.ExpandHeight(true));
      {
        GUILayout.Space(Margin);
      
        GUILayout.BeginHorizontal();
        {
          GUILayout.FlexibleSpace();
          GUILayout.Label("Retro: VHS", styleLogo);
          GUILayout.FlexibleSpace();
        }
        GUILayout.EndHorizontal();

        scrollView = GUILayout.BeginScrollView(scrollView);
        {
          GUILayout.Space(Margin * 0.5f);

          settings.intensity = Slider("Intensity", settings.intensity, 0.0f, 1.0f);

          Vector3 yiq = settings.yiq;
          yiq.x = Slider("Luma", yiq.x, -2.0f, 2.0f);
          yiq.y = Slider("In-phase", yiq.y, -2.0f, 2.0f);
          yiq.z = Slider("Quadrature", yiq.z, -2.0f, 2.0f);
          settings.yiq = yiq;
          
          settings.tapeCreaseStrength = Slider("Tape crease", settings.tapeCreaseStrength, 0.0f, 1.0f);
          settings.colorNoise = Slider("Color noise", settings.colorNoise, 0.0f, 1.0f);
          settings.chromaBand = Slider("Chroma band", settings.chromaBand, 1, 64);
          settings.lumaBand = Slider("Luma band", settings.lumaBand, 1, 16);
          settings.tapeNoiseHigh = settings.tapeNoiseLow = Slider("Tape noise", settings.tapeNoiseHigh, 0.0f, 1.0f);
          settings.acBeatStrength = Slider("AC beat", settings.acBeatStrength, 0.0f, 1.0f);
          settings.bottomWarpHeight = Slider("Bottom warp", settings.bottomWarpHeight, 0.0f, 100.0f);
          settings.vignette = Slider("Vignette", settings.vignette, 0.0f, 1.0f);
        }
        GUILayout.EndScrollView();
      
        GUILayout.BeginHorizontal();
        {
          GUILayout.Space(Margin);

          if (GUILayout.Button("Reset", styleButton) == true)
            settings.ResetDefaultValues();

          GUILayout.Space(Margin);
        }
        GUILayout.EndHorizontal();

        GUILayout.FlexibleSpace();

        GUILayout.Space(Margin);
      }
      GUILayout.EndVertical();
    }
    else
      GUILayout.Label($"URP not available or '{Constants.Asset.Name}' is not correctly configured, please consult the documentation", styleLogo);

    GUI.matrix = guiMatrix;
  }
}