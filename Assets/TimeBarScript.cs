using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class TimeBarScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Variables v = GameObject.Find("HelmTimer").GetComponent<Variables>();
        Transform t = gameObject.transform;
        Vector3 s = t.localScale;

        float x = (float)v.declarations.Get("HelmHealth");

        t.localScale = new Vector3(x, s.y, s.z);
    }
}
