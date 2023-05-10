using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

/// <summary>
/// Used in the cave level to swap to the fireflies prefab and enable the light
/// - Henry Paul
///     - Edited by Rian to change the whole model instead of relying on the animation to change the sprite
/// </summary>
public class FlySwap : ObjectResponse
{
    [SerializeField] GameObject standardflies;
    [SerializeField] GameObject fireflies;
    //[SerializeField] Sprite standardFlies;
    //[SerializeField] Sprite fireflies;
    [SerializeField] GameObject sun;

    //[SerializeField] AnimationClip swapToFireAnim;

    bool usingFireflies;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        usingFireflies = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
    }

    /// <summary>
    /// This was originally going to be able to actually swap back to normal flies but... time kind of... got in the way a little bit and I never got round to finishing it
    /// It still swaps to fireflies though
    /// </summary>
    public void SwapFlies()
    {
        if (usingFireflies)
        {
            //spriteRenderer.sprite = standardFlies;
        }
        else
        {
            GetComponent<FlyController>().lockMovement = true;
            //spriteRenderer.sprite = fireflies;
            GetComponent<Animator>().enabled = true;
            GetComponent<Animator>().Play("swapToFireflies");
            StartCoroutine(SwapFlyModel(2));
            StartCoroutine(WaitForAnim(5));
        }
    }

    IEnumerator SwapFlyModel(float animLength)
    {
        yield return new WaitForSecondsRealtime(animLength);
        standardflies.SetActive(false);
        fireflies.SetActive(true);
    }

    public override void Interact()
    {
        SwapFlies();
    }

    IEnumerator WaitForAnim(float animLength)
    {
        yield return new WaitForSecondsRealtime(animLength);
        usingFireflies = true;
        GetComponent<FlyController>().lockMovement = false;
        GetComponent<Animator>().Play("Idle");
        GetComponent<Animator>().enabled = false;
        //spriteRenderer.sprite = fireflies;
        //transform.position = new Vector3(-55.9799995f, -60.2799988f, 0);
    }
}
