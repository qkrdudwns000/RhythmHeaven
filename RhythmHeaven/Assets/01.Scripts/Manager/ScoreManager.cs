using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Score
    [SerializeField] TMPro.TMP_Text txtScore = null;
    [SerializeField] float[] weight = null; // ������ ���� ���� ����ġ.
    [SerializeField] int increaseScore = 100;
    [SerializeField] int comboBonusScore = 100;
    int currentScore = 0;

    Animator myAnim;
    string scoreUp = "ScoreUp"; // �ִ� Ʈ���Źߵ����� ����.
    string comboUp = "ComboUp";

    // Combo
    [SerializeField] GameObject go_ComboImage;
    [SerializeField] TMPro.TMP_Text txtCombo = null;

    int currentCombo = 0;

    // Start is called before the first frame update
    void Start()
    {
        HideCombo();
        myAnim = GetComponent<Animator>();
        currentScore = 0;
        txtScore.text = "0";
    }

    public void IncreaseScore(int _judgementState)
    {
        // �޺� ����
        IncreaseCombo();
        // �ӽú���(temp)
        int t_bonusComboScore = (currentCombo / 10) * comboBonusScore;
        int t_increaseScore = increaseScore + t_bonusComboScore;

        // ����ġ ���
        t_increaseScore = (int)(t_increaseScore * weight[_judgementState]);

        // ���� �ݿ�
        currentScore += t_increaseScore;
        txtScore.text = string.Format("{0:#,##0}", currentScore);
        // �ִ� ����
        myAnim.SetTrigger(scoreUp);
    }

    public void IncreaseCombo(int _num = 1)
    {
        currentCombo += _num;
        txtCombo.text = string.Format("{0:#,##0}", currentCombo);

        if(currentCombo >2)
        {
            txtCombo.gameObject.SetActive(true);
            go_ComboImage.SetActive(true);

            // �޺� Anim
            myAnim.SetTrigger(comboUp);
        }
    }

    public void ResetCombo()
    {
        currentCombo = 0;
        txtCombo.text = "0";
        HideCombo();
    }

    private void HideCombo()
    {
        txtCombo.gameObject.SetActive(false);
        go_ComboImage.SetActive(false);
    }
}
