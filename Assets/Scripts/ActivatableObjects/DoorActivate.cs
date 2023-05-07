using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorActivate : MonoBehaviour, IActivatableObject
{
    [SerializeField] bool isOpen;
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject toad;

    public void Interact()
    {
        isOpen = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isOpen)
        {
            // level complete
            Debug.Log("Level complete");
            GameManager.instance.isLevelComplete = true;

            TextMeshProUGUI deathsText = winScreen.transform.Find("Panel").transform.Find("DeathsText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI timeText = winScreen.transform.Find("Panel").transform.Find("TimeText").GetComponent<TextMeshProUGUI>();

            winScreen.GetComponent<Canvas>().enabled = true;
            deathsText.text = ($"Deaths: {toad.GetComponent<ToadRespawn>().GetDeaths()}");
            timeText.text = ($"Time Taken: {GameManager.instance.GetTimeElapsed():0.00}s");
            winScreen.GetComponent<Animator>().SetBool("Win?", true);
        }
    }
}
