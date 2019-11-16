using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    public static CubeMovement CurrentCube { get; private set; }
    public static CubeMovement LastCube { get; private set; }

    [SerializeField] private float moveSpeed = 1.0f;

    private void OnEnable()
    {
        if (LastCube == null)
            LastCube = GameObject.Find("Home").GetComponent<CubeMovement>();

        CurrentCube = this;
        GetComponent<Renderer>().material.color = GetColor();
    }

    private Color GetColor()
    {
        return new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }

    public void Stop()
    {
        moveSpeed = 0.0f;
        float stopZpos = transform.position.z - LastCube.transform.position.z;

        float direction = stopZpos > 0 ? 1.0f : -1.0f;
        SplitCubeOnZ(stopZpos, direction);
    }

    private void SplitCubeOnZ(float stopZpos, float direction)
    {
        float newSize = transform.localScale.z - Mathf.Abs(stopZpos);
        float fallingBlockSize = transform.localScale.x - newSize;

        float newZposition = LastCube.transform.position.z + (stopZpos / 2);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZposition);

        float cubeEdge = transform.position.z + (newSize / 2 * direction);
        float fallingCubeZposition = cubeEdge + (fallingBlockSize / 2 * direction);

        SpawnDropCube(fallingCubeZposition,fallingBlockSize);
    }

    private void SpawnDropCube(float posZ, float sizeZ)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, sizeZ);
        cube.transform.position = new Vector3(transform.position.x, transform.position.y, posZ);

        cube.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
        cube.AddComponent<Rigidbody>();
        Destroy(cube, 2.0f);
    }

    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * moveSpeed;
    }
}
