using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pellet : MonoBehaviour
{
    void Start()
    {
        GameManager.AddPellet();
    }
    public virtual IEnumerator die(){
        GetComponent<BoxCollider>().enabled = false;
        GameManager.RemovePellet();
        GetComponent<SpriteRenderer>().enabled=false;
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1.25f);
        Destroy(gameObject);
    }
}
