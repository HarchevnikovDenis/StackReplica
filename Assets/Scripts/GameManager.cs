using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private CubeSpawner[] spawner;
    private int spawnerIndex;
    private CubeSpawner currentSpawner;
    private bool isGameStart;

    private void Awake()
    {
        spawner = FindObjectsOfType<CubeSpawner>();
    }

    private void Update()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (CubeMovement.CurrentCube != null)
                    CubeMovement.CurrentCube.Stop();

                spawnerIndex = spawnerIndex == 0 ? 1 : 0;
                currentSpawner = spawner[spawnerIndex];

                currentSpawner.Spawn();
                if (isGameStart)
                    Camera.main.GetComponent<CameraFollowing>().posY += 0.1f;

                isGameStart = true;
            }
        }
    }
}
