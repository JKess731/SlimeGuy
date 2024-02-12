using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttackRanged : MonoBehaviour
{
    [SerializeField] GameObject player;

    public GameObject bullet;
    public Transform bulletPos;
    public GameObject ring;



    private float shotCooldown;
    public float startShotCooldown;
    private Vector3 lastPosition = new Vector3();
    public float detectRange;

    // Start is called before the first frame update
    void Start()
    {
        shotCooldown = startShotCooldown;
        
    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector2.Distance(transform.position, player.transform.position);
        //Vector3 direction = player.transform.position - transform.position;
        //float rot = Mathf.Atan2(-direction.x, -direction.y) * Mathf.Rad2Deg;
        //ring.transform.rotation = Quaternion.Euler(0, 0, rot);

        Vector3 rotation = player.transform.position - ring.transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (distance < detectRange)
        {
            
            if (transform.position == lastPosition)
            {
                //Vector2 direction = (player.transform.position - transform.position).normalized;
                if (shotCooldown <= 0)
                {
                    Debug.Log("Shoot");
                    Instantiate(bullet, bulletPos.position, ring.transform.rotation);
                    shotCooldown = startShotCooldown;

                }
                else
                {
                    shotCooldown -= Time.deltaTime;
                }

            }
            else
            {
                shotCooldown = startShotCooldown;
            }
            lastPosition = transform.position;

        }
        lastPosition = transform.position;
        

    }
}
