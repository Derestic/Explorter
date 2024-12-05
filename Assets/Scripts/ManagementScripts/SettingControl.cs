using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

    [System.Serializable]
public class SettingControl : MonoBehaviour
{

    int j = 0;
    bool contador = false;
    private int timeConfirm = 0;
    private List<string> resolutionString = new List<string>();
    private List<int> widthList = new List<int>();
    private List<int> heightList = new List<int>();

    public TextMeshProUGUI timer;
    public TextMeshProUGUI resObj;


    public TMP_Dropdown ResolutionDropdown;
    public GameObject confirmOBJ;
    public GameObject optionsOBJ;


    public void zSetMusicV(Slider slide){
        SettingsSaving.musicV = slide.value;
    }

    public void zSetSfxV(Slider slide){
        SettingsSaving.sfxV = slide.value;
    }

    private void Start() {
        if (SettingsSaving.isFirstRun){
            Resolution[] resolutions = Screen.resolutions;

            foreach (var res in resolutions)
            {
                SettingsSaving.widthList.Add(res.width);
                SettingsSaving.heightList.Add(res.height);
                SettingsSaving.refreshList.Add(res.refreshRate);
                resolutionString.Add(res.width + "x" + res.height);
            }
            ResolutionDropdown.ClearOptions();
            ResolutionDropdown.AddOptions(resolutionString);
            foreach (var res in resolutionString)
            {
                Debug.Log(res);
            }
            SettingsSaving.i = SettingsSaving.widthList.Count - 1;

            SettingsSaving.width = SettingsSaving.widthList[SettingsSaving.i];
            SettingsSaving.height = SettingsSaving.heightList[SettingsSaving.i];

            resObj.text = SettingsSaving.widthList[SettingsSaving.i] + "x" + SettingsSaving.heightList[SettingsSaving.i];  
            Screen.SetResolution(SettingsSaving.width, SettingsSaving.height, SettingsSaving.fullscreen); 
        }
    }

    public void zPreSetRes(){
        int n = ResolutionDropdown.value; 
        Debug.Log(n);
        Debug.Log(resolutionString[n]);
        Debug.Log(resolutionString.FindIndex(x => x == resolutionString[n]));
        SettingsSaving.i = n;
        resObj.text = resolutionString[n];  
    }

    public void zApplyRes(int timeCon){
        Screen.SetResolution(SettingsSaving.widthList[SettingsSaving.i], SettingsSaving.heightList[SettingsSaving.i], SettingsSaving.fullscreen);
        timeConfirm = timeCon;
        contador = true;
        timer.text ="" + timeConfirm;   
        MenuControl.zSwapPanels(confirmOBJ);
        MenuControl.zSwapPanels(optionsOBJ);
        StartCoroutine("ResCountDown");
    }

    private void Update() {
        if(contador == true){
            timer.text ="" + j;   
        }
    }

    IEnumerator ResCountDown(){
        for(j = timeConfirm;j>=0;j--){
            yield return new WaitForSeconds(1f);
            if (j == 0){
                Screen.SetResolution(SettingsSaving.width, SettingsSaving.height, SettingsSaving.fullscreen);
                MenuControl.zSwapPanels(confirmOBJ);
                MenuControl.zSwapPanels(optionsOBJ);
                break;
            }
        }
    }

    public void zConfirmRes(){
        StopCoroutine("ResCountDown");        
        MenuControl.zSwapPanels(confirmOBJ);
        MenuControl.zSwapPanels(optionsOBJ);
        contador = false;
        SettingsSaving.width = SettingsSaving.widthList[SettingsSaving.i];
        SettingsSaving.height = SettingsSaving.heightList[SettingsSaving.i];
    }

    public void zCancelRes(){
        StopCoroutine("ResCountDown");   
        MenuControl.zSwapPanels(confirmOBJ);
        MenuControl.zSwapPanels(optionsOBJ);
        contador = false;
        Screen.SetResolution(SettingsSaving.width, SettingsSaving.height, SettingsSaving.fullscreen);
    }

    public void zSetToggle(Toggle t){
        if (SettingsSaving.fullscreen){
            t.isOn = true;
        }else{
            t.isOn = false;
        }
    }
    public void zSetSfxSlider(Slider s){
        s.value = SettingsSaving.sfxV;
    }
    public void zSetMusicSlider(Slider s){
        s.value = SettingsSaving.musicV;
    }

    public void zSetFullScreen(Toggle t){
        SettingsSaving.fullscreen = t.isOn;
    }
}
