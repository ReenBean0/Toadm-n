using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FireProjectile : ObjectResponse
{
    [SerializeField] float turnOnDelay;
    [SerializeField] GameObject projectilePrefab;
    GameObject projectileInstance;

    [SerializeField] Vector3 projectileVelocity;
    [SerializeField] float projectileLifespan;
    [SerializeField] bool projectileCameraFollow;
    [SerializeField] float targetCameraSize;
    bool isProjectileAlive;

    public override void Interact()
    {
        base.Interact();
        StartCoroutine(WaitAndFire(turnOnDelay));
    }

    public IEnumerator WaitAndFire(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        Fire();
    }

    void Fire()
    {
        projectileInstance = Instantiate(projectilePrefab, gameObject.transform);
        isProjectileAlive = true;

        GetComponent<Light2D>().enabled = true;
        StartCoroutine(MuzzleFlash());

        StartCoroutine(BeginLifetimeCountdown());
        if (projectileCameraFollow)
        {
            GameManager.instance.GetComponent<CameraController>().FollowObject(projectileInstance, targetCameraSize);
        }
    }

    void Update()
    {
        if (isProjectileAlive)
        {
            projectileInstance.transform.position += projectileVelocity * Time.deltaTime;
        }
    }

    IEnumerator BeginLifetimeCountdown()
    {
        yield return new WaitForSecondsRealtime(projectileLifespan);
        KillProjectile();
    }

    public void KillProjectile()
    {
        if (isProjectileAlive)
        {
            isProjectileAlive = false;
            GameManager.instance.GetComponent<CameraController>().MoveBackToCurrentCameraBound();
            Destroy(projectileInstance);
        }
    }

    IEnumerator MuzzleFlash()
    {
        GetComponent<Light2D>().enabled = true;
        yield return new WaitForSecondsRealtime(0.5f);
        GetComponent<Light2D>().enabled = false;
    }
}