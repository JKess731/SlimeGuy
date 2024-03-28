using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltSlimeWave : MonoBehaviour, IUltTriggerable
{
    // Player
    private GameObject player;
    private bool canWave = true;
    public float activationTime { get; set; }

    // Colliders
    [SerializeField] private List<GameObject> collidersList = new List<GameObject>();

    // Enemy
    private HashSet<GameObject> enemySet = new HashSet<GameObject>();

    private void Awake()
    {
        player = GameObject.FindWithTag("player");
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

        if (canWave)
        {
            #region Rotation
            Camera cam = Camera.main;
            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mousePos - player.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, angleAxis, Time.deltaTime * 50);
            #endregion

            if (Input.GetKeyDown(KeyCode.Q))
            {
                StartCoroutine(OnActivate(activationTime));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleTriggers(collision.gameObject);
    }

    public void HandleTriggers(GameObject enemy)
    {
        enemySet.Add(enemy);
        Debug.Log("Enemy in Trigger");
    }

    public IEnumerator OnActivate(float activationTime)
    {
        canWave = false;

        GameObject bigOne = collidersList[0];
        bigOne.SetActive(true);

        yield return new WaitForSeconds(activationTime / 2);
        for (int i = 1; i < collidersList.Count; i++)
        {
            collidersList[i].SetActive(true);
        }

        Debug.Log("Ult activating");
        yield return new WaitForSeconds(activationTime);
        Debug.Log("Dealing Damage");

        foreach (GameObject trigger in collidersList)
        {
            trigger.SetActive(false);
        }

        canWave = true;
    }
}
