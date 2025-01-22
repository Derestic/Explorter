using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public int prep = 2;
    public float vidaN = -10;
    public int days = 0;
    public int numSpawns = 0;
    public int count = 0;

    //public List<Vector3> posicion = new List<Vector3>();
    //public List<Quaternion> rotation = new List<Quaternion>();
    //public List<GameObject> obj = new List<GameObject>();


    public Dictionary<int,Vector3> posicion = new Dictionary<int, Vector3>();
    public Dictionary<int, Quaternion> rotation = new Dictionary<int, Quaternion>();
    public Dictionary<int, GameObject> obj = new Dictionary<int, GameObject>();

}
