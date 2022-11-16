using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pelletcounter : MonoBehaviour
{
    static int pelletCount=0;
    [SerializeField]
    static Text pelletCountText;
    static GameObject instance;
    public static int PelletCount { get { return pelletCount; } set
        {
            pelletCountText.text = value.ToString();
            pelletCount = value;
        } }
    void Awake()
    {
        
        if (instance != null)
        {
            Destroy(transform.parent.gameObject);
            return;
        }
        instance = gameObject.transform.parent.gameObject;
        pelletCountText = GetComponent<Text>();
        DontDestroyOnLoad(gameObject.transform.parent);
    }
}
