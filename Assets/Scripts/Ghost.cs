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
    protected AnimatedSprite ghostAnim;
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
    static Transform basePosition = null;
    public State currentState;
    public float baseSpeed = 6;
    protected Movement moveScript;
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
        if (basePosition == null)
        {
            basePosition = GameObject.Find("MiddleOfGhostHouse").transform;
        }
        ghostAnim = transform.Find("Sprite").GetComponent<AnimatedSprite>();
        spriteRenderer = transform.Find("Sprite").transform.GetComponent<SpriteRenderer>();
        moveScript = GetComponent<Movement>();
        eyes = transform.Find("Sprite/Eyes").GetComponent<GhostEyes>();
        eyesObject = eyes.gameObject;

        if (afraidGhostSprites.Count == 0)
        {
            LoadSprites();
        }
        transform.position = basePosition.position;

        ghostColor = spriteRenderer.color;

        moveScript.changeDirEvent += eyes.changeEyes;
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
        moveScript.SetDirection(Vector2.up);
    }
    /// <summary>
    /// Uses the algorithm that red ghost had in original pacman
    /// returns in which direction ghost should go on turn to reach pacman
    /// </summary>
    /// <param name="n">node that ghost touched</param>
    /// <param name="targetPosition">position where ghost wants to go, changing it
    /// might result in different ghost behaviour, for example flanking player from front</param>
    protected Vector2 BlinkyAlgorithm(Node n, Vector3 targetPosition)
    {
        float lowest = float.MaxValue;
        Vector2 curdir = Vector2.zero;
        foreach (var dir in n.whereWeCanGo)
        {
            if (dir == moveScript.moveDirection * -1) continue;
            Vector3 newpos = n.transform.position + (Vector3)dir;
            //don't need to use length since it's slower
            float dist = (targetPosition - newpos).sqrMagnitude;
            if (dist < lowest)
            {
                lowest = dist;
                curdir = dir;
            }
        }
        return curdir;
    }
    /// <summary>
    /// picks random legal direction where ghost can go
    /// </summary>
    /// <param name="n">node that ghost touched</param>
    /// <returns></returns>
    protected Vector2 ClydeAlg(Node n)
    {
        Vector2 curdir = Vector2.zero;
        if (n.whereWeCanGo.Count <= 1)
        {
            throw new System.Exception("something wrong with the graph node has less than 2 neighbours");
        }
        //count - 1 so that probability of ghost chosing after direction is the equal
        //if count we have 0 1 2 3 with one direction illegal let's say its direction 1
        //then if random number is 1 algorithm will choose direction 2 
        //so algorithm will choose direction 2 if rng gives either 1 or 2 meaning
        //that probability of getting this path chosen is 2 times the others
        int index = UnityEngine.Random.Range(0, n.whereWeCanGo.Count - 1);
        if (n.whereWeCanGo[index] == moveScript.moveDirection * -1)
        {
            index = (index + 1 == n.whereWeCanGo.Count) ? 0 : index + 1;
        }
        curdir = n.whereWeCanGo[index];

        return curdir;
    }
    /// <summary>
    /// changes state of ghost
    /// </summary>
    /// <param name="state">state ghost should change into</param>
    public void ChangeState(State state)
    {
        StopAllCoroutines();
        spriteRenderer.enabled = true;
        currentState = state;
        switch (state)
        {
            case State.afraid:
                Timer = 3;
                moveScript.speed = baseSpeed * 0.5f;
                ghostAnim.sprites = afraidGhostSprites;
                eyesObject.SetActive(false);
                eyes.enabled = false;
                spriteRenderer.color = Color.white;
                break;
            case State.chase:
                target = playerTarget;
                moveScript.speed = baseSpeed;
                ghostAnim.sprites = normalGhostSprites;
                eyesObject.SetActive(true);
                eyes.enabled = true;
                spriteRenderer.color = ghostColor;
                break;
            case State.scatter:
                target = scatterTarget;
                moveScript.speed = baseSpeed;
                break;
        }
    }
    /// <summary>
    /// Manages changing ghost state to the next one after current one ends
    /// </summary>
    public void NextState()
    {
        if (timerCor != null)
        {
            StopCoroutine(timerCor);
        }
        timerCor = null;
        if (currentState == State.afraid)
        {
            ChangeState(State.chase);
            Timer = 15;
            return;
        }
        if (currentState == State.chase)
        {
            ChangeState(State.scatter);
            Timer = 5;
            return;
        }
        if (currentState == State.scatter)
        {
            ChangeState(State.chase);
            Timer = 15;
            return;
        }
    }
    /// <summary>
    /// Defines what should happen when ghost is killed
    /// </summary>
    public void KillGhost()
    {
        moveScript.resetDirection();
        transform.position = basePosition.position;
        Invoke("SetDirOnStart", 2);
        ChangeState(State.chase);
    }
    /// <summary>
    /// Starts state timer, after timer ends ghost goes into nextstate
    /// </summary>
    /// <returns></returns>
    IEnumerator StartTimer()
    {
        if (currentState == State.afraid)
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
    /// <summary>
    /// ghost sprite starts blinking when timer is about to end
    /// </summary>
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

    public void OnDestroy()
    {
        BigPellet.MakeGhostsAfraid -= ChangeState;
    }
}
