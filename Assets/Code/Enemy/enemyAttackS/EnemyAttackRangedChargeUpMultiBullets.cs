using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackRangedChargeUpMultiBullets : MonoBehaviour
{
    [SerializeField] GameObject player;
    public GameObject bullet;
    public Transform bulletPos1;
    public Transform bulletPos2;
    public Transform bulletPos3;
    public GameObject ring;
    private float shotCooldown;
    public float startShotCooldown;
    private Vector3 lastPosition = new Vector3();
    public float detectRange;

    private void Awake()
    {
        player = GameObject.FindWithTag("player");
    }


    // Start is called before the first frame update
    void Start()
    {
        shotCooldown = startShotCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        Vector3 rotation = player.transform.position - ring.transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
        if (distance < detectRange)
        {
            if (transform.position == lastPosition)
            {
                if (shotCooldown <= 0)
                {
                    Debug.Log("Shoot");
                    StartCoroutine(MultiShot());
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

    IEnumerator MultiShot() {
        yield return new WaitForSeconds(1f);
        GameObject bullet1 = Instantiate(bullet, bulletPos1.position, ring.transform.rotation);
        bullet1.GetComponent<EnemyBulletDelayed1>().waitTime = 3;
        yield return new WaitForSeconds(1f);
        GameObject bullet2 = Instantiate(bullet, bulletPos2.position, ring.transform.rotation);
        bullet2.GetComponent<EnemyBulletDelayed1>().waitTime = 2;
        yield return new WaitForSeconds(1f);
        GameObject bullet3 = Instantiate(bullet, bulletPos3.position, ring.transform.rotation);
        bullet3.GetComponent<EnemyBulletDelayed1>().waitTime = 1;
        yield return new WaitForSeconds(1f);

    }
}
