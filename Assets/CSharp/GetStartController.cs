using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class GetStartController : MonoBehaviour
{
    // Start is called before the first frame update
    private string id;
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            int j = UnityEngine.Random.Range(0, 61);
            if (j < 10)
            {
                id += Convert.ToString(j);
            }
            else if (j < 36)
            {
                id += (char)(j + 55);
            }
            else
            {
                id += (char)(j + 61);
            }

        }
        PlayerPrefs.SetString("ID", id);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
