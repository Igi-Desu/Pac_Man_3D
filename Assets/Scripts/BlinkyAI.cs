using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkyAI : Ghost
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //get node object that ghost collided with
        Node n = collision.GetComponent<Node>();
        if (n == null) return;
        Vector2 curdir=Vector2.zero;
        //fcompare every direction ghost can go with pacman position to find optimal one
        if (currentstate == State.afraid)
        {
            curdir = ClydeAlg(n);
        }
        else
        {
            curdir = BlinkyAlgorithm(n, target.position + target.forward);
        }
        //set movement direction of ghost
        movScript.SetDirection(curdir);
    }
}
