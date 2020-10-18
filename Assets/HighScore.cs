using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMesh>().text = $"HIGH SCORE:\n {PlayerPrefs.GetInt("High Score")}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
