using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Debug;

public class FindCabs : MonoBehaviour
{
    public GameObject[] cabsArray;
    public int desiredCab;
    void Start()
    {
        cabsArray = GameObject.FindGameObjectsWithTag("cab");
    }

    public void FindCab()
    {
        int i = 0;
        foreach (GameObject go in cabsArray)
        {
            int cab = go.GetComponent<CabData>().cabNumber;
            if (cab == desiredCab)
            {
                Log($"Кабинет номер {desiredCab} найден. {go.name}");
                break;
            }
            else
            {
                if(i == cabsArray.Length-1)
                {
                    Log($"Кабинет номер {desiredCab} не найден.");
                }
            }
            i++;
        }
    }
}
