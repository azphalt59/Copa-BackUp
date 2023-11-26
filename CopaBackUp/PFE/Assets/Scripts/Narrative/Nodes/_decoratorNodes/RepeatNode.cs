using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Copacetic.Narrative;

namespace Copacetic.Narrative
{
    public class RepeatNode : DecoratorNode
    {
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override StateType OnUpdate()
        {
            /*
            if (Child.State != StateType.Running)
            {
                Child.State = StateType.Running;
            }
            */
            Child.Update();

            return StateType.Running;
        }
    }
}
