using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttackRanged : MonoBehaviour
{
    [SerializeField] GameObject player;

    public GameObject bullet;

    private float shotCooldown;

    public float startShotCooldown;



    // Start is called before the first frame update
    void Start()
    {
        shotCooldown = startShotCooldown;
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        
        if (shotCooldown <= 0)
        {
            GameObject bulletPrefab = Instantiate(bullet, transform.position, Quaternion.identity);
            enemyBullet instatiatedBullet = bulletPrefab.GetComponent<enemyBullet>();
            instatiatedBullet.playerpos = direction;
            shotCooldown = startShotCooldown;
        }
        else {
            shotCooldown -= Time.deltaTime;
        }
    }
}
