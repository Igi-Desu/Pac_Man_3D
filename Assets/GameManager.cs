using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private GameEnd endGameScreen;
    const int PelletsNumber=500;
    void Start()
    {
        Time.timeScale = 0;
        endGameScreen=GameEnd.Instance;
        endGameScreen.enabled=false;
    }

    public void RemovePellet()
    {
        pelletcounter.Instance.PelletCount--;
        if (pelletcounter.Instance.PelletCount == 0)
        {
            endGameScreen.enabled=true;
            endGameScreen.Win();
        }
    }
    public void AddPellet()
    {
        if (pelletcounter.Instance.PelletCount >= PelletsNumber) return;
        pelletcounter.Instance.PelletCount++;
    }
    public void StartGame()
    {
        Time.timeScale = 1;
    }
    
}
