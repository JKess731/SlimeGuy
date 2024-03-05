using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Edison Li
//Code Credit to BMo
//https://www.youtube.com/watch?v=VWaiU7W5HdE

public class AbsorptionClass : MonoBehaviour
{
    //Two states of absorption
    public bool keyAbsorption;
    public bool dashAbsorb;

    public Collider2D absorbDash;
    public Collider2D absorbClick;

    private bool canDash = true;
    [SerializeField] private float dashingPower = 24f;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;

    private PlayerMove playerMove;
    private Rigidbody2D rb;
    private TrailRenderer tr;

    private FMODUnity.StudioEventEmitter eventEmitterRef;
    private void Awake()
    {
        eventEmitterRef = GetComponent<FMODUnity.StudioEventEmitter>();
        playerMove = GameObject.Find("Player").GetComponent<PlayerMove>();
        rb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        tr = GameObject.Find("Player").GetComponent<TrailRenderer>();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            eventEmitterRef.Play();
            StartCoroutine(Dash());
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            eventEmitterRef.Play();
            StartCoroutine(Click());
        }

    }

    private IEnumerator Dash()
    {
        canDash = false;
        absorbDash.enabled = true;
        playerMove.isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = playerMove.playerFaceDirection * dashingPower;
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        absorbDash.enabled = false;
        playerMove.isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private IEnumerator Click()
    {
        absorbClick.enabled = true;
        yield return new WaitForSeconds(0.5f);
        absorbClick.enabled = false;
    }
}
