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
    public GameObject LongNotePrefab;

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
        public List<NoteData> DLongNotes;
        public List<NoteData> FLongNotes;
        public List<NoteData> JLongNotes;
        public List<NoteData> KLongNotes;
    }

    public List<Note> Notes = new List<Note>();
    public List<LongNote> LongNotes = new List<LongNote>();
    public List<Note> SNotes = new List<Note>();
    public List<Note> DNotes = new List<Note>();
    public List<Note> FNotes = new List<Note>();
    public List<Note> JNotes = new List<Note>();
    public List<Note> KNotes = new List<Note>();
    public List<Note> LNotes = new List<Note>();
    public List<LongNote> DLongNotes = new List<LongNote>();
    public List<LongNote> FLongNotes = new List<LongNote>();
    public List<LongNote> JLongNotes = new List<LongNote>();
    public List<LongNote> KLongNotes = new List<LongNote>();

    private float[] previousExpectedTime = new float[4];//ロングノーツ描画のための一時変数
    private float[] previousNoteBar = new float[4];//ロングノーツ描画のための一時変数

    const int D = 0;
    const int F = 1;
    const int J = 2;
    const int K = 3;

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
            CreateIsoNote(note.bar, "S", DRailBase, note.type);
        }
        foreach (var note in notesData.DNotes)
        {
            CreateNote(note.bar, "D", DRailBase, note.type);
        }
        foreach (var note in notesData.FNotes)
        {
            CreateNote(note.bar, "F", FRailBase, note.type);
        }
        foreach (var note in notesData.JNotes)
        {
            CreateNote(note.bar, "J", JRailBase, note.type);
        }
        foreach (var note in notesData.KNotes)
        {
            CreateNote(note.bar, "K", KRailBase, note.type);
        }
        foreach (var note in notesData.LNotes)
        {
            CreateIsoNote(note.bar, "L", KRailBase, note.type);
        }
        foreach (var note in notesData.DLongNotes)
        {
            float expectedTime = startTime + note.bar * barMillis;
            if (note.type == "s")
            {
                previousExpectedTime[D] = expectedTime;
                previousNoteBar[D] = note.bar;
            }
            else if (note.type == "e")
            {
                CreateLongNote(previousNoteBar[D], note.bar, "D", DRailBase, previousExpectedTime[D], expectedTime);
            }
        }
        foreach (var note in notesData.FLongNotes)
        {
            float expectedTime = startTime + note.bar * barMillis;
            if (note.type == "s")
            {
                previousExpectedTime[F] = expectedTime;
                previousNoteBar[F] = note.bar;
            }
            else if (note.type == "e")
            {
                CreateLongNote(previousNoteBar[F], note.bar, "F", FRailBase, previousExpectedTime[F], expectedTime);
            }
        }
        foreach (var note in notesData.JLongNotes)
        {
            float expectedTime = startTime + note.bar * barMillis;
            if (note.type == "s")
            {
                previousExpectedTime[J] = expectedTime;
                previousNoteBar[J] = note.bar;
            }
            else if (note.type == "e")
            {
                CreateLongNote(previousNoteBar[J], note.bar, "J", JRailBase, previousExpectedTime[J], expectedTime);
            }
        }
        foreach (var note in notesData.KLongNotes)
        {
            float expectedTime = startTime + note.bar * barMillis;
            if (note.type == "s")
            {
                previousExpectedTime[K] = expectedTime;
                previousNoteBar[K] = note.bar;
            }
            else if (note.type == "e")
            {
                CreateLongNote(previousNoteBar[K], note.bar, "K", KRailBase, previousExpectedTime[K], expectedTime);
            }
        }
    }


    private void CreateNote(float bar, string railStr, Transform rail, string type)
    {
        float expectedTime = startTime + bar * barMillis;//各ノーツの理想タイミング

        //ロングノーツの終点の時、始点のときのexpectedTimeを持ってくれば描画できるかも

        GameObject obj = Instantiate(NotePrefab, rail);//railを親、objを子として生成
        Note note = obj.GetComponent<Note>();
        note.scrollSpeed = scrollSpeed;
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

    private void CreateLongNote(float startBar, float endBar, string railStr, Transform rail, float longStartTime, float longEndTime)
    {
        GameObject obj = Instantiate(LongNotePrefab, rail);//あとでプレハブ作ってね
        LongNote longNote = obj.GetComponent<LongNote>();
        longNote.scrollSpeed = scrollSpeed;

        longNote.Init(startBar, endBar, longStartTime, longEndTime, railStr);

        switch (railStr)
        {
            case "D":

                DLongNotes.Add(longNote);
                break;
            case "F":

                FLongNotes.Add(longNote);
                break;
            case "J":

                JLongNotes.Add(longNote);
                break;
            case "K":

                KLongNotes.Add(longNote);
                break;
            default:
                break;
        }
        LongNotes.Add(longNote);
    }

    private void CreateIsoNote(float bar, string railStr, Transform rail, string type)
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
    void Update()
    {
        float currentTime = MusicManager.instance.CurrentPlayTime;
        float presentBar = (currentTime - startTime) / barMillis;//startTimeを入れているのはスタート演出での帳尻合わせ

        foreach (var note in new List<Note>(SNotes))
        {
            if (note != null) note.UpdatePosition(presentBar);
        }
        foreach (var note in new List<Note>(DNotes))
        {
            if (note != null) note.UpdatePosition(presentBar);
        }
        foreach (var note in new List<Note>(FNotes))
        {
            if (note != null) note.UpdatePosition(presentBar);
        }
        foreach (var note in new List<Note>(JNotes))
        {
            if (note != null) note.UpdatePosition(presentBar);
        }
        foreach (var note in new List<Note>(KNotes))
        {
            if (note != null) note.UpdatePosition(presentBar);
        }
        foreach (var note in new List<Note>(LNotes))
        {
            if (note != null) note.UpdatePosition(presentBar);
        }
        foreach (var note in new List<LongNote>(DLongNotes))
        {
            if (note != null) note.UpdatePosition(presentBar);
        }
        foreach (var note in new List<LongNote>(FLongNotes))
        {
            if (note != null) note.UpdatePosition(presentBar);
        }
        foreach (var note in new List<LongNote>(JLongNotes))
        {
            if (note != null) note.UpdatePosition(presentBar);
        }
        foreach (var note in new List<LongNote>(KLongNotes))
        {
            if (note != null) note.UpdatePosition(presentBar);
        }
    }

    public void RemoveNote(Note note, string lane)
    {
        switch (lane)
        {
            case "s":
                SNotes.Remove(note);
                break;
            case "d":
                DNotes.Remove(note);
                break;
            case "f":
                FNotes.Remove(note);
                break;
            case "j":
                JNotes.Remove(note);
                break;
            case "k":
                KNotes.Remove(note);
                break;
            case "l":
                LNotes.Remove(note);
                break;
            default:
                break;
        }
    }

    public void RemoveLongNote(LongNote longNote, string lane)
    {
        switch (lane)
        {
            case "d":
                DLongNotes.Remove(longNote);
                break;
            case "f":
                FLongNotes.Remove(longNote);
                break;
            case "j":
                JLongNotes.Remove(longNote);
                break;
            case "k":
                KLongNotes.Remove(longNote);
                break;
            default:
                break;
        }
    }
}
