using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Physics")]
    Rigidbody player_rb;
    [SerializeField]
    float player_move_force;
    [SerializeField]
    float player_top_speed;
    [SerializeField]
    float player_jump_force;
    [SerializeField]
    float player_fall_force;
    bool able_to_jump_off_ground;
    float jump_delay_timer;
    [SerializeField]
    float jump_delay;

    // Camera
    Transform camera_tr;
    public static Transform last_camera_tr;

    // Animations
    [System.NonSerialized]
    public Animator player_ani;
    [System.NonSerialized]
    public string running_animation;
    string jump_up_animation;
    string falling_down_animation;
    string kneel_animation;
    string stand_up_animation;

    [Header("Timeline")]
    bool enabled_controls;
    [SerializeField]
    float initial_disable_control_period;

    [Header("UI")]
    public GameObject pause_menu;
    public GameObject options_menu;
    public GameObject controls_menu;

    [Header("Debug Testing")]
    [SerializeField]
    bool player_can_fast_move;
    [SerializeField]
    float player_top_fast_move_speed;
    [SerializeField]
    private bool show_cursor;

    void Start()
    {
        InitialPlayerSetup();
    }

    void FixedUpdate()
    {
        if (!pause_menu.activeSelf && !options_menu.activeSelf && !controls_menu.activeSelf && enabled_controls)
        {
            LimitSpeedToMaximum();
            HandleGroundCheckMechanics();
            HandleMovementMechanics();
        }
    }
    void Update()
    {
        HandleJumpMechanics();
    }

    void InitialPlayerSetup()
    {
        player_rb = GetComponent<Rigidbody>();
        player_ani = GameObject.FindGameObjectWithTag("PlayerModel").GetComponent<Animator>();
        running_animation = "running";
        jump_up_animation = "jump_up";
        falling_down_animation = "falling_down";
        kneel_animation = "kneel";
        stand_up_animation = "stand_up";
        camera_tr = Camera.main.transform;
        Cursor.visible = show_cursor;
        print("player setup, cursor should be false");
        able_to_jump_off_ground = true;
        jump_delay_timer = 0.0f;
        DisableControlsForSeconds(initial_disable_control_period);
    }

    public void DisableControlsForSeconds(float delay)
    {
        enabled_controls = false;
        Invoke("EnableControls", delay);
    }
    void EnableControls()
    {
        enabled_controls = true;
        Cursor.visible = false;
    }

    void LimitSpeedToMaximum()
    {
        if (Input.GetKey(KeyCode.LeftShift) && player_can_fast_move)
        {
            if (player_rb.velocity.magnitude > player_top_fast_move_speed)
            {
                float original_vertical_speed = player_rb.velocity.y;
                player_rb.velocity = player_rb.velocity.normalized * player_top_fast_move_speed;
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
    }

    void HandleGroundCheckMechanics()
    {
        if (GroundCheck.is_grounded)
        {
            HandleFallingDownAnimation();
            DetermineIfReadyToJumpOffGround();
        }
        else if (player_rb.velocity.y < -0.1f)
        {
            HandlePlayerFallingDown();
        }
    }

    void HandleFallingDownAnimation()
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
    }

    void DetermineIfReadyToJumpOffGround()
    {
        if (!able_to_jump_off_ground && jump_delay_timer > jump_delay)
        {
            able_to_jump_off_ground = true;
            jump_delay_timer = 0.0f;
        }
        else if (!able_to_jump_off_ground)
        {
            jump_delay_timer += Time.fixedDeltaTime;
        }
    }

    void HandlePlayerFallingDown()
    {
        player_ani.SetBool(jump_up_animation, false);
        player_ani.SetBool(falling_down_animation, true);
        player_rb.AddForce(0.0f, -player_fall_force, 0.0f);
    }

    void HandleMovementMechanics()
    {
        if (Camera.main)
        {
            ApplyNormalAngleMovement();
        }
        else
        {
            ApplyCinematicAngleMovement();
        }
    }

    void ApplyNormalAngleMovement()
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

    void ApplyCinematicAngleMovement()
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

    void DetermineYIndependentVelocity(Vector3 horizontal_direction)
    {
        player_ani.SetBool(running_animation, true);
        horizontal_direction = new Vector3(horizontal_direction.x, 0.0f, horizontal_direction.z);
        horizontal_direction = horizontal_direction.normalized * player_move_force;
        player_rb.AddForce(horizontal_direction);
    }

    void HandleJumpMechanics()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GroundCheck.is_grounded && able_to_jump_off_ground)
            {
                player_ani.SetBool(jump_up_animation, true);
                Invoke("JumpUp", 0.25f);
                able_to_jump_off_ground = false;
            }
        }
    }

    void JumpUp()
    {
        player_rb.AddForce(0.0f, player_jump_force, 0.0f, ForceMode.Impulse);
    }

    public void InspectAnimation(float control_disable_duration, float kneel_duration)
    {
        player_ani.SetBool(running_animation, false);
        player_ani.SetTrigger(kneel_animation);
        Invoke("StandUpAnimation", kneel_duration);
        DisableControlsForSeconds(control_disable_duration);
    }

    void StandUpAnimation()
    {
        player_ani.SetTrigger(stand_up_animation);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "MovingPlatform" && transform.position.y > collision.gameObject.transform.position.y)
        {
            transform.SetParent(collision.gameObject.transform);
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            transform.SetParent(null);
        }
    }
}
