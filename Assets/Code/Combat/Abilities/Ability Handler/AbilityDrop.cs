using System.Collections.Generic;
using UnityEngine;

public class AbilityDrop : MonoBehaviour
{
    [Header("Ability Prefabs")]
    [SerializeField] private List<GameObject> abilityPrefabs;  

    private Dictionary<string, string> abilityNameToPrefabName = new Dictionary<string, string>
    {
        { "SlimeGrenade", "SlimeGrenadeDrop" },
        { "SlimeWhip", "SlimeWhipDrop" },
        { "SlimeWall", "SlimeWallDrop" },
        { "ShotgunSlimePellet", "SlimeShotgunDrop" },
        { "ShotgunWide", "SlimeShotgunWideDrop" },
        { "DividingSlash", "SlimeSlashDrop" },
        { "SlimeSwipe", "SlimeSwipeDrop" },
        { "SlimePush", "SlimePushDrop" },
        { "SlimePushPulse", "SlimePushPulseDrop" },
        { "SlimePunch", "SlimePunchDrop" },
        { "SlimeOrbit", "SlimeOrbitDrop" }
    };

    private HashSet<string> droppedAbilities = new HashSet<string>();

    public void DropAbility(string abilityName, Vector3 dropPosition)
    {
        if (droppedAbilities.Contains(abilityName))
        {
            Debug.LogWarning($"Ability '{abilityName}' has already been dropped.");
            return;
        }

        if (abilityNameToPrefabName.TryGetValue(abilityName, out string prefabName))
        {
            GameObject prefabToDrop = abilityPrefabs.Find(prefab => prefab.name == prefabName);

            if (prefabToDrop != null)
            {
                Instantiate(prefabToDrop, dropPosition, Quaternion.identity);

                droppedAbilities.Add(abilityName);
            }
            else
            {
                Debug.LogWarning($"No prefab found for ability: {abilityName}");
            }
        }
        else
        {
            Debug.LogWarning($"No prefab mapping found for ability: {abilityName}");
        }
    }
}




