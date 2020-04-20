using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource music1Source;
    [SerializeField] private AudioSource soundSource;

    [SerializeField] private string introBGMusic; // names of music clips
    [SerializeField] private string levelBGMusic;
    [SerializeField] private AudioClip sound; // click button sound

    public void PlayIntroMusic() // Dounload intro music from folder Resources
    {
        PlayMusic(Resources.Load("Music/" + introBGMusic) as AudioClip);
    }

    public void PlayLevelMusic() // Download main music from the folder Resources
    {
        PlayMusic(Resources.Load("Music/" + levelBGMusic) as AudioClip);
    }

    private void PlayMusic(AudioClip clip)
    {
        music1Source.clip = clip;
        music1Source.Play();
    }

    public void StopMusic()
    {
        music1Source.Stop();
    }

    public void PlayClick(AudioClip clip)
    {
        soundSource.PlayOneShot(clip);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
