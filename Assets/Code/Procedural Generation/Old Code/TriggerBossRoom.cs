using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBossRoom : MonoBehaviour
{
    [SerializeField] private LevelGenerator gen;
    private GameObject bossRoom;
    private GameObject player;

    private void Start()
    {
        gen = GameObject.FindWithTag("generator").GetComponent<LevelGenerator>();
        player = GameObject.FindWithTag("player");

        gen.Complete.AddListener(PlacePortal);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            EnableBossRoom();
        }
    }

    private void PlacePortal()
    {
        GameObject bossArena = gen.bossArena;

        GameObject placedArena = Instantiate(bossArena, new Vector2(0, 0), Quaternion.identity);
        bossRoom = placedArena;
        bossRoom.SetActive(false);

        transform.parent = gen.lastRoom.transform;
        transform.localPosition = new Vector2(0, 5);
    }

    private void EnableBossRoom()
    {
        gen.gameObject.SetActive(false);
        bossRoom.SetActive(true);

        player.transform.position = new Vector2(0, -6);
    }    
}
