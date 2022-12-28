using UnityEngine;
using UnityEngine.UI;
public class GameStart : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.StartGame();
            GetComponent<Text>().enabled = false;
            this.enabled = false;

        }
    }
}
