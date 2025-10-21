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
    }

    // Update is called once per frame
    void Update()
    {

    }

    void AddPerfect()
    {
        PlayResult[3]++;
    }

    void OnEnable()
    {
        JudgeManager.Perfect += AddPerfect;
    }

    void OnDisable()
    {
        JudgeManager.Perfect -= AddPerfect;
    }
}
