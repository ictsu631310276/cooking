using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SoundPlayerScript : MonoBehaviour
{
    [SerializeField] private AudioClip soundPick;
    [SerializeField] private AudioClip soundArrow;
    [SerializeField] private AudioClip soundThrow;
    [SerializeField] private AudioClip soundWalk;
    public AudioSource soundPlayerEffect;
    private void PlaySound(AudioClip i)
    {
        if (i != null)
        {
            soundPlayerEffect.clip = i;
            soundPlayerEffect.Play();
        }
    }
    public void PlaySoundPick()
    {
        PlaySound(soundPick);
    }
    public void PlaySoundWalk()
    {
        PlaySound(soundWalk);
    }
    public void PlaySoundArrow()
    {
        PlaySound(soundArrow);
    }
    public void PlaySoundThrow()
    {
        PlaySound(soundThrow);
    }
}
