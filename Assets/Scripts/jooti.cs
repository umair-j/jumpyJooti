using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jooti : MonoBehaviour
{
    private const float jumpAmount = 80f;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            jump();
        }
    }
    private void jump()
    {
        rb.velocity = Vector2.up * jumpAmount;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Dead!");
    }
}
