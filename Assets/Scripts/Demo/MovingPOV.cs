using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class MovingPOV : MonoBehaviour
{
    public float Speed = 1;
    public Vector2 Sensitivity = new Vector2(1, 1);
    public float MaxYawUnder = 90;
    public float MaxYawOver = 90;

    private Vector3 _rotation = Vector3.zero;

    void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
    }

    void Update()
    {
        Move();
        Rotate();
    }

    void Move() 
    {
        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) direction += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) direction += Vector3.back;
        if (Input.GetKey(KeyCode.A)) direction += Vector3.left;
        if (Input.GetKey(KeyCode.D)) direction += Vector3.right;
        if (Input.GetKey(KeyCode.Space)) direction += Vector3.up;
        if (Input.GetKey(KeyCode.LeftShift)) direction += Vector3.down;

        direction.Normalize();

        gameObject.transform.position += gameObject.transform.rotation * direction * Speed * Time.deltaTime;
    }

    void Rotate()
    {
        float pitch = Input.GetAxis("Mouse Y") * Sensitivity.y * Time.deltaTime * -1;
        float yaw = Input.GetAxis("Mouse X") * Sensitivity.x * Time.deltaTime;

        _rotation.x = Mathf.Clamp(_rotation.x + pitch, -MaxYawOver, MaxYawUnder);
        _rotation.y = (_rotation.y + yaw) % 360;

        gameObject.transform.rotation = Quaternion.Euler(_rotation);
    }
}
