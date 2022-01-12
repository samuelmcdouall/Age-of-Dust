using UnityEngine;

public class CinematicArea : MonoBehaviour
{
    public GameObject cinematic_camera;
    public GameObject cinematic_area_point;
    public GameObject regular_area_point;
    GameObject player_camera;

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            float distance_from_player_to_regular_point = Vector3.Distance(collider.gameObject.transform.position, regular_area_point.transform.position);
            float distance_from_player_to_cinematic_point = Vector3.Distance(collider.gameObject.transform.position, cinematic_area_point.transform.position);
            SwitchToCloserCamera(distance_from_player_to_regular_point, distance_from_player_to_cinematic_point);
        }
    }

    void SwitchToCloserCamera(float player_to_regular, float player_to_cinematic)
    {
        if (player_to_regular <= player_to_cinematic)
        {
            player_camera = GameObject.FindGameObjectWithTag("MainCamera");
            ChangeCameraTo(player_camera);
        }
        else
        {
            Player.last_camera_tr = Camera.main.transform;
            ChangeCameraTo(cinematic_camera);
        }
    }

    void ChangeCameraTo(GameObject camera)
    {
        CameraManager.DisableAllEnabledCameras();
        CameraManager.EnableCamera(camera);
    }
}
