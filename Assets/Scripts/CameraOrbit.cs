using UnityEngine;
using System.Collections;

public class CameraOrbit : MonoBehaviour
{

    protected Transform _XForm_Camera;
    protected Transform _XForm_Parent;

    protected Vector3 _LocalRotation;
    protected float _CameraDistance = 10f;

    public float MouseSensitivity = 4f;
    public float OrbitDampening = 10f;

    public bool CameraDisabled = false;

    // Use this for initialization
    void Start()
    {
        this._XForm_Camera = this.transform;
        this._XForm_Parent = this.transform.parent;
    }


    void LateUpdate()
    {
        if (!CameraDisabled)
        {

            bool drag = false;

            if (Application.isMobilePlatform)
            {
                drag = Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved;

                if (drag)
                {
                    // Get movement of the finger since last frame
                    Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

                    // Move object across XY plane
                    _LocalRotation.x += touchDeltaPosition.x * .1f;
                    _LocalRotation.y -= touchDeltaPosition.y * .1f;

                    if (_LocalRotation.y < 0f)
                        _LocalRotation.y = 0f;
                    else if (_LocalRotation.y > 25)
                        _LocalRotation.y = 25;
                }

            }
            //Rotation of the Camera based on Mouse Coordinates
            else if
                (Input.GetAxis("Mouse X") != 0 && Input.GetMouseButton(0) || Input.GetAxis("Mouse Y") != 0 && Input.GetMouseButton(0))
            {
                _LocalRotation.x += Input.GetAxis("Mouse X") * MouseSensitivity;
                _LocalRotation.y -= Input.GetAxis("Mouse Y") * MouseSensitivity;

                //Clamp the y Rotation to horizon and not flipping over at the top
                if (_LocalRotation.y < 0f)
                    _LocalRotation.y = 0f;
                else if (_LocalRotation.y > 25)
                    _LocalRotation.y = 25;
            }
       }

         // Rotation for the car
        Quaternion QT = Quaternion.Euler(_LocalRotation.y, _LocalRotation.x, 0);
        this._XForm_Parent.rotation = Quaternion.Lerp(this._XForm_Parent.rotation, QT, Time.deltaTime * OrbitDampening);

    }

}