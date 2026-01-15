using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class railfade : MonoBehaviour
{
    [SerializeField] private float Speed = 3;
    [SerializeField] private int num = 0;
    private Renderer rend;
    private float alfa = 0;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (!(rend.material.color.a <= 0)) {
            rend.material.color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, alfa);
        }
        if (KeyMap.ContainsKey(num) && Input.GetKeyDown(KeyMap[num])) {
            colorChange();
        }
        alfa -= Speed * Time.deltaTime;
    }

    void colorChange() {
        alfa = 0.3f;
        rend.material.color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, alfa);
    }
}
