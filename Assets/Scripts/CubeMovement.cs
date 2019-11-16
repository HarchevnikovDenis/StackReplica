using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    public static CubeMovement CurrentCube { get; private set; }
    public static CubeMovement LastCube { get; private set; }
    public MoveDirection MoveDirection;
    private bool isGoBack;

    [SerializeField] private float moveSpeed = 1.0f;

    private void OnEnable()
    {
        if (LastCube == null)
            LastCube = GameObject.Find("Home").GetComponent<CubeMovement>();

        CurrentCube = this;
        GetComponent<Renderer>().material.color = GetColor();

        transform.localScale = new Vector3(LastCube.transform.localScale.x, transform.localScale.y, LastCube.transform.localScale.z);
    }

    private Color GetColor()
    {
        return new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }

    public void Stop()
    {
        moveSpeed = 0.0f;
        float hangover = GetHangover();

        float max = MoveDirection == MoveDirection.Z ? LastCube.transform.localScale.z : LastCube.transform.localScale.x;
        if(Mathf.Abs(hangover) >= max)
        {
            LastCube = null;
            CurrentCube = null;
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

        float direction = hangover > 0 ? 1.0f : -1.0f;

        if(MoveDirection == MoveDirection.Z)
            SplitCubeOnZ(hangover, direction);
        else
            SplitCubeOnX(hangover, direction);

        LastCube = this;
    }

    private float GetHangover()
    {
        if (MoveDirection == MoveDirection.Z)
            return transform.position.z - LastCube.transform.position.z;
        else
            return transform.position.x - LastCube.transform.position.x;
    }

    private void SplitCubeOnX(float hangover, float direction)
    {
        float newSize = transform.localScale.x - Mathf.Abs(hangover);
        float fallingBlockSize = transform.localScale.x - newSize;

        float newXposition = LastCube.transform.position.x + (hangover / 2);
        transform.localScale = new Vector3(newSize, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(newXposition, transform.position.y, transform.position.z);

        float cubeEdge = transform.position.x + (newSize / 2 * direction);
        float fallingCubeZposition = cubeEdge + (fallingBlockSize / 2 * direction);

        SpawnDropCube(fallingCubeZposition, fallingBlockSize);
    }

    private void SplitCubeOnZ(float hangover, float direction)
    {
        float newSize = transform.localScale.z - Mathf.Abs(hangover);
        float fallingBlockSize = transform.localScale.z - newSize;

        float newZposition = LastCube.transform.position.z + (hangover / 2);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZposition);

        float cubeEdge = transform.position.z + (newSize / 2 * direction);
        float fallingCubeZposition = cubeEdge + (fallingBlockSize / 2 * direction);

        SpawnDropCube(fallingCubeZposition,fallingBlockSize);
    }

    private void SpawnDropCube(float posZ, float sizeZ)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        if (MoveDirection == MoveDirection.Z)
        {
            cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, sizeZ);
            cube.transform.position = new Vector3(transform.position.x, transform.position.y, posZ);
        }
        else
        {
            cube.transform.localScale = new Vector3(sizeZ, transform.localScale.y, transform.localScale.z);
            cube.transform.position = new Vector3(posZ, transform.position.y, transform.position.z);
        }

        cube.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
        cube.AddComponent<Rigidbody>();
        if (cube.transform.localScale.z == 0.0f || cube.transform.localScale.x == 0.0f)
            Destroy(cube);
        else
            Destroy(cube, 2.0f);
    }

    private void Update()
    {
        if (MoveDirection == MoveDirection.Z)
        {
            transform.position += transform.forward * Time.deltaTime * moveSpeed;
            if(transform.position.z > 2 && !isGoBack)
            {
                ChangeDirection();
                return;
            }
            
            if(transform.position.z < -2 && isGoBack)
            {
                ChangeDirection();
                return;
            }
        }
        else
        {
            transform.position += transform.right * Time.deltaTime * moveSpeed;
            if (transform.position.x > 2 && !isGoBack)
            {
                ChangeDirection();
                return;
            }

            if (transform.position.x < -2 && isGoBack)
            {
                ChangeDirection();
                return;
            }
        }
    }

    private void ChangeDirection()
    {
        isGoBack = !isGoBack;
        moveSpeed *= -1;
    }
}
