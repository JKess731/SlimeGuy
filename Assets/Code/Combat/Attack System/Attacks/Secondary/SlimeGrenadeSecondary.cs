using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGrenadeSecondary : SecondaryAttack
{
    [Space]

    [Header("Grenade Attributes")]
    // Throw & Curve
    [Header("Throw Attributes")]
    [SerializeField] private float throwTime = 0.5f;
    [SerializeField] private float maxHeight = 1.5f;
    [SerializeField] private float bombDistance = 4f;
    [SerializeField] private AnimationCurve curve;

    [Space]

    [Header("Bomb Info")]
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

    private GameObject stuckEnemy;
    private Vector3 direction;
    private Vector3 mousePos;
    private Vector3 targetPos;

    private bool stuckToEnemy = false;
    [HideInInspector] public bool throwEnded = false;

    void Start()
    {
        CircleCollider2D checkCollider = transform.GetComponent<CircleCollider2D>();
        checkCollider.radius = enemyCheckRadius;

        CircleCollider2D blastCollider = blastObject.GetComponent<CircleCollider2D>();
        blastCollider.radius = blastRadius;

        CircleCollider2D knockbackCollider = knockbackObject.GetComponent<CircleCollider2D>();
        knockbackCollider.radius = knockbackRadius;
    }

    void Update()
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

        if (stuckEnemy)
        {
            transform.position = stuckEnemy.transform.position;
        }

        if (throwEnded)
        {
            StartCoroutine(OnActivate(activationTime, damage, knockbackPower, reloadTime));
            throwEnded = false;
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

        transform.parent = null;
        collider.enabled = true;
        throwEnded = true;
    }
    private IEnumerator OnActivate(float activationTime, float dmg, float knockback, float reloadT)
    {
        yield return new WaitForSeconds(activationTime);

        HashSet<GameObject> enemiesToKnockback = new HashSet<GameObject>();


        foreach (GameObject enemy in enemiesInKockBackRadius)
        {
            Debug.Log("SET: " + enemy);
            if (!enemiesInBlastRadius.Contains(enemy))
            {
                enemiesToKnockback.Add(enemy);
            }
        }

        foreach (GameObject enemy in enemiesInBlastRadius)
        {
            EnemyBase eBase = enemy.GetComponent<EnemyBase>();
            Debug.Log("Call");
            eBase.Damage(dmg, -(eBase.faceDir), knockback, -(eBase.faceDir));
        }

        foreach (GameObject enemy in enemiesToKnockback)
        {
            EnemyBase eBase = enemy.GetComponent<EnemyBase>();
            eBase.Damage(0f, -(eBase.faceDir), knockback, -(eBase.faceDir));
        }

        transform.position = player.transform.position;
        transform.parent = player.transform;

        yield return new WaitForSeconds(reloadT);

        throwEnded = false;
        canActivate = true;
        stuckToEnemy = false;
        stuckEnemy = null;

        enemiesInKockBackRadius.Clear();
        enemiesInBlastRadius.Clear();

        StartCoroutine(AttackCooldown(cooldownTime));

    }

    // Override from parent
    public override IEnumerator OnActivate(float aTime)
    {
        yield return null;
    }
}
