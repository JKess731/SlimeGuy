using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePushSecondary : SecondaryAttack
{
    [Space]
    [Header("Push Attributes")]
    [SerializeField] private float scaleFactor;
    [SerializeField] private GameObject pushSlime;
    [SerializeField] private float damage;
    [SerializeField] private float knockBackPower;

    private Vector3 resetScale;

    // Start is called before the first frame update
    void Start()
    {
        pushSlime.SetActive(false);

        resetScale = pushSlime.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (canActivate)
        {
            HandleInput();
        }

        if (isActivated)
        {
            HandleCollision();
            pushSlime.transform.localScale =
                new Vector3(pushSlime.transform.localScale.x + scaleFactor * Time.deltaTime, 
                pushSlime.transform.localScale.y + scaleFactor * Time.deltaTime);
        }
    }

    public void HandleCollision()
    {
        HashSet<GameObject> clearEnemies = new HashSet<GameObject>();

        foreach (GameObject enemy in enemiesToAttack)
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
            if (enemiesToAttack.Contains(enemy))
            {
                enemiesToAttack.Remove(enemy);
            }
        }
    }

    public override IEnumerator OnActivate(float aTime)
    {
        canActivate = false;
        isActivated = true;
        pushSlime.SetActive(true);

        //Disable player movement
        //PlayerMove input = player.GetComponent<PlayerMove>();
        //input.input.Disable();

        yield return new WaitForSeconds(aTime);

        //Enable player movement
        //input.input.Enable();

        canActivate = true;
        isActivated = false;
        pushSlime.SetActive(false);
        pushSlime.transform.localScale = resetScale;
    }
}
