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
        SpawnRelics();
    }

    private void SpawnRelics()
    {
        (GameObject, GameObject, GameObject) relics = ChooseRandomRelics();
        GameObject r1 = relics.Item1;
        GameObject r2 = relics.Item2;
        GameObject r3 = relics.Item3;

        Instantiate(r1, transform.position, Quaternion.identity);
        Instantiate(r2, new Vector2(transform.position.x + distanceX, transform.position.y), Quaternion.identity);
        Instantiate(r3, new Vector2(transform.position.x - distanceX, transform.position.y), Quaternion.identity);
    }

    private (GameObject relic1, GameObject relic2, GameObject relic3) ChooseRandomRelics()
    {
        List<GameObject> relics = new List<GameObject>();
        GameObject[] arr = new GameObject[3];
        relics.AddRange(relicPickups); // Create a copy so we can remove options from the list

        for (int i = 0; i < arr.Length; i++)
        {
            (List<GameObject>, GameObject) t = GetRandom(relics);
            relics = t.Item1;
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
