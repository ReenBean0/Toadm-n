using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToadRespawn : MonoBehaviour
{
    [SerializeField] GameObject currentCheckpoint;

    int deaths;

    // Start is called before the first frame update
    void Start()
    {
        deaths = 0;
    }

    public void EnterCheckpoint(GameObject checkpoint)
    {
        currentCheckpoint = checkpoint;
    }

    public void IsThisDarkSoulsQuestionMark()
    {
        deaths++;
        Debug.Log($"Deaths: {deaths}");
        GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        transform.position = currentCheckpoint.GetComponent<Checkpoint>().GetRespawnPosition();
    }

    public int GetDeaths()
    {
        return deaths;
    }
}
