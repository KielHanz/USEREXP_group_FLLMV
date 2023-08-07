using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    public void ReplayGame()
    {
        SceneManager.LoadScene(2);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
