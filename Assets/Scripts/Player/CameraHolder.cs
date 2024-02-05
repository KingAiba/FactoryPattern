using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    public Camera Camera;
    void Update()
    {
        Camera.transform.position = transform.position;
    }
}
