using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RelicManager : MonoBehaviour
{
    public Stats playerStats;
    [SerializeField] private int maxRelics = 10;

    public RelicSO[] relicsEquipped;

    private void Awake()
    {
        relicsEquipped = new RelicSO[maxRelics];
    }

    // Adds a relic to the array at the first null position
    public void AddRelic(RelicSO r)
    {
        for (int i = 0; i < relicsEquipped.Length; i++) 
        {
            if (relicsEquipped[i] == null)
            {
                relicsEquipped[i] = r;
                break;
            }
        }
    }

    // Removes a relic and replaces null at it's position in the array
    public void RemoveRelic(RelicSO r)
    {
        for (int i = 0; i < relicsEquipped.Length; i++)
        {
            if (relicsEquipped[i] == r)
            {
                relicsEquipped[i] = null;
                break;
            }
        }
    }

    // Returns amount of relics equipped
    public int GetRelicCount()
    {
        int count = 0;
        foreach (RelicSO r in relicsEquipped)
        {
            if (r != null)
            {
                count++;
            }
        }

        return count;
    }

}
