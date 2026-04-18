using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CursorPreview : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    private RectTransform canvasRectTransform;
    public int rail = 4;
    Vector3 mousePos;
    public Transform RelativeEmpty;
    public Camera mainCamera;
    public GameObject previewPrefab;
    public int currentBar = 0;
    public int currentBeat = 4;
    public int barHeight = 400;

    void Start()
    {
        

        canvasRectTransform = canvas.GetComponent<RectTransform>();
        currentBar = EditorManager.instance.currentBar;
        currentBeat = EditorManager.instance.currentBeat;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRectTransform,
            Input.mousePosition,
            canvas.worldCamera,
            out localPoint
        );
        Debug.Log(localPoint);
    }

    void UpdatePreview()
    {
        
    }
}
