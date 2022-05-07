using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public enum MouseLookType
    {
        XAxis = 0,
        YAxis = 1,
        XYAxis = 2
    }

    public MouseLookType mouseLookType = MouseLookType.XYAxis;
    public float sensivityVert = 3.0f;
    public float sensivityHor = 3.0f;

    private float _maxVertAngle = 45;
    private float _minVertAngle = -45;

    private float _rotationX = 0f;

    private void Start()
    {
        var rigidBody = GetComponent<Rigidbody>();
        if(rigidBody != null)
        {
            rigidBody.freezeRotation = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var rotationY = transform.localEulerAngles.y;

        Debug.Log(mouseLookType);
        switch (mouseLookType)
        {
            case MouseLookType.XAxis:
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensivityHor, 0);
                break;
            case MouseLookType.YAxis:
                _rotationX -= Input.GetAxis("Mouse Y") * sensivityVert;
                _rotationX = Mathf.Clamp(_rotationX, _minVertAngle, _maxVertAngle);
                rotationY = transform.localEulerAngles.y;
                transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
                break;
            default:
                _rotationX -= Input.GetAxis("Mouse Y") * sensivityVert;
                _rotationX = Mathf.Clamp(_rotationX, _minVertAngle, _maxVertAngle);
                rotationY = transform.localEulerAngles.y;
                rotationY += Input.GetAxis("Mouse X") * sensivityHor;
                transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
                break;
        }
    }
}
