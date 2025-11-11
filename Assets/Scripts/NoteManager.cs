using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class NoteManager : MonoBehaviour
{
    public static NoteManager instance;
    public GameObject NotePrefab;//Unity上でNoteプレハブを設定

    public float bpm;

    public float scrollSpeed = 200f;

    private float startTime;

    private float barMillis;
    public int destroyedNotesCount = 0;

    //ノーツ情報
    [System.Serializable]
    public class NoteData
    {
        public float bar;
        public string type;
    }

    [System.Serializable]
    public class NotesData
    {
        public float bpm;
        public List<NoteData> DNotes;
        public List<NoteData> FNotes;
        public List<NoteData> JNotes;
        public List<NoteData> KNotes;
    }

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

    }

    public void LoadJson(string fileName)
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(fileName);
        NotesData notesData = JsonConvert.DeserializeObject<NotesData>(jsonFile.text);

        bpm = notesData.bpm;
        barMillis = (60f / bpm) * 4f;

        foreach (var note in notesData.DNotes)
        {
            CreateNote(note.bar, "D");
        }
        foreach (var note in notesData.FNotes)
        {
            CreateNote(note.bar, "F");
        }
        foreach (var note in notesData.JNotes)
        {
            CreateNote(note.bar, "J");
        }
        foreach (var note in notesData.KNotes)
        {
            CreateNote(note.bar, "K");
        }
    }
    private void CreateNote(float bar, string lane)
    {
        GameObject obj = Instantiate(NotePrefab);
        Note note = obj.GetComponent<Note>();
        note.scrollSpeed = scrollSpeed;

        float expectedTime = startTime + bar * barMillis;//各ノーツの理想タイミング
        note.Init(bar, expectedTime, lane);

        Notes.Add(note); //Listに追加
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

    public void RemoveNote(Note note)
    {
        if (Notes.Contains(note))
        {
            Notes.Remove(note);
        }
    }

}
