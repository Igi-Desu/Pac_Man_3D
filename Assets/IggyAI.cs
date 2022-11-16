using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IggyAI : Ghost
{

    List<Vector2> directions = new List<Vector2>();
    //Ghost that uses A* pathfinding algorithm
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Node n = collision.GetComponent<Node>();
        if(n==null)return;
        PathFinding.startnode = n.pathnode;
        if (currentstate == State.afraid)
        {
            Vector2 curdir = ClydeAlg(n);
            movScript.SetDirection(curdir);
            return;
        }
        directions = PathFinding.FindPath(PathFinding.startnode, PathFinding.finishnode);
        if (directions.Count == 0)
        {
         //   Debug.Log("why is that");
            Vector2 dir = ((Vector2)target.position - (Vector2)n.transform.position).normalized;
            Vector2Int movementvector = new(Mathf.RoundToInt(dir.x), Mathf.RoundToInt(dir.y));
            movScript.SetDirection(movementvector);
            return;
        }
        //move with the ways 
        Move();
    }
    void Move()
    {
        //Debug.Log($"moving to {directions[0]}");
        movScript.SetDirection(directions[0]);
        directions.RemoveAt(0);
    }
}
