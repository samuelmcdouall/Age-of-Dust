using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    public GameObject fx;
    public AudioClip sfx;
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
            AudioSource.PlayClipAtPoint(sfx, transform.position, VolumeManager.sfx_volume);
            pressure_pad_ani.SetTrigger("pad_down");

            foreach(GameObject obj in animation_triggered_objects)
            {
                obj.GetComponent<Animator>().SetTrigger("pressure_pad_pressed");
            }

            if (puzzle_manager)
            {
                puzzle_manager.GetComponent<PressurePadPuzzleManager>().PressurePadPressed(gameObject);
            }

            pressed = true;
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

    public void ResetPad()
    {
        pressure_pad_ani.SetTrigger("pad_up");
        pressed = false;
    }
}
