using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAnyButton : MonoBehaviour
{
    public CanvasGroup menuGroup;

    [SerializeField] TitleMenu titleMenu;

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            PressingAnyButton();
        }
    }

    void PressingAnyButton()
    {
        menuGroup.alpha = 1;
        menuGroup.blocksRaycasts = true;
        menuGroup.interactable = true;

        titleMenu.isPressAnyKey = true;
        titleMenu.MenuAnim();
        this.gameObject.SetActive(false);
    }
}
