using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Vector3 playerPosition;

    [Header("Camera Move")]
    public float extraMoveX;
    public float extraMoveY;

    // Start is called before the first frame update
    void Start()
    {
        playerPosition = PlayerController.instance.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        playerPosition = PlayerController.instance.transform.position;
        transform.position = new Vector3(playerPosition.x + extraMoveX, playerPosition.y + extraMoveY, transform.position.z);
    }
}
