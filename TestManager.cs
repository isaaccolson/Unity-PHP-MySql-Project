using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public int subjectID;
    public static TestManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            subjectID = -1;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
