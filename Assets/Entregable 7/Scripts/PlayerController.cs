using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 InitialPos = new Vector3 (0, 12, 0);

    private Rigidbody PlayerRigidbody;
    private float JumpForce = 5f;

    private float SkyLimit = 14f;

    private float MoneyCounter;

    //Audio
    public AudioClip BlipClip;
    public AudioClip BoingClip;
    public AudioClip BoomClip;
    public AudioClip BackgroundMusic;

    private AudioSource PlayerAudioSource;
    private AudioSource CameraAudioSource;

    //Sistemas de part�culas
    public ParticleSystem ExplosionParticleSystem;
    public ParticleSystem FireworksParticleSystem;

    void Start()
    {
        //Posici�n inicial
        transform.position = InitialPos;

        //Accedemos a la componente Rigidbody del Player que podr� ser modificada 
        PlayerRigidbody = GetComponent<Rigidbody>();

        //Accedemos al AudioSource de la Main Camera que recoge la m�sica de fondo del juego
        CameraAudioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        CameraAudioSource.PlayOneShot(BackgroundMusic , 1);
    }

    void Update()
    {
        //Si presionames la tecla Espaciadora nuestro Player sufre un peque�o impulso
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Aplicamos el eje en el cual va a saltar, la fuerza con la que va a saltar y por �ltimo aplicamos la fuerza de forma inmediata (tiene en cuenta la masa)
            PlayerRigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            CameraAudioSource.PlayOneShot(BoingClip, 1);
        }

        //L�mite superior
        if (transform.position.y > SkyLimit)
        {
            transform.position = new Vector3(transform.position.x, SkyLimit, transform.position.z);
        }
    }

    //Si el Player colisiona contra un obst�culo o contra el suelo pierde
    private void OnCollisionEnter(Collision otherCollider)
    {
        //L�mite inferior
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

            //Clip de explosi�n
            CameraAudioSource.PlayOneShot(BoomClip, 1);

            //FX de Explosi�n
            Instantiate(ExplosionParticleSystem, transform.position, ExplosionParticleSystem.transform.rotation);
            ExplosionParticleSystem.Play();
        }
    }

    //Contador de monedas
    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Money"))
        {
            MoneyCounter ++;
            Destroy(otherCollider.gameObject);
            Debug.Log($"�Tienes un total de {MoneyCounter} monedas, sigue as�!");
            CameraAudioSource.PlayOneShot(BlipClip, 1);
            FireworksParticleSystem.Play();
        }
    }


}
