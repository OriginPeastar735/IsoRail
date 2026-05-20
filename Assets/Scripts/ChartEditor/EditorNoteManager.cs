using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;

public class EditorNoteManager : MonoBehaviour
{
    public static EditorNoteManager instance;

    public Transform DRailEmpty;
    public Transform FRailEmpty;
    public Transform JRailEmpty;
    public Transform KRailEmpty;

    public GameObject EditorNotePrefab;
    public GameObject EditorIsoNotePrefab;
    public GameObject EditorLongNotePrefab;



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

    public List<EditorIsoNote> SNotes = new List<EditorIsoNote>();
    public List<EditorNote> DNotes = new List<EditorNote>();
    public List<EditorNote> FNotes = new List<EditorNote>();
    public List<EditorNote> JNotes = new List<EditorNote>();
    public List<EditorNote> KNotes = new List<EditorNote>();
    public List<EditorIsoNote> LNotes = new List<EditorIsoNote>();
    public List<EditorLong> DLongNotes = new List<EditorLong>();
    public List<EditorLong> FLongNotes = new List<EditorLong>();
    public List<EditorLong> JLongNotes = new List<EditorLong>();
    public List<EditorLong> KLongNotes = new List<EditorLong>();

    public int currentBar => EditorManager.instance.currentBar;

    public bool placedStartLongNote = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddNote(string railStr, float bar)
    {
        GameObject obj = Instantiate(
            EditorNotePrefab,
            SelectTransform(railStr));
        EditorNote note = obj.GetComponent<EditorNote>();
        note.Init(bar, railStr);
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
    }

    public void AddIsoNote(string railStr, float bar)
    {
        GameObject obj = Instantiate(
            EditorIsoNotePrefab,
            SelectTransform(railStr));
        EditorIsoNote note = obj.GetComponent<EditorIsoNote>();
        note.Init(bar, railStr);
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
    }

    public void AddLongNote(string railStr, float bar)
    {
        if(!placedStartLongNote)
        {
            placedStartLongNote = true;
        }
        else
        {
            
        }
    }

    public void RemoveNote(EditorNote note, string railStr)
    {
        switch (railStr)
        {
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
            default:
                break;
        }
    }

    public void RemoveIsoNote(EditorIsoNote note, string railStr)
    {
        switch (railStr)
        {
            case "S":
                SNotes.Remove(note);
                break;
            case "L":
                LNotes.Remove(note);
                break;
            default:
                break;
        }
    }

    public void UpdateVisibleNotes()
    {
        foreach (var note in SNotes) if(note != null) note.UpdateVisibility(currentBar);
        foreach (var note in DNotes) if(note != null) note.UpdateVisibility(currentBar);
        foreach (var note in FNotes) if(note != null) note.UpdateVisibility(currentBar);
        foreach (var note in JNotes) if(note != null) note.UpdateVisibility(currentBar);
        foreach (var note in KNotes) if(note != null) note.UpdateVisibility(currentBar);
        foreach (var note in LNotes) if(note != null) note.UpdateVisibility(currentBar);
    }

    public Transform SelectTransform(string str)
    {
        switch (str)
        {
            case "S":
                return DRailEmpty;
            case "D":
                return DRailEmpty;
            case "F":
                return FRailEmpty;
            case "J":
                return JRailEmpty;
            case "K":
                return KRailEmpty;
            case "L":
                return KRailEmpty;
            default:
                return DRailEmpty;

        }
    }
}
