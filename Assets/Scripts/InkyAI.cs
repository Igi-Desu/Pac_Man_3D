using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkyAI : Ghost
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Node n = collision.GetComponent<Node>();
        if (n == null) return;
        Vector2 curdir = Vector2.zero;
        if (currentstate == State.afraid)
        {
            curdir = ClydeAlg(n);
        }
        else
        {
            curdir = BlinkyAlgorithm(n, target.position + target.forward * 10+target.up*5);
        }
        movScript.SetDirection(curdir);
    }


}
