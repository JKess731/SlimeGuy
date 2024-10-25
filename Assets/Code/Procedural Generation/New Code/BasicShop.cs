using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasicShop : MonoBehaviour
{
    [SerializeField] private List<GameObject> abilityPickups = new List<GameObject>();
    [SerializeField] private List<GameObject> relicPickups = new List<GameObject>();
    [SerializeField] private float distanceX = 2;
    [SerializeField] private float distanceY = 2;

    private void Start()
    {
        SpawnObjects();
    }

    private void SpawnObjects()
    {
        (GameObject, GameObject, GameObject) relics = ChooseRandomObjects(relicPickups);
        GameObject r1 = relics.Item1;
        GameObject r2 = relics.Item2;

        Instantiate(r1, transform.position, Quaternion.identity);
        Instantiate(r2, new Vector2(transform.position.x + distanceX, transform.position.y), Quaternion.identity);

        (GameObject, GameObject, GameObject) abilitites = ChooseRandomObjects(abilityPickups);
        GameObject a1 = abilitites.Item1;
        GameObject a2 = abilitites.Item2;

        Instantiate(a1, new Vector2(transform.position.x, transform.position.y + distanceY), Quaternion.identity);
        Instantiate(a2, new Vector2(transform.position.x + distanceX, transform.position.y + distanceY), Quaternion.identity);
    }

    private (GameObject relic1, GameObject relic2, GameObject relic3) ChooseRandomObjects(List<GameObject> l)
    {
        List<GameObject> list = new List<GameObject>();
        GameObject[] arr = new GameObject[3];
        list.AddRange(l); // Create a copy so we can remove options from the list

        for (int i = 0; i < arr.Length; i++)
        {
            (List<GameObject>, GameObject) t = GetRandom(list);
            list = t.Item1;
            arr[i] = t.Item2;
        }

        return (arr[0], arr[1], arr[2]);
    }

    private (List<GameObject>, GameObject) GetRandom(List<GameObject> list)
    {
        GameObject c = list[Random.Range(0, list.Count)];
        list.Remove(c);

        return (list, c);
    }
}
