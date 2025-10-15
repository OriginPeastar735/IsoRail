using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    public float CurrentPlayTime; // 現在の再生時間
    public AudioClip shiningStar;
    private AudioSource ShiningStar;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        ShiningStar = gameObject.GetComponent<AudioSource>();
        ShiningStar.clip = shiningStar;
        ShiningStar.Play();
        CurrentPlayTime = ShiningStar.time;
        //CurrentPlayTime = 0;
    }

    void Update()
    {
        CurrentPlayTime = ShiningStar.time;
        //Debug.Log(CurrentPlayTime);
    }
}
