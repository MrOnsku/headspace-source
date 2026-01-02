using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public bool spawned;

    public GameObject enemyPrefab;

    public void Spawn()
    {
        if(spawned == false)
        {
            var spawn = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            spawn.GetComponent<Enemy>().owner = gameObject;
            spawned = true;
        }
    }
}