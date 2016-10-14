using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayButton : MonoBehaviour
{
    public void ChangeScene()
    {
        Debug.Log("Change Scene");
        SceneManager.LoadScene("game");
    }
    public void ExitGame()
    {
        Application.CancelQuit();
    }
}
