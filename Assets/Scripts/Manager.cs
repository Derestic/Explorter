using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private static Manager _instance;
    enum RoundState { preparation, oleada};
    [SerializeField] RoundState state = RoundState.preparation;
    int prep = 0;
    [SerializeField] int maxprep = 3;

    public static Manager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("Manager");
                go.AddComponent<Manager>();
            }
            return _instance;
        }
    }
    void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void nextState()
    {
        if (state == RoundState.oleada)
        {
            state = RoundState.preparation;
        }
        else
        {
            prep++;
            if (prep >= maxprep)
            {
                prep = 0;
                state = RoundState.oleada;
            }
        }
        
    }
}
