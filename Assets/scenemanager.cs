using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenemanager : MonoBehaviour
{
    public Button start;
    public Button quit;
    public Button highScore;

    // Start is called before the first frame update
    void Start()
    {
        start.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("TestScene");
        });
        highScore.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("HighScore");
        });
        quit.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }

    // Update is called once per frame
    void Update()
    {

    }

}
