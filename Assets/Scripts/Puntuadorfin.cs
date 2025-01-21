using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Puntuadorfin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TMP_Text text = transform.GetComponent<TMP_Text>();
        text.text = WaveControl.Instance().days.ToString();
    }

}
