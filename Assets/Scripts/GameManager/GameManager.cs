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
    public struct ToadRequirements
    {
        public int toad1;
        public double toad2;
        public int toad3;
    }

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
    int bestToadsEarnedForThisLevel;
    double bestTimeForThisLevel;

    ToadRequirements caveToadRequirements;
    ToadRequirements pushToadRequirements;
    ToadRequirements multiToadRequirements;

    public int totalDeaths;
    public int totalLicks;

    ToadRequirements currentRequirements;

    public ToadRequirements GetCurrentToadRequirements()
    {
        return currentRequirements;
    }

    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;
        }
    }

    private void Start()
    {
        totalLicks = 0;
        totalDeaths = 0;
        startTime = DateTime.UtcNow;
        elapsedTimeInSeconds = 0;
        isLevelComplete = false;
        levelName = SceneManager.GetActiveScene().name;
        LoadData();

        caveToadRequirements = new ToadRequirements();
        caveToadRequirements.toad1 = 3; // less than 3 deaths
        caveToadRequirements.toad2 = 180; // less than 250 seconds
        caveToadRequirements.toad3 = 8; // less than 8 licks

        pushToadRequirements = new ToadRequirements();
        pushToadRequirements.toad1 = 1; // less than 1 deaths
        pushToadRequirements.toad2 = 30; // less than 30 seconds
        pushToadRequirements.toad3 = 10; // less than 10 licks

        multiToadRequirements = new ToadRequirements();
        multiToadRequirements.toad1 = 1; // less than 1 deaths
        multiToadRequirements.toad2 = 30; // less than 30 seconds
        multiToadRequirements.toad3 = 10; // less than 10 licks

        currentRequirements = new ToadRequirements();
        switch (levelName)
        {
            case "Dark_Level":
                currentRequirements = caveToadRequirements;
                break;
            case "Push-Pull":
                currentRequirements = pushToadRequirements;
                break;
            case "Multi-Press":
                currentRequirements = multiToadRequirements;
                break;
        }
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
        int toadsToSave = bestToadsEarnedForThisLevel;
        double timeToSave = bestTimeForThisLevel;
        if (elapsedTimeInSeconds < bestTimeForThisLevel || bestTimeForThisLevel == 0.00)
        {
            timeToSave = elapsedTimeInSeconds;
        }

        int toadsEarnedNow = GetNumberOfToadsEarnedAtEndOfLevel();
        if (toadsEarnedNow > bestToadsEarnedForThisLevel)
        {
            toadsToSave = toadsEarnedNow;
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
                bestToadsEarnedForThisLevel = int.Parse(dataArray[i + 2]);
            }
        }

        Debug.Log($"Current level: {levelName} | Best time: {bestTimeForThisLevel} | Toads earned: {bestToadsEarnedForThisLevel}");
    }

    public int GetNumberOfToadsEarnedAtEndOfLevel()
    {
        int toads = 0;

        if (totalDeaths < currentRequirements.toad1)
        {
            toads++;
            if (elapsedTimeInSeconds < currentRequirements.toad2)
            {
                toads++;
                if (totalLicks < currentRequirements.toad3)
                {
                    toads++;
                }
            }
        }

        return toads;
    }
}
