using System;
using System.Collections;
using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    Coroutine rotationCoroutine;
    void Update()
    {
        //Don't allow player to rotate camera when it already is rotating by itself
        if (rotationCoroutine != null) return;
        if(Input.GetKey(KeyCode.RightArrow)){
            transform.rotation= Quaternion.Euler(transform.rotation.eulerAngles.x,transform.rotation.eulerAngles.y-100*Time.deltaTime,transform.rotation.eulerAngles.z);
        }
         if(Input.GetKey(KeyCode.LeftArrow)){
            transform.rotation= Quaternion.Euler(transform.rotation.eulerAngles.x,transform.rotation.eulerAngles.y+100*Time.deltaTime,transform.rotation.eulerAngles.z);
        }
         
    }
    /// <summary>
    /// Smoothly rotates camera toward specific angle
    /// </summary>
    /// <param name="rot">rotation in Euler angles</param>
    public void SmoothRotateTowards(Vector3 rot)
    {
        if(rotationCoroutine!=null){
            StopCoroutine(rotationCoroutine);
        }
        rotationCoroutine=StartCoroutine(RotateCor(transform.rotation.eulerAngles,rot)); 
    }

    IEnumerator RotateCor(Vector3 from, Vector3 to){
        //If difference between rotations is over 180
        if(Math.Abs(from.y-to.y)>180){
            from.y=-(360-from.y);
        }
        float rotateSpeed=200;
        while(from!=to){
            from = Vector3.MoveTowards(from,to,rotateSpeed*Time.deltaTime);
            transform.rotation=Quaternion.Euler(from);
            yield return new WaitForEndOfFrame();
        }
        rotationCoroutine=null;
    }
}
