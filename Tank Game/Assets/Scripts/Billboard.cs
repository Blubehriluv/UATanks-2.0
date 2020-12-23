using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{

    public Camera cameraToLookAt;
    // Start is called before the first frame update
    void Start()
    {
        cameraToLookAt = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void LateUpdate()
    {
        transform.LookAt(cameraToLookAt.transform);
    }
}
