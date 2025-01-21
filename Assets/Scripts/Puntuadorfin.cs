using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Puntuadorfin : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        text.text = WaveControl.Instance().days.ToString();
    }

}
