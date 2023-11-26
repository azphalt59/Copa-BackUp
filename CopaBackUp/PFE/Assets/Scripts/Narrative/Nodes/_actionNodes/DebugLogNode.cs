using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Copacetic.Narrative;

namespace Copacetic.Narrative
{
    public class DebugLogNode : ActionNode
    {
        public string Message;

        protected override void OnStart()
        {
            Debug.Log($"OnStart : {Message}");
        }

        protected override void OnStop()
        {
            Debug.Log($"OnStop : {Message}");
        }

        protected override StateType OnUpdate()
        {
            Debug.Log($"OnUpdate : {Message}");
            return StateType.Success;
        }
    }
}
