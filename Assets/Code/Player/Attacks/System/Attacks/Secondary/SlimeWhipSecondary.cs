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
            if (Input.GetKeyDown(KeyCode.Q))
            {
                StartCoroutine(OnActivate(activationTime));
                //Instantiate(whip, player.transform.position, Quaternion.identity);
            }
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
        canActivate = true;
        isActivated = false;
    }

}
