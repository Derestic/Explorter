using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveControl
{
    static WaveControl instance;
    public static WaveControl Instance()
    {
        if (instance == null)
        {
            instance = new WaveControl();
        }
        return instance;
    }
    WaveControl()
    {
        instance = this;
    }

    public int prep = 0;

}
