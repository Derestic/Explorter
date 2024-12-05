using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HPBar : MonoBehaviour
{
    [SerializeField]
    protected npc npcScript;
    protected Slider HPslider;
    // Start is called before the first frame update
    void Start()
    {
        HPslider = GetComponent<Slider>();
        HPslider.value = HPscript.getLife();
    }

    // Update is called once per frame
    void Update()
    {
        HPslider.value = HPscript.getLife();
        
    }
}
