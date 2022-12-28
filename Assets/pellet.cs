using System.Collections;
using UnityEngine;

public class pellet : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.AddPellet();
    }
    /// <summary>
    /// Eats pellet, plays sound then destroys object
    /// </summary>
    public virtual IEnumerator GetEaten(){
        GetComponent<BoxCollider>().enabled = false;
        GameManager.Instance.RemovePellet();
        GetComponent<SpriteRenderer>().enabled=false;
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1.25f);
        Destroy(gameObject);
    }
}
