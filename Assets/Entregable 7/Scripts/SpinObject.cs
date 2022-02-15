using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinObject : MonoBehaviour
{
    public float spinSpeed;

    private PlayerController PlayerControllerScript;

    void Start()
    {
        PlayerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    void Update()
    {
        if (!PlayerControllerScript.gameOver)
        {
            transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
        }
    }
}
