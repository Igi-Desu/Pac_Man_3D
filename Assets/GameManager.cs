using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private GameEnd endGameScreen;
    /// <summary>
    /// How many pellets pacman must eat to win
    /// </summary>
    const int PelletsNumber=500;
    void Start()
    {
        Time.timeScale = 0;
        endGameScreen=GameEnd.Instance;
        endGameScreen.enabled=false;
    }
    /// <summary>
    /// removes pellet from the total count, game is won if pelletcount is 0
    /// </summary>
    public void RemovePellet()
    {
        pelletcounter.Instance.PelletCount--;
        if (pelletcounter.Instance.PelletCount == 0)
        {
            endGameScreen.enabled=true;
            endGameScreen.Win();
        }
    }
    /// <summary>
    /// adds pellet to the total count 
    /// </summary>
    public void AddPellet()
    {
        if (pelletcounter.Instance.PelletCount >= PelletsNumber) return;
        pelletcounter.Instance.PelletCount++;
    }
    /// <summary>
    /// Everything that should happen when game starts
    /// </summary>
    public void StartGame()
    {
        Time.timeScale = 1;
    }
    
}
