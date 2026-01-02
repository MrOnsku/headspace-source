using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayMatterSpawn : MonoBehaviour
{
    public bool spawned;

    public GameObject grayMatterPrefab;

    public void Spawn()
    {
        if(spawned == false)
        {
            var spawn = Instantiate(grayMatterPrefab, transform.position, Quaternion.identity);
            spawn.GetComponent<GrayMatter>().owner = gameObject;
            spawned = true;
        }
    }
}