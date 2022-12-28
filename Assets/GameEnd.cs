using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEnd : Singleton<GameEnd>
{
    bool won = false;
    
    void Update()
    {
        if (won)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(0);
            }
        }
    }
    public void Win()
    {
        won = true;
        Time.timeScale = 0;
        gameObject.GetComponent<Text>().enabled = true;
    }
}
