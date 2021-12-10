using System.Collections;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    [Header("Interaction")]
    public GameObject fx;
    public AudioClip[] sfx_clips;
    public float[] post_sfx_delays;
    public GameObject[] animation_triggered_objects;
    public GameObject puzzle_manager;
    Animator pressure_pad_ani;
    bool pressed;
    
    void Start()
    {
        pressure_pad_ani = GetComponent<Animator>();
        pressed = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!pressed && other.gameObject.CompareTag("Player"))
        {
            if (fx)
            {
                fx.SetActive(true); // todo this will only play once, need to reset this (will need to instantiate this if want it playing multiple times, but i dont think it should)
            }

            DepressPad();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (puzzle_manager && puzzle_manager.GetComponent<PressurePadPuzzleManager>().incorrect_combination)
            {
                puzzle_manager.GetComponent<PressurePadPuzzleManager>().ResetPuzzle();
            }
        }
    }

    void DepressPad()
    {
        StartCoroutine(PlayAllSFXClips());
        AnimateSelectedObjects();
        pressure_pad_ani.SetTrigger("pad_down");

        if (puzzle_manager)
        {
            puzzle_manager.GetComponent<PressurePadPuzzleManager>().PressurePadPressed(gameObject);
        }

        pressed = true;
    }

    IEnumerator PlayAllSFXClips()
    {
        for (int count = 0; count < sfx_clips.Length; count++)
        {
            AudioSource.PlayClipAtPoint(sfx_clips[count], transform.position, VolumeManager.sfx_volume);
            yield return new WaitForSeconds(post_sfx_delays[count]);
        }
    }

    void AnimateSelectedObjects()
    {
        foreach (GameObject obj in animation_triggered_objects)
        {
            obj.GetComponent<Animator>().SetTrigger("pressure_pad_pressed");
        }
    }

    public void ResetPad()
    {
        pressure_pad_ani.SetTrigger("pad_up");
        pressed = false;
    }
}
