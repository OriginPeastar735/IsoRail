using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public static NoteManager instance;
    public GameObject NotePrefab;//Unity上でNoteプレハブを設定

    public float bpm;

    public float scrollSpeed = 200f;

    private float startTime;

    private float barMillis;
    public int destroyedNotesCount = 0;

    public List<Note> Notes = new List<Note>();

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
        startTime = 0f;//後で変更
        bpm = 158;
        barMillis = (60f / bpm) * 4f;//1小節あたりの時間(ms)

        float[] noteBars = { 1f, 1.5f, 2f };

        for (int i = 0; i < noteBars.Length; i++)
        {
            GameObject obj = Instantiate(NotePrefab);
            Note note = obj.GetComponent<Note>();
            note.scrollSpeed = scrollSpeed;
            float expectedTime = startTime + noteBars[i] * barMillis;//各ノーツの理想タイミング
            note.Init(noteBars[i], expectedTime);
            Notes.Add(note); //Listに追加
        }
    }

    // Update is called once per frame
    void Update()
    {
        float currentTime = MusicManager.instance.CurrentPlayTime;
        float presentBar = (currentTime - startTime) / barMillis;//startTimeを入れているのはスタート演出での帳尻合わせ

        foreach (var note in Notes)
        {
            if (note != null) note.UpdatePosition(presentBar);
        }
    }

    public void RemoveNote(Note note) {
        if (Notes.Contains(note))
        {
            Notes.Remove(note);
        }
    }

}
