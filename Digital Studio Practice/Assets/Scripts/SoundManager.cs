using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [System.NonSerialized]
    public AudioSource player_as;
    public AudioClip main_game_music;
    //[SerializeField]
    //float max_volume;
    // Start is called before the first frame update
    void Start()
    {
        player_as = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        player_as.velocityUpdateMode = AudioVelocityUpdateMode.Dynamic;
        player_as.volume = VolumeManager.music_volume;
        player_as.clip = main_game_music;
        //player_as.volume = max_volume;
        player_as.Play();
    }

    private void Update()
    {
        // should be covered in transitions from areas
        //if (!player_as.isPlaying)
        //{
        //    // todo may need a delay here of no music playing for a bit
        //    print("not playing");
        //    StopAllCoroutines();
        //    StartCoroutine(FadeInNewClip(main_game_music));
        //}
    }

    public void PlayMainMusic()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutOldClip(main_game_music));
    }
    public void PlayAreaClip(AudioClip cinematic_clip)
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutOldClip(cinematic_clip));
    }

    public void PlayEntranceClipAndAreaClip(AudioClip entrance_clip, AudioClip area_clip)
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutOldClip(entrance_clip));
    }

    private IEnumerator FadeOutOldClip(AudioClip cinematic_clip)
    {
        float time_to_fade = 1.0f;
        float fade_timer = 0.0f;

        while (fade_timer < time_to_fade)
        {
            player_as.volume = Mathf.Lerp(VolumeManager.music_volume, 0.0f, fade_timer / time_to_fade);
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
        //Invoke("StopAudioSource", player_as.clip.length + 3.0f);

        float time_to_fade = 1.0f;
        float fade_timer = 0.0f;

        while (fade_timer < time_to_fade)
        {
            player_as.volume = Mathf.Lerp(0.0f, VolumeManager.music_volume, fade_timer / time_to_fade);
            fade_timer += Time.deltaTime;
            yield return null;
        }
    }

    private void StopAudioSource()
    {
        player_as.Stop();
    }

}
