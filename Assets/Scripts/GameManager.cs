using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int[] PlayResult = new int[7];//(score, combo, maxCombo, parfect, great, good, miss)

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

    }

    void AddPerfect()
    {
        PlayResult[3]++;
    }
    void AddGreat()
    {
        PlayResult[4]++;
    }
    void AddGood()
    {
        PlayResult[5]++;
    }
    void AddMiss()
    {
        PlayResult[6]++;
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
