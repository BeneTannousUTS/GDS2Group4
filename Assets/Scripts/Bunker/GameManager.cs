using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int dayNum = 0;

    public void WinState()
    {
        SceneManager.LoadScene("WinCutscene");
        dayNum += 1;
    }

    public void DefenceLoseState()
    {
        SceneManager.LoadScene("LoseDefenceCutscene");
    }

    public void ScavengeLoseState()
    {
        SceneManager.LoadScene("LoseState");
    }
}
