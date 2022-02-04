using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody PlayerRigidbody;
    private float JumpForce = 5f;

    private float SkyLimit = 14f;

    private float MoneyCounter;

    // Start is called before the first frame update
    void Start()
    {
        //Accedemos a la componente Rigidbody del Player que podrá ser modificada 
        PlayerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Si presionames la tecla Espaciadora nuestro Player sufre un pequeño impulso
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Aplicamos el eje en el cual va a saltar, la fuerza con la que va a saltar y por último aplicamos la fuerza de forma inmediata
            PlayerRigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }

        //Límite superior
        if (transform.position.y > SkyLimit)
        {
            transform.position = new Vector3(transform.position.x, SkyLimit, transform.position.z);
        }
    }

    //Si el Player colisiona contra un obstáculo pierde
    private void OnCollisionEnter(Collision otherCollider)
    {
        //Límite inferior
        if (gameObject.CompareTag("Player") && otherCollider.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
            Debug.Log("GAME OVER");
        }

        //Si colisiona contra una bomba fin del juego
        if (gameObject.CompareTag("Player") && otherCollider.gameObject.CompareTag("Bomb"))
        {
            Debug.Log($"GAME OVER");
            Time.timeScale = 0;
        }
    }

    //Contador de monedas
    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Money"))
        {
            MoneyCounter ++;
            Destroy(otherCollider.gameObject);
            Debug.Log($"¡Tienes un total de {MoneyCounter} monedas, sigue así!");
        }
    }


}
