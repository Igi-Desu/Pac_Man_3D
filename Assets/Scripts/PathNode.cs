using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PathNode {
    public Node node;
    //heuristic cost
    public int hcost= int.MaxValue;
    //get to cost
    public int gcost= int.MaxValue;
    //expected cost (hcost+gcost)
    public int fcost= int.MaxValue;
    public PathNode fromwhere=null;
    public Vector2 fromwheredir=Vector2.zero;
    public PathNode(Node node){
            this.node=node;
    }

}