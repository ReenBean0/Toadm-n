using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The mainest of the menus
/// Don't know what to say, speaks for itself really
/// - Henry Paul
/// </summary>
public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject caveDropdown;
    [SerializeField] GameObject pushDropdown;
    [SerializeField] GameObject multiDropdown;


    int caveToads;
    double caveTime;

    int pushToads;
    double pushTime;

    int multiToads;
    double multiTime;

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

    public void LoadData()
    {
        // load level scores for level select UI
        StreamReader reader = new StreamReader("LevelData.txt");
        string data = reader.ReadLine();
        reader.Close();
        string[] dataArray = data.Split('/');

        double caveTime = double.Parse(dataArray[1]);
        int caveToads = int.Parse(dataArray[2]);
        caveDropdown.GetComponent<UpdateLevelBorder>().UpdateBorder(caveTime.ToString("0.00") + "s", caveToads);

        double pushTime = double.Parse(dataArray[4]);
        int pushToads = int.Parse(dataArray[5]);
        pushDropdown.GetComponent<UpdateLevelBorder>().UpdateBorder(pushTime.ToString("0.00") + "s", pushToads);

        double multiTime = double.Parse(dataArray[7]);
        int multiToads = int.Parse(dataArray[8]);
        multiDropdown.GetComponent<UpdateLevelBorder>().UpdateBorder(multiTime.ToString("0.00") + "s", multiToads);
    }
}
