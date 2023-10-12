using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    public AudioSource soundBG;
    public AudioSource soundEffect;

    public AudioClip buttonSound;


    public void PlaySoundButton()
    {
        soundEffect.clip = buttonSound;
        soundEffect.Play();
    }
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
