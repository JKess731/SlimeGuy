using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltSlimePush : MonoBehaviour, IAttackTriggerable
{
    private GameObject player;

    [SerializeField] private float activationTime;
    [SerializeField] private float scaleFactor;
    [SerializeField] private GameObject theSlime;
    private bool canPush = true;
    private bool isPushing;

    private Vector3 originalScale;

    void Awake()
    {
        player = GameObject.FindWithTag("player");
        theSlime.SetActive(false);

        originalScale = theSlime.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (canPush)
        {
            HandleInput();
        }

        if (isPushing)
        {
            theSlime.transform.localScale = new Vector3(theSlime.transform.localScale.x + scaleFactor * Time.deltaTime, theSlime.transform.localScale.y + scaleFactor * Time.deltaTime);
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
            Debug.Log("Keycode down");
            StartCoroutine(OnActivate(activationTime));
        }
    }

    public void HandleTriggers(GameObject enemy)
    {

    }

    public IEnumerator OnActivate(float activationTime)
    {
        Debug.Log("activate");
        canPush = false;
        isPushing = true;
        theSlime.SetActive(true);

        //Disable player movement
        PlayerMove input = player.GetComponent<PlayerMove>();
        input.input.Disable();

        yield return new WaitForSeconds(activationTime);

        //Enable player movement
        input.input.Enable();

        canPush = true;
        isPushing = false;
        theSlime.SetActive(false);
        theSlime.transform.localScale = originalScale;
    }
}
