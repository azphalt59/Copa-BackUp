using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Copacetic.Narrative;

namespace Copacetic.Narrative
{
    public class WaitNode : ActionNode
    {
        public float Duration = 1f;


        private float _startTime;
        protected override void OnStart()
        {
            _startTime = Time.time;
        }

        protected override void OnStop()
        {
        }

        protected override StateType OnUpdate()
        {
            if(Time.time - _startTime > Duration)
            {
                return StateType.Success;
            }
            else
            {
                return StateType.Running;
            }
        }
    }
}
