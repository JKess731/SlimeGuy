using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltSlimePush : MonoBehaviour
{
    private GameObject player;

    [SerializeField] private float activationTime;
    [SerializeField] private float scaleFactor;
    [SerializeField] private GameObject theSlime;
    private bool canPush = true;
    private bool isPushing = false;
    [SerializeField] private float damage;
    [SerializeField] private float knockBackPower;

    private Vector3 originalScale;

    // Enemy
    public HashSet<GameObject> enemies = new HashSet<GameObject>();

    void Awake()
    {
        player = GameObject.FindWithTag("player");
        theSlime.SetActive(false);

        originalScale = theSlime.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (canPush)
        {
            HandleInput();
        }

        if (isPushing)
        {
            HandleCollision();
            theSlime.transform.localScale = new Vector3(theSlime.transform.localScale.x + scaleFactor * Time.deltaTime, theSlime.transform.localScale.y + scaleFactor * Time.deltaTime);
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
                eBase.Damage(damage, -(eBase.faceDir), knockBackPower, -(eBase.faceDir));
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
        Debug.Log("activate");
        canPush = false;
        isPushing = true;
        theSlime.SetActive(true);

        //Disable player movement
        //PlayerMove input = player.GetComponent<PlayerMove>();
        //input.input.Disable();

        yield return new WaitForSeconds(activationTime);

        //Enable player movement
        //input.input.Enable();

        canPush = true;
        isPushing = false;
        theSlime.SetActive(false);
        theSlime.transform.localScale = originalScale;
    }
}
