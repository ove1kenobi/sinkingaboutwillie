using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScoreManager : MonoBehaviour
{
    public Button main;
    // Start is called before the first frame update
    void Start()
    {

        main.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Menu");
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
