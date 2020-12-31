using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreText : MonoBehaviour
{
    private Text score;
    private void Awake()
    {
        score = transform.Find("scoreText").GetComponent<Text>();
    }
    private void Update()
    {
        score.text = level.getInstance().getObstaclespassed().ToString();
    }
}
