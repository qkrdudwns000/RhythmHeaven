using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollMenu : MonoBehaviour
{
    // 스크롤 이동값 변수
    float[] scrollPos = null;
    float scrollDist = 0;
    int scrollIndex = 0;
    bool isScollMoveDown = false;
    bool isScollMoveUp = false;
    // 스크롤 이동시 색상 및 크기 변화 변수
    Vector2 orgScale = new Vector2(1.0f, 1.0f);
    Vector2 smallScale = new Vector2(0.7f, 0.7f);
    Color orgColor = new Color(1, 1, 1, 1);
    Color smallColor = new Color(1, 1, 1, 0.6f);

    [SerializeField] Scrollbar scrollBar;
    [SerializeField] TMPro.TMP_Text[] songNames = null;
    
    private void Start()
    {
        // text마다의 위치값 설정.
        CreateScrollValue();
        scrollBar.value = 1.0f;
        scrollIndex = 0;
        songNames = transform.GetComponentsInChildren<TMPro.TMP_Text>();
        ChangeScaleAndColor();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(ScrollMoveDown());
            ChangeScaleAndColor();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartCoroutine(ScrollMoveUp());
            ChangeScaleAndColor();
        } 
    }
    IEnumerator ScrollMoveDown()
    {
        if (scrollIndex + 1 >= scrollPos.Length)
            yield break;

        isScollMoveDown = true;
        scrollIndex++;

        while (isScollMoveDown)
        {
            scrollBar.value = Mathf.Lerp(scrollBar.value, scrollPos[scrollIndex], 0.1f);
            if (Mathf.Approximately(scrollPos[scrollIndex], scrollBar.value))
            {
                isScollMoveDown = false;
                scrollBar.value = scrollPos[scrollIndex];
            }
            yield return null;
        }
    }
    IEnumerator ScrollMoveUp()
    {
        if (scrollIndex - 1 < 0)
            yield break;

        isScollMoveUp = true;
        scrollIndex--;

        while (isScollMoveUp)
        {
            scrollBar.value = Mathf.Lerp(scrollBar.value, scrollPos[scrollIndex], 0.1f);
            if (Mathf.Approximately(scrollPos[scrollIndex], scrollBar.value))
            {
                isScollMoveUp = false;
                scrollBar.value = scrollPos[scrollIndex];
            }
            yield return null;
        }
    }
    void ChangeScaleAndColor()
    {
        transform.GetChild(scrollIndex).localScale = orgScale;
        songNames[scrollIndex].color = orgColor;
        for(int i = 0; i < scrollPos.Length; i++)
        {
            if(i != scrollIndex)
            {
                songNames[i].color = smallColor;
                transform.GetChild(i).localScale = smallScale;
            }
        }
    }

    void CreateScrollValue()
    {
        scrollPos = new float[transform.childCount];
        scrollDist = 1f / (scrollPos.Length - 1f);

        for(int i = 0; i < scrollPos.Length; i++)
        {
            scrollPos[i] = 1 - (scrollDist * i);
        }
    }
}
