using UnityEngine;

public class RotationCamera : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField]
    float rotation_speed = 10.0f;
    public Transform camera_target_to_rotate_around;
    float mouse_x;
    float mouse_y;

    [Header("Vertical Mouse Limits")]
    [SerializeField]
    float mouse_y_min_clamp = -35.0f;
    [SerializeField]
    float mouse_y_max_clamp = 60.0f;

    [Header("Menus")]
    public GameObject pause_menu;
    public GameObject options_menu;
    void Update()
    {
        if (!pause_menu.activeSelf && !options_menu.activeSelf)
        {
            GetMouseInput();
            camera_target_to_rotate_around.rotation = Quaternion.Euler(mouse_y, mouse_x, 0.0f);
        }
    }

    void GetMouseInput()
    {
        mouse_x += Input.GetAxis("Mouse X") * rotation_speed;
        mouse_y -= Input.GetAxis("Mouse Y") * rotation_speed;
        mouse_y = Mathf.Clamp(mouse_y, mouse_y_min_clamp, mouse_y_max_clamp);
    }
}
