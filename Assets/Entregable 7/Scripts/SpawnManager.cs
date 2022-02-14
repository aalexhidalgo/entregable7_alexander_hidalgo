using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] targetPrefabs;

    public float StartDelay = 0.1f;
    public float RepeatRate = 0.5f;

    private PlayerController PlayerControllerScript;

    private float XLimit = 10f;
    private float YSuperiorLimit = 14f;
    private float YGroundLimit = 0.75f;
    private float ZLimit = 0f;

    void Start()
    {
        PlayerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnRandomObject", StartDelay, RepeatRate);
    }

    void Update()
    {
        
    }

    public Vector3 RandomSpawnPos(int RandomSpawnPosX)
    {
        
        float RandomSpawnPosY = Random.Range (YSuperiorLimit, YGroundLimit);

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
        if (!PlayerControllerScript.gameOver)
        {
            int RandomIndex = Random.Range(0, targetPrefabs.Length); 

            //Variable que guarda de forma Random su spawn en el límite derecho o izquierdo
            int RandomSpawnPosX = Random.Range(0, 2);
            GameObject Prefab = Instantiate(targetPrefabs[RandomIndex], RandomSpawnPos(RandomSpawnPosX), targetPrefabs[RandomIndex].transform.rotation);
            
            //Si los obstáculos aparecen en el lado derecho de la pantalla accedemos a la component MoveRight de estos para invertir su dirección
            if (RandomSpawnPosX == 1)
            {
                Prefab.GetComponent<MoveRight>().Direction *= -1;
            }
        }
    }
}
