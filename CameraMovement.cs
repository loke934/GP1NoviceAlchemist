using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Handles camera rotation relative to mouse input*/
public class CameraMovement : MonoBehaviour
{
    [Header("Camera angle restriction")] 
    [SerializeField, Range(-10, -90)]
    private float minAngle = -30f;
    [SerializeField, Range(10, 90)]
    private float maxAngle = 30f;
    [SerializeField,Range(0.1f,10f)]
    [Header("Rotation speed in X and Y axis")]
    private float rotSpeedX = 1f; 
    [SerializeField,Range(0.1f,10f)]
    private float rotSpeedY = 1f;

    private float degX;
    private float degY;
    private Transform aim;

    private void UpdateAimPos()
    {
        aim.position = transform.position + transform.forward;
    }

    private void CameraRotation()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
        degX -= Input.GetAxis("Vertical") + Input.GetAxis("Mouse Y") * rotSpeedX;
        degX = Mathf.Clamp(degX, minAngle, maxAngle);
        degY += Input.GetAxis("Horizontal") + Input.GetAxis("Mouse X")  * rotSpeedY;
        transform.rotation  = Quaternion.Euler(degX, degY - 90, 0f);
    }

    private void Awake()
    {
        aim = GetComponentInChildren<AimUI>().transform;
    }

    private void Update()
    {
        CameraRotation();
        UpdateAimPos();
    }
}