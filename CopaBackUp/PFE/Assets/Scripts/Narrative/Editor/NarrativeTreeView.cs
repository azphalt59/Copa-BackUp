using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.Experimental.GraphView;

using Copacetic.Narrative;
using System;

using System.Linq;

public class NarrativeTreeView : GraphView
{
    public new class UxmlFactory : UxmlFactory<NarrativeTreeView, GraphView.UxmlTraits> { }
    public NarrativeTreeEditor Editor;

    NarrativeTree _tree;
    public string TreeName { get => (_tree != null ? _tree.name : ""); }

    public Vector2 MousePosition;

    public NarrativeTreeView()
    {
        Insert(0, new GridBackground());

        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(NarrativeTreeEditor.PATH + "NarrativeTreeEditor.uss");
        styleSheets.Add(styleSheet);
    }


    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        return ports.ToList().Where(endPort => endPort.direction != startPort.direction && endPort.node != startPort.node).ToList();
    }

    public void PopulateView(NarrativeTree tree)
    {
        _tree = tree;

        graphViewChanged -= OnGraphViewChanged;
        DeleteElements(graphElements);
        graphViewChanged += OnGraphViewChanged;

        tree.Nodes.ForEach(n => CreateNodeView(n));
    }

    private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
    {
        if (graphViewChange.elementsToRemove != null)
        {
            graphViewChange.elementsToRemove.ForEach(elem =>
            {
                NodeView nodeView = elem as NodeView;
                if (nodeView != null)
                {
                    _tree.DeleteNode(nodeView.Node);
                }
            });
        }
        return graphViewChange;
    }

    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        //base.BuildContextualMenu(evt);
        VisualElement contentViewContainer = ElementAt(1);
        Vector3 screenMousePosition = evt.localMousePosition;
        MousePosition = screenMousePosition - contentViewContainer.transform.position;
        MousePosition *= 1 / contentViewContainer.transform.scale.x;

        if(_tree != null)
        {
            BuildNodeMenu(evt);
        }
        else
        {
            BuildSelectTreeMenu(evt);
        }
    }

    public void BuildNodeMenu(ContextualMenuPopulateEvent evt)
    {
        foreach (Type type in GetNodeTypes<ActionNode>())
        {
            evt.menu.AppendAction($"[Action] {type.Name}", (a) => CreateNode(type, MousePosition));
        }
        foreach (Type type in GetNodeTypes<CompositeNode>())
        {
            evt.menu.AppendAction($"[Composite] {type.Name}", (a) => CreateNode(type, MousePosition));
        }
        foreach (Type type in GetNodeTypes<DecoratorNode>())
        {
            evt.menu.AppendAction($"[Decorator] {type.Name}", (a) => CreateNode(type, MousePosition));
        }
    }
    public void BuildSelectTreeMenu(ContextualMenuPopulateEvent evt)
    {
        string[] guids = AssetDatabase.FindAssets("t:" + typeof(NarrativeTree).Name);

        for (int i = 0; i < guids.Length; i++) 
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            NarrativeTree tree = AssetDatabase.LoadAssetAtPath<NarrativeTree>(path);

            evt.menu.AppendAction($"[Load] {tree.name}", (a) => Editor.SelectNewTree(tree));
        }
    }

    private TypeCache.TypeCollection GetNodeTypes<T>() where T : Copacetic.Narrative.Node
    {
        return TypeCache.GetTypesDerivedFrom<T>();
    }

    private void CreateNode(Type type, Vector2 position)
    {
        Copacetic.Narrative.Node node = _tree.CreateNode(type, position);
        CreateNodeView(node);
    }

    private void CreateNodeView(Copacetic.Narrative.Node n)
    {
        NodeView nodeView = new NodeView(n);
        AddElement(nodeView);
    }
}
