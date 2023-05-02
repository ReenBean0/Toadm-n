using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            GameManager.instance.GetComponent<CameraController>().MoveCameraBackToPreviousPosition();
            Destroy(projectileInstance);
        }
    }
}