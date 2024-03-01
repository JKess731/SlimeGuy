using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class SlimeShotgun : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private GameObject projectile;

    [SerializeField] private int bulletCount = 3;
    [SerializeField] private float spread = 30;
    [SerializeField] private float fireRate = 0.1f;
    [SerializeField] private LookAtMouse ring;
    
    private float timer = 0;

    private FMODUnity.StudioEventEmitter eventEmitterRef;
    private void Awake()
    {
        eventEmitterRef = GetComponent<FMODUnity.StudioEventEmitter>();
    }

    public void Update()
    {
        if (Input.GetMouseButton(0) && timer < 0)
        {
            eventEmitterRef.Play();
            Shoot();
            timer = fireRate;
        }
        timer -= Time.deltaTime;
    }

    private void Shoot()  
    {
        float angleDiff = spread * 2 / bulletCount;

        for (int i = 0; i < bulletCount; i++)
        {
            float addedOffset = -angleDiff * i;
            Quaternion newRot = ring.getRotation() * Quaternion.Euler(0,0,spread) * Quaternion.Euler(0, 0, addedOffset);
            Instantiate(projectile, attackPoint.position, newRot);
        }
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
}
