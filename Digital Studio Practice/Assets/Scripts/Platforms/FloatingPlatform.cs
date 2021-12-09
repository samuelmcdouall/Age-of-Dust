using UnityEngine;

public class FloatingPlatform : MonoBehaviour
{
    [Header("Falling")]
    [SerializeField]
    bool should_descend;
    [SerializeField]
    Transform starting_position;
    [SerializeField]
    Transform end_position;
    [SerializeField]
    float transition_time;
    float current_transition_lerp_value;
    FloatingPlatformStatus floating_platform_status;

    [Header("Rotating")]
    [SerializeField]
    bool should_rotate;
    [SerializeField]
    [Range(0.1f,60.0f)]
    float revolution_time_y;

    void Start()
    {
        floating_platform_status = FloatingPlatformStatus.stationary;
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
            if (floating_platform_status == FloatingPlatformStatus.descending)
            {
                DescendPlatform();
            }

            else if (floating_platform_status == FloatingPlatformStatus.ascending)
            {
                AscendPlatform();
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            floating_platform_status = FloatingPlatformStatus.descending;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            floating_platform_status = FloatingPlatformStatus.ascending;
        }
    }

    void AscendPlatform()
    {
        if (current_transition_lerp_value == 0.0f)
        {
            floating_platform_status = FloatingPlatformStatus.stationary;
        }
        else
        {
            transform.position = Vector3.Lerp(starting_position.position, end_position.position, current_transition_lerp_value / transition_time);
            current_transition_lerp_value -= Time.deltaTime;
            if (current_transition_lerp_value < 0.0f)
            {
                current_transition_lerp_value = 0.0f;
            }
        }
    }

    void DescendPlatform()
    {
        if (current_transition_lerp_value == transition_time)
        {
            floating_platform_status = FloatingPlatformStatus.stationary;
        }
        else
        {
            transform.position = Vector3.Lerp(starting_position.position, end_position.position, current_transition_lerp_value / transition_time);
            current_transition_lerp_value += Time.deltaTime;
            if (current_transition_lerp_value > transition_time)
            {
                current_transition_lerp_value = transition_time;
            }
        }
    }

    enum FloatingPlatformStatus
    {
        descending,
        ascending,
        stationary
    }
}
