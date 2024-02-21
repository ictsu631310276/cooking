using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundScript : MonoBehaviour
{
    [Header("OutSound")]
    public AudioSource soundBG;
    public AudioSource soundEffect;
    [Header("SliderSound")]
    public Slider soundBGBar;
    public Slider soundEffectBar;
    [Header("Sound")]
    public AudioClip musicBG;
    public AudioClip buttonSound;
    [SerializeField] private AudioClip soundWin;
    [SerializeField] private AudioClip soundLose;

    [SerializeField] private GameObject settingUI;
    public void PlaySoundButton()
    {
        if (buttonSound != null)
        {
            soundEffect.clip = buttonSound;
            soundEffect.Play();
        }
    }
    public void UpdateSettingSound()
    {
        soundBG.volume  = soundBGBar.value;
        soundEffect.volume = soundEffectBar.value;
        PlayerPrefs.SetFloat("valueSoundBG", soundBG.volume);
        PlayerPrefs.SetFloat("valueSoundEffect", soundEffect.volume);
    }
    public void SettingButtom()
    {
        settingUI.SetActive(true);
    }
    public void SettingBackButtom()
    {
        settingUI.SetActive(false);
    }
    private void PlaySound(AudioClip i)
    {
        if (i != null)
        {
            soundEffect.clip = i;
            soundEffect.Play();
        }
    }
    public void PlaySoundWin()
    {
        PlaySound(soundWin);
    }
    public void PlaySoundLose()
    {
        PlaySound(soundLose);
    }
    private void Start()
    {
        if (PlayerPrefs.GetFloat("valueSoundBG") == 0)
        {
            soundBGBar.value = 25;
            soundEffectBar.value = 25;
            UpdateSettingSound();
        }
        if (soundBGBar != null)
        {
            soundBG.clip = musicBG;
            soundBG.Play();
            settingUI.SetActive(false);
            soundBG.volume = PlayerPrefs.GetFloat("valueSoundBG");
            soundEffect.volume = PlayerPrefs.GetFloat("valueSoundEffect");
            soundBGBar.value = soundBG.volume;
            soundEffectBar.value = soundEffect.volume;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (soundBGBar != null)
        {
            if (soundBGBar.value != soundBG.volume)
            {
                if (soundBGBar.value != PlayerPrefs.GetFloat("valueSoundBG"))
                {
                    UpdateSettingSound();
                }
            }
            if (soundEffectBar.value != soundEffect.volume)
            {
                if (soundEffectBar.value != PlayerPrefs.GetFloat("valueSoundEffect"))
                {
                    UpdateSettingSound();
                }
            }
        }
    }
}
