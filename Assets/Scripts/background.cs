using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background : MonoBehaviour
{
    public Vector2 offset;

    public float speed;
    private Material material;
    // Start is called before the first frame update
    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }
    void Start()
    {
        offset = new Vector2(speed, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        material.mainTextureOffset += offset * Time.deltaTime;
    }
}
