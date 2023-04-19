using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    // Private Variables
    private float[] delayRange = { 5.0f, 45.0f };
    private float delay;
    private float rate = 270.0f;
    private AudioSource music;

    // Public Variables
    public AudioClip[] idleMusic;
    public AudioClip[] battleMusic;
    public GameObject player;
    public GameObject slime;

    // Start is called before the first frame update
    void Start()
    {
        music = GetComponent<AudioSource>();
        delay = Random.Range(delayRange[0], delayRange[1]);
        InvokeRepeating("IdleMusic", delay, rate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // IdleMusic is called in InvokeRepeating 
    void IdleMusic()
    {
        delay = Random.Range(delayRange[0], delayRange[1]);
        music.clip = idleMusic[Random.Range(0, idleMusic.Length)];
        music.PlayDelayed(delay);
    }

    void BattleMusic()
    {
        music.clip = battleMusic[Random.Range(0, battleMusic.Length)];
        music.Play();
    }

}
