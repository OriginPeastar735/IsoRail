using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EditorManager : MonoBehaviour
{
    public GameObject BeatLinePrefab;
    public Transform BeatLinesParent;
    public List<BeatLine> BeatLines = new List<BeatLine>();
    public static EditorManager instance;
    public TMP_InputField BeatInput;
    [SerializeField] private TextMeshProUGUI currrentBarText;
    [SerializeField] private TextMeshProUGUI currentBeatText;
    [SerializeField] private TextMeshProUGUI nextBarText;
    public int currentNote = 0;

    public int currentBar = 0;
    public int nextBar = 1;
    public int currentBeat = 4;

    public int stateX => CursorPreview.instance.stateX;
    public int stateY => CursorPreview.instance.stateY;

    


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

        for (int i = 0; i < currentBeat; i++)
        {
            GameObject obj = Instantiate(BeatLinePrefab, BeatLinesParent);
            BeatLine beatLine = obj.GetComponent<BeatLine>();
            beatLine.Init(currentBeat, i);
            BeatLines.Add(beatLine);
        }
    }

    // Update is called once per frame
    void Update()
    {
        currrentBarText.text = currentBar.ToString();
        currentBeatText.text = currentBeat.ToString();
        nextBarText.text = nextBar.ToString();

        if (stateX != -1 && stateY != -1 && Input.GetMouseButtonDown(0))
        {
            AddNotes();
        }
    }

    public float CulcBar()
    {
        return (1 * (stateY/currentBeat)) + currentBar;
    }

    public void AddNotes()
    {
        switch (currentNote)
        {
            case 0:
            EditorNoteManager.instance.AddNote
            (ConvertStateX(), CulcBar());
            break;
            case 1:
            EditorNoteManager.instance.AddIsoNote
            (ConvertStateX(), CulcBar());
            break;
            case 2:
            EditorNoteManager.instance.AddLongNote
            (ConvertStateX(), CulcBar());
            break;
        }
    }

    public string ConvertStateX()
    {
        switch(stateX)
        {
            case 1:
            if(currentNote == 1)return "S";
            else return "D";
            case 2:
            return "F";
            case 3:
            return "J";
            case 4:
            if(currentNote == 1)return "L";
            else return "K";
            default:
            return "D";
        }
    }

    public void NextBar()
    {
        currentBar++;
        nextBar++;
    }

    public void PreviousBar()
    {
        currentBar--;
        nextBar--;
    }

    public void RemoveBeatLine(BeatLine beatLine)
    {
        BeatLines.Remove(beatLine);
    }

    public void OnSubmit()
    {
        currentBeat = int.TryParse(BeatInput.text, out int result) ? result : currentBeat;
        Debug.Log(currentBeat);
        for (int i = BeatLines.Count - 1; i>=0; i--)
        {
           BeatLines[i].Delete();
        }
        for (int i = 0; i < currentBeat; i++)
        {
            GameObject obj = Instantiate(BeatLinePrefab, BeatLinesParent);
            BeatLine beatLine = obj.GetComponent<BeatLine>();
            beatLine.Init(currentBeat, i);
            BeatLines.Add(beatLine);
        }
        currentBeatText.text = currentBeat.ToString();
    }
}
