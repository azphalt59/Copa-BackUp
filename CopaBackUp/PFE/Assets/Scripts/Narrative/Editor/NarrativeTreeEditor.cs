using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Copacetic.Narrative;
using System;

public class NarrativeTreeEditor : EditorWindow
{
    public const string PATH = "Assets/Scripts/Narrative/Editor/";
    public const string TREEPATH = "Assets/Scripts/Narrative/Tree/";

    NarrativeTreeView _treeView;
    InspectorView _inspectorView;

    [MenuItem("NarrativeTreeEditor/Editor")]
    public static void OpenWindow()
    {
        NarrativeTreeEditor wnd = GetWindow<NarrativeTreeEditor>();
        wnd.titleContent = new GUIContent("NarrativeTreeEditor");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(PATH + "NarrativeTreeEditor.uxml");
        visualTree.CloneTree(root);

        // A stylesheet can be added to a VisualElement.
        // The style will be applied to the VisualElement and all of its children.
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(PATH + "NarrativeTreeEditor.uss");
        root.styleSheets.Add(styleSheet);

        _treeView = root.Q<NarrativeTreeView>();
        _treeView.Editor = this;
        _inspectorView = root.Q<InspectorView>();

        OnSelectionChange();
    }

    public void OnSelectionChange()
    {
        NarrativeTree tree = Selection.activeObject as NarrativeTree;
        if (tree)
        {
            SelectNewTree(tree);
        }
    }

    public void SelectNewTree(NarrativeTree tree)
    {
        _treeView.PopulateView(tree);

        Label treeLabel = rootVisualElement.Query<Label>("tree-label");

        if (treeLabel != null)
        {
            treeLabel.text = _treeView.TreeName;
        }
    }
}