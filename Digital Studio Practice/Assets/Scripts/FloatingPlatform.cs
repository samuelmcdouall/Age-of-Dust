using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatform : MonoBehaviour
{
    [SerializeField]
    bool should_descend;
    [SerializeField]
    bool should_rotate;
    PlatformStatus platform_moving_status;
    [SerializeField]
    Transform upper_position;
    [SerializeField]
    Transform lower_position;
    [SerializeField]
    float transition_time;
    float current_transition_lerp_value;
    [SerializeField]
    [Range(0.1f,60.0f)]
    float revolution_time_y;
    void Start()
    {
        platform_moving_status = PlatformStatus.stationary;
        current_transition_lerp_value = 0.0f;
    }

    void Update()
    {
        if (should_rotate)
        {
            transform.Rotate(Vector3.up, Time.deltaTime * 360.0f / revolution_time_y);
        }
        if (should_descend)
        {
            if (platform_moving_status == PlatformStatus.descending)
            {
                if (current_transition_lerp_value == transition_time)
                {
                    platform_moving_status = PlatformStatus.stationary;
                }
                else
                {
                    transform.position = Vector3.Lerp(upper_position.position, lower_position.position, current_transition_lerp_value / transition_time);
                    current_transition_lerp_value += Time.deltaTime;
                    if (current_transition_lerp_value > transition_time)
                    {
                        current_transition_lerp_value = transition_time;
                    }
                }
            }

            else if (platform_moving_status == PlatformStatus.ascending)
            {
                if (current_transition_lerp_value == 0.0f)
                {
                    platform_moving_status = PlatformStatus.stationary;
                }
                else
                {
                    transform.position = Vector3.Lerp(upper_position.position, lower_position.position, current_transition_lerp_value / transition_time);
                    current_transition_lerp_value -= Time.deltaTime;
                    if (current_transition_lerp_value < 0.0f)
                    {
                        current_transition_lerp_value = 0.0f;
                    }
                }
            }
        }
    }
    

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("player landed on platform");
            platform_moving_status = PlatformStatus.descending;
        }   
    }
    void OnCollisionExit(Collision collision)
    {
        print("player left platform");
        if (collision.gameObject.CompareTag("Player"))
        {
            platform_moving_status = PlatformStatus.ascending;
        }
    }

    enum PlatformStatus
    {
        descending,
        ascending,
        stationary
    }
}
