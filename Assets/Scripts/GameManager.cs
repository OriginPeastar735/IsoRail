using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int[] PlayResult = new int[7];//(score, combo, maxCombo, parfect, great, good, miss)

    [SerializeField] private TextMeshPro ComboText;
    string ComboStr;

    void Start()
    {
        for (int i = 0; i < 7; i++)
        {
            PlayResult[i] = 0;
        }
        NoteManager.instance.LoadJson("ShiningStar");
    }

    // Update is called once per frame
    void Update()
    {
        ComboText.text = PlayResult[1] + "COMBO";
    }

    void AddPerfect()
    {
        PlayResult[3]++;
        AddCombo();
    }
    void AddGreat()
    {
        PlayResult[4]++;
        AddCombo();
    }
    void AddGood()
    {
        PlayResult[5]++;
        AddCombo();
    }
    void AddMiss()
    {
        PlayResult[6]++;
        ResetCombo();
    }

    void AddCombo()
    {
        PlayResult[1]++;
        if (PlayResult[2] < PlayResult[1])
        {
            PlayResult[2] = PlayResult[1];
        }
    }

    void ResetCombo()
    {
        PlayResult[1] = 0;
    }

    void OnEnable()
    {
        JudgeManager.Perfect += AddPerfect;
        JudgeManager.Great += AddGreat;
        JudgeManager.Good += AddGood;
        JudgeManager.Miss += AddMiss;
    }

    void OnDisable()
    {
        JudgeManager.Perfect -= AddPerfect;
        JudgeManager.Great -= AddGreat;
        JudgeManager.Good -= AddGood;
        JudgeManager.Miss -= AddMiss;
    }
}
