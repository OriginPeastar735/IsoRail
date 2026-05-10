using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public GameObject perfectEffectPrefab;
    public GameObject greatEffectPrefab;
    public GameObject goodEffectPrefab;
    public GameObject holdEffectPrefab;

    public static EffectManager instance;

    public static GameObject DHoldEffect;
    public static GameObject FHoldEffect;
    public static GameObject JHoldEffect;
    public static GameObject KHoldEffect;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PerfectEffect(Transform rail)
    {
        GameObject effect = Instantiate(
        perfectEffectPrefab,
        rail.TransformPoint(0,0.002f,0),
        Quaternion.identity,
        rail);
    }

    public void GreatEffect(Transform rail)
    {
        GameObject effect = Instantiate(
        greatEffectPrefab,
        rail.TransformPoint(0,0.002f,0),
        Quaternion.identity,
        rail);
    }

    public void GoodEffect(Transform rail)
    {
        GameObject effect = Instantiate(
        goodEffectPrefab,
        rail.TransformPoint(0,0.002f,0),
        Quaternion.identity,
        rail);
    }

    public void HoldEffect(Transform rail, string key)
    {
        GameObject effect = Instantiate(
        holdEffectPrefab,
        rail.TransformPoint(0,0.002f,0),
        Quaternion.identity,
        rail);
        switch (key)
        {
            case "d":
                DHoldEffect = effect;
                break;
            case "f":
                FHoldEffect = effect;
                break;
            case "j":
                JHoldEffect = effect;
                break;
            case "k":
                KHoldEffect = effect;
                break;
            default:
                break;
        }
    }

}
