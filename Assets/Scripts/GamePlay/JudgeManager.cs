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
    public static event Action DRailMove;
    public static event Action KRailMove;


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
        if (Input.GetKeyDown("s"))
        {
            //インスタンスではなくリストのコピーを用いることで参照エラー回避。
            //現在の再生時間から探索範囲を絞れば数万ノーツでも軽い処理が可能になる
            var notesCopy = new List<Note>(NoteManager.instance.SNotes);

            foreach (var note in notesCopy)
            {
                if (note == null) continue;
                float judgeTiming = (currentPlayTime - note.expectedTime) * 1000f;
                if (judgeTiming <= 22.25 && judgeTiming >= -22.25)
                {
                    Debug.Log($"parfect: {judgeTiming}ms");
                    note.Delete("S");
                    Perfect?.Invoke();
                    DRailMove?.Invoke();
                    break; //多重判定回避
                }
                else if (judgeTiming <= 40 && judgeTiming >= -40)
                {
                    Debug.Log($"great: {judgeTiming}ms");
                    note.Delete("S");
                    Great?.Invoke();
                    DRailMove?.Invoke();
                    break; //多重判定回避
                }
                else if (judgeTiming <= 70 && judgeTiming >= -70)
                {
                    Debug.Log($"good: {judgeTiming}ms");
                    note.Delete("S");
                    Good?.Invoke();
                    DRailMove?.Invoke();
                    break; //多重判定回避
                }
            }
        }
        if (Input.GetKeyDown("d"))
        {
            //インスタンスではなくリストのコピーを用いることで参照エラー回避。
            //現在の再生時間から探索範囲を絞れば数万ノーツでも軽い処理が可能になる
            var notesCopy = new List<Note>(NoteManager.instance.DNotes);

            foreach (var note in notesCopy)
            {
                if (note == null) continue;
                float judgeTiming = (currentPlayTime - note.expectedTime) * 1000f;
                if (judgeTiming <= 22.25 && judgeTiming >= -22.25)
                {
                    Debug.Log($"parfect: {judgeTiming}ms");
                    note.Delete("D");
                    Perfect?.Invoke();
                    break; //多重判定回避
                }
                else if (judgeTiming <= 40 && judgeTiming >= -40)
                {
                    Debug.Log($"great: {judgeTiming}ms");
                    note.Delete("D");
                    Great?.Invoke();
                    break; //多重判定回避
                }
                else if (judgeTiming <= 70 && judgeTiming >= -70)
                {
                    Debug.Log($"good: {judgeTiming}ms");
                    note.Delete("D");
                    Good?.Invoke();
                    break; //多重判定回避
                }
            }
        }
        if (Input.GetKeyDown("f"))
        {
            var notesCopy = new List<Note>(NoteManager.instance.FNotes);

            foreach (var note in notesCopy)
            {
                if (note == null) continue;
                float judgeTiming = (currentPlayTime - note.expectedTime) * 1000f;
                if (judgeTiming <= 22.25 && judgeTiming >= -22.25)
                {
                    Debug.Log($"parfect: {judgeTiming}ms");
                    note.Delete("F");
                    Perfect?.Invoke();
                    break; //多重判定回避
                }
                else if (judgeTiming <= 40 && judgeTiming >= -40)
                {
                    Debug.Log($"great: {judgeTiming}ms");
                    note.Delete("F");
                    Great?.Invoke();
                    break; //多重判定回避
                }
                else if (judgeTiming <= 70 && judgeTiming >= -70)
                {
                    Debug.Log($"good: {judgeTiming}ms");
                    note.Delete("F");
                    Good?.Invoke();
                    break; //多重判定回避
                }
            }
        }
        if (Input.GetKeyDown("j"))
        {
            var notesCopy = new List<Note>(NoteManager.instance.JNotes);

            foreach (var note in notesCopy)
            {
                if (note == null) continue;
                float judgeTiming = (currentPlayTime - note.expectedTime) * 1000f;
                if (judgeTiming <= 22.25 && judgeTiming >= -22.25)
                {
                    Debug.Log($"parfect: {judgeTiming}ms");
                    note.Delete("J");
                    Perfect?.Invoke();
                    break; //多重判定回避
                }
                else if (judgeTiming <= 40 && judgeTiming >= -40)
                {
                    Debug.Log($"great: {judgeTiming}ms");
                    note.Delete("J");
                    Great?.Invoke();
                    break; //多重判定回避
                }
                else if (judgeTiming <= 70 && judgeTiming >= -70)
                {
                    Debug.Log($"good: {judgeTiming}ms");
                    note.Delete("J");
                    Good?.Invoke();
                    break; //多重判定回避
                }
            }
        }
        if (Input.GetKeyDown("k"))
        {
            var notesCopy = new List<Note>(NoteManager.instance.KNotes);

            foreach (var note in notesCopy)
            {
                if (note == null) continue;
                float judgeTiming = (currentPlayTime - note.expectedTime) * 1000f;
                if (judgeTiming <= 22.25 && judgeTiming >= -22.25)
                {
                    Debug.Log($"parfect: {judgeTiming}ms");
                    note.Delete("K");
                    Perfect?.Invoke();
                    break; //多重判定回避
                }
                else if (judgeTiming <= 40 && judgeTiming >= -40)
                {
                    Debug.Log($"great: {judgeTiming}ms");
                    note.Delete("K");
                    Great?.Invoke();
                    break; //多重判定回避
                }
                else if (judgeTiming <= 70 && judgeTiming >= -70)
                {
                    Debug.Log($"good: {judgeTiming}ms");
                    note.Delete("K");
                    Good?.Invoke();
                    break; //多重判定回避
                }
            }
        }
        var forExproreMissesCopy = new List<Note>(NoteManager.instance.Notes);
        foreach (var note in forExproreMissesCopy)
        {
            if (note == null) continue;
            float judgeTiming = (currentPlayTime - note.expectedTime) * 1000f;
            if (judgeTiming > 100)
            {
                Debug.Log($"miss: {judgeTiming}ms");
                note.Delete("D");
                Miss?.Invoke();
            }
        }
        if (Input.GetKeyDown("l"))
        {
            //インスタンスではなくリストのコピーを用いることで参照エラー回避。
            //現在の再生時間から探索範囲を絞れば数万ノーツでも軽い処理が可能になる
            var notesCopy = new List<Note>(NoteManager.instance.LNotes);

            foreach (var note in notesCopy)
            {
                if (note == null) continue;
                float judgeTiming = (currentPlayTime - note.expectedTime) * 1000f;
                if (judgeTiming <= 22.25 && judgeTiming >= -22.25)
                {
                    Debug.Log($"parfect: {judgeTiming}ms");
                    note.Delete("L");
                    Perfect?.Invoke();
                    KRailMove?.Invoke();
                    break; //多重判定回避
                }
                else if (judgeTiming <= 40 && judgeTiming >= -40)
                {
                    Debug.Log($"great: {judgeTiming}ms");
                    note.Delete("L");
                    Great?.Invoke();
                    KRailMove?.Invoke();
                    break; //多重判定回避
                }
                else if (judgeTiming <= 70 && judgeTiming >= -70)
                {
                    Debug.Log($"good: {judgeTiming}ms");
                    note.Delete("L");
                    Good?.Invoke();
                    KRailMove?.Invoke();
                    break; //多重判定回避
                }
            }
        }
    }
    

}
