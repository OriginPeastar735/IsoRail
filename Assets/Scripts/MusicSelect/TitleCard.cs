using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCard : MonoBehaviour
{
    public double x;
    public double y;
    public void Init(double x, double y)
    {
        transform.localPosition = new Vector2((float)x, (float)y);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
