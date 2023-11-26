using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor.Experimental.GraphView;
using Copacetic.Narrative;
using System;

public class NodeView : UnityEditor.Experimental.GraphView.Node
{
    public Port Input;
    public Port Output;

    public Copacetic.Narrative.Node Node;
    public NodeView(Copacetic.Narrative.Node node)
    {
        Node = node;
        title = node.name;
        viewDataKey = node.guid;

        style.left = node.position.x;
        style.top = node.position.y;

        CreateInputPorts();
        CreateOutputPorts();
    }

    private void CreateInputPorts()
    {
        Input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));

        if(Input != null)
        {
            Input.portName = "";
            inputContainer.Add(Input);
        }
    }

    private void CreateOutputPorts()
    {
        Output = null;
        if(Node is ActionNode)
        {

        }
        else if (Node is CompositeNode)
        {
            Output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(bool));
        }
        else if (Node is DecoratorNode)
        {
            Output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
        }

        if (Output != null)
        {
            Output.portName = "";
            outputContainer.Add(Output);
        }
    }

    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);
        Node.position.x = newPos.xMin;
        Node.position.y = newPos.yMin;
    }
}
