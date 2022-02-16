using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinObject : MonoBehaviour
{
    private float spinSpeed;

    private PlayerController PlayerControllerScript;

    void Start()
    {
        PlayerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    void Update()
    {
        //Girará constantemente en el eje Y siempre que esté vivo el Player
        if (!PlayerControllerScript.gameOver)
        {
            transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
        }
    }
}
