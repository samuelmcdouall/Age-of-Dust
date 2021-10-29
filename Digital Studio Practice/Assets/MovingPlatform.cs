using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject start_point;
    [SerializeField]
    GameObject end_point;
    [SerializeField]
    float platform_speed;
    [SerializeField]
    float stop_duration;
    float stop_timer;
    Vector3 start_to_end_direction;
    bool stopped;
    bool moving_to_end_point;
    float platform_destination_threshold;
    void Start()
    {
        stop_timer = 0.0f;
        stopped = false;
        moving_to_end_point = true;
        start_to_end_direction = (end_point.transform.position - start_point.transform.position).normalized;
        platform_destination_threshold = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopped)
        {
            if (moving_to_end_point)
            {
                transform.position += start_to_end_direction * platform_speed * Time.deltaTime;
                if (Vector3.Distance(transform.position, end_point.transform.position) < platform_destination_threshold)
                {
                    stopped = true;
                    moving_to_end_point = false;
                }
            }
            else
            {
                transform.position -= start_to_end_direction * platform_speed * Time.deltaTime;
                if (Vector3.Distance(transform.position, start_point.transform.position) < platform_destination_threshold)
                {
                    stopped = true;
                    moving_to_end_point = true;
                }
            }
        }
        else
        {
            if (stop_timer > stop_duration)
            {
                stop_timer = 0.0f;
                stopped = false;
            }
            else
            {
                stop_timer += Time.deltaTime;
            }
        }
    }
}
