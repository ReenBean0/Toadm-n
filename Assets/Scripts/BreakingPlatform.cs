using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingPlatform : MonoBehaviour
{
    [SerializeField] float animIntensity;
    [SerializeField] float animSpeed;
    [SerializeField] float secondsUntilBreak;
    bool breaking;
    Vector3 startPos;

    public void Start()
    {
        breaking = false;
        startPos = transform.parent.position;
    }

    public void Update()
    {
        if (breaking)
        {
            float x = startPos.x + Mathf.Sin(Time.time * animSpeed) * animIntensity;
            transform.parent.position = new Vector3(x, transform.parent.position.y, transform.parent.position.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.transform.position.y > transform.position.y)
        {
            breaking = true;
            StartCoroutine(Break());
        }
    }

    IEnumerator Break()
    {
        yield return new WaitForSecondsRealtime(secondsUntilBreak);
        Destroy(transform.parent.gameObject);
    }
}
