using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioSource player_as;
    public AudioClip main_game_music;
    // Start is called before the first frame update
    void Start()
    {
        player_as = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        player_as.clip = main_game_music;
        player_as.Play();
    }

    private void Update()
    {
        if (!player_as.isPlaying)
        {
            // todo may need a delay here of no music playing for a bit
            FadeInNewClip(main_game_music);
        }
    }

    public void PlayNewClip(AudioClip cinematic_clip)
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutOldClip(cinematic_clip));
    }

    private IEnumerator FadeOutOldClip(AudioClip cinematic_clip)
    {
        float time_to_fade = 1.0f;
        float fade_timer = 0.0f;

        while (fade_timer < time_to_fade)
        {
            player_as.volume = Mathf.Lerp(1.0f, 0.0f, fade_timer / time_to_fade);
            fade_timer += Time.deltaTime;
            yield return null;
        }
        yield return StartCoroutine(FadeInNewClip(cinematic_clip));
    }

    private IEnumerator FadeInNewClip(AudioClip cinematic_clip)
    {
        player_as.Stop();
        player_as.clip = cinematic_clip;
        player_as.volume = 0.0f;
        player_as.Play();
        print(player_as.clip.name);

        float time_to_fade = 1.0f;
        float fade_timer = 0.0f;

        while (fade_timer < time_to_fade)
        {
            player_as.volume = Mathf.Lerp(0.0f, 1.0f, fade_timer / time_to_fade);
            fade_timer += Time.deltaTime;
            yield return null;
        }
    }

}
