using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] GameObject[] stageArr = null;
    GameObject currentStage;
    Transform[] stagePlates;

    [SerializeField] float offSetY = 5;
    [SerializeField] float plateSpeed = 10;
    [SerializeField] float plateRemoveSpeed = 2;

    int stepCount = 3;
    int removeCount = 0;
    int totalPlateCount = 0;

    
    public void RemoveStage()
    {
        if (currentStage != null)
            Destroy(currentStage);
    }
    public void SettingStage(int _songNum)
    {
        stepCount = 3;
        removeCount = 0;

        currentStage = Instantiate(stageArr[_songNum], Vector3.zero, Quaternion.identity);
        stagePlates = currentStage.GetComponent<Stage>().plates;
        totalPlateCount = stagePlates.Length;

        for(int i = 3; i < totalPlateCount; i++)
        {
            stagePlates[i].position = new Vector3(stagePlates[i].position.x,
                                                  stagePlates[i].position.y - offSetY,
                                                  stagePlates[i].position.z);
        }
    }

    public void ShowNextPlate()
    {
        if (stepCount < totalPlateCount)
        {
            StartCoroutine(MovePlateCo(stepCount++));
        }
        if(removeCount < totalPlateCount)
        {
            StartCoroutine(RemovePlateCo(removeCount++));
        }
    }

    IEnumerator MovePlateCo(int _num)
    {
        Debug.Log("move" + _num);
        stagePlates[_num].gameObject.SetActive(true);
        Vector3 t_destPos = new Vector3(stagePlates[_num].position.x, 
                                        stagePlates[_num].position.y + offSetY,
                                        stagePlates[_num].position.z);

        while (Vector3.SqrMagnitude(stagePlates[_num].position - t_destPos) >= 0.001f)
        {
            stagePlates[_num].position = Vector3.Lerp(stagePlates[_num].position, t_destPos, Time.deltaTime * plateSpeed);
            yield return null;
        }

        stagePlates[_num].position = t_destPos;
    }

    IEnumerator RemovePlateCo(int _num)
    {
        Debug.Log("Remove" + _num);
        yield return new WaitForSeconds(0.2f);

        Vector3 t_destPos = new Vector3(stagePlates[_num].position.x,
                                        stagePlates[_num].position.y + offSetY,
                                        stagePlates[_num].position.z);

        while (Vector3.SqrMagnitude(stagePlates[_num].position - t_destPos) >= 0.1f)
        {
            stagePlates[_num].position = Vector3.Lerp(stagePlates[_num].position, t_destPos, Time.deltaTime * plateRemoveSpeed);
            yield return null;
        }

        stagePlates[_num].gameObject.SetActive(false);
    }
}
