using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EditorManager : MonoBehaviour
{
    public static EditorManager instance;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        currrentBarText.text = currentBar.ToString();
        currentBeatText.text = currentBeat.ToString();
        nextBarText.text = nextBar.ToString();
    }

    public void PlusBeat()
    {
        currentBeat++;
    }

    public void MinusBeat()
    {
        currentBeat--;
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

}
