using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField]protected GhostEyes eyes;
    [SerializeField]protected GameObject eyesobject;
    protected Transform target;//current target
    [SerializeField] protected Transform scatterTarget;
    [SerializeField] protected Transform playerTarget;
    [SerializeField] protected AnimatedSprite ghostanim;
    [SerializeField] protected Sprite[] normalghost;
    [SerializeField] protected Sprite[] afraidghost;
    [SerializeField] protected SpriteRenderer ghostSprite;
    protected Color ghostColor;
    

   
    [SerializeField] Vector3 basePosition;

   public enum State
    {
        chase,
        scatter,
        afraid,
    }
   public State currentstate;
    public float basespeed = 6;
    protected Movement movScript;
    float timer = 5f;
    Coroutine timerCor = null;
    public float Timer { get => timer; set {
            timer = value;
            timerCor = StartCoroutine(StartTimer());
        } }


    protected void Start()
    {
        basePosition = transform.position;
        ghostSprite = transform.parent.Find("Sprite").transform.GetComponent<SpriteRenderer>();
        ghostColor = ghostSprite.color;
        movScript = GetComponent<Movement>();
        movScript.changeDirEvent += eyes.changeEyes;
        ChangeState(State.chase);
        Invoke("SetDirOnStart", Random.Range(2,8));
        Timer = 10;
        BigPellet.ghostEvent += ChangeState;
    }
 protected void SetDirOnStart()
    {
        movScript.SetDirection(Vector2.up);
    }

    protected Vector2 BlinkyAlgorithm(Node n, Vector3 target)
    {
        float lowest = float.MaxValue;
        Vector2 curdir = Vector2.zero;
        foreach (var dir in n.whereWeCanGo)
        {
            if (dir == movScript.moveDirection * -1) continue;
            Vector3 newpos = n.transform.position + (Vector3)dir;
            //don't need to use length since it's slower
            float dist = (target - newpos).sqrMagnitude;
            if (dist < lowest)
            {
                lowest = dist;
                curdir = dir;
            }
        }
        return curdir;
    }
    protected Vector2 ClydeAlg(Node n)
    {
        Vector2 curdir = Vector2.zero ;
        if (n.whereWeCanGo.Count != 1)
        {
            int index = RngGenerator.GetRandomIntUniform(0, n.whereWeCanGo.Count);
            if (n.whereWeCanGo[index] == movScript.moveDirection * -1)
            {
                index = (index + 1 == n.whereWeCanGo.Count) ? 0 : index + 1;
            }
            curdir = n.whereWeCanGo[index];
        }
        return curdir;
    }
    public void ChangeState(State state)
    {
        StopAllCoroutines();
        ghostSprite.enabled = true;
        currentstate = state;
        switch (state)
        {
            case State.afraid:
                Timer = 3;
                movScript.speed = basespeed * 0.5f;
                ghostanim.sprites = afraidghost;
                eyesobject.SetActive(false);
                eyes.enabled = false;
                ghostSprite.color = Color.white;
                break;
            case State.chase:
                target = playerTarget;
                movScript.speed = basespeed;
                ghostanim.sprites = normalghost;
                eyesobject.SetActive(true);
                eyes.enabled = true;
                ghostSprite.color = ghostColor;
                break;
            case State.scatter:
                target = scatterTarget;
                movScript.speed = basespeed;
                break;
        }
    }
    public void KillGhost()
    {
        movScript.resetDirection();
        transform.position = basePosition;
        Invoke("SetDirOnStart", 2);
        ChangeState(State.chase);
    }

    IEnumerator StartTimer()
    {
        if (currentstate == State.afraid)
        {
            StartCoroutine(BlinkCor());
        }
       
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        NextState();
      }
    IEnumerator BlinkCor()
    {
        while (true)
        {
            if (timer < 1)
            {
                ghostSprite.enabled = !ghostSprite.enabled;
            }
            yield return new WaitForSeconds(0.0625f);
        }
    }
    public void NextState()
    {
        if (timerCor != null)
        {
            StopCoroutine(timerCor);
        }
        timerCor = null;
        if (currentstate == State.afraid)
        {
            ChangeState(State.chase);
            Timer = 15;
            return;
        }
        if (currentstate == State.chase)
        {
            ChangeState(State.scatter);
            Timer = 5;
            return;
        }
        if (currentstate == State.scatter)
        {
            ChangeState(State.chase);
            Timer = 15;
            return;
        }
    }
    public void OnDestroy()
    {
        BigPellet.ghostEvent -= ChangeState;
    }
}
