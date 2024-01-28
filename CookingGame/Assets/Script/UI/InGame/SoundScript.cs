using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundScript : MonoBehaviour
{
    public AudioSource soundBG;
    public AudioSource soundEffect;
    public Slider soundBGBar;
    public Slider soundEffectBar;

    public AudioClip musicBG;
    public AudioClip buttonSound;

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
    private void Start()
    {
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
