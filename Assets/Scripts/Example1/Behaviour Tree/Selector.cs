using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    // #17
    // The selector node is the same as sequence (and), but this is more like an (or) Gate
    // Return early when a child has succeeded or is running
    public class Selector : Node
    {
        public Selector() : base() { }
        public Selector(List<Node> children) : base(children) { }

        public override NodeState Evaluate()
        {
            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        continue;
                    case NodeState.SUCCES:
                        state = NodeState.SUCCES;
                        return state;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        return state;
                    default:
                        continue;
                }
            }
            state = NodeState.FAILURE;
            return state;
        }
    }
}

// this finishes the OR gate
