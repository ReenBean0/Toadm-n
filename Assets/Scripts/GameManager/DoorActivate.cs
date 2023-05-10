using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls the level completion door and what happens when the player enters it or if the player can enter it yet
/// - Henry Paul
/// </summary>
public class DoorActivate : MonoBehaviour, IActivatableObject
{
    [SerializeField] bool isOpen;
    [SerializeField] CanvasManager winScreen;
    [SerializeField] GameObject toad;

    public void Interact()
    {
        isOpen = !isOpen;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isOpen)
        {
            // level complete
            Debug.Log("Level complete");
            GameManager.instance.LevelComplete();

            // Find all required UI elements in level complete screen
            TextMeshProUGUI deathsText = winScreen.transform.Find("Panel").transform.Find("DeathsText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI timeText = winScreen.transform.Find("Panel").transform.Find("TimeText").GetComponent<TextMeshProUGUI>();

            UnityEngine.UI.Image toad1 = winScreen.transform.Find("Panel").transform.Find("Toad1").GetComponent<UnityEngine.UI.Image>();
            UnityEngine.UI.Image toad2 = winScreen.transform.Find("Panel").transform.Find("Toad2").GetComponent<UnityEngine.UI.Image>();
            UnityEngine.UI.Image toad3 = winScreen.transform.Find("Panel").transform.Find("Toad3").GetComponent<UnityEngine.UI.Image>();

            // GameManager will determine how many toads have been earned for this level based on performance
            int toadsEarned = GameManager.instance.GetNumberOfToadsEarnedAtEndOfLevel();

            // Update UI elements
            winScreen.GetComponent<Canvas>().enabled = true;
            deathsText.text = ($"Deaths: {toad.GetComponent<ToadRespawn>().GetDeaths()}");
            timeText.text = ($"Time Taken: {GameManager.instance.GetTimeElapsed():0.00}s");

            GameManager.ToadRequirements requirements = GameManager.instance.GetCurrentToadRequirements();
            toad1.gameObject.transform.Find("Toad1Req").GetComponent<TextMeshProUGUI>().text = $"< {requirements.toad1} deaths";
            toad2.gameObject.transform.Find("Toad2Req").GetComponent<TextMeshProUGUI>().text = $"< {requirements.toad2}s";
            toad3.gameObject.transform.Find("Toad3Req").GetComponent<TextMeshProUGUI>().text = $"< {requirements.toad3} licks";

            if (toadsEarned > 0)
            {
                toad1.GetComponent<UnityEngine.UI.Image>().color = Color.white;
                if (toadsEarned > 1)
                {
                    toad2.GetComponent<UnityEngine.UI.Image>().color = Color.white;
                    if (toadsEarned > 2)
                    {
                        toad3.GetComponent<UnityEngine.UI.Image>().color = Color.white;
                    }
                }
            }

            // Play animation
            winScreen.GetComponent<Animator>().SetBool("Win?", true);
        }
    }
}
