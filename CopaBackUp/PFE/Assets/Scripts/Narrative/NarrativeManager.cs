using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Copacetic.Narrative;

namespace Copacetic.Narrative
{
    public class NarrativeManager : MonoBehaviour
    {
        NarrativeTree Tree;

        // Start is called before the first frame update
        void Start()
        {
            /*
            Tree = ScriptableObject.CreateInstance<NarrativeTree>();

            DebugLogNode log1 = ScriptableObject.CreateInstance<DebugLogNode>();
            log1.Message = "1";
            DebugLogNode log2 = ScriptableObject.CreateInstance<DebugLogNode>();
            log2.Message = "2";
            DebugLogNode log3 = ScriptableObject.CreateInstance<DebugLogNode>();
            log3.Message = "3";

            DebugLogNode logEnd = ScriptableObject.CreateInstance<DebugLogNode>();
            log3.Message = "End";

            WaitNode wait1 = ScriptableObject.CreateInstance<WaitNode>();
            wait1.Duration = 1;
            WaitNode wait2 = ScriptableObject.CreateInstance<WaitNode>();
            wait2.Duration = 2;
            WaitNode wait3 = ScriptableObject.CreateInstance<WaitNode>();
            wait3.Duration = 3;

            SequencerNode sequence = ScriptableObject.CreateInstance<SequencerNode>();
            sequence.Childrens.Add(wait1);
            sequence.Childrens.Add(log1);
            sequence.Childrens.Add(wait2);
            sequence.Childrens.Add(log2);
            sequence.Childrens.Add(wait3);
            sequence.Childrens.Add(log3);
            sequence.Childrens.Add(logEnd);

            Tree.RootNode = sequence;
            */
        }

        // Update is called once per frame
        void Update()
        {
            Tree.Update();
        }
    }
}
