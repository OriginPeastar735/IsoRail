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

            var longNotesCopy = new List<LongNote>(NoteManager.instance.DLongNotes);
            foreach(var ln in longNotesCopy)
            {
                if(ln.state != LongNoteState.None)continue;
                float judgeTiming = (currentPlayTime - ln.previousExpectedTime) * 1000f;

                if(Mathf.Abs(judgeTiming) <= 70)
                {
                    ln.OnStartPress();//別スクリプトでもちゃんと認識してくれる
                    Good?.Invoke();
                    break;
                }
            }
        }
        if (Input.GetKey("d"))
        {
            var lnCopy = new List<LongNote>(NoteManager.instance.DLongNotes);
            foreach(var ln in lnCopy)
            {
                if(ln.state == LongNoteState.Holding)
                {
                    //押し続けている間のコンボ加算などをここで行う
                    float endTiming = (currentPlayTime - ln.expectedTime) * 1000f;
                    if(endTiming >= 0)
                    {
                        ln.Finish("D");
                    }
                }
            }
            
        }
        if (Input.GetKeyUp("d"))
        {
            var lnCopy = new List<LongNote>(NoteManager.instance.DLongNotes);
            foreach(var ln in lnCopy)
            {
                if(ln.state == LongNoteState.Holding)
                {
                    float judgeTiming = (currentPlayTime - ln.expectedTime) * 1000f;
                    if(judgeTiming < 0)
                    {
                        ln.OnReleaseEarly();
                        Miss?.Invoke();
                    }
                    else
                    {
                        ln.Finish(ln.railStr);
                        Perfect?.Invoke();
                    }
                }
            }
        }
        // --- Fレーンの判定 ---
        if (Input.GetKeyDown("f"))
        {
            var notesCopy = new List<Note>(NoteManager.instance.FNotes);
            foreach (var note in notesCopy)
            {
                if (note == null) continue;
                float judgeTiming = (currentPlayTime - note.expectedTime) * 1000f;
                if (Mathf.Abs(judgeTiming) <= 70) // 判定範囲を一纏めに記述（必要に応じて個別に戻してください）
                {
                    if (judgeTiming <= 22.25 && judgeTiming >= -22.25) { Debug.Log($"Perfect: {judgeTiming}ms"); Perfect?.Invoke(); }
                    else if (judgeTiming <= 40 && judgeTiming >= -40) { Debug.Log($"Great: {judgeTiming}ms"); Great?.Invoke(); }
                    else { Debug.Log($"Good: {judgeTiming}ms"); Good?.Invoke(); }
                    
                    note.Delete("F");
                    break;
                }
            }

            var longNotesCopy = new List<LongNote>(NoteManager.instance.FLongNotes);
            foreach (var ln in longNotesCopy)
            {
                if (ln.state != LongNoteState.None) continue;
                float judgeTiming = (currentPlayTime - ln.previousExpectedTime) * 1000f;
                if (Mathf.Abs(judgeTiming) <= 70)
                {
                    ln.OnStartPress();
                    Good?.Invoke();
                    break;
                }
            }
        }
        if (Input.GetKey("f"))
        {
            var lnCopy = new List<LongNote>(NoteManager.instance.FLongNotes);
            foreach (var ln in lnCopy)
            {
                if (ln.state == LongNoteState.Holding)
                {
                    float endTiming = (currentPlayTime - ln.expectedTime) * 1000f;
                    if (endTiming >= 0) ln.Finish("F");
                }
            }
        }
        if (Input.GetKeyUp("f"))
        {
            var lnCopy = new List<LongNote>(NoteManager.instance.FLongNotes);
            foreach (var ln in lnCopy)
            {
                if (ln.state == LongNoteState.Holding)
                {
                    float judgeTiming = (currentPlayTime - ln.expectedTime) * 1000f;
                    if (judgeTiming < 0) { ln.OnReleaseEarly(); Miss?.Invoke(); }
                    else { ln.Finish(ln.railStr); Perfect?.Invoke(); }
                }
            }
        }

        // --- Jレーンの判定 ---
        if (Input.GetKeyDown("j"))
        {
            var notesCopy = new List<Note>(NoteManager.instance.JNotes);
            foreach (var note in notesCopy)
            {
                if (note == null) continue;
                float judgeTiming = (currentPlayTime - note.expectedTime) * 1000f;
                if (Mathf.Abs(judgeTiming) <= 70)
                {
                    if (judgeTiming <= 22.25 && judgeTiming >= -22.25) { Debug.Log($"Perfect: {judgeTiming}ms"); Perfect?.Invoke(); }
                    else if (judgeTiming <= 40 && judgeTiming >= -40) { Debug.Log($"Great: {judgeTiming}ms"); Great?.Invoke(); }
                    else { Debug.Log($"Good: {judgeTiming}ms"); Good?.Invoke(); }
                    
                    note.Delete("J");
                    break;
                }
            }

            var longNotesCopy = new List<LongNote>(NoteManager.instance.JLongNotes);
            foreach (var ln in longNotesCopy)
            {
                if (ln.state != LongNoteState.None) continue;
                float judgeTiming = (currentPlayTime - ln.previousExpectedTime) * 1000f;
                if (Mathf.Abs(judgeTiming) <= 70)
                {
                    ln.OnStartPress();
                    Good?.Invoke();
                    break;
                }
            }
        }
        if (Input.GetKey("j"))
        {
            var lnCopy = new List<LongNote>(NoteManager.instance.JLongNotes);
            foreach (var ln in lnCopy)
            {
                if (ln.state == LongNoteState.Holding)
                {
                    float endTiming = (currentPlayTime - ln.expectedTime) * 1000f;
                    if (endTiming >= 0) ln.Finish("J");
                }
            }
        }
        if (Input.GetKeyUp("j"))
        {
            var lnCopy = new List<LongNote>(NoteManager.instance.JLongNotes);
            foreach (var ln in lnCopy)
            {
                if (ln.state == LongNoteState.Holding)
                {
                    float judgeTiming = (currentPlayTime - ln.expectedTime) * 1000f;
                    if (judgeTiming < 0) { ln.OnReleaseEarly(); Miss?.Invoke(); }
                    else { ln.Finish(ln.railStr); Perfect?.Invoke(); }
                }
            }
        }

        // --- Kレーンの判定 ---
        if (Input.GetKeyDown("k"))
        {
            var notesCopy = new List<Note>(NoteManager.instance.KNotes);
            foreach (var note in notesCopy)
            {
                if (note == null) continue;
                float judgeTiming = (currentPlayTime - note.expectedTime) * 1000f;
                if (Mathf.Abs(judgeTiming) <= 70)
                {
                    if (judgeTiming <= 22.25 && judgeTiming >= -22.25) { Debug.Log($"Perfect: {judgeTiming}ms"); Perfect?.Invoke(); }
                    else if (judgeTiming <= 40 && judgeTiming >= -40) { Debug.Log($"Great: {judgeTiming}ms"); Great?.Invoke(); }
                    else { Debug.Log($"Good: {judgeTiming}ms"); Good?.Invoke(); }
                    
                    note.Delete("K");
                    break;
                }
            }

            var longNotesCopy = new List<LongNote>(NoteManager.instance.KLongNotes);
            foreach (var ln in longNotesCopy)
            {
                if (ln.state != LongNoteState.None) continue;
                float judgeTiming = (currentPlayTime - ln.previousExpectedTime) * 1000f;
                if (Mathf.Abs(judgeTiming) <= 70)
                {
                    ln.OnStartPress();
                    Good?.Invoke();
                    break;
                }
            }
        }
        if (Input.GetKey("k"))
        {
            var lnCopy = new List<LongNote>(NoteManager.instance.KLongNotes);
            foreach (var ln in lnCopy)
            {
                if (ln.state == LongNoteState.Holding)
                {
                    float endTiming = (currentPlayTime - ln.expectedTime) * 1000f;
                    if (endTiming >= 0) ln.Finish("K");
                }
            }
        }
        if (Input.GetKeyUp("k"))
        {
            var lnCopy = new List<LongNote>(NoteManager.instance.KLongNotes);
            foreach (var ln in lnCopy)
            {
                if (ln.state == LongNoteState.Holding)
                {
                    float judgeTiming = (currentPlayTime - ln.expectedTime) * 1000f;
                    if (judgeTiming < 0) { ln.OnReleaseEarly(); Miss?.Invoke(); }
                    else { ln.Finish(ln.railStr); Perfect?.Invoke(); }
                }
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
