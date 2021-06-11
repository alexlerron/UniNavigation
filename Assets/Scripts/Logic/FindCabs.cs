using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.Debug;

public class FindCabs : MonoBehaviour
{
    public NavMeshAgent agent;
    [SerializeField] private NavMeshAgent tmpAgent;
    [Header("Кабинеты")]
    public int desiredCab;
    public int fromUserCab;
    public GameObject[] cabsArray;
    [Header("Префабы маршрута")]
    public GameObject line;
    public GameObject point;
    public float distance = 1;
    public float height = 0.01f;
    [Header("Кабинет у которого находится пользователь")]
    [SerializeField] private GameObject door1;
    [SerializeField] private GameObject tmp1;
    [Header("Кабинет поиска")]
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject tmp;
    [Header(" ")]
   [SerializeField] private List<GameObject> points;
    private Vector3 agentPoint;
    private Vector3 lastPoint;
    [SerializeField]private List<GameObject> lines;

    void Start()
    {
        cabsArray = GameObject.FindGameObjectsWithTag("cab");
    }

    private void Update()
    {
        try
        {
            agent = tmp1.GetComponentInChildren<NavMeshAgent>();
            if (agentPoint != agent.path.corners[agent.path.corners.Length - 1]) Way(); // рисуем путь если была изменена конечная точка назначения
            agentPoint = agent.path.corners[agent.path.corners.Length - 1]; // запоминаем текущую конечную точку назначения

            if (agent.path.corners.Length == 1 && points.Count > 1) Way(); // рисуем путь, после прибытия в точку назначения
        }
        catch { }
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
            }
            if( cab == fromUserCab)
            {
                CreateUserPoint(go);
                Log($"Кабинет номер {fromUserCab} найден. {go.name}");
            }

            i++;
        }
        Way();
    }
    void CreateDoor(GameObject go)
    {
        tmp = go;
        Instantiate(door, tmp.transform);
    }

    void CreateUserPoint (GameObject go)
    {
        tmp1 = go;
        Instantiate(door1, tmp1.transform);
    }
    void Way()
    {
        ClearArray();
        lastPoint = agent.transform.position + Vector3.forward; // чтобы создать точку в текущей позиции
        agent.SetDestination(tmp.transform.position);
        for (int j = 0; j < agent.path.corners.Length; j++)
        {
            if (IsDistance(agent.path.corners[j]))
            {
                GameObject p = Instantiate(point);
                p.transform.position = agent.path.corners[j] + Vector3.up * height; // создаем точку и корректируем позицию 
                points.Add(p);
            }
        }

        for (int j = 0; j < points.Count; j++)
        {
            if (j + 1 < points.Count)
            {
                Vector3 center = (points[j].transform.position + points[j + 1].transform.position) / 2; // находим центр между точками
                Vector3 vec = points[j].transform.position - points[j + 1].transform.position; // находим вектор от точки А, к точке Б
                float dis = Vector3.Distance(points[j].transform.position, points[j + 1].transform.position); // находим дистанцию между А и Б

                GameObject p = Instantiate(line);
                p.transform.position = center - Vector3.up * height;
                p.transform.rotation = Quaternion.FromToRotation(Vector3.right, vec.normalized); // разворот по вектору
                p.transform.localScale = new Vector3(dis, 0.09f, 0.09f); // растягиваем по Х
                lines.Add(p);
            }
        }
    }
    bool IsDistance(Vector3 distancePoint) 
    {
        bool result = false;
        float dis = Vector3.Distance(lastPoint, distancePoint);
        if (dis > distance) result = true;
        lastPoint = distancePoint;
        return result;
    }

    void ClearArray() 
    {
        foreach (GameObject obj in points)
        {
            Destroy(obj);
        }
        foreach (GameObject obj in lines)
        {
            Destroy(obj);
        }
        lines = new List<GameObject>();
        points = new List<GameObject>();
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
        if (tmp1 != null)
        {
            foreach (Transform child in tmp1.transform)
            {
                Destroy(child.gameObject);
            }
            tmp1 = null;
        }
    }
}
