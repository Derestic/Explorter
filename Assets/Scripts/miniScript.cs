using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class miniScript : MonoBehaviour
{
    [SerializeField] GameObject portal;
    [SerializeField] GameObject panelMision;
    [SerializeField] Color complete;
    [SerializeField] Color idle;
    // Start is called before the first frame update
    void Start()
    {
        panelMision.GetComponent<Image>().color = idle;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnDestroy()
    {
        if(portal!=null)portal.SetActive(true);
        panelMision.GetComponent<Image>().color = complete;
    }
}
