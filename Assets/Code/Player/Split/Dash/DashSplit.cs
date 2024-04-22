using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSplit : MonoBehaviour
{
    [SerializeField] private float dashingPower = 10f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    private bool canDash = true;

    private GameObject player;
    private PlayerMove playerMove;
    private Rigidbody2D rb;
    private SlimeSplit controller;
    private Camera cam;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("player");
        playerMove = player.GetComponent<PlayerMove>();
        rb = player.GetComponent<Rigidbody2D>();
        controller = FindAnyObjectByType<SlimeSplit>();
        cam = Camera.main;
    }

    public void OnSplit()
    {
        if (canDash)
        {
            Vector3 playerPos = player.transform.position;
            StartCoroutine(Dash());
            LeaveMinionInWake(playerPos);
        }
    }

    /// <summary>
    /// Spawn a minion at the position provided
    /// </summary>
    private void LeaveMinionInWake(Vector3 pos)
    {
        GameObject minion = Instantiate(controller.minionPrefab);
        minion.transform.position = pos;
        minion.transform.parent = controller.transform;
    }

    public IEnumerator Dash()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - player.transform.position;

        Debug.Log(mousePos);
        canDash = false;
        //absorbDash.enabled = true;
        playerMove.isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = direction * dashingPower;
        Debug.Log(rb.position);
        //tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        //tr.emitting = false;
        rb.gravityScale = originalGravity;
        //absorbDash.enabled = false;
        playerMove.isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
