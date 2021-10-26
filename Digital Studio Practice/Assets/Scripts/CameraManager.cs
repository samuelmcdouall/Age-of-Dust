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

    public static void EnableCamera(string camera_tag)
    {
        GameObject new_camera = GameObject.FindGameObjectWithTag(camera_tag);

        new_camera.GetComponent<Camera>().enabled = true;
        //new_camera.GetComponent<AudioListener>().enabled = true;
    }
}
