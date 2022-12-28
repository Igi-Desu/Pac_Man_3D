using UnityEngine;
/// <summary>
/// Represent specialnode that needs to be connected to other one manually 
/// i.e node that is connected by teleport to another
/// </summary>
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
