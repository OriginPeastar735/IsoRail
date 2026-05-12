using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [SerializeField] private int num = 0;
    private SpriteRenderer rend;
    private float alfa = 0;
    private bool isHover = false;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }
    

    // Update is called once per frame
    void Update()
    {
        if (EditorManager.instance.currentNote == num)
        {
            rend.color = new Color(1f,1f,1f,0.5f);
        }
        else
        {
            if (isHover)
            {
                rend.color  =   new Color(0.5f,0.5f,0.5f,0.3f);
            }
            else
            {
                rend.color  =   new Color(0.5f,0.5f,0.5f,0f);
            }
        }

        if (isHover && Input.GetMouseButtonDown(0))
        {
            EditorManager.instance.currentNote = num;
        }
    }

    void OnMouseEnter()
    {
        isHover = true;
    }

    void OnMouseExit()
    {
        isHover = false;
    }
}
