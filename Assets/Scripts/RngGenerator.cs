using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RngGenerator 
{
    //Summary:
    //returns random integer between [min ,max), max not inclusive
    public static int GetRandomIntUniform(int min, int max){
            return Random.Range(min,max);
    }
    //Summary:
    //returns random float between [min ,max), max not inclusive
      public static float GetRandomFloatUniform(float min, float max){
            return Random.Range(min,max);
    }
}
