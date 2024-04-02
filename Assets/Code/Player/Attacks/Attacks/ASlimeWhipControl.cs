using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASlimeWhipControl : MonoBehaviour
{
    // Whip Object
    [SerializeField] private GameObject whip;
    private AttackCollision collision;

    // Variables
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float damage;
    [SerializeField] private float knockBackPower;
    [SerializeField] private float activationTime;

    // Boolean control
    private bool canWhip = true;
    private bool whipStarted = false;

    // Player
    private GameObject player;

    // Enemies
    public HashSet<GameObject> enemies = new HashSet<GameObject>();

    private void Awake()
    {
        whip.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("player");

        collision = whip.GetComponent<AttackCollision>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canWhip)
        {
            HandleInput();
        }

        if (whipStarted)
        {
            HandleCollision();
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime * 5);
        }
    }

    private void HandleCollision()
    {
        HashSet<GameObject> clearEnemies = new HashSet<GameObject>();

        foreach(GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                EnemyBase eBase = enemy.GetComponent<EnemyBase>();
                eBase.Damage(damage, transform.right, knockBackPower, Vector2.up, 0f);
                clearEnemies.Add(enemy);
            }
        }

        foreach(GameObject enemy in clearEnemies)
        {
            if (enemies.Contains(enemy))
            {
                enemies.Remove(enemy);
            }
        }
    }

    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(OnActivate(activationTime));  
            //Instantiate(whip, player.transform.position, Quaternion.identity);
        }
    }

    private IEnumerator OnActivate(float activationTime)
    {
        whip.SetActive(true);
        canWhip = false;
        whipStarted = true;

        yield return new WaitForSeconds(activationTime);

        whip.SetActive(false);
        canWhip = true;
        whipStarted = false;
    }
}
