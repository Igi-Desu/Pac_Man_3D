using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour
{
    public Transform target;
    
    //TODO unspaghetti this
    void Update()
    {
        TranslatePosition();
    }
    void TranslatePosition(){
        if(target.position.x<=15&&target.position.x>=-15){
            transform.SetPositionAndRotation(new Vector3(target.position.x,target.position.y,0), Quaternion.Euler(0,0,transform.rotation.eulerAngles.z));
            return;
        }
        if(target.position.x<=-15&&target.position.x>=-45){
            transform.SetPositionAndRotation(new Vector3(-15,target.position.y,(-target.position.x)-15), Quaternion.Euler(0,90,transform.rotation.eulerAngles.z));
            return;
        }
        if(target.position.x>=15&&target.position.x<=45){
            transform.SetPositionAndRotation(new Vector3(15,target.position.y,(target.position.x)-15), Quaternion.Euler(0,-90,transform.rotation.eulerAngles.z));
            return;
        }
        else{
            transform.SetPositionAndRotation(new Vector3(-target.position.x+60,target.position.y,30), Quaternion.Euler(0,180,transform.rotation.eulerAngles.z));
            return;
        }
    }
}
