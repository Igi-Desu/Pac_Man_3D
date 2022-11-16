using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public PathNode pathnode;
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
     public List<NeighbourNode> neightbourNodes { get; private set; } = new List<NeighbourNode>();
    public List<Vector2> whereWeCanGo { get; private set; }
    [SerializeField] LayerMask collisionLayer;
    [SerializeField] LayerMask nodeMask;
    protected void Start()
    {

        //add Node to pathfinding node list 
        whereWeCanGo = new List<Vector2>();
        Invoke("checkpositions", 0.5f);
    }
    void checkpositions()
    {
        CheckPos(Vector2.up);
        CheckPos(Vector2.right);
        CheckPos(Vector2.down);
        CheckPos(Vector2.left);
        pathnode = new PathNode(this);

        //after we check position
    }
    void CheckPos(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * .75f, .0f, direction, 1.5f, collisionLayer);
        //means we can go this way so there should be node there
        if (hit.collider == null)
        {
            //add node and calculate the cost(distance) between them
            RaycastHit2D nodehit = Physics2D.Raycast(gameObject.transform.position + (Vector3)direction, direction, 15f, nodeMask);
            //if there's no node in that direction just return
            if (nodehit.collider == null)
            {
                return;
            }
            whereWeCanGo.Add(direction);
            //else add that node into neighbours list 
            Node n = nodehit.transform.GetComponent<Node>();
            NeighbourNode node = new NeighbourNode(n, getcost(this, n));
            neightbourNodes.Add(node);
        }
    }
    int getcost(Node a, Node b)
    {
        Vector2 dist = a.transform.position - b.transform.position;
        int distance = (int)Mathf.Abs(dist.x + dist.y);
        // Debug.Log($" vectors  {a.transform.position} , {b.transform.position}");
        // Debug.Log(  $"AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA {distance}");
        return distance;
    }
    // private void OnDrawGizmos() {
    //     Gizmos.DrawRay(new Ray(transform.position,Vector2.up));
    //      Gizmos.DrawRay(new Ray(transform.position,Vector2.down));
    //      Gizmos.DrawRay(new Ray(transform.position,Vector2.right));
    //      Gizmos.DrawRay(new Ray(transform.position,Vector2.left));
    // }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawCube(transform.position + Vector3.left, Vector3.one * .9f);
    //}
}
