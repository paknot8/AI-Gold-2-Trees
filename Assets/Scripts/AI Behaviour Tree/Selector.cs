using System.Collections.Generic;

public class Selector : Node
{
    private List<Node> nodes = new List<Node>();

    public Selector(List<Node> nodes)
    {
        this.nodes = nodes;
    }

    public override bool Execute()
    {
        foreach (Node node in nodes)
        {
            if (node.Execute())
            {
                return true;
            }
        }
        return false;
    }
}
