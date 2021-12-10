using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //todo refactor this entire class
    Rigidbody player_rb;
    [System.NonSerialized]
    public Animator player_ani;

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
    bool able_to_jump_off_ground;
    float jump_delay_timer;
    [SerializeField]
    private float jump_delay;
    [SerializeField] 
    private bool show_cursor;
    Transform camera_tr;
    public static Transform last_camera_tr;
    Vector3 starting_position;

    // Animations
    [System.NonSerialized]
    public string running_animation = "running";
    string jump_up_animation = "jump_up";
    string falling_down_animation = "falling_down";
    string land_animation = "land";
    string kneel_animation = "kneel";
    string stand_up_animation = "stand_up";

    // Cinematics
    bool enabled_controls;

    public GameObject pause_menu;
    public GameObject options_menu;

    // Testing
    [SerializeField]
    bool player_can_sprint;
    [SerializeField]
    float player_top_sprint_speed;

    // Start is called before the first frame update
    void Start()
    {
        player_rb = GetComponent<Rigidbody>();
        player_ani = GameObject.FindGameObjectWithTag("PlayerModel").GetComponent<Animator>();
        camera_tr = Camera.main.transform;
        Cursor.visible = show_cursor;
        starting_position = transform.position;
        jumped_twice = false;
        able_to_jump_off_ground = true;
        jump_delay_timer = 0.0f;
        enabled_controls = true;
    }

    public void DisableControlsForSeconds(float delay)
    {
        enabled_controls = false;
        Invoke("EnableControls", delay);
    }
    void EnableControls()
    {
        enabled_controls = true;
    }

    void StandUpAnimation()
    {
        player_ani.SetTrigger(stand_up_animation);
    }

    public void InspectAnimation(float control_disable_duration, float kneel_duration)
    {
        player_ani.SetBool(running_animation, false);
        player_ani.SetTrigger(kneel_animation);
        Invoke("StandUpAnimation", kneel_duration);
        DisableControlsForSeconds(control_disable_duration);
    }

    void Update()
    {
        if (!pause_menu.activeSelf && !options_menu.activeSelf && enabled_controls)
        {
            if (Input.GetKey(KeyCode.LeftShift) && player_can_sprint)
            {
                if (player_rb.velocity.magnitude > player_top_sprint_speed)
                {
                    float original_vertical_speed = player_rb.velocity.y;
                    player_rb.velocity = player_rb.velocity.normalized * player_top_sprint_speed;
                    player_rb.velocity = new Vector3(player_rb.velocity.x, original_vertical_speed, player_rb.velocity.z);
                }
            }
            else
            {
                if (player_rb.velocity.magnitude > player_top_speed)
                {
                    float original_vertical_speed = player_rb.velocity.y;
                    player_rb.velocity = player_rb.velocity.normalized * player_top_speed;
                    player_rb.velocity = new Vector3(player_rb.velocity.x, original_vertical_speed, player_rb.velocity.z);
                }
            }
            // able to jump only after 
            if (GroundCheck.is_grounded)
            {
                if (player_ani.GetCurrentAnimatorStateInfo(0).IsName("Jump Up"))
                {
                    player_ani.SetBool(falling_down_animation, true);
                }
                else
                {
                    player_ani.SetBool(falling_down_animation, false);
                }
                player_ani.SetBool(jump_up_animation, false);
                if (!able_to_jump_off_ground && jump_delay_timer > jump_delay)
                {
                    able_to_jump_off_ground = true;
                    jump_delay_timer = 0.0f;
                }
                else if (!able_to_jump_off_ground)
                {
                    jump_delay_timer += Time.deltaTime;
                }
            }
            else if (player_rb.velocity.y > 0.1f)
            {

            }
            else if (player_rb.velocity.y < -0.1f)
            {
                player_ani.SetBool(jump_up_animation, false);
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

            if (able_to_jump_off_ground && double_jump_enabled)
            {
                jumped_twice = false;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (GroundCheck.is_grounded && able_to_jump_off_ground)
                {
                    player_ani.SetBool(jump_up_animation, true);
                    Invoke("JumpUp", 0.25f);
                    able_to_jump_off_ground = false;
                }
                else if (!jumped_twice && double_jump_enabled)
                {
                    player_rb.AddForce(0.0f, player_jump_force, 0.0f, ForceMode.Impulse);
                    jumped_twice = true;
                }
            }
        }
    }

    private void JumpUp()
    {
        player_rb.AddForce(0.0f, player_jump_force, 0.0f, ForceMode.Impulse);
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
            transform.SetParent(collision.gameObject.transform);
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
