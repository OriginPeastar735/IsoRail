using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatLine : MonoBehaviour
{

    public void Init(float beats, float count)
    {
        Vector3 pos = transform.localPosition;
        pos.y = 400 * (count / beats);
        transform.localPosition = pos;//transform.positonはworld基準で座標を指定する。localPositionにすれば親基準の座標を指定できる。
    }

    public void Delete()
    {
        EditorManager.instance.RemoveBeatLine(this);//自身をListから削除
        Destroy(gameObject);
    }
}
