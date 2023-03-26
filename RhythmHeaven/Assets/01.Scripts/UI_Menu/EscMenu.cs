using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscMenu : MonoBehaviour
{
    bool isPause = false;

    [SerializeField] Animator countAnim = null;
    string count = "Count";

    private void OnEnable()
    {
        isPause = true;
        Time.timeScale = 0;
        AudioManager.inst.PauseBGM();
    }

    private void Update()
    {
        if(isPause)
        {
            if(Input.GetKeyDown(KeyCode.F5))
            {
                Btn_Retry();
            }
            if (Input.GetKeyDown(KeyCode.F6))
            {
                Btn_Menu();
            }
            if (Input.GetKeyDown(KeyCode.F7))
            {
                Btn_Continue();
            }
        }
    }

    public void Btn_Retry()
    {
        isPause = false;
        GameManager.inst.GameStart();
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }
    public void Btn_Menu()
    {
        isPause = false;
        GameManager.inst.MainMenu();
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }
    
    public void Btn_Continue()
    {
        countAnim.SetTrigger(count);
    }
    public void ContinueGame()
    {
        isPause = false;
        Time.timeScale = 1;
        AudioManager.inst.ReplayBGM();

        this.gameObject.SetActive(false);
    }
}
