using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeWhipSecondary : SecondaryAttack
{
    [Space]

    [Header("Whip Attributes")]
    [SerializeField] private GameObject whip;
    [SerializeField] private float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        whip.SetActive(false);
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
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime * 5);
        }
    }

    public override IEnumerator OnActivate(float aTime)
    {
        whip.SetActive(true);
        canActivate = false;
        isActivated = true;

        yield return new WaitForSeconds(aTime);

        whip.SetActive(false);
        isActivated = false;
        StartCoroutine(AttackCooldown(cooldownTime));
    }

}
