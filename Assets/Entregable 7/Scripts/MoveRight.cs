using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRight : MonoBehaviour
{
    public Vector3 Direction = Vector3.right;
    public float Speed = 5f;

    private float XLimit = 12f;

    private PlayerController PlayerControllerScript;

    void Start()
    {
        PlayerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Los obstáculos se mueven constantemente hacia la derecha (Siempre que esté vivo el Player)
        if(!PlayerControllerScript.gameOver)
        {
            transform.Translate(Direction * Speed * Time.deltaTime);
        }

        //Limites
        if (transform.position.x > XLimit)
        {
            Destroy(gameObject);
        }

        if (transform.position.x < -XLimit)
        {
            Destroy(gameObject);
        }
    } 
}
