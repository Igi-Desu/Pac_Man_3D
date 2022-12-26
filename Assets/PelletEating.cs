using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletEating : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision) {
        if(collision.transform.tag=="Pellet"){
            pellet pelletscript = collision.GetComponent<pellet>();
            if (pelletscript == null) return;
            StartCoroutine(collision.gameObject.GetComponent<pellet>().GetEaten());
            return;
        }
    }
      
}
