using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleFrame : MonoBehaviour
{
    bool musicStart = false;

    public string bgmName = "";

    public void ResetMusic()
    {
        musicStart = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!musicStart)
        {
            if (collision.CompareTag("Note"))
            {
                AudioManager.inst.PlayBGM(bgmName);
                musicStart = true;
            }
        }
    }
}
