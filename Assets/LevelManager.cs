using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    void Start()
    {
        AudioManager.instance.PlayBGMAudio("BGM_Quirky Companion");
    }
}
