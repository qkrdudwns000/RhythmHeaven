using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[System.Serializable]
public class SaveData
{
    public int[] _scores; // 노래 점수들.
    public int _Level = 1; // 레벨
    public int _experience; // 경험치
}
public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager inst = null;
    public SaveData saveData = new SaveData();

    private string SAVE_DATA_DIRECTORY;
    private string SAVE_FILENAME = "/SaveFile.txt";

    private void Awake()
    {
        if (inst == null)
        {
            inst = this;
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        SAVE_DATA_DIRECTORY = Application.dataPath + "/Saves/";

        if (!Directory.Exists(SAVE_DATA_DIRECTORY)) // 경로에 해당 데이터 존재하는지 확인.
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY); // 없다면 해당 데이터 디렉토리 생성.

        LoadDataJson();
    }
    public void SaveDataJson()
    {
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, json);

        Debug.Log("저장완료");
    }
    public void LoadDataJson()
    {
        if(File.Exists(SAVE_DATA_DIRECTORY + SAVE_FILENAME))
        {
            string loadJson = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);
        }
        GameManager.inst.playerInfo._lv = saveData._Level;
        GameManager.inst.playerInfo._experience = saveData._experience;
        GameManager.inst.ShowLevelUI();
    }
}
