using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.Debug;

public class TestWay : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject endPoint;
    [Header("Префабы маршрута")]
    public GameObject line;
    public GameObject point;
    public float distance = 1;
    public float height = 0.01f;

    private List<GameObject> points;
    private Vector3 agentPoint;
    private Vector3 lastPoint;
    private List<GameObject> lines;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
           
            Way();
        }
    }
    void Way()
    {
        points = new List<GameObject>();
        lines = new List<GameObject>();
        lastPoint = agent.transform.position + Vector3.forward * 100f; // чтобы создать точку в текущей позиции
        agent.SetDestination(endPoint.transform.position);
        ClearArray();

        for (int j = 0; j < agent.path.corners.Length; j++)
        {
            if (IsDistance(agent.path.corners[j]))
            {
                GameObject p = Instantiate(point) as GameObject;
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

                GameObject p = Instantiate(line) as GameObject;
                p.transform.position = center - Vector3.up * height;
                p.transform.rotation = Quaternion.FromToRotation(Vector3.right, vec.normalized); // разворот по вектору
                p.transform.localScale = new Vector3(dis, 1, 1); // растягиваем по Х
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

    
}


