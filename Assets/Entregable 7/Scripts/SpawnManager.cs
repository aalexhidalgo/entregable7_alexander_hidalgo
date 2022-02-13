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

    public Vector3 RandomSpawnPos()
    {
        float RandomSpawnPosX = Random.Range (0, 2);
        float RandomSpawnPosY = Random.Range (YSuperiorLimit, YGroundLimit);

        /*if (RandomSpawnPosX == 0)
        {
            return new Vector3 (-XLimit, RandomSpawnPosY, ZLimit);
        }

        if (RandomSpawnPosX == 1)
        {
            return new Vector3 (XLimit, RandomSpawnPosY, ZLimit);
            //MoveRightScript.Direction *= -1 o girar con un quaternion;
        }
        */
    }

    public void SpawnRandomObject()
    {
        if (!PlayerControllerScript.gameOver)
        {
            int RandomIndex = Random.Range(0, targetPrefabs.Length);
            Instantiate(targetPrefabs[RandomIndex], RandomSpawnPos(), targetPrefabs[RandomIndex].transform.rotation);
        }
    }
}
