using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using Copacetic.Narrative;
using System;

namespace Copacetic.Narrative
{
    public abstract class Node : ScriptableObject
    {
        public enum StateType
        {
            Running,
            Failure,
            Success
        }



        public StateType State = StateType.Running;
        public bool Active;
        public string guid;
        public Vector2 position;

        public StateType Update()
        {
            if (!Active)
            {
                OnStart();
                Active = true;
            }

            State = OnUpdate();

            if(State == StateType.Failure || State == StateType.Success)
            {
                OnStop();
                Active = false;
            }

            return State;
        }


        protected abstract void OnStart();
        protected abstract void OnStop();
        protected abstract StateType OnUpdate();
    }
}