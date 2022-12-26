using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigPellet : pellet
{
    public delegate void ScareGhosts(Ghost.State state);
    public static event ScareGhosts ghostEvent;
    public override IEnumerator GetEaten()
    {
        Destroy(gameObject);
        yield return null;
    }
    void OnDestroy()
    {
        if (ghostEvent != null)
        {
            ghostEvent(Ghost.State.afraid);
        }
        
    }
}
