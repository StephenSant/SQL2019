using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldUserInfo : MonoBehaviour
{
    public static string username;

    public static HoldUserInfo instance = null;

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}