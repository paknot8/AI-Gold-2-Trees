using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Enemy : MonoBehaviour
{
    private IBaseNode behaviourTree = null;
    private Color originalColor; // Variable to store the original color
    private NavMeshAgent agent;
    public Transform player;
    public GameObject bulletPrefab;
    public Item item;
    public List<Transform> waypoints;
    public TextMeshProUGUI text;

    [Header("Ranges")]
    private readonly float pickupDetectionDistance = 20f;
    private readonly float attackDistance = 15f;
    private readonly float moveAwayDistance = 6f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        originalColor = GetComponent<Renderer>().material.color; // Save the original color
        CreateBehaviourTree();
    }

    void Update()
    {
        item = FindAnyObjectByType<Item>();
        behaviourTree?.Update();
    }
    
    private void CreateBehaviourTree()
    {
        List<IBaseNode> PassiveMode = new()
        {
            new EnemyTalkNode("Aaah! Don't come closer!",agent,text,moveAwayDistance,attackDistance),
            new PickupNode(agent,pickupDetectionDistance,item,text),
            new PatrolNode(agent,waypoints,attackDistance,pickupDetectionDistance,item),
        };

        List<IBaseNode> AggressiveMode = new()
        {
            new RetreatNode(agent,moveAwayDistance),
            new ChaseNode(agent,player,attackDistance,moveAwayDistance),
            new ShootNode(agent,bulletPrefab,attackDistance,moveAwayDistance),
        };

        List<IBaseNode> SelectBehaviour = new()
        {
            new SequenceNode(AggressiveMode), 
            new SequenceNode(PassiveMode),
        };


        behaviourTree = new SelectorNode(SelectBehaviour);
    }

    // Draw Gizmos to visualize the detection range
    void OnDrawGizmosSelected()
    {
        if (agent != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(agent.transform.position, attackDistance);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(agent.transform.position, moveAwayDistance);
        
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(agent.transform.position, pickupDetectionDistance);
        }
    }

    // Restore original color when needed
    public void RestoreOriginalColor()
    {
        GetComponent<Renderer>().material.color = originalColor;
    }
}
