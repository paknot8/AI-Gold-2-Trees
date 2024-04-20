using System.Collections.Generic;

// --- The Node Class ---
namespace BehaviorTrees
{
    // #1
    // Make the Node enum
    public enum NodeState
    {
        RUNNING,
        SUCCES,
        FAILURE
    }

    // #2 
    // Acces and modify this value, when derived from this Node
    public class Node
    {
        protected NodeState state;

        // #3  
        // Link by 2 directions to make it easier to create composite nodes by looking at the children
        // And to have shared data by looking at the parents and backtracking in the branch
        public Node parent;
        protected List<Node> children;

        // #8
        // Now store the data in a Dictionary
        // using the C# object Lazy type, there will be a mapping of named variables with a string
        // that can be of any type and can be any data you want to store.
        private Dictionary<string, object> _dataContext = new Dictionary<string, object>();

        // #4
        // Constructor will be assign in the class or else it will be empty (Null)
        public Node()
        {
            parent = null;
        }

        // #6 
        // Call in the Node constructor by doing an foreach child in children
        public Node(List<Node> children)
        {
            foreach (Node child in children)
            {
                _Attach(child);
            }
        }

        // #5
        //  Link parent field when creating the tree
        // rely on UTM method called attach
        // This makes he edge between a Node and its new child
        private void _Attach(Node node)
        {
            node.parent = this;
            children.Add(node);
        }

        // #7
        // prepare the evaluate function prototype
        // It's virtual so that each derived Node Class can implement its own evaluation function
        // and have a unique role in the behaviour tree
        public virtual NodeState Evaluate() => NodeState.FAILURE;

        // #9
        // To set the data, add a Key in the dictionary in #8 
        public void SetData(string key, object value)
        {
            _dataContext[key] = value;
        }

        // #10
        // Now we want to check if it's defined somewhere in our branch not just in this Node
        // this will make it easier to access and use the shared data in our behaviour tree
        // ---
        // Make data get Data Recursive 
        // (continue looking up in the branch until we found the key we were looking for) or
        // we have reached the root of the tree
        public object GetData(string key)
        {
            object value = null;
            if (_dataContext.TryGetValue(key, out value))
            {
                return value;
            }

            Node node = parent;

            while (node != null)
            {
                value = node.GetData(key);
                if (value != null)
                {
                    return value;
                }
                node = node.parent;
            }
            return null;
        }

        // #11
        // To clear data the process is the same,
        // Recursively search for the key, if found remove it or else
        // if reached root ignore the request
        public bool ClearData(string key)
        {
            if (_dataContext.ContainsKey(key))
            {
                _dataContext.Remove(key);
                return true;
            }

            Node node = parent;

            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                {
                    return true;
                }
                node = node.parent;
            }
            return false;
        }

        // for #12 Go to the Tree.cs
    }
}
