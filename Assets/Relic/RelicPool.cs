using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Relic Pool", menuName ="Relics/Relic Pool")]
public class RelicPool : ScriptableObject
{
    public List<GameObject> relics = new List<GameObject>();
}
