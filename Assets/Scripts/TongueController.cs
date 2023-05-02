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
    [SerializeField] float animationPauseBeforeReversing;

    // Serialized so the cooldown status can be seen in the editor
    [SerializeField] bool tongueCooldown;

    Transform flyTransform;
    float flyX;
    float flyY;
    float tongueWidth = 0.4f;
    [Range(0.0f, 10.0f)]
    public float tongueSpeed = 2;
    Color tongueColor = new Color(1, 0.47f, 0.47f);
    GameObject tongueObject;
    LineRenderer lineRender;
    [Range(0.0f, 10.0f)]
    public float maxDistance;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (!tongueCooldown)
        {
            lineRender.SetPosition(1, transform.position);
        }
        lineRender.SetPosition(0, transform.position);
        if (Input.GetKeyDown(KeyCode.Space) && !tongueCooldown)
        {
            flyX = flyTransform.position.x;
            flyY = flyTransform.position.y;
            Debug.Log($"Launch tongue at X={flyX}, y={flyY}");

            // Temporarily use mouse position
            //Vector3 mousePosition = Input.mousePosition;
            //mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            //Debug.Log($"Launch tongue at X={mousePosition.x}, y={mousePosition.y}");

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
            while (lineRender.GetPosition(1) != targetPos|| Vector3.Distance(lineRender.GetPosition(0), lineRender.GetPosition(1))<maxDistance)
            {
                targetPos = new Vector3(flyX, flyY, 1);
                float t = (Time.time - startTime) * tongueSpeed;
                Vector3 newPos = Vector3.Lerp(transform.position, targetPos, t);
                lineRender.SetPosition(1, newPos);
                yield return null;
            }
            //Debug.Log("Distance:"+Vector3.Distance(lineRender.GetPosition(0), lineRender.GetPosition(1)));
            while (!reverse)
            {
                //if touch a moveable object
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
