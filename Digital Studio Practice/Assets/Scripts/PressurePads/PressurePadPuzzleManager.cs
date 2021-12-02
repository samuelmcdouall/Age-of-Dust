using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePadPuzzleManager : MonoBehaviour
{
    //todo could make this for any number with a list
    public GameObject[] pressure_pads;
    public GameObject[] animation_triggered_objects;
    [System.NonSerialized]
    public bool incorrect_combination;
    bool puzzle_reset;
    List<int> combination;

    void Start()
    {
        incorrect_combination = false;
        puzzle_reset = false;
        combination = new List<int>();
    }

    public void PressurePadPressed(GameObject pressed_pad)
    {
        for (int count = 0; count < pressure_pads.Length; count++)
        {
            if (ReferenceEquals(pressed_pad, pressure_pads[count]))
            {
                combination.Add(count);
                EvaluateCurrentPuzzleCode();
                break;
            }
        }
    }
    void EvaluateCurrentPuzzleCode()
    {
        if (combination.Count == pressure_pads.Length)
        {
            for (int count = 0; count < pressure_pads.Length; count++)
            {
                if (combination[count] != count)
                {
                    incorrect_combination = true;
                    break;
                }
            }
            if (!incorrect_combination)
            {
                PuzzleComplete();
            }
        }
    }

    public void ResetPuzzle()
    {
        combination.Clear();
        incorrect_combination = false;
        foreach (GameObject pad in pressure_pads)
        {
            pad.GetComponent<PressurePad>().ResetPad();
        }
    }

    void PuzzleComplete()
    {
        foreach (GameObject obj in animation_triggered_objects)
        {
            obj.GetComponent<Animator>().SetTrigger("pressure_pad_pressed");
        }
    }
}
