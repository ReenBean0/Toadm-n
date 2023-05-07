using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void LoadCaveLevel()
    {
        Debug.Log("Load dark level");
        SceneManager.LoadScene("Dark_Level");
    }

    public void LoadPushPullLevel()
    {
        Debug.Log("Load push-pull level");
        SceneManager.LoadScene("Push-Pull");
    }

    public void LoadMultiPressLevel()
    {
        Debug.Log("Load multi-press level");
        SceneManager.LoadScene("Multi-Press");
    }
}
