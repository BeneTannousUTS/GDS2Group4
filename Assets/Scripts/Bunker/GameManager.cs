using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int dayNum = 0;

    public void WinState()
    {
        SceneManager.LoadScene("WinState");
        dayNum += 1;
    }

    public void LoseState()
    {
        SceneManager.LoadScene("LoseState");
    }
}
