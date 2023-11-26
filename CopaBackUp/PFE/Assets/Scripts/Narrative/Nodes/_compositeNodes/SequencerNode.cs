using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Copacetic.Narrative;

namespace Copacetic.Narrative
{
    public class SequencerNode : CompositeNode
    {
        int Index = 0;

        protected override void OnStart()
        {
            Index = 0;
        }

        protected override void OnStop()
        {

        }

        protected override StateType OnUpdate()
        {
            Node child = Childrens[Index];

            switch (child.Update())
            {
                case StateType.Running:
                    return StateType.Running;

                case StateType.Failure:
                    return StateType.Failure;

                case StateType.Success:
                    Index++;
                    break;


            }

            return (Index == Childrens.Count ? StateType.Success : StateType.Running);
        }
    }
}