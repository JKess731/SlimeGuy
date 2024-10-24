using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RelicPickup : MonoBehaviour
{
    public RelicSO relicScriptableObject;
    [SerializeField] private RelicManager relicManager;

    private void Awake()
    {
        relicManager = FindFirstObjectByType<RelicManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            StatsSO playerReference = collision.gameObject.GetComponent<PlayerStateMachine>().playerStats;
            if (relicManager.GetRelicCount() - 1 < relicManager.relicsEquipped.Length)
            {
                relicManager.AddRelic(relicScriptableObject);

                relicScriptableObject.Initialize(playerReference);
                relicScriptableObject.OnPickup();

                //Debug.Log("Player Reference: " + playerReference);
                //Debug.Log("Relic SO: " + relicScriptableObject);
                //Debug.Log("Relic Manager: " + relicManager);

                gameObject.SetActive(false);
            }
        }
    }
}
