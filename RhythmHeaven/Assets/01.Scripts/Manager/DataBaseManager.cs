using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[System.Serializable]
public class SaveData
{
    public int[] _scores; // �뷡 ������.
    public int _Level = 1; // ����
    public int _experience; // ����ġ
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

        if (!Directory.Exists(SAVE_DATA_DIRECTORY)) // ��ο� �ش� ������ �����ϴ��� Ȯ��.
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY); // ���ٸ� �ش� ������ ���丮 ����.

        LoadDataJson();
    }
    public void SaveDataJson()
    {
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, json);

        Debug.Log("����Ϸ�");
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
