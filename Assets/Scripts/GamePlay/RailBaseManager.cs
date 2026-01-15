using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailBaseManager : MonoBehaviour
{
    public GameObject DRailBase;
    public GameObject KRailBase;

    bool DRailMoving;
    bool DRailIsorated;
    float DTime;
    float DStartWaitTime;
    float DEndWaitTime;
    float DEasingTime;

    bool KRailMoving;
    bool KRailIsorated;
    float KTime;
    float KStartWaitTime;
    float KEndWaitTime;
    float KEasingTime;

    
    void Start()
    {
        DRailMoving = false;
        KRailMoving = false;
        DRailIsorated = false;
        KRailIsorated = false;
    }

    void Update()
    {
        if(DRailMoving)DRailMove();
        if(KRailMoving)KRailMove();
    }

    void DRailMove()
    {
        Vector3 pos = DRailBase.transform.position;
        if(0<DTime && DTime <= DEasingTime)
        {
            pos.x = DRailIsorated?QuartOut(DTime, DEasingTime, 3, 4):QuartOut(DTime, DEasingTime, 4, 3);
            DRailBase.transform.position = pos;
        }
        else if(DEasingTime < DTime && DTime <= DEasingTime * 2)
        {
            pos.x = DRailIsorated?4:3;
            DRailBase.transform.position = pos;
            DRailMoving = false;
        }
        DTime += Time.deltaTime;
    }

    void KRailMove()
    {
        Vector3 pos = KRailBase.transform.position;
        if(0<KTime && KTime <= KEasingTime)
        {
            pos.x = KRailIsorated?QuartOut(KTime, KEasingTime, 0, -1):QuartOut(KTime, KEasingTime, -1, 0);
            KRailBase.transform.position = pos;
        }
        else if(KEasingTime < KTime && KTime <= KEasingTime * 2)
        {
            pos.x = KRailIsorated?-1:0;
            KRailBase.transform.position = pos;
            KRailMoving = false;
        }
        KTime += Time.deltaTime;
    }

    

    void DTrigger()
    {
        DTime = 0f;
        DEasingTime = 0.5f;
        DRailMoving = true;
        DRailIsorated = !DRailIsorated;
    }

    void KTrigger()
    {
        KTime = 0f;
        KEasingTime = 0.5f;
        KRailMoving = true;
        KRailIsorated = !KRailIsorated;
    }

    public static float QuartOut(float t, float totaltime, float min, float max)
    {
        max -= min;
        t = t/totaltime -1;
        return -max * (t*t*t*t-1)+min;
    }

    void OnEnable()
    {
        JudgeManager.DRailMove += DTrigger;
        JudgeManager.KRailMove += KTrigger;
    }

    void OnDisable()
    {
        JudgeManager.DRailMove -= DTrigger;
        JudgeManager.KRailMove -= KTrigger;
    }
}
