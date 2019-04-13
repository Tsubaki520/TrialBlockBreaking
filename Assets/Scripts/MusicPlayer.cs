using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer s_instance;

    private void Awake()
    {
        if (s_instance != null)
        {
            GameObject.Destroy(gameObject);
        }
        else
        {
            s_instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }
}
