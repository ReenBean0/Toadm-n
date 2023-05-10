using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Hub for controlling the toad sounds
/// - Henry Paul
/// </summary>
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

    /// <summary>
    /// Plays the ribbit sound, I have no idea if there's an actual name for this sound that they make but ribbit works fine
    /// </summary>
    /// <param name="ribbitCooldown">time in seconds before toad can ribbit again</param>
    /// <returns></returns>
    public IEnumerator Ribbit(int ribbitCooldown)
    {
        Debug.Log("ribbit");
        canRibbit = false;
        audioSource.PlayOneShot(toadRibbit);
        yield return new WaitForSecondsRealtime(ribbitCooldown);
        canRibbit = true;
    }

    /// <summary>
    /// Called when toad hits floor in leapingcontroller script
    /// </summary>
    public void PlayFloorHit()
    {
        audioSource.PlayOneShot(floorHit);
    }

    /// <summary>
    /// Play a knock sound when toad hits an object that isn't landing on a platform
    /// </summary>
    public void PlayCollisionSound()
    {
        audioSource.PlayOneShot(collision);
    }

    /// <summary>
    /// Play a random noise from a selection of 'whips' when the tongue is fired, called in tonguecontroller
    /// </summary>
    public void PlayRandomTongueWhip()
    {
        int rnd = Random.Range(0, tongueWhipSounds.Count);
        audioSource.PlayOneShot(tongueWhipSounds[rnd]);
    }

    /// <summary>
    /// Called in tongue controller when tongue hits something
    /// </summary>
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
