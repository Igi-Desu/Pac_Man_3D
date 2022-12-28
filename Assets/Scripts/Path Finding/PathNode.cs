using UnityEngine;

[System.Serializable]
/// <summary>
/// represents a pathnode used in A* pathfinding algorithm
/// </summary>
public class PathNode {
    /// <summary>
    /// Represents Node object that this pathnode is attached to
    /// </summary>
    public Node node;
    /// <summary>
    /// heuristic cost of getting to end node from this one
    /// </summary>
    public int hCost= int.MaxValue;
    /// <summary>
    /// Real cost of getting from start node to this one 
    /// </summary>
    public int gCost= int.MaxValue;
    /// <summary>
    /// gcost + fcost -> the lower, the higher chance way is better according to A*
    /// </summary>
    public int fCost= int.MaxValue;
    /// <summary>
    /// From which pathnode we got to this one
    /// </summary>
    public PathNode fromWhere=null;
    /// <summary>
    /// direction from which we got to this node
    /// </summary>
    public Vector2 fromwheredir=Vector2.zero;
    public PathNode(Node node){
            this.node=node;
    }

}