using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceNode : IBaseNode
{
    private List<IBaseNode> children;

    public SequenceNode(List<IBaseNode> childs) 
    {
        children = childs;
    }

    public virtual bool Update() 
    {
        foreach(IBaseNode node in children) 
        {
            bool result = node.Update();

            if(!result) 
            {
                return false;
            }
        }

        return true;
    }
}
