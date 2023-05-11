using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private void Start()
    {
        CinemachineCore.GetInputAxis = GetAxisCustom;
    }

    private float GetAxisCustom(string axisName)
    {
        return axisName switch
        {
            "Mouse X" when Input.GetMouseButton(0) => Input.GetAxis("Mouse X"),
            "Mouse X" => 0,
            "Mouse Y" when Input.GetMouseButton(0) => Input.GetAxis("Mouse Y"),
            "Mouse Y" => 0,
            _ => Input.GetAxis(axisName)
        };
    }
}