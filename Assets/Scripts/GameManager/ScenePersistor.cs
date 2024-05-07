using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersistor : MonoBehaviour
{
    ScenePersistor instance = null;
    private void Start() {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
