using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private MoveDirection moveDirection;

    public void Spawn()
    {
        GameObject cube = Instantiate(cubePrefab);

        if (CubeMovement.LastCube != null && CubeMovement.LastCube.gameObject != GameObject.Find("Home"))
        {
            float x = moveDirection == MoveDirection.X ? transform.position.x : CubeMovement.LastCube.transform.position.x;
            float z = moveDirection == MoveDirection.Z ? transform.position.z : CubeMovement.LastCube.transform.position.z;
            
            cube.transform.position = new Vector3(x, CubeMovement.LastCube.transform.position.y + cubePrefab.transform.localScale.y, z);
        }
        else
        {
            cubePrefab.transform.position = transform.position;
        }

        cube.GetComponent<CubeMovement>().MoveDirection = moveDirection;
    }
}

public enum MoveDirection
{
    X,
    Z
}
