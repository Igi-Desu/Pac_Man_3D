using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// represents node from map
/// </summary>
public class Node : MonoBehaviour
{
    public PathNode pathnode;
    /// <summary>
    /// represents a single node connected to current one 
    /// </summary>
    [System.Serializable]
    public struct NeighbourNode
    {
        public Node node;
        public int dist;
        public NeighbourNode(Node node, int dist)
        {
            this.node = node;
            this.dist = dist;
        }
        public override string ToString()
        {
            return "Node with cost of " + dist;
        }
    }
    /// <summary>
    /// lst of all nodes connect to current one 
    /// </summary>
    public List<NeighbourNode> neightbourNodes { get; private set; } = new List<NeighbourNode>();
    /// <summary>
    /// List of vectors representing possible turns from current node
    /// all vectors contained are simple directions left right up down
    /// </summary>
    public List<Vector2> whereWeCanGo { get; private set; }
    /// <summary>
    /// represents walls - objects that we can't go through
    /// </summary>
    [SerializeField] LayerMask walls;
    [SerializeField] LayerMask nodeMask;
    protected void Start()
    {
        whereWeCanGo = new List<Vector2>();
        //invoke checkpositions after a moment so that all nodes already got their
        //start method finished
        Invoke("CheckPositions", 0.5f);
    }
    /// <summary>
    /// Checks all possible directions looking for neighbours
    /// </summary>
    void CheckPositions()
    {
        CheckPos(Vector2.up);
        CheckPos(Vector2.right);
        CheckPos(Vector2.down);
        CheckPos(Vector2.left);
        pathnode = new PathNode(this);
    }
    /// <summary>
    /// Checks for neighbour and adds it to neighbour list if found
    /// </summary>
    /// <param name="direction">direction in which we are looking for a neighbour</param>
    void CheckPos(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * .75f, .0f, direction, 1.5f, walls);
        //means we can go this way so there should be node there
        if (hit.collider == null)
        {
            //add node and calculate the cost(distance) between them
            RaycastHit2D nodehit = Physics2D.Raycast(gameObject.transform.position + (Vector3)direction, direction, 15f, nodeMask);
            if (nodehit.collider == null)
            {
                return;
            }
            whereWeCanGo.Add(direction);
            //else add that node into neighbours list 
            Node n = nodehit.transform.GetComponent<Node>();
            NeighbourNode node = new NeighbourNode(n, CalculateDistance(this, n));
            neightbourNodes.Add(node);
        }
    }
    /// <summary>
    /// calculates distance between two given nodes
    /// it calculates manhattan distance since we can't diagonally
    /// </summary>
    public static int CalculateDistance(Node a, Node b)
    {
        Vector2 dist = a.transform.position - b.transform.position;
        int distance = (int)Mathf.Abs(dist.x + dist.y);
        return distance;
    }
}
