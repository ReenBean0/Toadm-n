using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script that, believe it or not, controls the tongue
/// Currently allows player to shoot tongue at a target location using the spacebar
///  - Henry Paul
/// </summary>
public class TongueController : MonoBehaviour
{
    [SerializeField] GameObject flyCursor;
    [SerializeField] GameObject tonguePrefab;
    [SerializeField] float animationSpeed;
    [SerializeField] float animationPauseBeforeReversing;

    // Serialized so the cooldown status can be seen in the editor
    [SerializeField] bool tongueCooldown;

    Transform flyTransform;
    float flyX;
    float flyY;

    GameObject tongueInstance;

    // Start is called before the first frame update
    void Start()
    {
        flyTransform = flyCursor.GetComponent<Transform>();
        tongueCooldown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !tongueCooldown)
        {
            flyX = flyTransform.position.x;
            flyY = flyTransform.position.y;
            Debug.Log($"Launch tongue at X={flyX}, y={flyY}");

            // Temporarily use mouse position
            //Vector3 mousePosition = Input.mousePosition;
            //mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            //Debug.Log($"Launch tongue at X={mousePosition.x}, y={mousePosition.y}");

            Vector3 tongueDestination = new Vector3(flyX, flyY, 1);
            LaunchTongue(tongueDestination);
        }
    }

    void LaunchTongue(Vector3 targetPos)
    {
        // Spawn tongue
        tongueInstance = Instantiate(tonguePrefab, new Vector3(transform.position.x, transform.position.y, 1), Quaternion.identity);

        // Rotate tongue to face target point
        Vector3 direction = targetPos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        tongueInstance.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Stretch tongue (multiplied by two because of the size of the prefab)
        float distance = Vector2.Distance(tongueInstance.transform.position, targetPos) * 2;
        StartCoroutine(AnimateTongue(tongueInstance, distance));
    }

    IEnumerator AnimateTongue(GameObject tongue, float distance)
    {
        // Enable cooldown so tongue can't be spammed
        tongueCooldown = true;

        // Set initial scale of tongue
        tongue.transform.localScale = new Vector3(0.1f, 1, 1);

        // Scale tongue until target size is reached
        while (tongue.transform.localScale.x < distance)
        {
            // Calculate new scale based on elapsed time and speed
            float newScale = tongue.transform.localScale.x + (animationSpeed * Time.deltaTime);
            tongue.transform.localScale = new Vector3(newScale, 1, 1);

            // Pause coroutine until next frame
            yield return null;
        }

        // If here - animation is complete
        // Set final scale of tongue
        tongue.transform.localScale = new Vector3(distance, 1, 1);

        // Wait before reversing animation
        yield return new WaitForSeconds(animationPauseBeforeReversing);
        yield return StartCoroutine(AnimateTongueReverse(tongue));
    }

    IEnumerator AnimateTongueReverse(GameObject tongue)
    {
        float reversedSpeed = -animationSpeed;

        // Scale tongue until target size is reached
        while (tongue.transform.localScale.x > 0)
        {
            // Calculate new scale based on elapsed time and speed
            float newScale = tongue.transform.localScale.x + (reversedSpeed * Time.deltaTime);
            tongue.transform.localScale = new Vector3(newScale, 1, 1);

            // Pause coroutine until next frame
            yield return null;
        }

        // Set final scale of tongue
        tongue.transform.localScale = new Vector3(0.1f, 1, 1);

        // Be gone foul tongue
        Destroy(tongue);
        tongueCooldown = false;
    }
}
