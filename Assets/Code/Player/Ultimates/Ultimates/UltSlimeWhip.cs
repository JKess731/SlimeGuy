using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltSlimeWhip : MonoBehaviour, IUltTriggerable
{
    public float activationTime;

    private bool canWhip = true;
    private bool whipStarted = false;

    private GameObject player;
    [SerializeField] private GameObject whip;
    [SerializeField] private float rotationSpeed;

    void Awake()
    {
        whip.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("player");
    }

    // Update is called once per frame
    void Update()
    {
        if (canWhip)
        {
            HandleInput();
        }

        if (whipStarted)
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleTriggers(collision.gameObject);
    }

    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(OnActivate(activationTime));
        }
    }

    public void HandleTriggers(GameObject enemy)
    {
        // Deal Damage & Knockback
    }

    public IEnumerator OnActivate(float activationTime)
    {
        whip.SetActive(true);
        canWhip = false;
        whipStarted = true;

        yield return new WaitForSeconds(activationTime);

        whip.SetActive(false);
        canWhip = true;
        whipStarted = false;
    }
}
