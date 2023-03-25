using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPlate : Plate
{
    AudioSource theAudio;
    Result theResult;

    // Start is called before the first frame update
    void Start()
    {
        theResult = FindObjectOfType<Result>();
        theAudio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            theAudio.Play();
            PlayerController.isCanPressKey = false;
            theResult.HideUI();
            StartCoroutine(ShowTimeline());
        }
    }
    
    IEnumerator ShowTimeline()
    {
        yield return new WaitForSeconds(1.0f);

        TimelineController.inst.Play();

        yield return null;
        theResult.ShowResult();
    }

}
