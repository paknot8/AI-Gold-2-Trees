using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// --- Generic Behaviour Tree --- //
namespace BehaviorTrees
{
    public abstract class Tree : MonoBehaviour
    {
        // #12
        // reference to a root itself recursively contains the entire tree
        private Node _root = null; 

        // #13
        // Two Things
        // Upon start the tree Class will build the behaviour tree according to the setup tree function defined
        protected void Start()
        {
            _root = SetupTree();
        }

        // #14
        // If it has a tree it will evaluate it continuesly
        private void Update()
        {
            if (_root != null)
            {
                _root.Evaluate();
            }
        }

        protected abstract Node SetupTree();
    
        // after this prepare 2 Composite Nodes
    }
}
