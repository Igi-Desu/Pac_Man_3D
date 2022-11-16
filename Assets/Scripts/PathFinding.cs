using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    //Variables for debugging purposes like list of all nodes to get random nodes. 
    //Start and finish node are variables where I save those randomly picked nodes.
    public static List<Node> allNodes = new List<Node>();
    public static PathNode startnode = null;
    public static PathNode finishnode = null;
    public static PathNode oldNode = null;
    [SerializeField] Node firstNode;
    float timer = 0.25f;
    const float timerbase = 2;
    //A* pathfinding
    private void Start()
    {
        Invoke("SetNode", 2f);
    }
    void SetNode()
    {
            finishnode = firstNode.pathnode;
        
    }
    public static List<Vector2> FindPath(PathNode start, PathNode finish)
    {
        //if we are already at finish return empty list
       // return new List<Vector2>();
        if (start == finish) return new List<Vector2>();
        //closed list represents nodes already searched
        List<PathNode> closedList = new List<PathNode>();
        //open list represents nodes ready to be searched
        List<PathNode> openList = new List<PathNode>();
        start.hcost = calculatecost(start, finish);
        start.fcost = start.hcost;
        //Actual lowest cost
        int lowestcost = int.MaxValue;
        //currentnode we are searching
        PathNode currentnode = start;
        
        while (currentnode.node.neightbourNodes.Count != 0)
        {
            //gcost of current node
            int localgcost = currentnode.gcost;
            int i = -1;
            foreach (var neighbour in currentnode.node.neightbourNodes)
            {
                i++;
                int nodehcost = calculatecost(neighbour.node.pathnode, finish);
                int nodegcost = localgcost + neighbour.dist;
                if(nodegcost>neighbour.node.pathnode.gcost)
                {
                    continue;
                }
                int nodefcost = nodegcost + nodehcost;
                neighbour.node.pathnode.fromwhere = currentnode;
                neighbour.node.pathnode.hcost = nodehcost;
                neighbour.node.pathnode.gcost = nodegcost;
                neighbour.node.pathnode.fcost = nodefcost;
                neighbour.node.pathnode.fromwheredir=currentnode.node.whereWeCanGo[i];
                //if we reach the end
                if (neighbour.node.transform.position == finish.node.transform.position)
                {
                    //add current nodes to lists
                    closedList.Add(currentnode);
                    closedList.Add(neighbour.node.pathnode);
                    //retrack the way to the finish
                    List<Vector2> dirs = new List<Vector2>();
                    dirs = Retrack(neighbour.node.pathnode,start);
                    //clear node values
                    ClearNodes(ref closedList);
                    ClearNodes(ref openList);
                    return dirs;
                }
                openList.Add(neighbour.node.pathnode);
            }
            //remove already searched node from the openlist and add it to closedlist
            openList.Remove(currentnode);
            closedList.Add(currentnode);
            //if we don't have any more nodes to search
            if (openList.Count == 0)
            {
                break;
            }
            //find first node with the lowest cost 
             currentnode = openList.Find(x => x.fcost <= lowestcost);
            //iIf we can't find good node
            if (currentnode == null)
            {
                List<PathNode> nodeList=new List<PathNode>();
                //find new node with lowest cost
                lowestcost = int.MaxValue;
                foreach (var node in openList)
                {
                    if (node.node.pathnode.fcost <= lowestcost)
                    {
                        if(node.node.pathnode.fcost<lowestcost)
                        {
                            nodeList.Clear();
                        }
                        nodeList.Add(node);
                        lowestcost = node.node.pathnode.fcost;
                    }
                }
                currentnode = nodeList[RngGenerator.GetRandomIntUniform(0, nodeList.Count)];
            }
        }
        //if we search everything and don't find a path
        //clear both node lists 
        ClearNodes(ref closedList);
        ClearNodes(ref openList);
        //return empty list
        return new List<Vector2>();
    }
    public static List<Vector2> Retrack(PathNode n, PathNode start)
    {
        //create list of directions ghost should take to reach player
        List<Vector2> directions = new List<Vector2>();
        while (n!=start)
        {
          //  Debug.Log("AAAAAAAAAAAAAA");
            //get direction such as 0,1 1,0 -1,0 0,-1
           // Vector2 dir = (n.node.transform.position - n.fromwhere.node.transform.position).normalized;
           Vector2 dir = n.node.pathnode.fromwheredir;
           
            //insert it at start of the list
            directions.Insert(0, dir);
            Debug.DrawLine(n.node.transform.position, n.fromwhere.node.transform.position, Color.red, timerbase/2);
            //get next node in the chain
            n = n.fromwhere;
        }
        return directions;
    }
    static void ClearNodes(ref List<PathNode> nodeList)
    {
        //clear everything in nodelist
        foreach (var node in nodeList)
        {
            node.fcost = int.MaxValue;
            node.gcost =  int.MaxValue;
            node.hcost =  int.MaxValue;
            node.fromwhere = null;
        }
    }
    static int calculatecost(PathNode node, PathNode other)
    {
        //heuristic cost -> pl. Heureza
        int x = (int)Mathf.Abs(node.node.transform.position.x - other.node.transform.position.x);
        int y = (int)Mathf.Abs(node.node.transform.position.y - other.node.transform.position.y);
        return x + y;
    }
}
