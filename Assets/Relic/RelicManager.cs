using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RelicManager : MonoBehaviour
{
    public static RelicManager instance;
    [SerializeField] private AbilityManager _abilityManager;
    [SerializeField] private int maxRelics = 10;


    //Removed the end becuase it keeps crashing the game
    public AbilityManager abilityManager; //{ get { return _abilityManager; } }
    public RelicCalculator calculator;
    public RelicSO[] relicsEquipped;

    private void Awake()
    {
        relicsEquipped = new RelicSO[maxRelics];
        abilityManager = FindObjectOfType<AbilityManager>();

        // Singleton pattern: only one instance of this class can exist
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
       
    }

    // Adds a relic to the array at the first null position
    public void AddRelic(RelicSO r)
    {
        for (int i = 0; i < relicsEquipped.Length; i++) 
        {
            if (relicsEquipped[i] == null)
            {
                relicsEquipped[i] = r;
                UiManager.instance?.UpdateRelicImage(r, i, true);
                break;
            }
        }
    }
    
    /// <summary>
    /// Removes a relic and replaces null at it's position in the array
    /// </summary>
    /// <param name="r"> Relic scriptable object</param>
    public void RemoveRelic(RelicSO r)
    {
        for (int i = 0; i < relicsEquipped.Length; i++)
        {
            if (relicsEquipped[i] == r)
            {
                relicsEquipped[i] = null;
                UiManager.instance.UpdateRelicImage(r, i, false);
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

    private void CheckRelics()
    {
        foreach (RelicSO r in relicsEquipped)
        {
            if (r.HasCondition && r.Condition())
            {
                r.ActivateBuffs();
            }
        }
    }

}
