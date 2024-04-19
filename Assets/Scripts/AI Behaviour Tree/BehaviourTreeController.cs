using UnityEngine;
using System.Collections.Generic;

public class BehaviourTreeController : MonoBehaviour
{
    private Node root; // Reference to the root node of the behavior tree

    void Start()
    {
        // Construct the behavior tree
        ConstructBehaviourTree();
    }

    void Update()
    {
        // Execute the behavior tree
        root.Execute();
    }

    void ConstructBehaviourTree()
    {
        // Construct the behavior tree
        List<Node> nodes = new List<Node>();

        // Add your behavior nodes here
        nodes.Add(new MoveAwayBehaviour(transform));
        nodes.Add(new ChaseBehaviour(transform, transform));
        nodes.Add(new ShootBehaviour(transform));
        nodes.Add(new PatrolBehaviour(transform));

        // Construct selectors and sequences as needed
        Selector checkPlayerProximity = new Selector(nodes);
        Selector checkLineOfSight = new Selector(nodes);

        // Add conditions and actions to selectors and sequences

        // Set the root of the behavior tree
        List<Node> rootNodes = new List<Node>();
        rootNodes.Add(checkPlayerProximity);
        rootNodes.Add(checkLineOfSight);
        root = new Selector(rootNodes);
    }
}
