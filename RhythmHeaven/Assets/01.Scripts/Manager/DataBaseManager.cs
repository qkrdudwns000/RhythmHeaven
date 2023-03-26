using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    public int[] scores;

    private void Start()
    {
        LoadScore();
    }
    public void SaveScore()
    {
        for(int i = 0; i < scores.Length; i++)
        {
            PlayerPrefs.SetInt("Score" + i.ToString(), scores[i]);
        }
    }

    public void LoadScore()
    {
        if(PlayerPrefs.HasKey("Score0"))
        {
            for(int i = 0; i <scores.Length; i++)
            {
                scores[i] = PlayerPrefs.GetInt("Score" + i.ToString());
            }
        }
    }
}
