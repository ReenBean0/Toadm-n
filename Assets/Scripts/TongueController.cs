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
    float tongueWidth = 0.4f;
    float tongueSpeed = 5;
    Color tongueColor = new Color(1, 0.47f, 0.47f);
    GameObject tongueObject;
    LineRenderer lineRender;

    GameObject tongueInstance;

    //added for moveable objects by Yin
    bool reverse = true;

    // Start is called before the first frame update
    void Start()
    {
        flyTransform = flyCursor.GetComponent<Transform>();
        tongueCooldown = false;
        flyX = transform.position.x;
        flyY = transform.position.y;
        tongueObject = new GameObject("tongue");
        tongueObject.transform.parent = transform;
        tongueObject.AddComponent<LineRenderer>();
        lineRender = tongueObject.GetComponent<LineRenderer>();
        lineRender.startWidth = tongueWidth;
        lineRender.endWidth = tongueWidth;
        lineRender.SetPosition(0, Vector3.zero);
        lineRender.SetPosition(1, Vector3.zero);
        lineRender.material = new Material(Shader.Find("Sprites/Default"));
        lineRender.startColor = tongueColor;
        lineRender.endColor = tongueColor;
        //tongueInstance = Instantiate(tonguePrefab, new Vector3(transform.position.x, transform.position.y, 1), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (tongueInstance != null)
        {
            //keep tongue attach to toad
            lineRender.SetPosition(0, Vector3.zero);
            slope = (flyY - transform.position.y) / (flyX - transform.position.x);
            c = flyY - slope * flyX;
            if (tongueCooldown)
            {
                //tongue had shoot
                tongueInstance.transform.position = transform.position;
            }
        }
        */
        if (tongueCooldown)
        {
            lineRender.SetPosition(0, transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Space) && !tongueCooldown)
        {
            flyX = flyTransform.position.x;
            flyY = flyTransform.position.y;
            Debug.Log($"Launch tongue at X={flyX}, y={flyY}");

            // Temporarily use mouse position
            //Vector3 mousePosition = Input.mousePosition;
            //mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            //Debug.Log($"Launch tongue at X={mousePosition.x}, y={mousePosition.y}");

            //Vector3 tongueDestination = new Vector3(flyX, flyY, 1);
            //LaunchTongue(tongueDestination);

            StartCoroutine(ShootTongue());
        }
    }
    IEnumerator ShootTongue()
    {
        float startTime = Time.time;
        Vector3 targetPos = new Vector3(flyX, flyY, 1);
        tongueCooldown = true;
        while (tongueCooldown)
        {
            //launch
            while (lineRender.GetPosition(1) != targetPos)
            {
                targetPos = new Vector3(flyX, flyY, 1);
                float t = (Time.time - startTime) * tongueSpeed;
                Vector3 newPos = Vector3.Lerp(transform.position, targetPos, t);
                lineRender.SetPosition(1, newPos);
                yield return null;
            }
            startTime = Time.time;
            //reverse
            while (lineRender.GetPosition(1) != transform.position)
            {
                float t = (Time.time - startTime) * tongueSpeed;
                Vector3 newPos = Vector3.Lerp(lineRender.GetPosition(1),transform.position,  t);
                lineRender.SetPosition(1, newPos);
                yield return null;
            }
            tongueCooldown = false;
        }
    }
    void LaunchTongue(Vector3 targetPos)
    {
        // Spawn tongue
        //tongueInstance = Instantiate(tonguePrefab, new Vector3(transform.position.x, transform.position.y, 1), Quaternion.identity);

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
        //yield return StartCoroutine(AnimateTongueReverse(tongue));
        //added for moveable objects by Yin
        while (!reverse)
        {
            //if touch a moveable object
            yield return null;
        }
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

    //added for moveable objects by Yin
    public void TriggerMoveableObject()
    {
        reverse = false;
    }
    public void FinishMoveableObject()
    {
        reverse = true;
    }
    public void ChangeTonguePosition(float xMovement, float yMovement)
    {

    }
}
