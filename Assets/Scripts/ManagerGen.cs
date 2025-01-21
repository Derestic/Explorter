using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagerGen : MonoBehaviour
{

    [Header("Control Inventario")]
    protected Inventario inventory; 

    [Header("Control mazmorras")]
    [SerializeField] int[] indexMazmorra;
    public void goDungeon(int numDungeon)
    {
        SceneManager.LoadScene(indexMazmorra[numDungeon]);
        
    }
    public Inventario GetInventario() { return inventory; }
    public void SetInventario(Inventario inv) { inventory = inv; }

    [Header("Control dias")]
    [SerializeField] GameObject ContadorDias;
    [SerializeField] TMP_Text showDias;
    [SerializeField] Sprite[] dias = new Sprite[5];

    protected void updateDay(int d)
    {
        ContadorDias.GetComponent<Image>().sprite = dias[d];
        showDias.text = WaveControl.Instance().days.ToString();
    }

}
