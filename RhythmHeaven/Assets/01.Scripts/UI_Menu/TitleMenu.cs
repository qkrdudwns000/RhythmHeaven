using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenu : MonoBehaviour
{
    [SerializeField] GameObject go_StageUI = null;

    public bool isPressAnyKey = false;
    public int curMenuNum = 0;
    public UnityEngine.UI.Image[] menuGause = null;

    Animator myAnim;
    string play = "Play", option = "Option", quit = "Quit";

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isPressAnyKey)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                curMenuNum++;
                if (curMenuNum >= menuGause.Length)
                    curMenuNum = 0;
                MenuAnim();
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                curMenuNum--;
                if (curMenuNum < 0)
                    curMenuNum = menuGause.Length - 1;
                MenuAnim();
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                ResetAnim();
                MenuAction();
            }
        }
    }
    public void MenuAnim()
    {
        switch (curMenuNum)
        {
            case 0:
                myAnim.SetTrigger(play);
                break;
            case 1:
                myAnim.SetTrigger(option);
                break;
            case 2:
                myAnim.SetTrigger(quit);
                break;
        }
    }

    public void MenuAction()
    {
        switch(curMenuNum)
        {
            // Play
            case 0:
                BtnPlay();
                break;
            // Option
            case 1:
                
                break;
            // Quit
            case 2:
                Application.Quit();
                break;
        }
    }

    public void BtnPlay()
    {
        go_StageUI.SetActive(true);
        curMenuNum = 0;
        this.gameObject.SetActive(false);
    }
    
    void ResetAnim()
    {
        for(int i = 0; i < menuGause.Length; i++)
        {
            menuGause[i].fillAmount = 0;
        }
    }
}
