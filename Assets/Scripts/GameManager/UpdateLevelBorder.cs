using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

/// <summary>
/// Used to update the UI elements on the level select screen after the data has been loaded
/// - Henry Paul
/// </summary>
public class UpdateLevelBorder : MonoBehaviour
{
    [SerializeField] GameObject toad1;
    [SerializeField] GameObject toad2;
    [SerializeField] GameObject toad3;
    [SerializeField] GameObject timeText;

    public void UpdateBorder(string time, int numberOfToads)
    {
        if (time == "0.00s")
        {
            time = "N/A";
        }
        timeText.GetComponent<TextMeshProUGUI>().text = $"Best: {time}";

        if (numberOfToads > 0)
        {
            toad1.GetComponent<UnityEngine.UI.Image>().color = Color.white;
            if (numberOfToads > 1)
            {
                toad2.GetComponent<UnityEngine.UI.Image>().color = Color.white;
                if (numberOfToads > 2)
                {
                    toad3.GetComponent<UnityEngine.UI.Image>().color = Color.white;
                }
            }
        }
    }
}
