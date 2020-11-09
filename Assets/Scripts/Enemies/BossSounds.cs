using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSounds : MonoBehaviour
{
    public float timeBtwSounds;
    float nextSoundEffectTime;

    public AudioClip[] clips;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextSoundEffectTime)
        {
            int randomIndex = Random.Range(0, clips.Length);
            audioSource.clip = clips[randomIndex];
            audioSource.Play();
            nextSoundEffectTime = Time.time + timeBtwSounds;
        }
    }
}
