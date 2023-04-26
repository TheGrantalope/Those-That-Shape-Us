using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    // Audio Management
    public AudioClip[] idleClip;
    public AudioClip battleClip;
    private AudioSource music;
    private float[] delayRange = { 45.0f, 180.0f };
    private float delay;

    // Fade Management
    private float threshold;
    private float minVolume = 0;
    private float maxVolume = 0.75f;
    private float timeDuration = 5.0f;
    private float adjustedDuration;
    private float timeElapsed = 0.0f;
    private float clipOffset = 5.0f;
    private bool fade;

    // Battle Music Management
    private float distanceThreshold = 25.0f;
    private float distance;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        // Camera Audio Source
        music = GetComponent<AudioSource>();

        // Idle Music
        delay = Random.Range(delayRange[0] / 3, delayRange[0]);
        float rate = Random.Range(delayRange[0] * 2, delayRange[1]);
        InvokeRepeating("IdleMusic", delay, rate);
    }

    // Update is called once per frame
    void Update()
    {
        // Fade Conditions
        if (fade)
        {
            FadeIn();
        }
        else if (music.time >= threshold)
        {
            FadeOut();
        }

        // Battle Music Conditions
        if (FindEnemyInRange())
        {
            if (music.clip != battleClip)
            {
                BattleMusic();
            }
        }
        else
        {
            if (music.isPlaying && music.clip == battleClip)
            {
                FadeOut();
            }
        }
    
    }

    void FadeIn()
    {
        if (music.isPlaying)
        {
            music.volume = Mathf.Lerp(minVolume, maxVolume, timeElapsed / adjustedDuration);
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
            music.volume = Mathf.Lerp(maxVolume, minVolume, timeElapsed / adjustedDuration);
            timeElapsed += Time.deltaTime;
        }
        if (music.volume == minVolume)
        {
            timeElapsed = 0.0f;
            if (!music.loop)
            {
                music.Stop();
                music.clip = null;
            }
            fade = true;
        }
    }

    void IdleMusic()
    {
        if (!music.isPlaying)
        {
            fade = true;
            music.clip = idleClip[Random.Range(0, idleClip.Length)];
            threshold = music.clip.length - clipOffset;
            delay = Random.Range(delayRange[0] / 3, delayRange[0]);
            adjustedDuration = timeDuration + delay;
            music.PlayDelayed(delay);
        }
    }

    void BattleMusic()
    {
        if (music.isPlaying && music.clip != battleClip)
        {
            FadeOut();
        }
        else
        {
            fade = true;
            music.clip = battleClip;
            threshold = music.clip.length - clipOffset;
            adjustedDuration = timeDuration;
            music.Play();
        }
    }

    bool FindEnemyInRange()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Damaging");
        bool inRange = false;
        float distance = Mathf.Infinity;
        Vector3 position = player.transform.position;
        foreach (GameObject go in gos)
        {
            if (go != GameObject.Find("Tilemap-Damage"))
            {
                float diff = Mathf.Abs(go.transform.position.x - position.x);
                if (diff < distanceThreshold)
                {
                    inRange = true;
                }
            }
        }
        return inRange;
    }

}
