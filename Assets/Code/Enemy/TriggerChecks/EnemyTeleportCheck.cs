using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTeleportCheck : MonoBehaviour
{
    public GameObject playerTarget { get; set; }
    private EnemyBase enemy;
    //[SerializeField] private float tpCooldown = 20f;
    //[SerializeField] private float teleportDistance = 5f;
    ////[SerializeField] private GameObject lookPoint;
    //[SerializeField] private float tpDelay = 3f;
    //[SerializeField] private List<GameObject> tpPoints = new List<GameObject>();


    private void Awake()
    {
        playerTarget = GameObject.FindGameObjectWithTag("player");

        enemy = GetComponentInParent<EnemyBase>();        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == playerTarget)
        {
            enemy.setTeleportingDistance(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == playerTarget)
        {
            enemy.setTeleportingDistance(false);
        }
    }

    /*

    private Vector2 GetTeleportPosition()
    {
        Vector2 direction = Random.insideUnitCircle.normalized;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.forward);
        lookPoint.transform.parent.rotation = Quaternion.EulerAngles(0, 0, rotation.z);
        Debug.Log("DIRECTION: " + direction);

        // Cast a Raycast from the attackpoint at a max distance of teleportDistance
        RaycastHit2D hit = Physics2D.Raycast(enemy.transform.position, direction,
            teleportDistance, layerMask);

        if (hit.collider != null)
        {
            Debug.Log("HIT: " + hit.point);
            Debug.Log("HIT!");

            float distanceToHit = Vector2.Distance(enemy.transform.position, hit.point);
            Vector2 pos = new Vector2(hit.point.x - distanceToHit, hit.point.y - distanceToHit);
            Debug.DrawLine(enemy.transform.position, hit.point, Color.green, 10f);

            return pos;
        }
        else
        {
            Debug.DrawLine(enemy.transform.position, direction, Color.green, 10f);
            return hit.point;
        }
    }
    */
    
}
