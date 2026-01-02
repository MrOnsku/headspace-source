using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCameraController : MonoBehaviour
{
    public GameObject target;

    public float gameplayZoom;
    public float cutsceneZoom;

    public Camera gameCamera;

    public SpriteRenderer[] brain;

    public Vector3 defaultPos;

    private void Update()
    {
        if (GameManager.instance.gameRunning)
        {
            gameCamera.orthographicSize = Mathf.Lerp(gameCamera.orthographicSize, gameplayZoom, Time.deltaTime * 8);

            foreach (var item in brain)
            {
                item.color = new Color(item.color.r, item.color.g, item.color.b, Mathf.Lerp(item.color.a, 0, Time.deltaTime * 8));
            }
        }
        else
        {
            gameCamera.orthographicSize = Mathf.Lerp(gameCamera.orthographicSize, cutsceneZoom, Time.deltaTime * 8);

            foreach (var item in brain)
            {
                if(item.gameObject.name != "Shadow")
                {
                    item.color = new Color(item.color.r, item.color.g, item.color.b, Mathf.Lerp(item.color.a, 1, Time.deltaTime * 8));
                }
                else
                {
                    item.color = new Color(item.color.r, item.color.g, item.color.b, Mathf.Lerp(item.color.a, 0.5f, Time.deltaTime * 8));
                }
            }
        }
    }

    public void LateUpdate()
    {
        if (GameManager.instance.gameRunning)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(target.transform.position.x, target.transform.position.y, -10), Time.deltaTime * 8);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(defaultPos.x, defaultPos.y, -10), Time.deltaTime * 3);
        }
    }
}