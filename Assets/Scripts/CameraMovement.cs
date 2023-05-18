using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 playerPosition;
    [SerializeField] [Range(0f, 1f)] private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] Vector3 offset = new Vector3(0f, 5f, -10f);

    [SerializeField] private Transform target;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    private void FixedUpdate()
    {
    }
}
