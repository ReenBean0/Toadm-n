using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Used as a trigger for the death / respawn system
///  - Henry Paul
/// </summary>
public class Checkpoint : MonoBehaviour
{
    [SerializeField] Vector3 respawnPosition;
    [SerializeField] GameObject toad;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "Player")
            {
                toad.GetComponent<ToadRespawn>().EnterCheckpoint(gameObject);
            }
        }
    }

    public Vector3 GetRespawnPosition()
    {
        return respawnPosition;
    }
}