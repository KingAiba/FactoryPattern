using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController
{
    public float sensX;
    public float sensY;

    public Camera camera;
    public Transform orientation;

    float xRotation;
    float yRotation;

    public CameraController(Camera camera, Transform orientation)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        this.camera = camera;
        this.orientation = orientation;
    }

    public void OnMousePositionChange(float mouseX, float mouseY)
    {
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        camera.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
