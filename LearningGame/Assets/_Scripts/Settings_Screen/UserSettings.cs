using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserSettings : MonoBehaviour
{
    PlayerData playerData;

    [SerializeField] public Slider _effectsSlider;
    [SerializeField] public TextMeshProUGUI effectsText;

    [SerializeField] public Slider _musicSlider;
    [SerializeField] public TextMeshProUGUI musicText;

    private void Awake()
    {
        playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
    }

    private void Start()
    {
        _effectsSlider.onValueChanged.AddListener((v) =>
        {
            effectsText.text = (v.ToString() + "%");
            AudioManager.instance.ChangeEffectsVolume(v);

            playerData.ChangeEffectsVolume(v);
        });

        _musicSlider.onValueChanged.AddListener((v) =>
        {
            musicText.text = (v.ToString() + "%");
            AudioManager.instance.ChangeMusicVolume(v);
            playerData.ChangeMusicVolume(v);
        });


    }

    private void OnEnable()
    {
        int effectsVol = playerData.GetEffectsVolume();
        _effectsSlider.value = effectsVol;
        effectsText.text = (effectsVol.ToString() + "%");


        int musicVol = playerData.GetMusicVolume();
        _musicSlider.value = musicVol;
        musicText.text = (musicVol.ToString() + "%");
    }

    // open settings panel in minigame function
    public void OpenSettingsPanel()
    {
        gameObject.SetActive(true);
    }

    public void CloseSettingsPanel()
    {
        gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        
        if (ExperimentalGM.instance.currentMode == ExperimentalGM.GameMode.FIB)
        {
            GameObject.Find("Fill_In_The_Blank").GetComponent<FIB_E>().EndGame();
        } else if (ExperimentalGM.instance.currentMode == ExperimentalGM.GameMode.FlashCards)
        {
            GameObject.Find("FlashCards").GetComponent<FlashCards_E>().EndGame();
        }

        gameObject.SetActive(false);
    }
}
