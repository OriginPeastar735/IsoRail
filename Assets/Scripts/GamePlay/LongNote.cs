using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongNote : MonoBehaviour
{
    public float startBar;//ノーツの小節位置
    public float scrollSpeed = 1000f;//スクロール定数
    public float previousExpectedTime;
    public float expectedTime; //予定ヒット時間。今は座標0が理想タイミングと仮定してプログラム
    private float presentBar;//楽曲の現在の小節位置
    private float endBar;//ロングノーツ終点
    public bool judged = false;

    [Header("Children Objects")]
    public Transform StartNote;
    public Transform EndNote;
    public Transform HoldNote;

    public void Init(float startBar, float endBar, float previousExpectedTime, float expectedTime)
    {
        this.startBar = startBar;
        this.endBar = endBar;
        this.expectedTime = expectedTime;
        this.previousExpectedTime = previousExpectedTime;

        transform.localPosition = Vector3.zero;//transform.positonはworld基準で座標を指定する。localPositionにすれば親基準の座標を指定できる。
    
    }

    public void UpdatePosition(float presentBar)
    {
        this.presentBar = presentBar;
        float startZ = (presentBar - startBar) * scrollSpeed;
        float endZ = (presentBar - endBar) * scrollSpeed;

        Vector3 local = transform.localPosition;
        local.z = startZ;
        local.y = 0f;
        transform.localPosition = local;

        if(StartNote != null)StartNote.localPosition = new Vector3(0,0,startZ);
        if(EndNote != null)EndNote.localPosition = new Vector3(0,0,endZ);
        if(EndNote != null)
        {
            float length = Mathf.Abs(endZ-startZ);
            HoldNote.localPosition = new Vector3(0f,0f,(startZ+endZ)/2);
            HoldNote.localScale = new Vector3(0.8f,0.1f,length);
        }
        

    }

    /*public void Delete(string lane)
    {
        NoteManager.instance.RemoveNote(this, lane);//自身をListから削除
        Destroy(gameObject);
    }*/
}
