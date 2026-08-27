#nullable disable

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class settingsManager : MonoBehaviour
{
    public Light sun;

    public Slider sensitivity;
    public Image checkMark;
    public bool shadowsDisabled;
    void Start()
    {
        //StartCoroutine(lightCheck());
        //sun = GameObject.FindWithTag("lightSettings").GetComponent<Light>();
        if (PlayerPrefs.GetInt("shadowsDisabled") == 1) { shadowsDisabled = true; }
        sensitivity.value = PlayerPrefs.GetFloat("sensitivityPlus");
        QualitySettings.shadows = ShadowQuality.Disable;
    }

    void FixedUpdate()
    {
        if (shadowsDisabled) { QualitySettings.shadows = ShadowQuality.Disable; checkMark.enabled = true; }
        if (!shadowsDisabled) { QualitySettings.shadows = ShadowQuality.HardOnly; checkMark.enabled = false; }
    }
    public void shadowSettingChange()
    {
        shadowsDisabled = !shadowsDisabled;
        if (shadowsDisabled) PlayerPrefs.SetInt("shadowsDisabled", 1);
        if (!shadowsDisabled) PlayerPrefs.SetInt("shadowsDisabled", 0);
    }
    public void sensitivitySettingChange()
    {
        PlayerPrefs.SetFloat("sensitivityPlus", sensitivity.value);
    }
    //IEnumerator lightCheck()
    //{
    //    yield return new WaitForSeconds(1);

    //    GameObject[] lightingObjects = GameObject.FindGameObjectsWithTag("lightSettings");

    //    foreach (GameObject obj in lightingObjects)
    //    {
    //        Light lightComponent = obj.GetComponent<Light>();

    //        if (lightComponent != null)
    //        {
    //            if (shadowsDisabled) { lightComponent.shadows = LightShadows.None; }
    //            if (!shadowsDisabled) { lightComponent.shadows = LightShadows.Hard; }
    //        }
    //    }
    //    StartCoroutine(lightCheck());
    //}
}
