using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerGen : MonoBehaviour
{

    [Header("Control mazmorras")]
    [SerializeField] int[] indexMazmorra;
    public void goDungeon(int numDungeon)
    {
        SceneManager.LoadScene(indexMazmorra[numDungeon]);
    }
}
