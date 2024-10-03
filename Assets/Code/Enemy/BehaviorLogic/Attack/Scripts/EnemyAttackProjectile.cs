using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "EnemyLogic/AttackLogic/Projectile")]
public class EnemyAttackProjectile : EnemyAttackSOBase
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    [SerializeField] private float knockbackPower;
    [SerializeField] private float delay;

    private Collider2D teleportTrigger;
    private Projectile p;
    public GameObject ring;
    private bool canShoot = true;

    private float timer = 3;
    

    #region State Functions
    public override void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);

        if (triggerType == EnemyBase.AnimationTriggerType.WizardCastTrigger)
        {
            Debug.Log("WizardCast");
            AudioManager.instance.PlayOneShot(FmodEvents.instance.WizardCast, _transform.position);
        }
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();

        canShoot = true;
        teleportTrigger = _enemy.transform.GetChild(0).GetChild(0).GetComponent<Collider2D>();

        attackPoint = _enemy.transform.GetChild(1).GetChild(0);
        ring = _enemy.transform.GetChild(1).gameObject;
    }

    public override void DoExitLogic()
    {
        if (p != null)
        {
            p.OnDeath();
        }

        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        if (canShoot)
        {
            OnPreLoadUp();
        }
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }
    #endregion

    #region Shooting Phases

    private void OnPreLoadUp()
    {
        Debug.Log("PRE LOAD");
        // Grab reference to the Teleport Collider & Disable it
        teleportTrigger.enabled = false;

        canShoot = false;
        OnLoadUp();
    }

    private void OnLoadUp()
    {
        Debug.Log("LOAD");
        // Create Projectile
        p = CreateProjectile();
        p.OnCollide.AddListener(OnProjectileDeath);
        OnShoot();
    }

    private void OnShoot()
    {
        Debug.Log("ON SHOOT");
        p.StartShoot(_enemy.gameObject, damage, speed, _playerTransform, knockbackPower, delay);
        // Grab reference to the Teleport Collider & Enable it
    }

    private void OnProjectileDeath()
    {
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
        }

        canShoot = true;
    }

    private Projectile CreateProjectile()
    {
        // REMOVE ONCE ANIMATIONS IN
        canShoot = false;
        GameObject newProjectile = Instantiate(projectilePrefab, attackPoint.transform.position, Quaternion.identity);
        Projectile p = newProjectile.GetComponent<Projectile>();
        return p;
    }

    #endregion

}
