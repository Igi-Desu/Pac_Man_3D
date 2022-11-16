using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static int pelletCount = 0;
    public static GameEnd endgame;
    static GameManager instance;
    void Start()
    {
        if (instance != null) Destroy(gameObject);
        instance=this;
        endgame = GameObject.Find("Gameend").transform.GetChild(0).GetComponent<GameEnd>() ;
        Time.timeScale = 0;
        DontDestroyOnLoad(gameObject);
    }

    public static void RemovePellet()
    {
        pelletCount--;
        pelletcounter.PelletCount = pelletCount;
        if (pelletCount == 0)
        {
            endgame.Win();
        }
    }
    public static void AddPellet()
    {
        if (pelletCount >= 500) return;
        pelletCount++;
        pelletcounter.PelletCount = pelletCount;
    }
    public static void StartGame()
    {
        Time.timeScale = 1;
    }
   
}
