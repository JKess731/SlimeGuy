using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class EnemyBulletReturn : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject player;
    public GameObject enemy;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private int bulletDamage;
    public Vector3 playerPos = Vector3.zero;
    public Vector3 startPos = Vector3.zero;
    public float waitTime;

    private void Awake()
    {
        player = GameObject.FindWithTag("player");
        playerPos = player.transform.position;
        startPos = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("player");
        StartCoroutine(DelayShot(waitTime));

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerStats>().Damage(bulletDamage);
            if (enemy != null)
            {
                enemy.GetComponent<DwarfBehavior>().RestartAttackCounter();

            }
            Debug.Log("Bullet dmg");
            Destroy(gameObject);
        }
    }

    IEnumerator DelayShot(float delay)
    {
        yield return new WaitForSeconds(delay);

        float distance = Vector3.Distance(startPos, playerPos);
        float remainingDistance = distance;
        while(remainingDistance > 0)
        {
            transform.position = Vector3.Slerp(startPos, playerPos, 1 - (remainingDistance/distance));
            remainingDistance -= bulletSpeed * Time.deltaTime;
            yield return null;

        }
        distance = Vector3.Distance(playerPos, startPos);
        remainingDistance = distance;
        while (remainingDistance > 0)
        {
            transform.position = Vector3.Slerp(playerPos, startPos, 1 - (remainingDistance / distance));
            remainingDistance -= bulletSpeed * Time.deltaTime;
            yield return null;

        }
        if (enemy != null) {
            enemy.GetComponent<DwarfBehavior>().RestartAttackCounter();

        }
        Destroy(gameObject);
        
    }
}
