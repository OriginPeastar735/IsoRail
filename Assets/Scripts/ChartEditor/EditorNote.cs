using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorNote : MonoBehaviour
{
    public float noteBar;

    public string type;
    public string railStr;

    private SpriteRenderer rend;
    private float alfa = 0;
    private bool isHover = false;
    private bool isDestroyed = false;

    void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        
    }

    void Update()
    {
        if (isHover && Input.GetMouseButtonDown(1))
        {
            EditorNoteManager.instance.RemoveNote(this, railStr);
            Destroy(gameObject);
        }
    }

    public void Init(float noteBar, string railStr)
    {
        this.noteBar = noteBar;
        this.railStr = railStr;
        this.type = "tap";
        isHover = true;
        if (rend != null) {
        rend.color = new Color(1f, 1f, 1f, 0.6f);
    }


        transform.localPosition = new Vector3(
            transform.localPosition.x,
            400 * (noteBar - EditorManager.instance.currentBar),
            transform.localPosition.z
        );
    }

    public void UpdateVisibility(int currentBar)
    {
        bool visible = Mathf.FloorToInt(noteBar) == currentBar;
        if(!visible) isHover = false; 
        gameObject.SetActive(visible);
    }

    void OnMouseOver()
    {
        isHover = true;
        rend.color = new Color(1f, 1f, 1f, 0.6f);
    }

    void OnMouseExit()
    {
        isHover = false;
        rend.color = new Color(1f, 1f, 1f, 1f);
    }
}
