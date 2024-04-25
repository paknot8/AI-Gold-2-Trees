using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkNode : IBaseNode
{
    private string destination;

    public WalkNode(string destination) 
    {
        this.destination = destination;
    }

    public virtual bool Update() 
    {
        Debug.Log(" " + destination);
        return true;
    }
}
