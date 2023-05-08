using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraBounds : MonoBehaviour
{
    [SerializeField] float targetScale = 5;
    [SerializeField] Vector3 targetCamPos = new Vector3(0, 0, 0);
    [SerializeField] bool followToadOnTrigger;

    [SerializeField] List<GameObject> activateObjects;
    [SerializeField] float activateObjectsDelay;
    [SerializeField] GameObject ambientSource;
    [SerializeField] AudioClip ambientAudioClip;

    CameraController cameraController;

    void Start()
    {
        cameraController = GameManager.instance.GetComponent<CameraController>();
    }

    public Vector3 TargetCamPos
    {
        get { return targetCamPos; }
    }

    public float TargetScale
    {
        get { return targetScale; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (followToadOnTrigger)
            {
                cameraController.FollowObject(collision.gameObject, targetScale);
            }
            else
            {
                cameraController.onCollideWithCameraBounds(this);
            }

            if (activateObjects != null)
            {
                StartCoroutine(ActivateObjects());
            }

            if (ambientAudioClip != null)
            {
                AudioSource ambience = ambientSource.GetComponent<AudioSource>();
                if (ambience.clip != ambientAudioClip)
                {
                    ambience.clip = ambientAudioClip;
                    ambience.Play();
                }
            }
        }
    }

    IEnumerator ActivateObjects()
    {
        yield return new WaitForSecondsRealtime(activateObjectsDelay);
        foreach (GameObject gameObject in activateObjects)
        {
            gameObject.GetComponent<ObjectResponse>().Interact();
        }
    }
}
