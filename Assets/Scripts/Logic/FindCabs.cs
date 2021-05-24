using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Debug;

public class FindCabs : MonoBehaviour
{
    public GameObject[] cabsArray;
    public int desiredCab;
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject tmp;
    void Start()
    {
        cabsArray = GameObject.FindGameObjectsWithTag("cab");
    }

    public void FindCab()
    {
        int i = 0;
        DeleteDoor();
        foreach (GameObject go in cabsArray)
        {
            int cab = go.GetComponent<CabData>().cabNumber;
            if (cab == desiredCab)
            {
                CreateDoor(go);
                Log($"Кабинет номер {desiredCab} найден. {go.name}");
                break;
            }
            else
            {
                if (i == cabsArray.Length - 1)
                {
                    Log($"Кабинет номер {desiredCab} не найден.");
                }
            }
            i++;
        }
    }
    void CreateDoor(GameObject go)
    {
        tmp = go;
        Instantiate(door, tmp.transform);
    }
    void DeleteDoor()
    {
        if (tmp != null)
        {
            foreach (Transform child in tmp.transform)
            {
                Destroy(child.gameObject);
            }
            tmp = null;
        }
    }
}
