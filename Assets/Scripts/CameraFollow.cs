using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smothing = 6f;
    Vector3 offset;
    // Start is called before the first frame update
    private void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector3 targetCamPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPosition, smothing * Time.deltaTime);
    }
}
