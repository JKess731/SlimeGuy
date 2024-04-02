using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltSlimeWave : MonoBehaviour
{
    // Player
    private GameObject player;
    private bool canWave = true;
    private bool waveStarted = false;
    public float activationTime;
    [SerializeField] private float damage;
    [SerializeField] private float knockBackPower;

    // Colliders
    [SerializeField] private List<GameObject> collidersList = new List<GameObject>();

    // Enemy
    public HashSet<GameObject> enemies = new HashSet<GameObject>();

    private void Awake()
    {
        player = GameObject.FindWithTag("player");
        foreach (GameObject trigger in collidersList)
        {
            if (trigger != null)
            {
                trigger.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canWave)
        {
            #region Rotation
            Camera cam = Camera.main;
            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mousePos - player.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, angleAxis, Time.deltaTime * 50);
            #endregion

            HandleInput();
        }

        if (waveStarted)
        {
            HandleCollision();
        }
    }

    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(OnActivate(activationTime));
        }
    }

    public void HandleCollision()
    {
        HashSet<GameObject> clearEnemies = new HashSet<GameObject>();

        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                EnemyBase eBase = enemy.GetComponent<EnemyBase>();
                eBase.Damage(damage, transform.right, knockBackPower, Vector2.up, 0f);
                clearEnemies.Add(enemy);
            }
        }

        foreach (GameObject enemy in clearEnemies)
        {
            if (enemies.Contains(enemy))
            {
                enemies.Remove(enemy);
            }
        }
    }

    public IEnumerator OnActivate(float activationTime)
    {
        canWave = false;
        waveStarted = true;

        GameObject bigOne = collidersList[0];
        bigOne.SetActive(true);

        yield return new WaitForSeconds(activationTime / 2);
        for (int i = 1; i < collidersList.Count; i++)
        {
            collidersList[i].SetActive(true);
        }

        Debug.Log("Ult activating");
        yield return new WaitForSeconds(activationTime);

        foreach (GameObject trigger in collidersList)
        {
            trigger.SetActive(false);
        }

        canWave = true;
        waveStarted = false;
    }
}
