using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashNav : MonoBehaviour
{

    [SerializeField] GameObject splash;
    [SerializeField] GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
        splash = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            MenuControl.zSwapPanels(splash);
            MenuControl.zSwapPanels(menu);
            Debug.Log("Enter Pulsado");
        }
    }
}
