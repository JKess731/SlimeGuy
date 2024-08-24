using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeWaveSecondary : SecondaryAttack
{
    [Space]

    [Header("Wave Attributes")]
    [SerializeField] private float damage;
    [SerializeField] private float knockBackPower;

    // Colliders
    [SerializeField] private List<GameObject> collidersList = new List<GameObject>();

    public override void OnEnable()
    {
        base.OnEnable();
        foreach (GameObject trigger in collidersList)
        {
            trigger.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
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
        if (canActivate)
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

        if (isActivated)
        {
            HandleCollision();
        }
    }

    public override IEnumerator OnActivate(float aTime)
    {
        canActivate = false;

        GameObject bigOne = collidersList[0];
        bigOne.SetActive(true);

        yield return new WaitForSeconds(aTime / 2);
        for (int i = 1; i < collidersList.Count; i++)
        {
            collidersList[i].SetActive(true);
        }

        isActivated = true;
        yield return new WaitForSeconds(aTime);

        foreach (GameObject trigger in collidersList)
        {
            trigger.SetActive(false);
        }

        canActivate = true;
        isActivated = false;

        StartCoroutine(AttackCooldown(cooldownTime));
    }


    public void HandleCollision()
    {
        HashSet<GameObject> clearEnemies = new HashSet<GameObject>();

        foreach (GameObject enemy in enemiesToAttack)
        {
            if (enemy != null)
            {
                EnemyBase eBase = enemy.GetComponent<EnemyBase>();
                eBase.Damage(damage, transform.right, knockBackPower, Vector2.up);
                clearEnemies.Add(enemy);
            }
        }

        foreach (GameObject enemy in clearEnemies)
        {
            if (enemiesToAttack.Contains(enemy))
            {
                enemiesToAttack.Remove(enemy);
            }
        }
    }
}
