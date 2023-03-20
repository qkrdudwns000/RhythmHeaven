using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleFrame : MonoBehaviour
{
    bool musicStart = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!musicStart)
        {
            if (collision.CompareTag("Note"))
            {
                AudioManager.inst.PlayBGM("BGM0");
                musicStart = true;
            }
        }
    }
}
