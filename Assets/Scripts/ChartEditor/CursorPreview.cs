using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

public class CursorPreview : MonoBehaviour
{
    public static CursorPreview instance;
    [SerializeField] private Canvas canvas;
    private SpriteRenderer rend;
    private RectTransform canvasRectTransform;
    public int rail = 4;
    Vector3 mousePos;
    Vector3 LeftEnd;//レールの左下端
    public int xRailDiff = 70;
    public int nextBarDiff = 400;
    public GameObject LeftEndEmpty;
    public Camera mainCamera;
    public GameObject PreviewNote;
    public int currentBar = 0;
    public int currentBeat = 4;
    public int barHeight = 400;

    public bool inX;
    public bool inY;

    public int stateX;
    public int stateY;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        canvasRectTransform = canvas.GetComponent<RectTransform>();
        LeftEnd = LeftEndEmpty.transform.localPosition;
        rend = PreviewNote.GetComponent<SpriteRenderer>();
        rend.color = new Color(0.5f, 0.5f, 0.5f, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        currentBar = EditorManager.instance.currentBar;
        currentBeat = EditorManager.instance.currentBeat;
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRectTransform,
            Input.mousePosition,
            canvas.worldCamera,
            out localPoint
        );
        CursorState(localPoint);

        Debug.Log(nextBarDiff);

        if(inX && inY)
        {
            PreviewNote.SetActive(true);
        }
        else PreviewNote.SetActive(false);
    }

    public void CursorState(Vector2 localPoint)
    {
        inX = false;
        inY = false;
        for (int i = 1; i <= 4; i++)
        {
            if (localPoint.x >= LeftEnd.x + 70 * (i - 1)
            && localPoint.x <= LeftEnd.x + 70 * i)
            {
                stateX = i;
                inX = true;
                for (int j = 0; j < currentBeat; j++)
                {
                    float searchY = LeftEnd.y + (nextBarDiff * (j / (float)currentBeat));
                    if (localPoint.y >= searchY - 10
                     && localPoint.y <= searchY + 10)
                    {
                        stateY = j;
                        inY = true;
                        Vector3 tmp = PreviewNote.transform.localPosition;
                        tmp.x = LeftEnd.x + 70 * stateX - 35;
                        tmp.y = searchY;
                        PreviewNote.transform.localPosition = tmp;
                        break;
                    }
                }
                break;
            }
        }
        stateX = inX ? stateX : -1;
        stateY = inY ? stateY : -1;
    }

    public void CreatePreview()
    {
        if (inX && inY)
        {

        }
    }



    void UpdatePreview()
    {

    }
}
