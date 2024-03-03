using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyAttackThrowBommerang : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;
    public GameObject boomerangObject;
    public Transform boomerangPos;
    public GameObject ring;
    private float shotCooldown;
    public float startShotCooldown;
    private Vector3 lastPosition = new Vector3();
    public float detectRange;

    private void Awake()
    {
        player = GameObject.FindWithTag("player");
    }

    void Start()
    {
        shotCooldown = startShotCooldown;
    }

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
                    StartCoroutine(ThrowAttack());
                    
                }
                else
                {
                    shotCooldown -= Time.deltaTime;
                }
            }
            else
            {
                RestartCooldown();
            }
            lastPosition = transform.position;
        }
        lastPosition = transform.position;
    }

    IEnumerator ThrowAttack()
    {
        yield return new WaitForSeconds(1f);
        GameObject bullet1 = Instantiate(boomerangObject, boomerangPos.position, ring.transform.rotation);
        yield return new WaitForSeconds(1f);

    }

    public void RestartCooldown() {
        shotCooldown = startShotCooldown;
    }

}
