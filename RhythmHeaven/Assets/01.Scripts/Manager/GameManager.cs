using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;
    [SerializeField] GameObject go_StageTitle = null;

    public bool isStartGame = false;

    // Start is called before the first frame update
    private void Awake()
    {
        inst = this;
    }

    public void GameStart()
    {
        isStartGame = true;
    }

    public void MainMenu()
    {
        isStartGame = false;

        go_StageTitle.SetActive(true);
    }
}
