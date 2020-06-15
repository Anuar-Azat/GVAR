using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsSettings : MonoBehaviour
{
    [Header("Разрешение")]
    int valueSlider;
    public Slider SliderResolution;
    public Text TextResolution;
    public Resolution[] resolutions;

    [Space(10)]
    [Header("Полноэкранный режим")]
    public int valueFullScreen;
    public Slider SliderFullscreen;
    public Text TextFullscreen;

    [Space(10)]
    [Header("Качество графики")]
    public int valueQuality;
    public Slider SliderQuality;
    public Text TextQuality;
    public string Quality;

    [Header("Качество Текстур")]
    [Space(10)]
    public int valueTexture;
    public Slider SliderTexture;
    public Text TextTexture;

    [Header("Сглаживание")]
    [Space(10)]
    public int valueAntiAliasing;
    public Slider SliderAntiAliasing;
    public Text TextAlising;

    [Header("Вертикальная синхронизация")]
    [Space(10)]
    public int valueVSync;
    public Slider SliderVSync;
    public Text TextVSync;

    [Header("Качество теней")]
    [Space(10)]
    public int valueQualityShadow;
    public Slider SliderQualityShadow;
    public Text TextQualitySHadow;

    [Header("Дальность отрисовки теней")]
    [Space(10)]
    public int valueRangeShadow;
    public Slider SliderRangeShadow;
    public Text TextRangeShadow;

    [Header("Количество источников освещения")]
    [Space(10)]
    public int valueCountLight;
    public Slider SliderCountLight;
    public Text TextCountLight;

    private void Start()
    {
        resolutions = Screen.resolutions;
        SliderResolution.maxValue = resolutions.Length - 1;
    }

    private void Update()
    {
        TextResolution.text = "Разрешение: " + resolutions[(int)SliderResolution.value];

        valueFullScreen = (int)SliderFullscreen.value;
        valueTexture = (int)SliderTexture.value;
        valueAntiAliasing = (int)SliderAntiAliasing.value;
        valueVSync = (int)SliderVSync.value;
        valueQualityShadow = (int)SliderQualityShadow.value;
        valueRangeShadow = (int)SliderRangeShadow.value;
        valueCountLight = (int)SliderCountLight.value;


        if (SliderQuality.value != valueQuality)
        {
            valueQuality = (int)SliderQuality.value;
            if (valueQuality == 1)
            {
                TextQuality.text = "Качество низкое";
                SliderTexture.value = 1;
                SliderAntiAliasing.value = 1;
                SliderVSync.value = 0;
                SliderQualityShadow.value = 1;
                SliderRangeShadow.value = 30;
                SliderCountLight.value = 4;
            }
            else if (valueQuality == 2)
            {
                TextQuality.text = "Качество: Средние";
                SliderTexture.value = 2;
                SliderAntiAliasing.value = 2;
                SliderVSync.value = 0;
                SliderQualityShadow.value = 2;
                SliderRangeShadow.value = 70;
                SliderCountLight.value = 5;
            }
            else if (valueQuality == 3)
            {
                TextQuality.text = "Качество: Высокое";
                SliderTexture.value = 3;
                SliderAntiAliasing.value = 3;
                SliderVSync.value = 1;
                SliderQualityShadow.value = 3;
                SliderRangeShadow.value = 100;
                SliderCountLight.value = 6;
            }
            else if (valueQuality == 4)
            {
                TextQuality.text = "Качество: Ультра";
                SliderTexture.value = 4;
                SliderAntiAliasing.value = 4;
                SliderVSync.value = 1;
                SliderQualityShadow.value = 4;
                SliderRangeShadow.value = 200;
                SliderCountLight.value = 8;

            }

        }


        //Отображение названия текстур
        #region
        if (valueTexture == 1)
        {
            TextTexture.text = "Качество тестур Низкое";
        }
        else if (valueTexture == 2)
        {
            TextTexture.text = "Качество тестур Средние";
        }
        else if (valueTexture == 3)
        {
            TextTexture.text = "Качество тестур Высокое";
        }
        else if (valueTexture == 4)
        {
            TextTexture.text = "Качество тестур Ультра";
        }
        #endregion
        //Отображения названия полноэкранного режима
        #region
        if (valueFullScreen == 1)
        {
            TextFullscreen.text = "Оконный режим";
        }
        else if (valueFullScreen == 2)
        {
            TextFullscreen.text = "Полноэкранный режим";
        }
        #endregion
        //Отображение названия сглаживания
        #region
        if (valueAntiAliasing == 1)
        {
            TextAlising.text = "Сглаживание: Отключено";
        }
        else if (valueAntiAliasing == 2)
        {
            TextAlising.text = "Сглаживание: x2";
        }
        else if (valueAntiAliasing == 3)
        {
            TextAlising.text = "Сглаживание: x4";
        }
        else if (valueAntiAliasing == 4)
        {
            TextAlising.text = "Сглаживание: x8";
        }
        #endregion
        //Отображение вертикальная синхрнизация
        #region
        {
            if (valueVSync == 0)
            {
                TextVSync.text = "Отключено";
            }
            else if (valueVSync == 1)
            {
                TextVSync.text = "Включено";
            }
        }
        #endregion
        //Отображение качества теней
        #region
        {
            if (valueQualityShadow == 1)
            {
                TextQualitySHadow.text = "Низкое";
            }
            else if (valueQualityShadow == 2)
            {
                TextQualitySHadow.text = "Средние";
            }
            else if (valueQualityShadow == 3)
            {
                TextQualitySHadow.text = "Высокая";
            }
            else if (valueQualityShadow == 4)
            {
                TextQualitySHadow.text = "Ультра";
            }
        }
        #endregion
        //Отображение Дальность теней
        #region
        TextRangeShadow.text = "Дальность теней: " + SliderRangeShadow.value + "м";
        #endregion
        //Отображение Количества источников освещения
        #region
        {
            TextCountLight.text = "Число источников света: " + SliderCountLight.value + "x";
        }
        #endregion


    }

    public void Appy()
    {
        //Применение разрешения
        Screen.SetResolution(resolutions[(int)SliderResolution.value].width, resolutions[(int)SliderResolution.value].height, true);

        //Качество графики
        switch (valueQuality)
        {
            case 1: QualitySettings.SetQualityLevel(1); break;
            case 2: QualitySettings.SetQualityLevel(2); break;
            case 3: QualitySettings.SetQualityLevel(3); break;
            case 4: QualitySettings.SetQualityLevel(4); break;
        }
        //Полноэкранный режим
        switch (valueFullScreen)
        {
            case 1: Screen.fullScreen = false; break;
            case 2: Screen.fullScreen = true; break;
        }
        //Применение текстур
        switch (valueTexture)
        {
            case 1: QualitySettings.masterTextureLimit = 3; break;
            case 2: QualitySettings.masterTextureLimit = 2; break;
            case 3: QualitySettings.masterTextureLimit = 1; break;
            case 4: QualitySettings.masterTextureLimit = 0; break;

        }
        //Применение сглаживания
        switch (valueAntiAliasing)
        {
            case 1: QualitySettings.antiAliasing = 1; break;
            case 2: QualitySettings.antiAliasing = 2; break;
            case 3: QualitySettings.antiAliasing = 4; break;
            case 4: QualitySettings.antiAliasing = 8; break;
        }
        // Применение верт/синхронизации
        switch (valueVSync)
        {
            case 1: QualitySettings.vSyncCount = 0; break;
            case 2: QualitySettings.vSyncCount = 1; break;
            case 3: QualitySettings.vSyncCount = 2; break;

        }
        //Применение Качества теней
        switch (valueQualityShadow)
        {
            case 1: QualitySettings.shadowResolution = ShadowResolution.Low; break;
            case 2: QualitySettings.shadowResolution = ShadowResolution.Medium; break;
            case 3: QualitySettings.shadowResolution = ShadowResolution.High; break;
            case 4: QualitySettings.shadowResolution = ShadowResolution.VeryHigh; break;
        }
        //Применение Качества теней
        QualitySettings.shadowDistance = valueRangeShadow;

        //Применение количества источников освещения
        QualitySettings.pixelLightCount = valueCountLight;

    }
}
