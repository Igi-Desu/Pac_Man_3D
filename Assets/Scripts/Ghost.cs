using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public enum State
    {
        chase,
        scatter,
        afraid,
    }
    protected GhostEyes eyes;
    protected GameObject eyesObject;
    /// <summary>
    ///  Position where ghost goes in scatter state
    /// </summary>
    [SerializeField] protected Transform scatterTarget;
    /// <summary>
    /// Player position
    /// </summary>
    [SerializeField] protected Transform playerTarget;
    /// <summary>
    /// Current target that ghost should follow
    /// </summary>
    protected Transform target;
    protected AnimatedSprite ghostanim;
    static protected List<Sprite> normalGhostSprites = new();
    static protected List<Sprite> afraidGhostSprites = new();
    protected SpriteRenderer spriteRenderer;
    /// <summary>
    /// Base color of ghost
    /// </summary>
    protected Color ghostColor;
    /// <summary>
    /// Place where ghost starts game (should refer to the position of ghost house 
    /// in the middle of map)
    /// </summary>
    static Transform basePosition=null;
    public State currentstate;
    public float basespeed = 6;
    protected Movement movScript;
    float timer = 0f;
    Coroutine timerCor = null;

    public float Timer
    {
        get => timer; set
        {
            timer = value;
            timerCor = StartCoroutine(StartTimer());
        }
    }


    protected void Start()
    {
        if(basePosition==null){
            basePosition = GameObject.Find("MiddleOfGhostHouse").transform;
        }
        ghostanim = transform.Find("Sprite").GetComponent<AnimatedSprite>();
        spriteRenderer = transform.Find("Sprite").transform.GetComponent<SpriteRenderer>();
        movScript = GetComponent<Movement>();
        eyes = transform.Find("Sprite/Eyes").GetComponent<GhostEyes>();
        eyesObject = eyes.gameObject;

        if (afraidGhostSprites.Count == 0)
        {
            LoadSprites();
        }
        transform.position = basePosition.position;
        
        ghostColor = spriteRenderer.color;
        
        movScript.changeDirEvent += eyes.changeEyes;
        ChangeState(State.chase);
        Invoke("SetDirOnStart", UnityEngine.Random.Range(2, 8));
        Timer = 10;
        BigPellet.MakeGhostsAfraid += ChangeState;
    }

    void LoadSprites()
    {
        afraidGhostSprites = new List<Sprite>((Resources.LoadAll<Sprite>("Sprites/Ghost/GhostAfraid")));
        normalGhostSprites = new List<Sprite>((Resources.LoadAll<Sprite>("Sprites/Ghost/GhostNormal")));
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
        Vector2 curdir = Vector2.zero;
        if (n.whereWeCanGo.Count != 1)
        {
            int index = UnityEngine.Random.Range(0, n.whereWeCanGo.Count);
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
        spriteRenderer.enabled = true;
        currentstate = state;
        switch (state)
        {
            case State.afraid:
                Timer = 3;
                movScript.speed = basespeed * 0.5f;
                ghostanim.sprites = afraidGhostSprites;
                eyesObject.SetActive(false);
                eyes.enabled = false;
                spriteRenderer.color = Color.white;
                break;
            case State.chase:
                target = playerTarget;
                movScript.speed = basespeed;
                ghostanim.sprites = normalGhostSprites;
                eyesObject.SetActive(true);
                eyes.enabled = true;
                spriteRenderer.color = ghostColor;
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
        transform.position = basePosition.position;
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
                spriteRenderer.enabled = !spriteRenderer.enabled;
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
        BigPellet.MakeGhostsAfraid -= ChangeState;
    }
}
