using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsSaving : MonoBehaviour
{
    public static SettingsSaving instance;

    //List of resolutions
    public static List<int> widthList = new List<int>();
    public static List<int> heightList = new List<int>();
    public static List<string> WxH = new List<string>();

    //Objective resolution - fullScreen
    public static int i = 0;
    public static bool fullscreen = false;

    //Actual resolution - fullscreen
    public static int resolutionIndex = 0;
    public static bool isFullscreen = false;
    
    //Volumes
    public static float musicV = 1;
    public static float sfxV = 1;

    //Controles 
    //string -> KeyCode



    //Mirror Variables
    public List<int>        MWidthList       = new List<int>()   ;
    public List<int>        MHeightList      = new List<int>()   ;
    public List<string>     MWXH             = new List<string>();
    public int              MI               = 0                 ;
    public bool             MFullscreen      = false             ;
    public int              MResolutionIndex = 0                 ;
    public bool             MIsFullscreen    = false             ;
    public float            MMusicV          = 1                 ;
    public float            MSfxV            = 1                 ;


    private void Awake() {
        if(instance == null){
            instance = this;
        }else if(instance != null){
            Destroy(transform.parent.gameObject);
            Destroy(this);
        }
        //Data Recopilation
        Resolution[] resolutions = Screen.resolutions;
        foreach (var res in resolutions){
            widthList.Add(res.width);
            heightList.Add(res.height);
            WxH.Add(res.width + "x" + res.height);
        }
        
        //Default config
        i = WxH.Count - 1;
        resolutionIndex = i;

        DontDestroyOnLoad(this);
    }
    private void Update() {
        
    MWidthList = widthList;
    MHeightList = heightList;
    MWXH = WxH;
    MI = i;
    MFullscreen = fullscreen;
    MResolutionIndex = resolutionIndex;
    MIsFullscreen = isFullscreen;
    MMusicV = musicV;
    MSfxV = sfxV;
    }
}

