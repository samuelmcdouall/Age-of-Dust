using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float rotation_speed;
    public GameObject pause_menu;
    public GameObject options_menu;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!pause_menu.activeSelf && !options_menu.activeSelf)
        {
            Transform camera_tr;
            if (Camera.main)
            {
                camera_tr = Camera.main.transform;
            }
            else
            {
                camera_tr = Player.last_camera_tr;
            }
            Vector3 camera_pos_y_independent;
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
            {
                camera_pos_y_independent = new Vector3((camera_tr.forward + camera_tr.right).x, 0.0f, (camera_tr.forward + camera_tr.right).z);
                transform.rotation = RotateSlowly(camera_pos_y_independent);
            }
            else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
            {
                camera_pos_y_independent = new Vector3((camera_tr.forward - camera_tr.right).x, 0.0f, (camera_tr.forward - camera_tr.right).z);
                transform.rotation = RotateSlowly(camera_pos_y_independent);
            }
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
            {
                camera_pos_y_independent = new Vector3((-camera_tr.forward + camera_tr.right).x, 0.0f, (-camera_tr.forward + camera_tr.right).z);
                transform.rotation = RotateSlowly(camera_pos_y_independent);
            }
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
            {
                camera_pos_y_independent = new Vector3((-camera_tr.forward - camera_tr.right).x, 0.0f, (-camera_tr.forward - camera_tr.right).z);
                transform.rotation = RotateSlowly(camera_pos_y_independent);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                camera_pos_y_independent = new Vector3(camera_tr.forward.x, 0.0f, camera_tr.forward.z);
                transform.rotation = RotateSlowly(camera_pos_y_independent);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                camera_pos_y_independent = new Vector3(-camera_tr.forward.x, 0.0f, -camera_tr.forward.z);
                transform.rotation = RotateSlowly(camera_pos_y_independent);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                camera_pos_y_independent = new Vector3(camera_tr.right.x, 0.0f, camera_tr.right.z);
                transform.rotation = RotateSlowly(camera_pos_y_independent);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                camera_pos_y_independent = new Vector3(-camera_tr.right.x, 0.0f, -camera_tr.right.z);
                transform.rotation = RotateSlowly(camera_pos_y_independent);
            }
        }
    }

    Quaternion RotateSlowly(Vector3 camera_pos)
    {
        return Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(camera_pos), rotation_speed);
    }
}
