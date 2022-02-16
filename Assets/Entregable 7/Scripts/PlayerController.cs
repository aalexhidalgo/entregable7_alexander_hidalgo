using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 InitialPos = new Vector3 (0, 12, 0);

    private Rigidbody PlayerRigidbody;
    private float JumpForce = 5f;
    private float DownForce = 0.1f;

    private float SkyLimit = 14f;

    private float MoneyCounter;

    //Audio
    public AudioClip BlipClip;
    public AudioClip BoingClip;
    public AudioClip BoomClip;

    private AudioSource PlayerAudioSource;
    private AudioSource CameraAudioSource;

    //Sistemas de partículas
    public ParticleSystem ExplosionParticleSystem;
    public ParticleSystem FireworksParticleSystem;

    public bool gameOver;

    //Extra
    private SpawnManager SpawnManagerScript;

    void Start()
    {
        //Empieza el juego 
        gameOver = false;
        //Posición inicial
        transform.position = InitialPos;

        //Accedemos a la componente Rigidbody y AudioSource del Player
        PlayerRigidbody = GetComponent<Rigidbody>();
        PlayerAudioSource = GetComponent<AudioSource>();

        //Accedemos al AudioSource de la Main Camera que recoge la música de fondo del juego
        CameraAudioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();

        //Extra
        SpawnManagerScript = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    void Update()
    {
        //Si presionames la tecla Espaciadora nuestro Player sufre un pequeño impulso (Siempre que esté vivo el Player)
        if (Input.GetKeyDown(KeyCode.Space) & !gameOver)
        {
            //Aplicamos el eje en el cual va a saltar, la fuerza con la que va a saltar y por último aplicamos la fuerza de forma inmediata (tiene en cuenta la masa)
            PlayerRigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            PlayerAudioSource.PlayOneShot(BoingClip, 1);
        }

        //Límite superior
        if (transform.position.y > SkyLimit)
        {
            /*De esta manera no solo no traspasa el límite sino que empuja el globo hacia abajo, 
             ya que con el segundo método se quedaba estático en el aire durante unos segundos*/
            PlayerRigidbody.AddForce(Vector3.down * DownForce, ForceMode.Impulse);

            //Segundo método: transform.position = new Vector3(transform.position.x, SkyLimit, transform.position.z);
        }
    }

    //Si el Player colisiona contra un obstáculo o contra el suelo pierde
    private void OnCollisionEnter(Collision otherCollider)
    {
        //Límite inferior
        if (gameObject.CompareTag("Player") && otherCollider.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
            Debug.Log("GAME OVER");
            gameOver = true;
            //Extra: Si pierdes también se desactiva la música de fondo
            CameraAudioSource.Pause();
        }

        //Si colisiona contra una bomba fin del juego
        if (gameObject.CompareTag("Player") && otherCollider.gameObject.CompareTag("Bomb"))
        {
            Debug.Log($"GAME OVER");

            //Clip de explosión
            PlayerAudioSource.PlayOneShot(BoomClip, 1);

            //FX de Explosión
            Instantiate(ExplosionParticleSystem, transform.position, ExplosionParticleSystem.transform.rotation);
            ExplosionParticleSystem.Play();

            gameOver = true;
            //Extra: Si pierdes también se desactiva la música de fondo
            CameraAudioSource.Pause();

            //Extra: Si choca contra la bomba, freezeamos su posición para evitar un doble mensaje de "GAME OVER" al chocar luego contra el suelo
            PlayerRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    //Contador de monedas (+1 cada vez)
    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Money"))
        {
            MoneyCounter ++;
            Destroy(otherCollider.gameObject);
            Debug.Log($"¡Tienes un total de {MoneyCounter} monedas, sigue así!");

            //Clip que indica que se ha recogido la moneda
            PlayerAudioSource.PlayOneShot(BlipClip, 1);

            //FX de Fuegos artificiales
            Instantiate(FireworksParticleSystem, transform.position, FireworksParticleSystem.transform.rotation);
            FireworksParticleSystem.Play();

            //Extra: Si el jugador consigue 10 monedas los obstáculos comienzan a aparecer de forma más rápida
            if (MoneyCounter == 10)
            {
                SpawnManagerScript.RepeatRate *= 0.1f;
            }
        }
    }


}
