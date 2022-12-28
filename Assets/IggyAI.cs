using System;
using System.Collections.Generic;
using UnityEngine;

public class IggyAI : Ghost
{
    /// <summary>
    /// List of directions needed to take on every turn to reach pacman
    /// </summary>
    List<Vector2> directions = new List<Vector2>();


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Node n = collision.GetComponent<Node>();
        if(n==null)return;
        if (currentState == State.afraid)
        {
            Vector2 curdir = ClydeAlg(n);
            moveScript.SetDirection(curdir);
            return;
        }
        try{
            directions = PathFinding.Instance.FindPath(n, Player.Instance.CurrentNode);
        }
        catch(Exception e){
            Debug.LogError(e.Message);
            Destroy(gameObject);
        }
        if (directions.Count == 0)
        {
            Vector2 dir = BlinkyAlgorithm(n,target.position+target.forward);
            moveScript.SetDirection(dir);
            return;
        }

        MoveUsingPathFindingDir();
    }


    void MoveUsingPathFindingDir()
    {
        moveScript.SetDirection(directions[0]);
        directions.RemoveAt(0);
    }
}
