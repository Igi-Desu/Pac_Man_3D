using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialNode : Node
{
    [SerializeField]Node secondone;
    [SerializeField]Vector2 direction;
    new void Start(){
        base.Start();
         Invoke("setup",1.5f);
    }
    void setup(){
    whereWeCanGo.Add(direction);
    neightbourNodes.Add(new NeighbourNode(secondone,2));
    }
}
