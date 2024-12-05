using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

    [System.Serializable]
public class SettingControl : MonoBehaviour
{

    int j = 0;
    [SerializeField]
    int timeConfirm = 10;


    public TextMeshProUGUI resObj;
    public TMP_Dropdown ResolutionDropdown;
    public TextMeshProUGUI timer;
    


    public GameObject confirmOBJ;
    public GameObject optionsOBJ;


    public void zSetMusicV(Slider slide){
        SettingsSaving.musicV = slide.value;
    }

    public void zSetSfxV(Slider slide){
        SettingsSaving.sfxV = slide.value;
    }

    private void Start() {
        Screen.SetResolution(SettingsSaving.widthList[SettingsSaving.resolutionIndex], SettingsSaving.heightList[SettingsSaving.resolutionIndex], SettingsSaving.isFullscreen);
    }

    public void zSetFullScreen(Toggle t){
        SettingsSaving.fullscreen = t.isOn;
    }
    public void zPreSetRes(TMP_Dropdown d){
        SettingsSaving.i = d.value; 
        //Debug.Log(SettingsSaving.i);
        //Debug.Log(SettingsSaving.WxH[SettingsSaving.i]);
        //Debug.Log(SettingsSaving.WxH.FindIndex(x => x == SettingsSaving.WxH[SettingsSaving.i]));
    }

    public void zApplyRes(){
        resObj.text = SettingsSaving.WxH[SettingsSaving.i];
        Screen.SetResolution(SettingsSaving.widthList[SettingsSaving.i], SettingsSaving.heightList[SettingsSaving.i], SettingsSaving.fullscreen);
        timer.text ="" + timeConfirm;   
        MenuControl.zSwapPanels(confirmOBJ);
        MenuControl.zSwapPanels(optionsOBJ);
        StartCoroutine("ResCountDown");
    }

    IEnumerator ResCountDown(){
        for(j = timeConfirm;j>=0;j--){
            timer.text ="" + j;   
            yield return new WaitForSeconds(1f);
        }
        zCancelRes();
    }

    public void zConfirmRes(){
        StopCoroutine("ResCountDown");        
        
        MenuControl.zSwapPanels(confirmOBJ);
        MenuControl.zSwapPanels(optionsOBJ);

        SettingsSaving.isFullscreen = SettingsSaving.fullscreen;
        SettingsSaving.resolutionIndex = SettingsSaving.i;
    }

    public void zCancelRes(){
        StopCoroutine("ResCountDown");   
        MenuControl.zSwapPanels(confirmOBJ);
        MenuControl.zSwapPanels(optionsOBJ);
        Screen.SetResolution(SettingsSaving.widthList[SettingsSaving.resolutionIndex], SettingsSaving.heightList[SettingsSaving.resolutionIndex], SettingsSaving.isFullscreen);
    }

    public void zSetToggle(Toggle t){
        t.isOn = SettingsSaving.isFullscreen;
        SettingsSaving.fullscreen = SettingsSaving.isFullscreen;
    }
    public void zSetResolutionsDropdown(TMP_Dropdown d){
        d.ClearOptions();
        d.AddOptions(SettingsSaving.WxH);
        SettingsSaving.i = SettingsSaving.resolutionIndex;
        d.value = SettingsSaving.resolutionIndex;
    }
    public void zSetSfxSlider(Slider s){
        s.value = SettingsSaving.sfxV;
    }
    public void zSetMusicSlider(Slider s){
        s.value = SettingsSaving.musicV;
    }
}
