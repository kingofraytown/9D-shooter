using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public ObjectPool clip;
    public GameFloat fireRate;
    public float fireRateTimer;
    public bool straightShot;
    public float targetAngle;
    public bool rightDirection;
    public GameObject target;
    public bool auto;
    public GameFloat targetDistance;
    private float distance;
    public bool multiShot;
    public Gun[] guns;
    public bool laser;
    public BaseLaser laserBeam;


    private void Update()
    {
        if (auto)
        {
            //check for player distance

            distance = Vector3.Distance(gameObject.transform.position, target.transform.position);

            if (distance <= targetDistance.value())
            {
                if (straightShot == false)
                {
                    Vector3 direction = target.transform.position - gameObject.transform.position;
                    targetAngle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg - 180;
                    //Vector3 look = gameObject.transform.InverseTransformPoint(target.transform.position);
                }

                Fire();
            }
        }

        fireRateTimer += Time.deltaTime;
    }

    public void Fire()
    {
        if (multiShot)
        {
            foreach(Gun gun in guns)
            {
                gun.Fire();
            }
        }
        else
        {
            if (laser)
            {
                laserBeam.StartLaser();

            }
            else
            {
                if (fireRateTimer >= fireRate.value())
                {
                    GameObject bullet = clip.GetPooledObject();
                    if (bullet != null && bullet.GetComponent<BaseBullet>().currentState == BaseBullet.BulletStates.Dead)
                    {
                        bullet.GetComponent<BaseBullet>().Restore();
                        if (straightShot)
                        {
                            if (rightDirection)
                            {
                                bullet.transform.position = gameObject.transform.position;
                                bullet.transform.rotation = gameObject.transform.rotation;
                            }
                            else
                            {
                                bullet.transform.position = gameObject.transform.position;
                                bullet.transform.Rotate(0, 0, -180);
                            }

                        }
                        else
                        {
                            bullet.transform.Rotate(0, 0, targetAngle);
                        }
                        bullet.SetActive(true);
                        bullet.GetComponent<BaseBullet>().ChangeState(BaseBullet.BulletStates.Active);
                        fireRateTimer = 0;
                    }
                }
            }
        }
    }
    public void StopFiring()
    {
        if (laser)
        {
            laserBeam.EndLaser();
        }
    }

}
