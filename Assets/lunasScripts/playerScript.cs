using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class playerScript : MonoBehaviour
{
    public playerStats stats;

    public Transform armHolder, grabPos, Wheel;
    private Rigidbody2D rb;
    public Animator bodyAnim, armAnim;
    private SpriteRenderer bodyRenderer;
    private GameObject heldObject;
    private Rigidbody2D heldRb;
    public boatScript boat;

    [Space(10)]
    private Vector3 mousePos;
    public Vector3 groundCheckSize, groundCheckOffset;
    private float wheelGrabbedStartRot, wheelStartRot;

    [Space(10)]
    public Vector3 grabCheckSize;
    private float x;

    private bool spriteFlipped, grounded, grabbing, grabbingWheel;

    [Space(10)]
    public LayerMask groundLayer, grabableLayer, wheelLayer;

    private Vector3 mouseDelta = Vector3.zero;
    private Vector3 lastMousePosition = Vector3.zero;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bodyRenderer = bodyAnim.transform.Find("body").GetComponent<SpriteRenderer>();
        rb.gravityScale = stats.gravityScale;
    }

    void Update()
    {
        #region movement
        grounded = Physics2D.OverlapBox((transform.position + groundCheckOffset), groundCheckSize, 0, groundLayer);
        if (grounded && Input.GetButtonDown("Jump"))
            Jump();

        Run();
        #endregion

        Variables v = GameObject.Find("HelmTimer").GetComponent<Variables>();
        Transform wheel = GameObject.Find("Wheel").GetComponent<Transform>();
        float dist = (gameObject.GetComponent<Transform>().position - wheel.position).sqrMagnitude;
        if (grabbingWheel)
        {
            if(dist > 30.0) 
            {
                if (v != null) v.declarations.Set("IsGrabbed", false);
                releaseHold();
            }
        } else if (v != null) v.declarations.Set("IsGrabbed", false);
        
        #region arms
        moveArms();
        if (Input.GetButtonDown("Grab")) Grab();
        armAnim.SetBool("Grabbing", grabbing);

        if (Input.GetButton("Grab")) Hold();
        else releaseHold();

        mouseDelta = grabPos.position - lastMousePosition;
        lastMousePosition = grabPos.position;
        #endregion
    }

    private void moveArms()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x, mousePos.y, 0);
        Vector3 armRot = (mousePos - transform.position).normalized;

        float radvalue = Mathf.Atan2(armRot.y, armRot.x);
        float angle = radvalue * (180 / Mathf.PI);

        armHolder.eulerAngles = new Vector3(0f, 0f, angle);
    }

    private void Grab()
    {
        grabbing = true;
        Collider2D[] overlappedGrabable = Physics2D.OverlapBoxAll(grabPos.position, grabCheckSize, 0, grabableLayer);
        if (overlappedGrabable.Length != 0)
        {
            heldObject = overlappedGrabable[0].gameObject;
            heldRb = heldObject.transform.GetComponent<Rigidbody2D>();
        }
        else
        { 
            heldObject = null;
            grabbingWheel = Physics2D.OverlapBox(grabPos.position, grabCheckSize, 0, wheelLayer);
            if (grabbingWheel)
            {
                wheelGrabbedStartRot = armHolder.eulerAngles.z;
                wheelStartRot = Wheel.transform.eulerAngles.z;

                Variables v = GameObject.Find("HelmTimer").GetComponent<Variables>();
                if (v != null) 
                {
                    v.declarations.Set("IsGrabbed", true);
                }
            }

        }
    }
    private void Hold()
    {
        if(heldObject != null)
        {
            //heldObject.transform.position = grabPos.position;
            heldRb.position = grabPos.position;
            heldRb.velocity = new Vector2(0, 0);
        }
        if (grabbingWheel)
        {
            Wheel.transform.rotation = Quaternion.Euler(new Vector3(0,0, wheelStartRot+(wheelGrabbedStartRot- armHolder.eulerAngles.z) * stats.wheelRotSpeed));
            
            boat.rotateBoat(wheelGrabbedStartRot - armHolder.eulerAngles.z);
        }
    }
    private void releaseHold()
    {
        grabbing = false;
        if (heldObject != null)
        {
            heldRb.AddForce(mouseDelta * (stats.throwForce*1000));
        }
        grabbingWheel = false;
        heldObject = null;
        heldRb = null;
    }

    private void Run()
    {
        x = Input.GetAxis("Horizontal");

        bodyAnim.SetBool("Moving", x < 0f ? true : x > 0f ? true : false);


        float targetSpeed = x * stats.moveSpeed;
        float speedDif = targetSpeed - rb.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > .01f) ? stats.runAcceleration : stats.runDecceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, stats.runVelocity) * Mathf.Sign(speedDif);

        rb.AddForce(movement * transform.right);

        spriteFlipped = x < 0f ? true : x > 0f ? false : spriteFlipped;
        bodyRenderer.flipX = spriteFlipped;
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * (stats.jumpHeight));
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube((transform.position + groundCheckOffset), groundCheckSize);
        Gizmos.DrawWireCube(grabPos.position, grabCheckSize);
    }
}
