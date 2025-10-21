using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeManager : MonoBehaviour
{
    public static JudgeManager instance;

    public static event Action Perfect;
    public static event Action Great;
    public static event Action Good;
    public static event Action Miss;

    private float currentPlayTime;
    private Note[] Notes;
    private int destroyedNotesCount;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        currentPlayTime = MusicManager.instance.CurrentPlayTime;
        if (Input.GetKeyDown("k"))
        {
            //インスタンスではなくリストのコピーを用いることで参照エラー回避。
            //現在の再生時間から探索範囲を絞れば数万ノーツでも軽い処理が可能になる
            var notesCopy = new List<Note>(NoteManager.instance.Notes);

            foreach (var note in notesCopy)
            {
                if (note == null) continue;
                float judgeTiming = (currentPlayTime - note.expectedTime) * 1000f;
                if (judgeTiming <= 50 && judgeTiming >= -50)
                {
                    //この判定が通ってbreak;すれば多重判定を防げるかも
                    Debug.Log($"parfect: {judgeTiming}ms");
                    note.Delete();
                    Perfect?.Invoke();
                    break; //多重判定回避
                }
            }
        }
    }


}
