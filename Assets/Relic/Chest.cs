using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private RelicPool pool;

    private GameObject player;
    private RelicManager rManager;

    void Start()
    {
        player = GameObject.FindWithTag("player");
        rManager = player.GetComponent<RelicManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            SpawnRelic();
        }
    }

    void SpawnRelic()
    {
        GameObject relic = pool.relics[Random.Range(0, pool.relics.Count)];
        RelicPickup rp = relic.GetComponent<RelicPickup>();
        RelicSO rpSO = rp.relicScriptableObject;

        if (rManager.relicsEquipped.Contains(rpSO))
        {
            SpawnRelic();
        }
        else
        {
            GameObject spawnedRelic = Instantiate(relic);
            spawnedRelic.transform.position = transform.position;
            gameObject.SetActive(false);
        }
    }
}
