using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class Player : Singleton<Player>
{
    Node currentNode=null;
    /// <summary>
    /// returns current node pacman is at 
    /// </summary>
    public Node CurrentNode => currentNode;
    Movement moveScript;
    [SerializeField]GameObject spriteObject;
    [SerializeField] public InputPlayer controls;
    [SerializeField]CameraFollow playercamera;
    System.Action<InputAction.CallbackContext> horizontalMove, verticalMove;

    //   Rigidbody2D rb;
    protected void Start()
    {
        //  rb = GetComponent<Rigidbody2D>();    
       // spriteObject=transform.root.Find("Sprite").gameObject;
        controls = new InputPlayer();
        controls.Enable();
        moveScript = GetComponent<Movement>();
        verticalMove = ctx => movement(new Vector2(0,ctx.ReadValue<float>()));
        horizontalMove = ctx => movement(new Vector2(ctx.ReadValue<float>(),0));
        controls.PlayerBase.TopDownMove.performed += verticalMove;
        controls.PlayerBase.LeftRightMove.performed += horizontalMove;
        moveScript.changeDirEvent += flip;

    }

    //Friendship with void update() ended now input system performed delegate are my new best friends
    void movement(Vector2 direction)
    {
           moveScript.SetDirection(direction);
    }
    void flip(Vector2 dir)
    {
        if (dir == Vector2.up)
        {
           transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
            spriteObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
        }
        else if (dir == Vector2.left)
        {
            spriteObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
        }
        else if (dir == Vector2.right)
        {
            spriteObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else if (dir == Vector2.down)
        {
            spriteObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 270));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Node")
        {
            Node n = collision.GetComponent<Node>();
            if (n == null) return;
            currentNode=n;
            return;
        }
        if(collision.transform.tag=="Ghost"){
            Ghost ghostScript = collision.transform.GetComponent<Ghost>();
            if (ghostScript.currentState == Ghost.State.afraid)
            {
                ghostScript.KillGhost();             
                return;
            }
          SceneManager.LoadScene(0);
          return;
        }
        if (collision.transform.tag == "wall1")
        {
            playercamera.SmoothRotateTowards(new Vector3(0, 0, 0));
            return;
        }
        if (collision.transform.tag == "wall2")
        {
            playercamera.SmoothRotateTowards(new Vector3(0, -90, 0));
            return;
        }
        if (collision.transform.tag == "wall3")
        {
            playercamera.SmoothRotateTowards(new Vector3(0, 90, 0));
            return;
        }
        if (collision.transform.tag == "wall4")
        {
            playercamera.SmoothRotateTowards(new Vector3(0, 180, 0));
            return;
        }
    }
    public void OnDestroy()
    {
        if(horizontalMove!=null)
        controls.PlayerBase.LeftRightMove.performed -= horizontalMove;
        if (verticalMove != null)
            controls.PlayerBase.TopDownMove.performed -= verticalMove;
    }
}
