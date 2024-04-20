using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// --- and gate --- //
namespace BehaviorTree
{
    // #15
    // Derive from Node Class
    // Sequence is a composite that acts like an AND logic gate
    // only if all child nodes succeeds, it will succeed itself
    public class Sequence : Node
    {
        public Sequence() : base() { }
        public Sequence(List<Node> children) : base(children) { }

        // #16
        // Iterate through the children and check the state after evaluation
        // if any child fails STOP there and return fail state
        // else keep on processing the children and
        // eventually check if some are running then block in the running state
        // or if all have succeeded
        public override NodeState Evaluate()
        {
            bool anyChildISRunning = false;
            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE: // if any child fails then STOP and fail state
                        state = NodeState.FAILURE;
                        return state;
                    case NodeState.SUCCES: // else continue processing
                        continue;
                    case NodeState.RUNNING: // if some are running true and continue
                        anyChildISRunning = true;
                        continue;
                    default:
                        state = NodeState.SUCCES; // if all have succeeded
                        return state;
                }
                
            }
            state = anyChildISRunning ? NodeState.RUNNING : NodeState.SUCCES;
            return state;
        }
    }
}

// Go to #17 in selector node