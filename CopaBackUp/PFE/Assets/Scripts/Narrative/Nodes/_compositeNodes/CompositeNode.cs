using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Copacetic.Narrative;

namespace Copacetic.Narrative
{
    public abstract class CompositeNode : Node
    {
        public List<Node> Childrens = new List<Node>();
    }
}