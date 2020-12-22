using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    public float mouseSensitivity;

    private float xAxisClamp = 0;

    public Transform tankNozzle;

    public Transform tank;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        RotateCamera();
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float rotAmountX = mouseX * mouseSensitivity;
        float rotAmountY = mouseY * mouseSensitivity;

        xAxisClamp -= rotAmountY;

        Vector3 rotTankCanon = tankNozzle.transform.rotation.eulerAngles;
        Vector3 rotTank = tank.transform.rotation.eulerAngles;

        rotTankCanon.x -= rotAmountY;
        rotTankCanon.z = 0;
        rotTank.y += rotAmountX;

        if (xAxisClamp > 10)
        {
            xAxisClamp = 10;
            rotTankCanon.x = 10;
        }
        else if (xAxisClamp < -10)
        {
            xAxisClamp = -10;
            rotTankCanon.x = 350;
        }

        tankNozzle.rotation = Quaternion.Euler(rotTankCanon);
        tank.rotation = Quaternion.Euler(rotTank);
    }
}
