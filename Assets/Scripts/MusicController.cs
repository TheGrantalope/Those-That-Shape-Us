using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    // Audio Management
    public AudioClip[] idleClip;
    public AudioClip battleClip;
    private AudioSource music;
    private float[] delayRange = { 45.0f, 46.0f };

    // Fade Management
    private float threshold;
    private float minVolume = 0;
    private float maxVolume = 0.75f;
    private float timeDuration = 5.0f;
    private float timeElapsed = 0.0f;
    private float clipOffset = 5.0f;
    private bool fade;

    // Start is called before the first frame update
    void Start()
    {
        music = GetComponent<AudioSource>();
        float delay = Random.Range(delayRange[0], delayRange[1]);
        InvokeRepeating("IdleMusic", delay, delayRange[0]);
    }

    // Update is called once per frame
    void Update()
    {
        if (fade)
        {
            FadeIn();
        } else if (music.time >= threshold)
        {
            FadeOut();
        }

        if (gameObject.GetComponent<FindClosestSlime>().FindClosestEnemy() != null)
        {
            if (music.clip != battleClip)
            {
                BattleMusic();
            }
        }

    }

    void FadeIn()
    {
        if (music.isPlaying)
        {
            music.volume = Mathf.Lerp(minVolume, maxVolume, timeElapsed / timeDuration);
            timeElapsed += Time.deltaTime;
        }
        if (music.volume == maxVolume)
        {
            timeElapsed = 0.0f;
            fade = false;
        }
    }

    void FadeOut()
    {
        if (music.isPlaying)
        {
            music.volume = Mathf.Lerp(maxVolume, minVolume, timeElapsed / timeDuration);
            timeElapsed += Time.deltaTime;
        }
        if (music.volume == minVolume)
        {
            timeElapsed = 0.0f;
            music.Stop();
            fade = true;
        }
    }

    void IdleMusic()
    {
        if (!music.isPlaying)
        {
            fade = true;
            float delay = Random.Range(delayRange[0], delayRange[1]);
            music.clip = idleClip[Random.Range(0, idleClip.Length)];
            threshold = music.clip.length - clipOffset;
            music.Play();
        }   
    }

    void BattleMusic()
    {
        if (music.isPlaying && music.clip != battleClip)
        {
            FadeOut();
        } else
        {
            music.clip = battleClip;
            threshold = music.clip.length - clipOffset;
            music.Play();
        }   
    }

}
