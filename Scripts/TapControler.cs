using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class TapControler : MonoBehaviour {
    public delegate void PlayerDelegate();
    public static event PlayerDelegate OnPlayerDied;
    public static event PlayerDelegate OnPlayerScored;
    GameManager game;

    public float tapforce = 30f;
   

    Rigidbody2D body;

    public bool IsGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask WhatIsGround;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        game = GameManager.Instance;
        body.simulated = false;
    }
    void Update()
    {
        if (Input.GetMouseButton(0) && IsGrounded)
        {           
            body.velocity = Vector2.up * tapforce;
            SoundCTRL.PlaySound("Jump");
        }
        /*if(Input.GetMouseButton(1))
        {
            body.velocity = Vector2.right * tapforce;
        }*/
    }
    void OnEnable()
    {
        GameManager.OnGameStarted += OnGameStarted;
        GameManager.OnGameOverConfirmed += OnGameOverConfirmed;
    }
    void OnDisible()
    {
        GameManager.OnGameStarted -= OnGameStarted;
        GameManager.OnGameOverConfirmed -= OnGameOverConfirmed;
    }

    void FixedUpdate()
    {
        IsGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius,WhatIsGround );
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "ScoreZone") {
            SoundCTRL.PlaySound("Coin");
            OnPlayerScored();
        }
        if (collider.gameObject.tag == "DeadZone")
        {
            SoundCTRL.PlaySound("Dead");
            body.simulated = false;
            OnPlayerDied();
        }
    }
    void OnGameStarted()
    {
        body.velocity = Vector3.zero;
        body.simulated = true;
    }
    void OnGameOverConfirmed()
    {
        transform.localPosition = new Vector3(-2, -3);
    }
}
