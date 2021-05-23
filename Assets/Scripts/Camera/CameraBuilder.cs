using UnityEngine;

public class CameraBuilder : MonoBehaviour
{
    public Transform target = null;
    public static Vector3 offset;

    public float limit = 80; // ограничение вращения 
    public float minLimit = 50;
    public float zoom = 0.25f; // чувствительность при увеличении, колесиком мышки
    public float zoomMax = 10; // макс. увеличение
    public float zoomMin = 2.6f; // мин. увеличение
    public float mouseSense = 3; // чувствительность мышки
    private float X, Y;

    void Start()
    {
        limit = Mathf.Abs(limit);
        if (limit > 90) limit = 90;
        offset = new Vector3(offset.x, offset.y, -Mathf.Abs(zoomMax));
        transform.position = target.position + offset;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1) && !Input.GetKey(KeyCode.Mouse0))
        {
            MouseInputR();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0 && offset.z < -zoomMin)// приблизить
        {
            offset.z += zoom;
            offset.z = Mathf.Clamp(offset.z, - Mathf.Abs(zoomMax), -Mathf.Abs(zoomMin));
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && offset.z > -zoomMax)// отдалить
        {
            offset.z -= zoom;
            offset.z = Mathf.Clamp(offset.z, -Mathf.Abs(zoomMax), -Mathf.Abs(zoomMin));
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        transform.position = transform.rotation * offset + target.position;
    }
    void MouseInputR()
    {
        X = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSense;
        Y += Input.GetAxis("Mouse Y") * mouseSense;
        Y = Mathf.Clamp(Y, -limit, -minLimit);
        transform.localEulerAngles = new Vector3(-Y, X, 0);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

}