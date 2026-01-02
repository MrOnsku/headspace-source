using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoJoyOverlay : MonoBehaviour
{
    public Vector3 overlayPos;
    public Vector3 noOverlayPos;

    public Emotion joy;

    public BoxCollider2D viewArea;

    private void Update()
    {
        if(joy.hp == 0)
        {
            transform.position = Vector3.Lerp(transform.position, overlayPos, Time.deltaTime * 8);

            viewArea.offset = new Vector2(-2.747658f, -0.05676746f);
            viewArea.size = new Vector2(5.019351f, 10.82085f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, noOverlayPos, Time.deltaTime * 8);

            viewArea.offset = new Vector2(-4.882041f, -0.05676746f);
            viewArea.size = new Vector2(9.288117f, 10.82085f);
        }
    }
}