using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TitleCardManager : MonoBehaviour
{
    public static TitleCardManager instance;
    public GameObject TitleCardPrefab;
    public List<TitleCard> titleCards;
    // Start is called before the first frame update

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
    void Start()
    {
        for(int i = 0; i < 5; i++)
        {
            CreateTitleCard(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateTitleCard(int i)
    {
        GameObject obj = Instantiate(TitleCardPrefab, this.transform);
        TitleCard titleCard = obj.GetComponent<TitleCard>();
        double r = 560f;
        double x = 960 + r * Math.Cos(Math.PI + ((Math.PI / 6)*i));
        double y = -r * Math.Sin((Math.PI/6) * i);
        titleCard.Init(x, y);
    }
}
