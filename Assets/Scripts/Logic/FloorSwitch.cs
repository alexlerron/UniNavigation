using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSwitch : MonoBehaviour
{
    [SerializeField] private GameObject[] floor1;
    [SerializeField] private GameObject[] floor2;

    public void Floor1Switcher ()
    {
        foreach (GameObject go in floor1)
        {
            if (go.layer == 0)
            {
                go.layer = 6;
            }
            else
            {
                go.layer = 0;
            }
        }
    }
    public void Floor2Switcher()
    {
       foreach (GameObject go in floor2)
        {
            if (go.layer == 0)
            {
                go.layer = 6;
            }
            else
            {
                go.layer = 0;
            }
        }
    }
}
