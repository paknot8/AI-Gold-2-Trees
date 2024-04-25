using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is a Composite Node
public class SequenceNode : IBaseNode
{
    private List<IBaseNode> children;

    public SequenceNode(List<IBaseNode> childs) 
    {
        children = childs;
    }

    // If 1st node of the sequence fails then it goes to the second node.
    // if the 1st and 2nd node fails, then it goes to the third.
    // The sequence are in order, so the node at the bottom will execute and not the above
    // unitl the last one at the bottom is false
    public virtual bool Update() 
    {
        foreach(IBaseNode node in children) 
        {
            bool result = node.Update();

            // Has the above node Failed?
            if(!result)
            {
                return false;
            }
        }
        return true; 
    }
}
