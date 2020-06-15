using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject entryGamePanel;
    [SerializeField]
    private GameObject garagePanel;
    [SerializeField]
    private GameObject settingPanel;
    [SerializeField]
    private GameObject graphicsPanel;
    [SerializeField]
    private GameObject soundPanel;

    [SerializeField]
    private GameObject garagePrefab;

    [SerializeField]
    private Slider sliderS;
    [SerializeField]
    private Text valueCount;

    [SerializeField]
    private Button backBttn;

    private void Update()
    {
        valueCount.text = sliderS.value.ToString();
        AudioListener.volume = sliderS.value;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Panel_Garage_Open()
    {
        garagePanel.SetActive(true);
        entryGamePanel.SetActive(false);
        garagePrefab.SetActive(true);
    }

    public void Panel_Garage_Close()
    {
        garagePanel.SetActive(false);
        entryGamePanel.SetActive(true);
        garagePrefab.SetActive(false);
    }

    public void Panel_Setting_Open()
    {
        settingPanel.SetActive(true);
        entryGamePanel.SetActive(false);
    }

    public void Panel_Setting_Close()
    {
        settingPanel.SetActive(false);
        entryGamePanel.SetActive(true);
    }

    public void Panel_VideoGraphic_Open()
    {
        soundPanel.SetActive(false);
        graphicsPanel.SetActive(true);
    }

    public void Panel_Sound_Open()
    {
        soundPanel.SetActive(true);
        graphicsPanel.SetActive(false);
    }

    public void Back() // Panel_Setting_Close() == Back()
    {
        settingPanel.SetActive(false);
        entryGamePanel.SetActive(true);
    }
}
