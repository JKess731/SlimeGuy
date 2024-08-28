using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicPickup : MonoBehaviour
{
    [SerializeField] private RelicSO relicScriptableObject;
    [SerializeField] private RelicManager relicManager;
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player") && collision.gameObject.name == "Player")
        {
            if (relicManager.GetRelicCount() - 1 < relicManager.relicsEquipped.Length)
            {
                relicManager.AddRelic(relicScriptableObject);
            }
        }

        relicScriptableObject.ActivateBuff(relicManager.playerStats);
        gameObject.SetActive(false);
    }
}
