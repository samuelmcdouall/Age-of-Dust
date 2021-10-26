using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desert : MonoBehaviour
{
    public GameObject main_camera;
    private void OnCollisionEnter(Collision collision)
    {
        if (!main_camera.GetComponent<Camera>().enabled)
        {
            if (collision.gameObject.tag == "Player")
            {
                //CameraManager.transition_back_to_player = true;
                CameraManager.DisableAllEnabledCameras();
                CameraManager.EnableCamera("MainCamera");
            }
        }
    }
    //private void OnCollisionExit(Collision collision)
    //{
    //    if (!main_camera.GetComponent<Camera>().enabled)
    //    {
    //        if (collision.gameObject.tag == "Player")
    //        {
    //            CameraManager.transition_back_to_player = false;
    //        }
    //    }
    //}
}
