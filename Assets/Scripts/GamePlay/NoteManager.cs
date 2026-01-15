using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class NoteManager : MonoBehaviour
{
    public static NoteManager instance;
    public GameObject NotePrefab;//Unity上でNoteプレハブを設定
    public GameObject IsoNotePrefab;

    public Transform DRailBase;
    public Transform FRailBase;
    public Transform JRailBase;
    public Transform KRailBase;

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
        public List<NoteData> SNotes;
        public List<NoteData> DNotes;
        public List<NoteData> FNotes;
        public List<NoteData> JNotes;
        public List<NoteData> KNotes;
        public List<NoteData> LNotes;
    }

    public List<Note> Notes = new List<Note>();
    public List<Note> SNotes = new List<Note>();
    public List<Note> DNotes = new List<Note>();
    public List<Note> FNotes = new List<Note>();
    public List<Note> JNotes = new List<Note>();
    public List<Note> KNotes = new List<Note>();
    public List<Note> LNotes = new List<Note>();


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

        foreach (var note in notesData.SNotes)
        {
            CreateIsoNote(note.bar, "S", DRailBase);
        }
        foreach (var note in notesData.DNotes)
        {
            CreateNote(note.bar, "D", DRailBase);
        }
        foreach (var note in notesData.FNotes)
        {
            CreateNote(note.bar, "F", FRailBase);
        }
        foreach (var note in notesData.JNotes)
        {
            CreateNote(note.bar, "J", JRailBase);
        }
        foreach (var note in notesData.KNotes)
        {
            CreateNote(note.bar, "K", KRailBase);
        }
        foreach (var note in notesData.LNotes)
        {
            CreateIsoNote(note.bar, "L", KRailBase);
        }
    }
    private void CreateNote(float bar, string railStr, Transform rail)
    {
        GameObject obj = Instantiate(NotePrefab, rail);
        Note note = obj.GetComponent<Note>();
        note.scrollSpeed = scrollSpeed;

        float expectedTime = startTime + bar * barMillis;//各ノーツの理想タイミング

        note.Init(bar, expectedTime);
        //Debug.Log($"{rail.name} worldX={rail.position.x}");


        switch (railStr)
        {
            case "D":
                
                DNotes.Add(note);
                break;
            case "F":
                
                FNotes.Add(note);
                break;
            case "J":
                
                JNotes.Add(note);
                break;
            case "K":
                
                KNotes.Add(note);
                break;
            default:
                break;
        }
        Notes.Add(note);
    }

    private void CreateIsoNote(float bar, string railStr, Transform rail)
    {
        GameObject obj = Instantiate(IsoNotePrefab, rail);
        Note note = obj.GetComponent<Note>();
        note.scrollSpeed = scrollSpeed;

        float expectedTime = startTime + bar * barMillis;//各ノーツの理想タイミング

        note.Init(bar, expectedTime);
        //Debug.Log($"{rail.name} worldX={rail.position.x}");


        switch (railStr)
        {
            case "S":
                SNotes.Add(note);
                break;
            case "L":
                LNotes.Add(note);
                break;
            default:
                break;
        }
        Notes.Add(note);
    }





    // Update is called once per frame
    void Update()
    {
        float currentTime = MusicManager.instance.CurrentPlayTime;
        float presentBar = (currentTime - startTime) / barMillis;//startTimeを入れているのはスタート演出での帳尻合わせ

        foreach (var note in SNotes)
        {
            if (note != null) note.UpdatePosition(presentBar);
        }
        foreach (var note in DNotes)
        {
            if (note != null) note.UpdatePosition(presentBar);
        }
        foreach (var note in FNotes)
        {
            if (note != null) note.UpdatePosition(presentBar);
        }
        foreach (var note in JNotes)
        {
            if (note != null) note.UpdatePosition(presentBar);
        }
        foreach (var note in KNotes)
        {
            if (note != null) note.UpdatePosition(presentBar);
        }
        foreach (var note in LNotes)
        {
            if (note != null) note.UpdatePosition(presentBar);
        }
    }

    public void RemoveNote(Note note, string lane)
    {
        switch (lane)
        {
            case "S":
                SNotes.Remove(note);
                break;
            case "D":
                DNotes.Remove(note);
                break;
            case "F":
                FNotes.Remove(note);
                break;
            case "J":
                JNotes.Remove(note);
                break;
            case "K":
                KNotes.Remove(note);
                break;
            case "L":
                LNotes.Remove(note);
                break;
            default:
                break;
        }
    }
}
