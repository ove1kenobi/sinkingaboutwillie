using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New stat preset", menuName = "Stat preset")]
public class playerStats : ScriptableObject
{
    public float moveSpeed, runAcceleration, runDecceleration, runVelocity, frictionAmount;
    [Space(10)]
    public float jumpHeight;
    public float gravityScale;
    [Space(10)]
    public float throwForce, wheelRotSpeed;
}
