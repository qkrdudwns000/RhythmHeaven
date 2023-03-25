using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimelineController : MonoBehaviour
{
    public static TimelineController inst;

    private void Awake()
    {
        inst = this;
    }

    PlayableDirector playableDirector = null;

    private void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
    }
    public void Play()
    {
        playableDirector.Play();
    }

}
