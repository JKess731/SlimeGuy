using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltSlimeWhip : MonoBehaviour
{
    public float activationTime;

    private bool canWhip = true;
    private bool whipStarted = false;

    private GameObject player;
    [SerializeField] private GameObject whip;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private float damage;
    [SerializeField] private float knockBackPower;

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
    }

    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Instantiate(whip,player.transform.position, Quaternion.identity);
        }
    }

}
