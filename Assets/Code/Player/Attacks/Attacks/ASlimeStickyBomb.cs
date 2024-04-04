using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Jared Kessler

// Curve code: https://www.youtube.com/watch?v=DwGdoURXZag
public class ASlimeStickyBomb : MonoBehaviour
{

    // Throw & Curve
    [Header("Throw Attributes")]
    [SerializeField] private float throwTime = 0.5f;
    [SerializeField] private float maxHeight = 1.5f;
    [SerializeField] private float bombDistance = 4f;
    [SerializeField] private AnimationCurve curve;

    [Space]

    [Header("Bomb Info")]
    [SerializeField] private float activationTime = 1.75f;
    [SerializeField] private float reloadTime = 2f;
    [SerializeField] private float damage = 5f;
    [SerializeField] private float knockbackPower = 4f;

    [Space]

    [Header("Radiuses")]
    [SerializeField] private float blastRadius = 3f;
    [SerializeField] private float knockbackRadius = 6f;
    [SerializeField] private float enemyCheckRadius = 1.5f;
    [SerializeField] private GameObject blastObject;
    [SerializeField] private GameObject knockbackObject;

    [HideInInspector] public HashSet<GameObject> enemiesInKockBackRadius = new HashSet<GameObject>();
    [HideInInspector] public HashSet<GameObject> enemiesInBlastRadius = new HashSet<GameObject>();

    private GameObject player;
    private GameObject stuckEnemy;
    private Vector3 direction;
    private Vector3 mousePos;
    private Vector3 targetPos;

    private bool stuckToEnemy = false;
    private bool canActivate = true;
    [HideInInspector] public bool throwEnded = false;

    private void Awake()
    {
        player = GameObject.FindWithTag("player");

        CircleCollider2D checkCollider = transform.GetComponent<CircleCollider2D>();
        checkCollider.radius = enemyCheckRadius;

        CircleCollider2D blastCollider = blastObject.GetComponent<CircleCollider2D>();
        blastCollider.radius = blastRadius;
        
        CircleCollider2D knockbackCollider = knockbackObject.GetComponent<CircleCollider2D>();
        knockbackCollider.radius = knockbackRadius;
    }

    void Update()
    {
        HandleInput();

        if (stuckEnemy)
        {
            transform.position = stuckEnemy.transform.position;
        }

        if (throwEnded)
        {
            StartCoroutine(OnActivate(activationTime, damage, knockbackRadius, reloadTime));
            throwEnded = false;
        }
    }

    private void HandleInput()
    {
        if (canActivate)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                #region Rotation
                Camera cam = Camera.main;
                mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                direction = mousePos - player.transform.position;

                #endregion

                StartCoroutine(OnThrow(transform.position, bombDistance, direction.normalized));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (!stuckToEnemy && collision.gameObject.tag == "enemy")
        {
            stuckEnemy = collision.gameObject;
            Debug.Log(collision.transform.name);
            transform.parent = stuckEnemy.transform;
            stuckToEnemy = true;
        }
    }

    private IEnumerator OnThrow(Vector3 start, float dist, Vector3 direction)
    {
        canActivate = false;

        float timePassed = 0f;
        Collider2D collider = transform.GetComponent<Collider2D>();
        collider.enabled = false;

        Vector3 pos = direction * dist;
        targetPos = player.transform.position + pos;

        while (timePassed < throwTime)
        {
            timePassed += Time.deltaTime;

            float linearTime = timePassed / throwTime;
            float heightTime = curve.Evaluate(linearTime);

            float height = Mathf.Lerp(0f, maxHeight, heightTime);

            transform.position = Vector2.Lerp(start, targetPos, linearTime) + new Vector2(0f, height);

            yield return null;
        }

        collider.enabled = true;
        throwEnded = true;
    }
    
    private IEnumerator OnActivate(float activationTime, float dmg, float knockback, float reloadT)
    {
        yield return new WaitForSeconds(activationTime);

        HashSet<GameObject> enemiesToKnockback = new HashSet<GameObject>();

        foreach (GameObject enemy in enemiesInKockBackRadius)
        {
            if (!enemiesInBlastRadius.Contains(enemy))
            {
                enemiesToKnockback.Add(enemy);
            }
        }

        foreach (GameObject enemy in enemiesInBlastRadius)
        {
            EnemyBase eBase = enemy.GetComponent<EnemyBase>();
            eBase.Damage(dmg, -(eBase.faceDir), knockback, -(eBase.faceDir));
        }

        foreach (GameObject enemy in enemiesToKnockback)
        {
            EnemyBase eBase = enemy.GetComponent<EnemyBase>();
            eBase.Damage(0f, -(eBase.faceDir), knockback, -(eBase.faceDir));
        }

        Debug.Log("activating");

        yield return new WaitForSeconds(reloadT);

        throwEnded = false;
        canActivate = true;
        stuckToEnemy = false;
        stuckEnemy = null;
        transform.position = player.transform.position;
        transform.parent = player.transform;

        enemiesInKockBackRadius.Clear();
        enemiesInBlastRadius.Clear();

    }
}
