using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserPoint : MonoBehaviour
{
    [SerializeField] private GameObject pointerObj;
    Ray ray;
    RaycastHit hit;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag.Contains("ground"))
                {
                    Instantiate(pointerObj, new Vector3(hit.point.x, 0.09f, hit.point.z), Quaternion.identity);
                }
            }

        }
    }
}
