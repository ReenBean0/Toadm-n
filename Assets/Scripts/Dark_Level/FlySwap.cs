using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlySwap : ObjectResponse
{
    [SerializeField] Sprite standardFlies;
    [SerializeField] Sprite fireflies;
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
            StartCoroutine(WaitForAnim(5));
        }
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
        spriteRenderer.sprite = fireflies;
        //transform.position = new Vector3(-55.9799995f, -60.2799988f, 0);
    }
}
