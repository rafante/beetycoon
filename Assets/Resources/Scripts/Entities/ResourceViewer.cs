using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.UI.Text))]
public class ResourceViewer : MonoBehaviour
{
    private UnityEngine.UI.Text label;
    public ResourceType type;

    // Use this for initialization
    void Awake()
    {
        label = GetComponentInChildren<UnityEngine.UI.Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (type)
        {
            case ResourceType.HONEY:
                label.text = BeeHive.main.getCurHoney().ToString();
                break;
            case ResourceType.WAX:
                label.text = BeeHive.main.getCurWax().ToString();
                break;
            case ResourceType.PROPOLIS:
                label.text = BeeHive.main.getCurPropolis().ToString();
                break;
            case ResourceType.ROYALJAM:
                label.text = BeeHive.main.getCurRoyalJam().ToString();
                break;
            case ResourceType.POLLEN:
                label.text = BeeHive.main.getCurPollen().ToString();
                break;
            case ResourceType.NECTAR:
                label.text = BeeHive.main.getCurNectar().ToString();
                break;
        }
    }
}

public enum ResourceType
{
    HONEY,
    WAX,
    PROPOLIS,
    ROYALJAM,
    POLLEN,
    NECTAR
}