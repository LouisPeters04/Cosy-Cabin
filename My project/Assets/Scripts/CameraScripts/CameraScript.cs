using Unity.Mathematics;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    #region REFERENCES

    [Header("REFERENCES")]
    [Tooltip("A reference to the cameras pivot. REQUIRED: Camera must know what to use as a pivot")]
    [SerializeField] private Transform cameraPivot;

    [Header("CAMERA SETTINGS")]
    [SerializeField] private float orbitSpeed;
    [SerializeField] private float panSpeed;
    [SerializeField] private float zoomSpeed;

    [SerializeField] private float orbitSmooth;
    [SerializeField] private float panSmooth;
    [SerializeField] private float zoomSmooth;

    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;

    float yaw;
    float pitch = 45f;
    float distance = 20f;

    float smoothYaw;
    float smoothPitch;
    float smoothDistance;

    Vector3 _targetPivotPos;
    Vector3 _smoothPivotPos;

    #endregion
    #region UNITY FUNCTIONS

    private void Start()
    {
        _targetPivotPos = cameraPivot.position;
        _smoothPivotPos = cameraPivot.position;

        smoothYaw = yaw;
        smoothPitch = pitch;
        smoothDistance = distance;
    }
    private void Update()
    {
        HandleOrbit();
        //HandlePan();
        HandleZoom();

        smoothYaw = Mathf.Lerp(smoothYaw, yaw, Time.deltaTime * orbitSmooth);
        smoothPitch = Mathf.Lerp(smoothPitch, pitch, Time.deltaTime * orbitSmooth);
        smoothDistance = Mathf.Lerp(smoothDistance, distance, Time.deltaTime * zoomSmooth);
        _smoothPivotPos = Vector3.Lerp(_smoothPivotPos, _targetPivotPos, Time.deltaTime * panSmooth);

        Quaternion rot = Quaternion.Euler(smoothPitch, smoothYaw, 0);
        Vector3 offset = rot * new Vector3(0, 0, -smoothDistance);

        transform.position = _smoothPivotPos + offset;
        transform.rotation = rot;
    }
    #endregion
    #region CAMERA SETTINGS FUNCTIONS
    private void HandleOrbit()
    {
        if (Input.GetMouseButton(1))
        {
            yaw += Input.GetAxis("Mouse X") * orbitSpeed * Time.deltaTime;
            pitch -= Input.GetAxis("Mouse Y") * orbitSpeed * Time.deltaTime;
            pitch = Mathf.Clamp(pitch, 20f, 80f);
        }
    }

    //private void HandlePan()
    //{
    //    if (Input.GetMouseButton(0))
    //    {
    //        float x = -Input.GetAxis("Mouse X") * panSpeed * Time.deltaTime;
    //        float Z = -Input.GetAxis("Mouse Y") * panSpeed * Time.deltaTime;

    //        _targetPivotPos += transform.right * x + transform.forward * Z;
    //    }
    //}

    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * zoomSpeed;
        distance = Mathf.Clamp(distance, minZoom, maxZoom);
    }
    #endregion
}
