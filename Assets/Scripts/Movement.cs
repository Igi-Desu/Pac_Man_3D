using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    [SerializeField]LayerMask collisionLayer;
    public delegate void ChangeDirAction(Vector2 dir);
    public event ChangeDirAction changeDirEvent;
    public Vector2 moveDirection { get; private set; }
    public Vector2 nextDirection { get; private set; }
    bool block = false;
    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.constraints=RigidbodyConstraints2D.FreezeRotation;
        moveDirection = Vector2.zero;
        nextDirection = Vector2.zero;
    }

    void Update()
    {
        SetDirection(nextDirection);
    }
    private void FixedUpdate()
    {
       //Debug.Log($"direction = {moveDirection}");
      //  Debug.Log($"nextdirection = {nextDirection}");
        rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
    }
    public void resetDirection()
    {
        block = true;
        moveDirection = Vector2.zero;
        Invoke("changeBlock", 1);
    }
    void changeBlock()
    {
        block = false;
    }
    public void SetDirection(Vector2 direction)
    {
        if (block) return;
        if (direction == Vector2.zero)
        {
            return;
        }
        if (CheckDirection(direction))
        {
            if (changeDirEvent != null)
            {
                changeDirEvent(direction);
            }

            moveDirection = direction;
            nextDirection = Vector2.zero;
        }
        else
        {
          //  Debug.Log("Ale jak to");
            nextDirection = direction;
        }
    }
    //returns true if we can go this way
    //returns false if we can't go this way
    //@param
    //direction - they way we are going.
   public bool CheckDirection(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one*.65f, .0f, direction, 0.5f, collisionLayer);
        return hit.collider == null;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.tag=="RightTP"){
            transform.position=new Vector3(-45,transform.position.y,transform.position.z);
            return;
        }
        if(other.transform.tag=="LeftTP"){
            //tp to the right
              transform.position=new Vector3(75,transform.position.y,transform.position.z);
            return;
        }
    }
}
