using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField]
    float rotation_speed;
    GameObject player;

    [Header("Menus")]
    public GameObject pause_menu;
    public GameObject options_menu;
    public GameObject controls_menu;

    // Cinematics
    bool enabled_controls;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enabled_controls = true;
    }

    void Update()
    {
        if (!pause_menu.activeSelf && !options_menu.activeSelf && !controls_menu.activeSelf && enabled_controls)
        {
            Transform camera_tr;
            if (Camera.main)
            {
                camera_tr = Camera.main.transform;
            }
            else
            {
                camera_tr = Player.last_camera_tr;
            }
            Vector3 camera_pos_y_independent;
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
            {
                camera_pos_y_independent = new Vector3((camera_tr.forward + camera_tr.right).x, 0.0f, (camera_tr.forward + camera_tr.right).z);
                transform.rotation = RotateSlowly(camera_pos_y_independent);
            }
            else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
            {
                camera_pos_y_independent = new Vector3((camera_tr.forward - camera_tr.right).x, 0.0f, (camera_tr.forward - camera_tr.right).z);
                transform.rotation = RotateSlowly(camera_pos_y_independent);
            }
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
            {
                camera_pos_y_independent = new Vector3((-camera_tr.forward + camera_tr.right).x, 0.0f, (-camera_tr.forward + camera_tr.right).z);
                transform.rotation = RotateSlowly(camera_pos_y_independent);
            }
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
            {
                camera_pos_y_independent = new Vector3((-camera_tr.forward - camera_tr.right).x, 0.0f, (-camera_tr.forward - camera_tr.right).z);
                transform.rotation = RotateSlowly(camera_pos_y_independent);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                camera_pos_y_independent = new Vector3(camera_tr.forward.x, 0.0f, camera_tr.forward.z);
                transform.rotation = RotateSlowly(camera_pos_y_independent);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                camera_pos_y_independent = new Vector3(-camera_tr.forward.x, 0.0f, -camera_tr.forward.z);
                transform.rotation = RotateSlowly(camera_pos_y_independent);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                camera_pos_y_independent = new Vector3(camera_tr.right.x, 0.0f, camera_tr.right.z);
                transform.rotation = RotateSlowly(camera_pos_y_independent);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                camera_pos_y_independent = new Vector3(-camera_tr.right.x, 0.0f, -camera_tr.right.z);
                transform.rotation = RotateSlowly(camera_pos_y_independent);
            }
        }
    }

    Quaternion RotateSlowly(Vector3 camera_pos)
    {
        return Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(camera_pos), rotation_speed);
    }


    public void LookAtTargetForSeconds(GameObject target, float delay)
    {
        Vector3 y_independent_target = new Vector3(target.transform.position.x, player.transform.position.y, target.transform.position.z);
        transform.LookAt(y_independent_target);
        DisableRotationForSeconds(delay);
    }

    void DisableRotationForSeconds(float delay)
    {
        enabled_controls = false;
        Invoke("EnableControls", delay);
    }
    void EnableControls()
    {
        enabled_controls = true;
    }
}
