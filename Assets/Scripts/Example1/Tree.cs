using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTrees
{
    public class Tree : MonoBehaviour
    {
        // reference to a root itself recursively contains the entire tree
        private Node _root = null; 

        protected void Start()
        {
            _root = SetupTree();
        }
    }
}
