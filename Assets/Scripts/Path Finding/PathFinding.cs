using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : Singleton<PathFinding>
{
    const float timerbase = 2;
    //A* pathfinding
    /// <summary>
    /// Finds bath between two given nodes, uses A* algorithm
    /// </summary>
    /// <param name="start">start node</param>
    /// <param name="finish">finish node</param>
    /// <returns>list of directions objects need to take to arrive at finish node</returns>
    public List<Vector2> FindPath(Node start, Node finish)
    {
        if (start == null || finish == null) return new List<Vector2>();
        if (start == finish) return new List<Vector2>();
        //closed list represents nodes already searched
        List<PathNode> closedList = new List<PathNode>();
        //open list represents nodes ready to be searched
        List<PathNode> openList = new List<PathNode>();
        start.pathnode.hCost = CalculatHCost(start.transform.position, finish.transform.position);
        start.pathnode.fCost = start.pathnode.hCost;
        start.pathnode.gCost = 0;
        //Actual lowest cost
        int lowestcost = int.MaxValue;
        //currentnode we are searching
        Node currentnode = start;

        while (currentnode.neightbourNodes.Count != 0)
        {
            //gcost of current node
            int localgcost = currentnode.pathnode.gCost;
            int i = -1;
            foreach (var neighbour in currentnode.neightbourNodes)
            {
                i++;
                //calculate all costs
                int nodehcost = CalculatHCost(neighbour.node.transform.position, finish.transform.position);
                int nodegcost = localgcost + neighbour.dist;
                if (nodegcost > neighbour.node.pathnode.gCost)
                {
                    continue;
                }
                //if costs are optimal assign them to node
                int nodefcost = nodegcost + nodehcost;
                neighbour.node.pathnode.fromWhere = currentnode.pathnode;
                neighbour.node.pathnode.hCost = nodehcost;
                neighbour.node.pathnode.gCost = nodegcost;
                neighbour.node.pathnode.fCost = nodefcost;
                neighbour.node.pathnode.fromwheredir = currentnode.whereWeCanGo[i];
                //if we reach the end
                openList.Add(neighbour.node.pathnode);
                if (neighbour.node == finish)
                {
                    closedList.Add(currentnode.pathnode);
                    List<Vector2> dirs = new List<Vector2>();
                    dirs = Retrack(start.pathnode, neighbour.node.pathnode);
                    ResetNodes(ref closedList);
                    ResetNodes(ref openList);
                    return dirs;
                }

            }
            //remove already searched node from the openlist and add it to closedlist
            openList.Remove(currentnode.pathnode);
            closedList.Add(currentnode.pathnode);
            //if we don't have any more nodes to search
            if (openList.Count == 0)
            {
                break;
            }


            lowestcost = int.MaxValue;
            PathNode currentBest = null;
            foreach (var pathNode in openList)
            {
                if (pathNode.node.pathnode.fCost <= lowestcost)
                {
                    lowestcost = pathNode.fCost;
                    currentBest = pathNode;
                }
            }
            currentnode = currentBest.node;

        }
        //checking all nodes and not finding path means that graph was not made properly
        ResetNodes(ref closedList);
        ResetNodes(ref openList);
        throw new System.Exception("Path could not be found");
    }

    /// <summary>
    /// returns list of turns needed to take destination
    /// </summary>
    /// <returns></returns>
    public List<Vector2> Retrack(PathNode startNode, PathNode finishNode)
    {
        List<Vector2> directions = new List<Vector2>();
        while (finishNode != startNode)
        {
            Vector2 dir = finishNode.node.pathnode.fromwheredir;
            directions.Insert(0, dir);
            //Debug.DrawLine(finishNode.node.transform.position, finishNode.fromWhere.node.transform.position, Color.red, timerbase/2);
            finishNode = finishNode.fromWhere;
        }
        return directions;
    }
    /// <summary>
    /// Reset all nodes in given list so that it will be ready for next A* usage
    /// </summary>
    /// <param name="nodeList">List to be reset</param>
    void ResetNodes(ref List<PathNode> nodeList)
    {
        foreach (var node in nodeList)
        {
            node.fCost = int.MaxValue;
            node.gCost = int.MaxValue;
            node.hCost = int.MaxValue;
            node.fromWhere = null;
        }
    }

    int CalculatHCost(Vector3 firstPos, Vector3 otherPos)
    {
        int x = (int)Mathf.Abs(firstPos.x - otherPos.x);
        int y = (int)Mathf.Abs(firstPos.y - otherPos.y);
        return x + y;
    }
}
