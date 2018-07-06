using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanCamera : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 100.0f;
    private float clampAngleX = 30.0f;
    private float clampAngleYmin = 150f;
    private float clampAngleYmax = 210.0f;
    private float rotY; 
    private float rotX; 

    void Start()
    {
        Vector3 rot = transform.localEulerAngles;
        rotY = rot.y;
        rotX = rot.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(2))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = -Input.GetAxis("Mouse Y");

            rotY += mouseX * mouseSensitivity * Time.deltaTime;
            rotX += mouseY * mouseSensitivity * Time.deltaTime;

            rotY = Mathf.Clamp(rotY, clampAngleYmin, clampAngleYmax);
            rotX = Mathf.Clamp(rotX, -clampAngleX, clampAngleX);

            Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
            transform.rotation = localRotation;
            
        }
    }
}
