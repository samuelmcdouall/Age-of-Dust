using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCamera : MonoBehaviour
{
    [SerializeField]
    float rotation_speed = 10.0f;
    public Transform camera_target_to_rotate_around;
    float mouse_x;
    float mouse_y;
    [SerializeField]
    float mouse_y_min_clamp = -35.0f;
    [SerializeField]
    float mouse_y_max_clamp = 60.0f;
    void Start()
    {

    }
    void Update()
    {
        mouse_x += Input.GetAxis("Mouse X") * rotation_speed;
        mouse_y -= Input.GetAxis("Mouse Y") * rotation_speed;
        mouse_y = Mathf.Clamp(mouse_y, mouse_y_min_clamp, mouse_y_max_clamp);
        camera_target_to_rotate_around.rotation = Quaternion.Euler(mouse_y, mouse_x, 0.0f);
    }
}
