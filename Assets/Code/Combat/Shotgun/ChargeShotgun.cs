using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class ChargeShotgun : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private GameObject projectile;

    [SerializeField] private int bulletCount = 3;
    [SerializeField] private float spread = 30;
    [SerializeField] private float chargeRate = 3f;
    [SerializeField] private float holdRate = 1f;
    [SerializeField] private LookAtMouse ring;
    
    private float timer = 0;
    private bool charging = false;

    private FMODUnity.StudioEventEmitter eventEmitterRef;
    private void Awake()
    {
        eventEmitterRef = GetComponent<FMODUnity.StudioEventEmitter>();
    }

    public void Update()
    {
        if (timer >= chargeRate + holdRate)
        {
            Shoot();
        }
        if (Input.GetMouseButtonUp(0))
        {
            eventEmitterRef.Play();
            Shoot();
        }
        if (Input.GetMouseButtonDown(0) && timer < chargeRate + holdRate)
        {
            Charge();
        }
    }

    private void Shoot()  
    {
        float angleDiff = spread * 2 / bulletCount;

        for (int i = 0; i < bulletCount; i++)
        {
            float addedOffset = -angleDiff * i;
            Quaternion newRot = ring.getRotation() * Quaternion.Euler(0,0,spread) * Quaternion.Euler(0, 0, addedOffset);
            GameObject chargedBullet = Instantiate(projectile, attackPoint.position, newRot);
            chargedBullet.GetComponent<ChargedBullet>().IncreaseBulletSpeed(timer/chargeRate);
            chargedBullet.GetComponent<ChargedBullet>().IncreaseBulletDamage(timer/chargeRate);
            chargedBullet.GetComponent<ChargedBullet>().IncreaseBulletSize(timer/chargeRate);
        }

        charging = false;
        timer = 0;
    }


    //Draws the cone spread rays of the bullets in the editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector2 test1 = Quaternion.AngleAxis(spread, Vector3.forward) * attackPoint.right;
        Gizmos.DrawRay(attackPoint.position, test1 * 5);

        Vector2 test2 = Quaternion.AngleAxis(-spread, Vector3.forward) * attackPoint.right;
        Gizmos.DrawRay(attackPoint.position, test2 * 5);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(attackPoint.position, attackPoint.right * 5);
    }

    private void Charge()
    {
        if (charging)
        {
            if (timer < chargeRate)
            {
                timer += Time.deltaTime;
            }
        }
    }
}
