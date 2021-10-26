using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicArea : MonoBehaviour
{
    public GameObject cinematic_camera;
    [SerializeField]
    string camera_tag;
    private void OnCollisionEnter(Collision collision)
    {
        if (!cinematic_camera.GetComponent<Camera>().enabled)
        {
            if (collision.gameObject.tag == "Player")
            {
                Player.last_camera_tr = Camera.main.transform;
                CameraManager.DisableAllEnabledCameras();
                CameraManager.EnableCamera(camera_tag);
            }
        }
    }
}
