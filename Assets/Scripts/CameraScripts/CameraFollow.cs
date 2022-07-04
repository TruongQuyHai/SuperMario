using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //[SerializeField] private float resetSpeed = 0.5f;
    [SerializeField] private float cameraSpeed = 0.3f;

    //public Bounds cameraBounds;

    private Transform target;

    private float offsetZ;
    private Vector3 lastTargetPosition;
    private Vector3 currentVelocity;

    private bool followPlayer;
    private Vector3 start;

    public void ResetPos()
    {
        transform.position = start;
    }

    void Awake()
    {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.size = new Vector2(Camera.main.aspect * 2f * Camera.main.orthographicSize, 15f);
        start = transform.position;
    }

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        lastTargetPosition = target.position;
        offsetZ = (transform.position - target.position).z;
        followPlayer = true;
    }

    void FixedUpdate()
    {
        if (followPlayer)
        {
            Vector3 aheadTargetPosition = target.position + Vector3.forward * offsetZ;
            if (aheadTargetPosition.x >= transform.position.x)
            {
                Vector3 newCameraPosition = Vector3.SmoothDamp(transform.position, aheadTargetPosition, ref currentVelocity, cameraSpeed);

                transform.position = new Vector3(newCameraPosition.x, transform.position.y, newCameraPosition.z);

                lastTargetPosition = target.position;
            }
        }
    }
}
