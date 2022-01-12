using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Moving Paramters")]
    [SerializeField]
    GameObject start_point;
    [SerializeField]
    GameObject end_point;
    [SerializeField]
    float transition_time;
    [SerializeField]
    float stop_duration;
    float platform_speed;

    float stop_timer;
    Vector3 start_to_end_direction;
    bool moving_to_end_point;
    float platform_destination_threshold;
    MovingPlatformStatus moving_platform_status;
    void Start()
    {
        stop_timer = 0.0f;
        moving_to_end_point = true;
        start_to_end_direction = (end_point.transform.position - start_point.transform.position).normalized;
        platform_destination_threshold = 0.1f;
        moving_platform_status = MovingPlatformStatus.stationary;
        platform_speed = Vector3.Distance(start_point.transform.position, end_point.transform.position) / transition_time;
    }

    void Update()
    {
        if (moving_platform_status == MovingPlatformStatus.moving_to_end)
        {
            MovePlatformToEndPoint();
        }
        else if (moving_platform_status == MovingPlatformStatus.moving_to_start)
        {
            MovePlatformToStartPoint();
        }
        else if (moving_platform_status == MovingPlatformStatus.stationary)
        {
            DetermineIfTimeToMovePlatform();
        }

    }

    void MovePlatformToEndPoint()
    {
        transform.position += start_to_end_direction * platform_speed * Time.deltaTime;
        if (Vector3.Distance(transform.position, end_point.transform.position) < platform_destination_threshold)
        {
            moving_platform_status = MovingPlatformStatus.stationary;
            moving_to_end_point = false;
        }
    }

    void MovePlatformToStartPoint()
    {
        transform.position -= start_to_end_direction * platform_speed * Time.deltaTime;
        if (Vector3.Distance(transform.position, start_point.transform.position) < platform_destination_threshold)
        {
            moving_platform_status = MovingPlatformStatus.stationary;
            moving_to_end_point = true;
        }
    }

    void DetermineIfTimeToMovePlatform()
    {
        if (stop_timer > stop_duration)
        {
            stop_timer = 0.0f;
            if (moving_to_end_point)
            {
                moving_platform_status = MovingPlatformStatus.moving_to_end;
            }
            else
            {
                moving_platform_status = MovingPlatformStatus.moving_to_start;
            }
        }
        else
        {
            stop_timer += Time.deltaTime;
        }
    }

    enum MovingPlatformStatus
    {
        moving_to_end,
        moving_to_start,
        stationary
    }

}
