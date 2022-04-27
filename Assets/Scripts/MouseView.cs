using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseView : MonoBehaviour
{
    private float cameraRotation = 0f;
    public Transform player;
    public float sensitivity = 200f;
    private float mouseX;
    private float mouseY;
        

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        player.Rotate(Vector3.up * mouseX);
        cameraRotation -= mouseY * (sensitivity / 40);
        cameraRotation = Mathf.Clamp(cameraRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(cameraRotation, 0, 0);

        if (GameManager.paused)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
