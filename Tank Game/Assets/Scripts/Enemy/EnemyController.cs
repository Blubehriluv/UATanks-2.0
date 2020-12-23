using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public float movespeed;
    public float rotatespeed;

    void Start()
    {
    }

    void Update()
    {
        transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * movespeed);
        transform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * rotatespeed, 0);
    
    }
}