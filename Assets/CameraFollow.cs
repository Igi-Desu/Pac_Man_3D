using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    Coroutine coroutine;
    // Update is called once per frame
    void Update()
    {
        if (coroutine != null) return;
        if(Input.GetKey(KeyCode.RightArrow)){
            transform.rotation= Quaternion.Euler(transform.rotation.eulerAngles.x,transform.rotation.eulerAngles.y-100*Time.deltaTime,transform.rotation.eulerAngles.z);
        }
         if(Input.GetKey(KeyCode.LeftArrow)){
            transform.rotation= Quaternion.Euler(transform.rotation.eulerAngles.x,transform.rotation.eulerAngles.y+100*Time.deltaTime,transform.rotation.eulerAngles.z);
        }
         
    }
    public void RotateTowards(Vector3 rot)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        Vector3 currentRotation = transform.rotation.eulerAngles;
        Vector3 altRot = currentRotation;
        altRot.y = -(360 - altRot.y);
        if ((currentRotation - rot).sqrMagnitude < (altRot - rot).sqrMagnitude)
        {
            if (currentRotation.y < rot.y)
            {
                coroutine = StartCoroutine(MoveRight(currentRotation, rot));
            }
           else
            {
                coroutine = StartCoroutine(MoveLeft(currentRotation, rot));
            }
        }
        else
        {
            if (altRot.y < rot.y)
            {
                coroutine = StartCoroutine(MoveRight(altRot, rot));
            }
            else
            {
                coroutine = StartCoroutine(MoveLeft(altRot, rot));
            }
        }
      
     
    }
   IEnumerator MoveLeft(Vector3 from, Vector3 to)
    {
        while (from != to)
        {
            from.y -= 200 * Time.deltaTime;
            if (from.y < to.y)
            {
                from.y = to.y;
            }
            transform.rotation = Quaternion.Euler(from);
            yield return new WaitForEndOfFrame();
        }
        coroutine = null;
    }
    IEnumerator MoveRight(Vector3 from, Vector3 to)
    {
        while (from != to)
        {
            from.y += 200 * Time.deltaTime;
            if (from.y > to.y)
            {
                from.y = to.y;
            }
            transform.rotation = Quaternion.Euler(from);
            yield return new WaitForEndOfFrame();
        }
        coroutine = null;
    }
}
