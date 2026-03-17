using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class railfade : MonoBehaviour
{
    [SerializeField] private float Speed = 3;
    [SerializeField] private int num = 0;
    private Renderer rend;
    private float alfa = 0;
    private bool isPressed = false;
    
    Dictionary<int, KeyCode> KeyMap = new Dictionary<int, KeyCode>() {
        {1,KeyCode.D },
        {2,KeyCode.F },
        {3,KeyCode.J },
        {4,KeyCode.K }
    };
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();

        Color c = rend.material.color;
        rend.material.color = new Color(c.r, c.g, c.b, 0);
    }
    

    // Update is called once per frame
    void Update()
    {
        isPressed = false;
        if (!(rend.material.color.a <= 0)) {
            rend.material.color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, alfa);
        }
        if (KeyMap.ContainsKey(num) && Input.GetKeyDown(KeyMap[num])) {
            colorChange();
        }
        if (KeyMap.ContainsKey(num) && Input.GetKey(KeyMap[num])) {
            colorChange();
            isPressed = true;
        }
        if(!isPressed)alfa -= Speed * Time.deltaTime;
    }

    void colorChange() {
        alfa = 0.3f;
        rend.material.color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, alfa);
    }
}
