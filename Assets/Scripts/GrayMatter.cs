using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayMatter : MonoBehaviour
{
    public GameObject owner;

    public void AlertOwner()
    {
        owner.GetComponent<GrayMatterSpawn>().spawned = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AlertOwner();
            collision.gameObject.GetComponent<PlayerController>().grayMatterCollectSound.Play();
            GameManager.instance.grayMatter++;
            GameManager.instance.grayMatterDisplayAnimator.Play("GrayMatterDisplayCollect");
            //GameManager.instance.grayMatterDisplayObject.transform.localPosition = new Vector3(GameManager.instance.grayMatterDisplayObject.transform.position.x, GameManager.instance.grayMatterDisplayMainPos.y + 25, GameManager.instance.grayMatterDisplayObject.transform.position.z);
            Destroy(gameObject);
        }
    }
}