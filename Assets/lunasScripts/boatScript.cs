using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boatScript : MonoBehaviour
{
    [SerializeField]
    private GameObject Wheel;
    public float healSpeed, damageSpeed;
    public float boatHealth;
    int rotDir;
    void Start()
    {
        
    }


    void Update()
    {
        rotDir = transform.rotation.z < 0 ? 1 : -1;

        boatHealth -= damageSpeed*rotDir;
        transform.eulerAngles = new Vector3(0, 0, boatHealth);
    }

    public void rotateBoat(float wheelRot)
    {
        boatHealth += healSpeed*rotDir;
        Mathf.Clamp(boatHealth, -45, 0);

        
    }
}
