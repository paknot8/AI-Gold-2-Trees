using System.Collections.Generic;

// This is a Composite Node
public class SequenceNode : IBaseNode
{
    private List<IBaseNode> children;

    public SequenceNode(List<IBaseNode> childs) 
    {
        children = childs;
    }

    // The sequence are in order
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
