using UnityEngine;

public class LegendFilter : MonoBehaviour
{
    public CabData[] cabDatas;
    public Material[] cabColors;

    public void LegendCabFilter(int cabType)
    {
        foreach (var cab in cabDatas)
        {
            if (cab.GetComponent<MeshRenderer>() != null)
            {
                if ((int)cab.typeName == cabType)
                {
                    if (cab.isCabActive)
                    {
                        cab.gameObject.GetComponent<MeshRenderer>().enabled = false;
                        cab.isCabActive = false;
                    }
                    else
                    {
                        cab.isCabActive = true;
                        cab.gameObject.GetComponent<MeshRenderer>().enabled = true;
                    }
                }
                else
                {
                    cab.gameObject.GetComponent<MeshRenderer>().enabled = false;
                    cab.isCabActive = false;
                }
            }
        }
    }

    private void Awake()
    {
        cabDatas = FindObjectsOfType<CabData>();

        foreach (var cab in cabDatas)
        {
            if (cab.GetComponent<MeshRenderer>() != null)
            {   
                for (int i = 0; i < cabColors.Length; i++)
                {
                    if((int)cab.typeName == i)
                    {
                        cab.gameObject.GetComponent<MeshRenderer>().material = cabColors[i];
                    }
                }

                cab.gameObject.GetComponent<MeshRenderer>().enabled = false;
                cab.isCabActive = false;
            }
        }
    }
}
