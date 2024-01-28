using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenemanager : MonoBehaviour
{
    public Button start;
    
    // Start is called before the first frame update
    void Start()
    {
       start.onClick.AddListener(() =>
       {
           SceneManager.LoadScene("BazoScene");
       });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
