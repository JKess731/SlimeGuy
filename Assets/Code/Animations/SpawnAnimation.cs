using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAnimation : MonoBehaviour
{
    private Animator anim;
    public GameObject enemy;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("EnemySpawn");
    }

    public void StartEnemy()
    {
        enemy.SetActive(true);

        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
