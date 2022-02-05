using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRight : MonoBehaviour
{
    public float Speed = 5f;

    private float XLimit = 10f;

    private PlayerController PlayerControllerScript;

    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerControllerScript.gameOver)
        {
            transform.Translate(Vector3.right * Speed * Time.deltaTime);
        }

        if (transform.position.y > XLimit)
        {
            Destroy(gameObject);
        }

        if (transform.position.x < -XLimit)
        {
            Destroy(gameObject);
        }
    }
}
