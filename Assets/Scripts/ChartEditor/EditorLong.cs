using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorLong : MonoBehaviour
{

    public float startBar;//ノーツの小節位置
    private float endBar;//ロングノーツ終点
    public string railStr;
    public bool isHover = false;
    public SpriteRenderer startNoteObj;
    public SpriteRenderer endNoteObj;
    public SpriteRenderer holdNoteObj;

    void Awake()
    {
        if (StartNote != null)
        {
            startNoteObj = StartNote.GetComponent<SpriteRenderer>();
        }
        if (EndNote != null)
        {
            endNoteObj = EndNote.GetComponent<SpriteRenderer>();
        }
        if (HoldNote != null)
        {
            holdNoteObj = HoldNote.GetComponent<SpriteRenderer>();
        }
    }



    void Update()
    {
        if (isHover && Input.GetMouseButtonDown(1))
        {
            EditorNoteManager.instance.RemoveLongNote(this, railStr);
            Destroy(gameObject);
        }
    }


    [Header("Children Objects")]
    public GameObject StartNote;
    public GameObject EndNote;
    public GameObject HoldNote;

    [Header("Children Transforms")]
    public Transform StartNoteTransform;
    public Transform EndNoteTransform;
    public Transform HoldNoteTransform;

    public void Init(float startBar, float endBar, string railStr)
    {
        this.startBar = startBar;
        this.endBar = endBar;
        this.railStr = railStr;
        isHover = true;
        startNoteObj.color = new Color(0.4f, 1f, 1f, 0.6f);
        endNoteObj.color = new Color(0.4f, 1f, 1f, 0.6f);
        holdNoteObj.color = new Color(0.4f, 1f, 1f, 0.3f);

        transform.localPosition = new Vector3(
            transform.localPosition.x,
            //400 * (startBar + (endBar - startBar) / 2 - EditorManager.instance.currentBar),
            400 * (startBar - EditorManager.instance.currentBar),
            transform.localPosition.z
        );

        StartNoteTransform.localPosition = Vector3.zero;

        EndNoteTransform.localPosition =
        new Vector3(EndNoteTransform.localPosition.x,
        400 * (endBar - startBar - EditorManager.instance.currentBar),
        EndNoteTransform.localPosition.z);



        float length = (endBar - startBar) * 400;
        HoldNoteTransform.localScale = new Vector3(
            HoldNoteTransform.localScale.x,
            length,
            HoldNoteTransform.localScale.z);
        HoldNoteTransform.localPosition = new Vector3(
            HoldNoteTransform.localPosition.x,
            StartNoteTransform.localPosition.y + length / 2,
            HoldNoteTransform.localPosition.z);

        BoxCollider2D col = GetComponent<BoxCollider2D>();
        if (col != null)
        {
            // Colliderのサイズをノーツの長さ（縦）に合わせる
            col.size = new Vector2(75f, length); // 横幅はレーンの幅に合わせて調整(例: 1f)
            col.offset = new Vector2(0f, length / 2); // Pivotが中心ならゼロ
        }
    }

    public void UpdatePosition(float presentBar)
    {
        transform.localPosition = Vector3.zero;
    }

    public void UpdateVisibility(int currentBar)
    {
        bool visible = Mathf.FloorToInt(startBar) == currentBar;
        if (!visible) isHover = false;
        gameObject.SetActive(visible);
    }


    void OnMouseOver()
    {
        isHover = true;
        startNoteObj.color = new Color(0.4f, 1f, 1f, 0.6f);
        endNoteObj.color = new Color(0.4f, 1f, 1f, 0.6f);
        holdNoteObj.color = new Color(0.4f, 1f, 1f, 0.3f);
        Debug.Log("Hovering IsoNote");
    }

    void OnMouseExit()
    {
        isHover = false;
        startNoteObj.color = new Color(0.4f, 1f, 1f, 1f);
        endNoteObj.color = new Color(0.4f, 1f, 1f, 1f);
        holdNoteObj.color = new Color(0.4f, 1f, 1f, 0.5f);
        Debug.Log("exit IsoNote");
    }



    /*public void Delete(string lane)
    {
        NoteManager.instance.RemoveNote(this, lane);//自身をListから削除
        Destroy(gameObject);
    }*/
}