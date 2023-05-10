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
    /// <summary>
    /// Requirements for "starts/trophies/toads" for each level
    /// </summary>
    public struct ToadRequirements
    {
        public int toad1; // less than this amount of deaths
        public double toad2; // less than this number of seconds
        public int toad3; // less than this amount of licks
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
     * -Rian (created the class with singleton design)
     * - Populated by Henry Paul
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

        // Set the requirements for each level
        caveToadRequirements = new ToadRequirements();
        caveToadRequirements.toad1 = 3; // less than 3 deaths
        caveToadRequirements.toad2 = 180; // less than 180 seconds
        caveToadRequirements.toad3 = 15; // less than 15 licks

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
        if (Input.GetKey(KeyCode.Escape))
        {
            ReturnToMenu();
        }
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

    /// <summary>
    /// Called by door activation script when the player enters the door
    /// </summary>
    public void LevelComplete()
    {
        isLevelComplete = true;
        
        // Begins save process by getting the current best toads and time
        int toadsToSave = bestToadsEarnedForThisLevel;
        double timeToSave = bestTimeForThisLevel;

        // If current time is better than saved time, overwrite timeToSave with current time
        if (elapsedTimeInSeconds < bestTimeForThisLevel || bestTimeForThisLevel == 0.00)
        {
            timeToSave = elapsedTimeInSeconds;
        }

        // If toads earned now is better than saved toads, overwrite toadsToSave
        int toadsEarnedNow = GetNumberOfToadsEarnedAtEndOfLevel();
        if (toadsEarnedNow > bestToadsEarnedForThisLevel)
        {
            toadsToSave = toadsEarnedNow;
        }
        
        // Save both values in the leveldata file. If no new best is achieved, this will just re-write the current best
        SaveData(timeToSave, toadsToSave);
    }

    void SaveData(double time, int toads)
    {
        TextAsset levelDataFile = Resources.Load<TextAsset>("LevelData"); // I'm too scared to delete this line, it was a haily mary fix for an issue found that only exists in the build version
                                                                          // we know how to properly fix it but heads are too fried. This line didn't end up being the solution, but, removing it may
                                                                          // or may not break it again, too scared to find out

        // LevelData is a single line in a text file, with each value seperated by '/'
        // Get the whole line so that specific values can be overwritten where needed, and
        // the whole line being written again
        StreamReader reader = new StreamReader("LevelData.txt");
        string data = reader.ReadLine();
        //string data = levelDataFile.text;
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
            if (s != "")
            {
                writer.Write("/");
            }
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

    /// <summary>
    /// Use at end of level
    /// Determines how many toads the player has unlocked based on their total deaths, time taken and total licks
    /// </summary>
    /// <returns>Toads earned for this level in this attempt</returns>
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

        Debug.Log($"Licks: {totalLicks}");

        return toads;
    }
}
