using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMachineGun : MonoBehaviour
{
    public float fireRate = 0.1f;   // Time between shots
    public int damage = 10;         // Damage per bullet
    public GameObject bulletPrefab; // The bullet to spawn
    public Transform firePoint;     // Where the bullet will spawn

    private float nextFireTime = 0f;

    public SlimeMachineGun(float fireRate, int damage, GameObject bulletPrefab, Transform firePoint)
    {
        this.fireRate = fireRate;
        this.damage = damage;
        this.bulletPrefab = bulletPrefab;
        this.firePoint = firePoint;
    }

    public void Fire()
    {
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bullet = GameObject.Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
    }
}
