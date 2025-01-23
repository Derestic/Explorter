using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public void zSceneChange(int i){
        SceneManager.LoadScene(i);
    }
    public void zSceneChange(string s){
        SceneManager.LoadScene(s);
    }

    public static void zSwapPanels(GameObject target){
        target.SetActive(!target.activeSelf);
    }

    public static void zPauseTime(){
        Time.timeScale = (Time.timeScale + 1) % 2;
        visibilityRaton();
    }

    public static void visibilityRaton()
    {
        if (Cursor.visible)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    } 
    public static void zEndGame(){
        Application.Quit();
        Debug.Log("Adios!");
    }
}