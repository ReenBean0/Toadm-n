using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /* This is an example of a singleton. This acts as a global
     * instance of a MonoBehaviour script, which can be applied
     * to a game object, whereas a static script would not be.
     * 
     * This allows you to call any public variable or method from
     * this class by preceeding it with "GameManager.instance."
     * 
     * You may want to put game code in here, or have specific other
     * singletons for other managers, eg. an audio manager, a scene
     * manager, an input manager, so on and so forth. 
     * 
     * If you didn't know about this design pattern, I hope this helps!
     * Of course, you are under no obligation to use it if you prefer
     * abstraction or whatever else, but sometimes it helps to have your
     * game logic and variables in one accessible place.
     * -Rian
     */
    public static GameManager instance { get; private set; }

    DateTime startTime;
    double elapsedTimeInSeconds;
    DateTime currentTime;

    public bool isLevelComplete;

    string levelName;
    int toadsEarnedForThisLevel;
    double bestTimeForThisLevel;

    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;
        }
    }

    private void Start()
    {
        startTime = DateTime.UtcNow;
        elapsedTimeInSeconds = 0;
        isLevelComplete = false;
        levelName = SceneManager.GetActiveScene().name;
        LoadData();
    }

    private void Update()
    {
        if (!isLevelComplete)
        {
            currentTime = DateTime.UtcNow;
            elapsedTimeInSeconds = currentTime.Subtract(startTime).TotalSeconds;
        }
    }

    public double GetTimeElapsed()
    {
        return elapsedTimeInSeconds;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LevelComplete()
    {
        isLevelComplete = true;
        int toadsToSave = toadsEarnedForThisLevel;
        double timeToSave = bestTimeForThisLevel;
        if (elapsedTimeInSeconds < bestTimeForThisLevel || bestTimeForThisLevel == 0.00)
        {
            timeToSave = elapsedTimeInSeconds;
        }
        SaveData(timeToSave, toadsToSave);
    }

    void SaveData(double time, int toads)
    {
        StreamReader reader = new StreamReader("LevelData.txt");
        string data = reader.ReadLine();
        string[] dataArray = data.Split('/');
        reader.Close();

        switch (levelName)
        {
            case "Dark_Level":
                dataArray[1] = time.ToString("0.00");
                dataArray[2] = toads.ToString();
                break;
            case "Push-Pull":
                dataArray[4] = time.ToString("0.00");
                dataArray[5] = toads.ToString();
                break;
            case "Multi-Press":
                dataArray[7] = time.ToString("0.00");
                dataArray[8] = toads.ToString();
                break;
        }

        StreamWriter writer = new StreamWriter("LevelData.txt", false);
        foreach (string s in dataArray)
        {
            writer.Write(s);
            writer.Write("/");
        }
        writer.Close();
    }

    void LoadData()
    {
        StreamReader reader = new StreamReader("LevelData.txt");
        string data = reader.ReadLine();
        reader.Close();

        string[] dataArray = data.Split('/');
        for (int i = 0; i < dataArray.Length; i += 3)
        {
            if (dataArray[i] == levelName)
            {
                bestTimeForThisLevel = double.Parse(dataArray[i + 1]);
                toadsEarnedForThisLevel = int.Parse(dataArray[i + 2]);
            }
        }

        Debug.Log($"Current level: {levelName} | Best time: {bestTimeForThisLevel} | Toads earned: {toadsEarnedForThisLevel}");
    }
}
