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
