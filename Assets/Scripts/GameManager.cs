using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private CubeSpawner[] spawner;
    private int spawnerIndex;
    private CubeSpawner currentSpawner;
    private bool isGameStart;
    [SerializeField] private TextMeshProUGUI TapToStartText;
    [SerializeField] private TextMeshProUGUI scoreText;
    private int score;


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
                
                if(!isGameStart)
                {
                    TapToStartText.gameObject.SetActive(false);
                    scoreText.gameObject.SetActive(true);
                }

                if (isGameStart)
                {
                    Camera.main.GetComponent<CameraFollowing>().posY += 0.1f;
                    score++;
                    scoreText.text = score.ToString();
                }

                isGameStart = true;
            }
        }
    }
}
