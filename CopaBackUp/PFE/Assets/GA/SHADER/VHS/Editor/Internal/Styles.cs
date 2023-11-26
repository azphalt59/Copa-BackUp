////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Martin Bustos @FronkonGames <fronkongames@gmail.com>. All rights reserved.
//
// THIS FILE CAN NOT BE HOSTED IN PUBLIC REPOSITORIES.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using UnityEditor;

namespace FronkonGames.Retro.VHS.Editor
{
  /// <summary>
  /// Styles for the Editor.
  /// </summary>
  internal static class Styles
  {
    public static Color Splitter => EditorGUIUtility.isProSkin ? splitterDark : splitterLight;

    public static Texture2D PaneOptionsIcon => EditorGUIUtility.isProSkin ? paneOptionsIconDark : paneOptionsIconLight;

    public static Color HeaderBackground => EditorGUIUtility.isProSkin ? headerBackgroundDark : headerBackgroundLight;

    public static GUIStyle HeaderStyle => headerStyle;

    public static GUIStyle FoldoutStyle => foldoutStyle;

    public static GUIStyle CheckboxStyle => checkboxStyle ??= "ShurikenCheckMark";

    public static GUIStyle LogoStyle => logoStyle;

    public static Texture2D WhiteTexture
    {
      get
      {
        if (whiteTexture == null)
        {
          whiteTexture = new Texture2D(1, 1, TextureFormat.ARGB32, false) { name = "White Texture" };
          whiteTexture.SetPixel(0, 0, Color.white);
          whiteTexture.Apply();
        }

        return whiteTexture;
      }
    }

    public static Texture2D BlackTexture
    {
      get
      {
        if (blackTexture == null)
        {
          blackTexture = new Texture2D(1, 1, TextureFormat.ARGB32, false) { name = "Black Texture" };
          blackTexture.SetPixel(0, 0, Color.black);
          blackTexture.Apply();
        }

        return blackTexture;
      }
    }

    public static Texture2D TransparentTexture
    {
      get
      {
        if (transparentTexture == null)
        {
          transparentTexture = new Texture2D(1, 1, TextureFormat.ARGB32, false) { name = "Transparent Texture" };
          transparentTexture.SetPixel(0, 0, Color.clear);
          transparentTexture.Apply();
        }

        return transparentTexture;
      }
    }

    private static readonly GUIStyle headerStyle;
    private static readonly GUIStyle foldoutStyle;
    private static readonly GUIStyle logoStyle;
    private static GUIStyle checkboxStyle;
    private static readonly GUIStyle smallTickbox;
    public static readonly GUIStyle miniLabelButton;
    private static readonly GUIStyle headerLabel;
    private static readonly GUIStyle wheelLabel;
    private static readonly GUIStyle wheelThumb;
    private static readonly GUIStyle preLabel;

    private static readonly Color splitterDark;
    private static readonly Color splitterLight;
    private static readonly Color headerBackgroundDark;
    private static readonly Color headerBackgroundLight;

    private static readonly Texture2D paneOptionsIconDark;
    private static readonly Texture2D paneOptionsIconLight;
    private static Texture2D whiteTexture;
    private static Texture2D blackTexture;
    private static Texture2D transparentTexture;

    private static readonly Vector2 wheelThumbSize;

    static Styles()
    {
      smallTickbox = new GUIStyle("ShurikenToggle");

      miniLabelButton = new(EditorStyles.miniLabel)
      {
        richText = true,
        normal = new()
        {
          background = TransparentTexture,
          scaledBackgrounds = null,
          textColor = Color.grey,
        }
      };

      GUIStyleState activeState = new GUIStyleState
      {
        background = TransparentTexture,
        scaledBackgrounds = null,
        textColor = Color.white
      };

      Font font = null;
      string[] ids = AssetDatabase.FindAssets("FronkonGames-Black");
      if (ids.Length == 1)
        font = AssetDatabase.LoadAssetAtPath<Font>(AssetDatabase.GUIDToAssetPath(ids[0]));

      if (font != null)
      {
        logoStyle = new GUIStyle(EditorStyles.boldLabel)
        {
          font = font,
          alignment = TextAnchor.LowerLeft,
          fontSize = 24
        };
      }
      
      miniLabelButton.active = activeState;
      miniLabelButton.onNormal = activeState;
      miniLabelButton.onActive = activeState;

      splitterDark = new Color(0.12f, 0.12f, 0.12f, 1.333f);
      splitterLight = new Color(0.6f, 0.6f, 0.6f, 1.333f);

      headerBackgroundDark = new Color(0.1f, 0.1f, 0.1f, 0.2f);
      headerBackgroundLight = new Color(1.0f, 1.0f, 1.0f, 0.2f);

      paneOptionsIconDark = (Texture2D)EditorGUIUtility.Load("Builtin Skins/DarkSkin/Images/pane options.png");
      paneOptionsIconLight = (Texture2D)EditorGUIUtility.Load("Builtin Skins/LightSkin/Images/pane options.png");

      headerLabel = new GUIStyle(EditorStyles.miniLabel);

      wheelThumb = new GUIStyle("ColorPicker2DThumb");

      wheelThumbSize = new Vector2(!Mathf.Approximately(wheelThumb.fixedWidth, 0f) ? wheelThumb.fixedWidth : wheelThumb.padding.horizontal,
                                   !Mathf.Approximately(wheelThumb.fixedHeight, 0f) ? wheelThumb.fixedHeight : wheelThumb.padding.vertical);

      wheelLabel = new GUIStyle(EditorStyles.miniLabel);

      preLabel = new GUIStyle("ShurikenLabel");

      headerStyle = "ShurikenModuleTitle";
      headerStyle.font = new GUIStyle("Label").font;
      headerStyle.fontSize = 12;
      headerStyle.border = new RectOffset(15, 7, 4, 4);
      headerStyle.fixedHeight = 22;
      headerStyle.contentOffset = new Vector2(5.0f, -2.0f);

      foldoutStyle = "ShurikenModuleTitle";
      foldoutStyle.font = new GUIStyle("Label").font;
      foldoutStyle.fontSize = 12;
      foldoutStyle.border = new RectOffset(15, 7, 4, 4);
      foldoutStyle.fixedHeight = 22;
      foldoutStyle.contentOffset = new Vector2(20.0f, -2.0f);
    }
  }
}