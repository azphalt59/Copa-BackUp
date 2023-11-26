using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Copacetic.Narrative;
using System;
using UnityEditor;

namespace Copacetic.Narrative
{
    [CreateAssetMenu(menuName = "Narrative/Tree")]
    public class NarrativeTree : ScriptableObject
    {
        public Node RootNode;
        public List<Node> Nodes = new List<Node>();

        public Node.StateType State = Node.StateType.Running;

        public Node.StateType Update()
        {
            if(RootNode.State == Node.StateType.Running)
            {
                State = RootNode.Update();
            }

            return State;
        }

        public Node CreateNode(Type type)
        {
            return CreateNode(type, Vector2.zero);
        }
        public Node CreateNode(Type type, Vector2 position)
        {
            Node node = (Node)ScriptableObject.CreateInstance(type);
            node.name = type.Name;
            node.guid = GUID.Generate().ToString();
            node.position = position;
            Nodes.Add(node);

            AssetDatabase.AddObjectToAsset(node, this);
            AssetDatabase.SaveAssets();

            return node;
        }

        public void DeleteNode(Node node)
        {
            Nodes.Remove(node);
            AssetDatabase.RemoveObjectFromAsset(node);
            AssetDatabase.SaveAssets();
        }

    }
}

