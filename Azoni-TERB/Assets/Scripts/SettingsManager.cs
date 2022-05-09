using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    // Get the objects and scripst form the editor
    [SerializeField] AudioMixer mainVolume;
    [SerializeField] BasicSaveManager bsm;
    [SerializeField] Slider GeneralSlider, MusicSlider, efxSlider;
    // this one is public because it is accessed from other script 
    public string[] volumeParameter = { "GeneralVolume", "MusicVolume", "efxVolume" };

    //when the game launch, set the volume sliders to the saved value 
    private void Awake()
    {
        for (int i=0; i < volumeParameter.Length; i++) {
            SetSliderValue(bsm.GetVolumeData(volumeParameter[i]), volumeParameter[i]);           
        }
        SetQuality(0);
    }
    
    //Set and save the value of the Main volume and slider when it changes
    public void SetGeneralVolume(float volume) {
        mainVolume.SetFloat(volumeParameter[0], volume);
        bsm.SetVolumeData(volumeParameter[0], volume);
    }

    //Set and save the value of the effects volume and slider when it changes
    public void SetEffectsVolume(float volume)
    {
        bsm.SetVolumeData(volumeParameter[2], volume);
    }

    //Set and save the value of the Music voulme and slider when it changes
    public void SetMusiclVolume(float volume)
    {
        bsm.SetVolumeData(volumeParameter[1], volume);
    }

    //set the value of the 3 slaiders when nedded
    public void SetSliderValue(float value, string slider) {
        switch (slider) {
            case "GeneralVolume":
                GeneralSlider.value = value;
                return;
            case "MusicVolume":
                MusicSlider.value = value;
                return;
            case "efxVolume":
                efxSlider.value = value;
                return;
        }
    }
    //set the quality with a dropdown box
    public void SetQuality(int quality) {
        QualitySettings.SetQualityLevel(quality);
    }
}
