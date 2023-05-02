using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayTextMeshPro : ObjectResponse
{
    [SerializeField] List<GameObject> textPrefabs;
    [SerializeField] float delay;

    public override void Interact()
    {
        StartCoroutine(InteractWithDelay());
    }

    IEnumerator InteractWithDelay()
    {
        yield return new WaitForSecondsRealtime(delay);
        foreach (GameObject text in textPrefabs)
        {
            Instantiate(text, gameObject.transform);
        }
    }
}
