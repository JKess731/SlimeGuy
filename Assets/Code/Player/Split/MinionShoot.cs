using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionShoot : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float shootDelay = 0.3f;

    private Rigidbody2D rigidBody;
    private Vector3 shootPos;
    private MinionMove minionMovement;
    private LookAtEnemy lookAtTarget;

    private bool isShooting;

    public bool canShoot = true;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        minionMovement = GetComponent<MinionMove>();
        lookAtTarget = transform.GetChild(0).GetComponent<LookAtEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        shootPos = transform.GetChild(0).GetChild(0).position;
        minionMovement.isShooting = isShooting;
    }

    public IEnumerator Shoot()
    {
        isShooting = true;
        canShoot = false;

        GameObject spawnedBullet = Instantiate(bullet);
        spawnedBullet.transform.position = shootPos;
        spawnedBullet.transform.rotation = lookAtTarget.transform.rotation;

        yield return new WaitForSeconds(shootDelay);

        isShooting = false;
        canShoot = true;
    }


}
