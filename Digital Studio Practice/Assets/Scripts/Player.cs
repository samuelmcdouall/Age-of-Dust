using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody player_rb;
    Animator player_ani;

    [SerializeField]
    private float player_move_force;
    [SerializeField]
    private float player_top_speed;
    [SerializeField]
    private float player_jump_force;
    [SerializeField]
    private float player_fall_force;
    [SerializeField]
    private bool double_jump_enabled;
    bool jumped_twice;
    [SerializeField] 
    private bool show_cursor;
    Transform camera_tr;
    public static Transform last_camera_tr;
    Vector3 starting_position;

    // Animations
    string running_animation = "running";
    string jump_up_animation = "jump_up";
    string falling_down_animation = "falling_down";

    // Start is called before the first frame update
    void Start()
    {
        player_rb = GetComponent<Rigidbody>();
        player_ani = GetComponent<Animator>();
        camera_tr = Camera.main.transform;
        Cursor.visible = show_cursor;
        starting_position = transform.position;
        jumped_twice = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (player_rb.velocity.magnitude > player_top_speed)
        {
            float original_vertical_speed = player_rb.velocity.y;
            player_rb.velocity = player_rb.velocity.normalized * player_top_speed;
            player_rb.velocity = new Vector3(player_rb.velocity.x, original_vertical_speed, player_rb.velocity.z);
        }
        if (GroundCheck.is_grounded)
        {
            player_ani.SetBool(falling_down_animation, false);
        }
        else if (player_rb.velocity.y > 0.1f)
        {
            player_ani.SetTrigger(jump_up_animation);
        }
        else if (player_rb.velocity.y < -0.1f)
        {
            player_ani.SetBool(falling_down_animation, true);
            player_rb.AddForce(0.0f, -player_fall_force, 0.0f);
        }
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
                player_ani.SetBool(running_animation, false);
            }
        }
        else
        {
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
                player_ani.SetBool(running_animation, false);
            }
        }

        if (GroundCheck.is_grounded && double_jump_enabled)
        {
            jumped_twice = false;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GroundCheck.is_grounded)
            {
                player_rb.AddForce(0.0f, player_jump_force, 0.0f, ForceMode.Impulse);
            }
            else if (!jumped_twice && double_jump_enabled)
            {
                player_rb.AddForce(0.0f, player_jump_force, 0.0f, ForceMode.Impulse);
                jumped_twice = true;
            }
        }
    }

    public void DetermineYIndependentVelocity(Vector3 horizontal_direction)
    {
        player_ani.SetBool(running_animation, true);
        horizontal_direction = new Vector3(horizontal_direction.x, 0.0f, horizontal_direction.z);
        horizontal_direction = horizontal_direction.normalized * player_move_force;
        player_rb.AddForce(horizontal_direction);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // TODO running into side of platform when in motion causes issues
        if (collision.gameObject.tag == "MovingPlatform" && transform.position.y > collision.gameObject.transform.position.y)
        {
            transform.SetParent(collision.gameObject.transform.parent);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            transform.SetParent(null);
        }
    }

}
