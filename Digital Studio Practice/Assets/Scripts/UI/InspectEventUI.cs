using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InspectEventUI : MonoBehaviour
{
    public Text ui_text;
    Image ui_image;

    [SerializeField]
    float fade_in_time;
    [SerializeField]
    float display_time;
    [SerializeField]
    float fade_out_time;
    // Start is called before the first frame update
    void Start()
    {
        ui_image = GetComponent<Image>();
        ui_image.color = new Color(ui_image.color.r, ui_image.color.g, ui_image.color.b, 0.0f);
        ui_text.color = new Color(ui_text.color.r, ui_text.color.g, ui_text.color.b, 0.0f);
    }

    public void DisplayInspectEventUI()
    {
        StartCoroutine(FadeInUIAndText());
    }

    IEnumerator FadeInUIAndText()
    {
        float fade_in_timer = 0.0f;

        while (fade_in_timer < fade_in_time)
        {
            float faded_opacity = Mathf.Lerp(0.0f, 1.0f, fade_in_timer / fade_in_time);
            ui_image.color = new Color(ui_image.color.r, ui_image.color.g, ui_image.color.b, faded_opacity);
            ui_text.color = new Color(ui_text.color.r, ui_text.color.g, ui_text.color.b, faded_opacity);
            fade_in_timer += Time.deltaTime;
            yield return null;
        }
        yield return StartCoroutine(DisplayUI());
    }

    IEnumerator DisplayUI()
    {
        yield return new WaitForSeconds(display_time);
        yield return StartCoroutine(FadeOutUIAndText());
    }

    IEnumerator FadeOutUIAndText()
    {
        float fade_out_timer = 0.0f;

        while (fade_out_timer < fade_out_time)
        {
            float faded_opacity = Mathf.Lerp(1.0f, 0.0f, fade_out_timer / fade_out_time);
            ui_image.color = new Color(ui_image.color.r, ui_image.color.g, ui_image.color.b, faded_opacity);
            ui_text.color = new Color(ui_text.color.r, ui_text.color.g, ui_text.color.b, faded_opacity);
            fade_out_timer += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }
}
