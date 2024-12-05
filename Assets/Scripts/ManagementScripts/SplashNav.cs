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
        StartCoroutine("AutoOpen");
    }

    IEnumerator AutoOpen(){
        yield return new WaitForSeconds(10f);
        OpenMenu();
    }
    void OpenMenu(){
        MenuControl.zSwapPanels(splash);
        MenuControl.zSwapPanels(menu);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            OpenMenu();
            Debug.Log("Space Pulsado");
        }
    }
}
