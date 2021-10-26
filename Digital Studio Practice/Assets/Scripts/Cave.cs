using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cave : MonoBehaviour
{
    public GameObject cinematic_camera;
    private void OnCollisionEnter(Collision collision)
    {
        if (!cinematic_camera.GetComponent<Camera>().enabled)
        {
            if (collision.gameObject.tag == "Player")
            {
                Player.last_camera_tr = Camera.main.transform;
                CameraManager.DisableAllEnabledCameras();
                CameraManager.EnableCamera("CaveCamera");
            }
        }
    }
}
