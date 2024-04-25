using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugNode : IBaseNode
{
    private string destination;

    public DebugNode(string destination) 
    {
        this.destination = destination;
    }

    public virtual bool Update() 
    {
        Debug.Log(" " + destination);
        return true;
    }
}