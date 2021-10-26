using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicViewCamera : MonoBehaviour
{
    GameObject player;
    //GameObject player_camera;
    //Camera cinematic_camera;
    //Vector3 cinematic_camera_position;
    //Quaternion cinematic_camera_rotation;
    //Vector3 starting_player_camera_position;
    //bool at_cinematic_view;
    //bool moved_to_player_view;
    //bool camera_moving;
    //[SerializeField]
    //float camera_transition_time_frame;
    //float interpolate_value = 0.0f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //player_camera = GameObject.FindGameObjectWithTag("MainCamera");
        //cinematic_camera = GetComponent<Camera>();
        //cinematic_camera_position = transform.position;
        //cinematic_camera_rotation = transform.rotation;
        //starting_player_camera_position = player_camera.transform.position;
        //at_cinematic_view = false;
        //moved_to_player_view = false;
        //camera_moving = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (CameraManager.transition_back_to_player && at_cinematic_view)
        //{
        //    if (at_cinematic_view)
        //    {
        //        if (!camera_moving)
        //        {
        //            cinematic_camera_position = transform.position;
        //            camera_moving = true;
        //            print("start moving to player");
        //        }
        //        else
        //        {
        //            print("interpolate value: " + interpolate_value);
        //            transform.position = Vector3.Slerp(cinematic_camera_position, player_camera.transform.position, interpolate_value);
        //            //transform.rotation = Quaternion.Slerp(cinematic_camera_rotation, player_camera.transform.rotation, interpolate_value); -> not sure if needed TODO
        //            interpolate_value += (1.0f/camera_transition_time_frame) * Time.deltaTime;
        //            if (interpolate_value >= 1.0f)
        //            {
        //                at_cinematic_view = false;
        //                camera_moving = false;
        //                interpolate_value = 0.0f;
        //                CameraManager.DisableAllEnabledCameras();
        //                CameraManager.EnableCamera("MainCamera");
        //                CameraManager.transition_back_to_player = false;
        //                print("stop moving, arrived at player");
        //            }
        //        }
        //    }
        //}
        //if (cinematic_camera.enabled)
        //{
        //    if (!at_cinematic_view)
        //    {
        //        if (!camera_moving)
        //        {
        //            starting_player_camera_position = player_camera.transform.position;
        //            camera_moving = true;
        //            print("start moving to cinematic view");
        //        }
        //        else
        //        {
        //            print("interpolate value: " + interpolate_value);
        //            transform.position = Vector3.Slerp(starting_player_camera_position, cinematic_camera_position, interpolate_value);
        //            interpolate_value += (1.0f/camera_transition_time_frame) * Time.deltaTime;
        //            if (interpolate_value >= 1.0f)
        //            {
        //                at_cinematic_view = true;
        //                camera_moving = false;
        //                interpolate_value = 0.0f;
        //                print("stop moving, arrived at cinematic view");
        //            }
        //        }
        //    }
        //    else
        //    {
        //        transform.position = cinematic_camera_position;
        //    }
        //}

        transform.LookAt(player.transform.position);
    }
}
