using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple script to control the death and respawn of the toad
/// - Henry Paul
/// </summary>
public class ToadRespawn : MonoBehaviour
{
    [SerializeField] GameObject currentCheckpoint;

    int deaths;

    // Start is called before the first frame update
    void Start()
    {
        deaths = 0;
    }

    /// <summary>
    /// Called by a checkpoint trigger
    /// </summary>
    /// <param name="checkpoint"></param>
    public void EnterCheckpoint(GameObject checkpoint)
    {
        currentCheckpoint = checkpoint;
    }

    /// <summary>
    /// whatcanisayimafunnyguy
    /// Called by the DeathZone script when the toad collides with its trigger (toad dies)
    /// </summary>
    public void IsThisDarkSoulsQuestionMark()
    {
        deaths++;
        GameManager.instance.totalDeaths++;
        Debug.Log($"Deaths: {deaths}");
        GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        transform.position = currentCheckpoint.GetComponent<Checkpoint>().GetRespawnPosition();
    }

    public int GetDeaths()
    {
        return deaths;
    }
}
