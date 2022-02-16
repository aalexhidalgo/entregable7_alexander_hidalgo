using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //Lista que guarda los prefabs de los obstáculos
    public GameObject[] TargetPrefabs;

    //Timers
    public float StartDelay = 0.1f;
    public float RepeatRate = 1f;

    private PlayerController PlayerControllerScript;

    //Límites
    private float XLimit = 10f;
    private float YSuperiorLimit = 14f;
    private float YGroundLimit = 1f;
    private float ZLimit = 0f;

    void Start()
    {
        PlayerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnRandomObject", StartDelay, RepeatRate);
    }

    public Vector3 RandomSpawnPos(int RandomSpawnPosX)
    {
        //Variable que guarda de forma Random su spawn en el límite superior o inferior
        float RandomSpawnPosY = Random.Range (YSuperiorLimit, YGroundLimit);

        //Le indicamos que si vale 0 aparezca en el límite izquierdo, mientras que si vale 1 (else) lo haga en el límite derecho
        if (RandomSpawnPosX == 0)
        {
            return new Vector3 (-XLimit, RandomSpawnPosY, ZLimit);
        }

        else
        {
            return new Vector3 (XLimit, RandomSpawnPosY, ZLimit);
        }
        
    }

    public void SpawnRandomObject()
    {
        //Solo se ejecutará mientras el jugador esté vivo
        if (!PlayerControllerScript.gameOver)
        {
            //Variable que guarda de forma Random si aparece un Prefab o otro dependiendo de la longuitud de la lista
            int RandomIndex = Random.Range(0, TargetPrefabs.Length);

            //Variable que guarda de forma Random su spawn en el límite derecho o izquierdo
            int RandomSpawnPosX = Random.Range(0, 2);
            
            //Instanciamos la lista de Prefabs que guardamos en una variable para después modificarla
            GameObject Prefabs = Instantiate(TargetPrefabs[RandomIndex], RandomSpawnPos(RandomSpawnPosX), TargetPrefabs[RandomIndex].transform.rotation);

            //Si los obstáculos aparecen en el lado derecho de la pantalla accedemos a la component MoveRight de estos para invertir su dirección
            if (RandomSpawnPosX == 1)
            {
                Prefabs.GetComponent<MoveRight>().Direction *= -1;
            }
        }
    }
}
