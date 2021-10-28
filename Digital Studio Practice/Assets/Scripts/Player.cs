using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody player_rb;

    [SerializeField]
    private float player_speed;
    [SerializeField]
    private float player_jump_speed;
    [SerializeField]
    private bool enable_double_jump;
    bool jumped_twice;
    [SerializeField] 
    private bool show_cursor;
    Transform camera_tr;
    public static Transform last_camera_tr;
    Vector3 starting_position;

    // Start is called before the first frame update
    void Start()
    {
        player_rb = GetComponent<Rigidbody>();
        camera_tr = Camera.main.transform;
        Cursor.visible = show_cursor;
        starting_position = transform.position;
        jumped_twice = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -10.0f)
        {
            transform.position = starting_position;
        }
        if (Camera.main)
        {
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
            {
                DetermineYIndependentVelocity(camera_tr.forward - camera_tr.right);
            }
            else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
            {
                DetermineYIndependentVelocity(camera_tr.forward + camera_tr.right);
            }
            else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
            {
                DetermineYIndependentVelocity(camera_tr.forward - camera_tr.right);
            }
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
            {
                DetermineYIndependentVelocity(-camera_tr.forward + camera_tr.right);
            }
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
            {
                DetermineYIndependentVelocity(-camera_tr.forward - camera_tr.right);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                DetermineYIndependentVelocity(camera_tr.forward);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                DetermineYIndependentVelocity(-camera_tr.forward);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                DetermineYIndependentVelocity(camera_tr.right);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                DetermineYIndependentVelocity(-camera_tr.right);
            }
            else
            {
                player_rb.velocity = new Vector3(0.0f, player_rb.velocity.y, 0.0f);
            }
        }
        else
        {
            //Camera[] enabled_camera = Camera.allCameras;
            //Transform current_camera_transform = enabled_camera[0].transform.parent.gameObject.transform;
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
            {
                DetermineYIndependentVelocity(last_camera_tr.forward - last_camera_tr.right);
            }
            else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
            {
                DetermineYIndependentVelocity(last_camera_tr.forward + last_camera_tr.right);
            }
            else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
            {
                DetermineYIndependentVelocity(last_camera_tr.forward - last_camera_tr.right);
            }
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
            {
                DetermineYIndependentVelocity(-last_camera_tr.forward + last_camera_tr.right);
            }
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
            {
                DetermineYIndependentVelocity(-last_camera_tr.forward - last_camera_tr.right);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                DetermineYIndependentVelocity(last_camera_tr.forward);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                DetermineYIndependentVelocity(-last_camera_tr.forward);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                DetermineYIndependentVelocity(last_camera_tr.right);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                DetermineYIndependentVelocity(-last_camera_tr.right);
            }
            else
            {
                player_rb.velocity = new Vector3(0.0f, player_rb.velocity.y, 0.0f);
            }
        }

        if (GroundCheck.is_grounded && enable_double_jump)
        {
            jumped_twice = false;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GroundCheck.is_grounded)
            {
                player_rb.velocity = new Vector3(player_rb.velocity.x, player_jump_speed, player_rb.velocity.z);
            }
            else if (!jumped_twice && enable_double_jump)
            {
                player_rb.velocity = new Vector3(player_rb.velocity.x, player_jump_speed, player_rb.velocity.z);
                jumped_twice = true;
            }
        }
    }

    public void DetermineYIndependentVelocity(Vector3 horizontal_direction)
    {
        horizontal_direction = new Vector3(horizontal_direction.x, 0.0f, horizontal_direction.z);
        horizontal_direction = horizontal_direction.normalized * player_speed;
        player_rb.velocity = new Vector3(horizontal_direction.x, player_rb.velocity.y, horizontal_direction.z);
    }

}
