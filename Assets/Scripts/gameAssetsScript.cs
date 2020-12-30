using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameAssetsScript : MonoBehaviour
{
    private static gameAssetsScript instance;
    public static gameAssetsScript getGameAssetsScript()
    {
        return instance;
    }
    private void Awake()
    {
        instance = this;
    }
    public Sprite legDown;
    public Transform legObstacle1;
    public Transform straightLegObstacle1;
}
