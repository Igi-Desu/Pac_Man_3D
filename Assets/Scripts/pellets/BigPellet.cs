using System.Collections;

public class BigPellet : pellet
{
    public delegate void ScareGhosts(Ghost.State state);
    /// <summary>
    /// makes all ghosts go into afraid
    /// </summary>
    public static event ScareGhosts MakeGhostsAfraid;
    public override IEnumerator GetEaten()
    {
        Destroy(gameObject);
        yield return null;
    }
    /// <summary>
    /// When big pellets gets eaten make ghosts afraid
    /// </summary>
    void OnDestroy()
    {
        MakeGhostsAfraid?.Invoke(Ghost.State.afraid);
    }
}
