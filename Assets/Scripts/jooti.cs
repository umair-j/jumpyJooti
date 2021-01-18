using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jooti : MonoBehaviour
{
    private State state;
    private static jooti instance;
    private const float jumpAmount = 80f;
    private Rigidbody2D rb;
    
    private enum State
    {
        waiting,
        playing,
        dead,
    }

    private void Awake()
    {
        
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        state = State.waiting;
        rb.bodyType = RigidbodyType2D.Static;
    }

    public static jooti returnJooti()
    {
        return instance;
    }
    public event EventHandler onDied;
    public event EventHandler onStartPlaying;

    // Update is called once per frame
    void Update()
    {
        switch(state){
            case State.waiting:

                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    
                    state = State.playing;
                    rb.bodyType = RigidbodyType2D.Dynamic;
                    jump();
                    if(onStartPlaying != null)
                    {
                        onStartPlaying(this, EventArgs.Empty);
                    }
                }
                break;

            case State.playing:

                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    jump();
                }
                break;

            case State.dead:
                break;
        }
        if(this.transform.position.y>50 || this.transform.position.y < -50)
        {
            if (onDied != null)
            {
                onDied(this, EventArgs.Empty);
            }
        }
    }
    private void jump()
    {
        rb.velocity = Vector2.up * jumpAmount;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Dead!");
    }
    private void OnCollisionEnter2D(Collision2D collider)
    {
        rb.bodyType = RigidbodyType2D.Static;
        if (onDied != null)
        {
            onDied(this, EventArgs.Empty);
        }
    }
}
