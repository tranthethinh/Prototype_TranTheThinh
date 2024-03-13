using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VolumeController : MonoBehaviour
{
    public Slider musicSlider;
    public Slider soundSlider;

    private void Start()
    {
        musicSlider.value = AudioManager.instance.musicVolume;
        soundSlider.value = AudioManager.instance.soundVolume;
        //Debug.Log(musicSlider.value);
    }

    public void SetMusicVolume()
    {
        AudioManager.instance.SetMusicVolume(musicSlider.value);
    }


    public void SetSoundVolume()
    {
        AudioManager.instance.SetSoundVolume(soundSlider.value);
    }
}
