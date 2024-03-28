using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionShoot : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    private float shootDelay = 0.3f;

    private Rigidbody2D rigidBody;
    private Vector3 shootPos;
    private MinionMove minionMovement;
    private LookAtEnemy lookAtTarget;
    private SlimeSplit controller;
    private GameObject player;
    private MinionStateManager stateManager;
    
    private bool isShooting;

    public bool canShoot = true;

    private void Awake()
    {
        player = GameObject.FindWithTag("player");
        stateManager = GetComponent<MinionStateManager>();
        rigidBody = GetComponent<Rigidbody2D>();
        minionMovement = GetComponent<MinionMove>();
        lookAtTarget = transform.GetChild(0).GetComponent<LookAtEnemy>();
        controller = FindAnyObjectByType<SlimeSplit>();
        shootDelay = controller.shootDelay;
    }

    // Update is called once per frame
    void Update()
    {
        shootPos = transform.GetChild(0).GetChild(0).position;
        
        if (stateManager.GetTarget() != null)
        {
            if (Vector3.Distance(transform.position, stateManager.GetTarget().transform.position) > controller.minionStopDistanceFromEnemy)
            {
                stateManager.currentState = MinionStateEnum.MoveToEnemy;
            }
        }
        else
        {
            stateManager.currentState = MinionStateEnum.MoveToPlayer;
        }
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
