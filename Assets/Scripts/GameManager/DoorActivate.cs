using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

            //TextMeshProUGUI deathsText = winScreen.transform.Find("Panel").transform.Find("DeathsText").GetComponent<TextMeshProUGUI>();
            //TextMeshProUGUI timeText = winScreen.transform.Find("Panel").transform.Find("TimeText").GetComponent<TextMeshProUGUI>();

            //UnityEngine.UI.Image toad1 = winScreen.transform.Find("Panel").transform.Find("Toad1").GetComponent<UnityEngine.UI.Image>();
            //UnityEngine.UI.Image toad2 = winScreen.transform.Find("Panel").transform.Find("Toad2").GetComponent<UnityEngine.UI.Image>();
            //UnityEngine.UI.Image toad3 = winScreen.transform.Find("Panel").transform.Find("Toad3").GetComponent<UnityEngine.UI.Image>();

            int toadsEarned = GameManager.instance.GetNumberOfToadsEarnedAtEndOfLevel();

            //winScreen.gameObject.SetActive(true);
            winScreen.deathsText.text = ($"Deaths: {toad.GetComponent<ToadRespawn>().GetDeaths()}");
            winScreen.timeText.text = ($"Time Taken: {GameManager.instance.GetTimeElapsed():0.00}s");

            GameManager.ToadRequirements requirements = GameManager.instance.GetCurrentToadRequirements();
            winScreen.toad1.gameObject.transform.Find("Toad1Req").GetComponent<TextMeshProUGUI>().text = $"< {requirements.toad1} deaths";
            winScreen.toad2.gameObject.transform.Find("Toad2Req").GetComponent<TextMeshProUGUI>().text = $"< {requirements.toad2}s";
            winScreen.toad3.gameObject.transform.Find("Toad3Req").GetComponent<TextMeshProUGUI>().text = $"< {requirements.toad3} licks";

            if (toadsEarned > 0)
            {
                winScreen.toad1.color = Color.white;
                if (toadsEarned > 1)
                {
                    winScreen.toad2.color = Color.white;
                    if (toadsEarned > 2)
                    {
                        winScreen.toad3.color = Color.white;
                    }
                }
            }

            winScreen.animator.SetBool("Win?", true);
        }
    }
}
