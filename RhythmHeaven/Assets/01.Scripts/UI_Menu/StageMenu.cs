using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMenu : MonoBehaviour
{
    [SerializeField] GameObject go_TitleMenu = null;
    [SerializeField] TitleMenu theTitle;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            BtnPlay();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            BtnBack();
        }
    }

    public void BtnPlay()
    {
        GameManager.inst.GameStart();
        this.gameObject.SetActive(false);
    }
    public void BtnBack()
    {
        go_TitleMenu.SetActive(true);
        theTitle.isPressAnyKey = true;
        theTitle.MenuAnim();
        this.gameObject.SetActive(false);
    }
}
