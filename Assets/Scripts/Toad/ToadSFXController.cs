using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ToadSFXController : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] AudioClip toadRibbit;
    [SerializeField] AudioClip floorHit;
    [SerializeField] AudioClip collision;
    [SerializeField] List<AudioClip> tongueWhipSounds;
    [SerializeField] AudioClip tongueHit;
    [SerializeField] int maxTimeUntilRibbitAgain;
    [SerializeField] int minTimeUntilRibbitAgain;
    bool canRibbit;


    private void Start()
    {
        canRibbit = true;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (canRibbit)
        {
            int rnd = Random.Range(minTimeUntilRibbitAgain, maxTimeUntilRibbitAgain);
            StartCoroutine(Ribbit(rnd));
        }
    }

    public IEnumerator Ribbit(int ribbitCooldown)
    {
        Debug.Log("ribbit");
        canRibbit = false;
        audioSource.PlayOneShot(toadRibbit);
        yield return new WaitForSecondsRealtime(ribbitCooldown);
        canRibbit = true;
    }

    public void PlayFloorHit()
    {
        audioSource.PlayOneShot(floorHit);
    }

    public void PlayCollisionSound()
    {
        audioSource.PlayOneShot(collision);
    }

    public void PlayRandomTongueWhip()
    {
        int rnd = Random.Range(-1, tongueWhipSounds.Count);
        audioSource.PlayOneShot(tongueWhipSounds[rnd]);
    }

    public void PlayTongueHit()
    {
        audioSource.PlayOneShot(tongueHit);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Platform"))
        {
            PlayCollisionSound();
        }
    }
}
