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
        rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
    }
    public void resetDirection()
    {
        moveDirection = Vector2.zero;
    }
    /// <summary>
    /// Sets direction objects goes, uf object can't go in given direction
    /// </summary>
    /// <param name="direction"></param>
    public void SetDirection(Vector2 direction)
    {
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
            nextDirection = direction;
        }
    }
    /// <summary>
    /// checks wether object can go in given direction
    /// </summary>
    /// <param name="direction">direction object is trying go</param>
    /// <returns>true if possible false otherwise</returns>
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
              transform.position=new Vector3(75,transform.position.y,transform.position.z);
            return;
        }
    }
}
