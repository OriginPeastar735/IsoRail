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
        for(int i = -5; i < 5; i++)
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
        double a = 610f;
        double b = 1220f;
        double x = 960 + a * Math.Cos(Math.PI + ((Math.PI / 10)*i));
        //double y = -b * Math.Sin((Math.PI/10) * i);
        double y = -180 * i;
        titleCard.Init(x, y);
    }
}
