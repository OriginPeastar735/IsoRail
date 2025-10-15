using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public static Note instance;
    public float noteBar;//ノーツの小節位置
    public float scrollSpeed = 1000f;//スクロール定数
    public float expectedTime; //予定ヒット時間。今は座標0が理想タイミングと仮定してプログラム
    private float presentBar;//楽曲の現在の小節位置
    public bool judged = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Init(float noteBar, float expectedTime)
    {
        this.noteBar = noteBar;
        this.expectedTime = expectedTime;
    }

    public void UpdatePosition(float presentBar)
    {
        this.presentBar = presentBar;
        float z = (presentBar - noteBar) * scrollSpeed;

        transform.position = new Vector3(0f, 0f, z);//x,z座標は後から変更
    }

    public void Delete()
    {
        NoteManager.instance.RemoveNote(this);//自身をListから削除
        Destroy(gameObject);
    }
}
