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
    public GameObject previewPrefab;
    public int currentBar = 0;
    public int currentBeat = 4;
    public int barHeight = 400;

    public int stateX;
    public int stateY;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        canvasRectTransform = canvas.GetComponent<RectTransform>();
        LeftEnd = LeftEndEmpty.transform.localPosition;
        rend = previewPrefab.GetComponent<SpriteRenderer>();
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
        for(int i = 1; i <= 4; i++)
        {
            if(localPoint.x >= LeftEnd.x + 70 * (i-1) 
            && localPoint.x <= LeftEnd.x + 70 * i)
            {
                stateX = i;
                for(int j = 0; j < currentBeat; j++)
                {
                    stateY = j;
                    float searchY = LeftEnd.y + (nextBarDiff * (j / (float)currentBeat));
                    Debug.Log(nextBarDiff);
                    if(localPoint.y >= searchY - 20 
                    && localPoint.y <= searchY + 20)
                    {
                        Vector3 tmp = previewPrefab.transform.localPosition;
                        tmp.x = LeftEnd.x + 70 * i -35;
                        tmp.y = searchY;
                        previewPrefab.transform.localPosition = tmp;
                    }
                    break;
                }
            }
            break;
        }
    }

    void UpdatePreview()
    {
        
    }
}
