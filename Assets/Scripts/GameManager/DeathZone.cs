using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Kills the poor little toad if it falls into it
/// - Henry Paul
/// </summary>
public class DeathZone : MonoBehaviour
{
    [SerializeField] GameObject toad;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "Player")
            {
                toad.GetComponent<ToadRespawn>().IsThisDarkSoulsQuestionMark();
            }
        }
    }
}
