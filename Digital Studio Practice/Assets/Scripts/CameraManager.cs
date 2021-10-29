using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static void DisableAllEnabledCameras()
    {
        Camera[] enabled_cameras = Camera.allCameras;
        
        foreach(Camera camera in enabled_cameras)
        {
            //camera.transform.parent.gameObject.GetComponent<AudioListener>();
            camera.enabled = false;
        }
    }

    public static void EnableCamera(GameObject camera_object)
    {
        camera_object.GetComponent<Camera>().enabled = true;
        //new_camera.GetComponent<AudioListener>().enabled = true;
    }
}
