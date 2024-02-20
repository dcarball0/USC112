using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    // Target de la camara para rotar alrededor
    public Vector3 _target = Vector3.zero;
    private Vector3 _newTarget = Vector3.zero;
    public GameObject focusItem;

    // Valores de velocidad
    [SerializeField]
    private float
        _distance = 10f,
        _xSpeed = 250f,
        _ySpeed = 120f,
        _zoomSpeed = 90f,
        _yMinLimit = -20f,
        _yMaxLimit = 80f,
        _lerpSpeed = 5f;

    // Valores Clamp movimiento
    public Vector3 minFocus = new Vector3(-5f, -5f, -5f);
    public Vector3 maxFocus = new Vector3(5f, 5f, 5f);
    public Vector3 minmaxDistance = new Vector2(200f, 2500f);

    private float _x, _y;

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f)
            angle += 360f;
        if (angle > 360f)
            angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }

    private void Start()
    {
        SetTarget(focusItem.transform.position);
        _x = _xSpeed * 0.03f;
        _y = ClampAngle(_y, _yMinLimit, _yMaxLimit);
    }

    private void LateUpdate()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            var axisX = Input.GetAxis("Mouse X");
            var axisY = Input.GetAxis("Mouse Y");

            // ALT + LEFT CLICK = ROTAR ALREDEDOR
            if (Input.GetMouseButton(0))
            {
                _x += axisX * _xSpeed * 0.03f;
                _y -= axisY * _ySpeed * 0.03f;
                _y = ClampAngle(_y, _yMinLimit, _yMaxLimit);
            }
            // ALT + RIGHT CLICK = ZOOM
            else if (Input.GetMouseButton(1))
            {
                var num = (Mathf.Abs(axisX) <= Mathf.Abs(axisY)) ? axisY : axisX;
                num = -num * _zoomSpeed * 0.03f;
                _distance = Mathf.Clamp(_distance + num * (Mathf.Max(_distance, 0.02f) * 0.03f), minmaxDistance.x, minmaxDistance.y);
            }
            // ALT + MIDDLE CLICK = MOVER
            else if (Input.GetMouseButton(2))
            {
                var a = transform.rotation * Vector3.right;
                var a2 = transform.rotation * Vector3.up;
                var a3 = -a * axisX * _xSpeed * 0.02f;
                var b = -a2 * axisY * _ySpeed * 0.02f;

                SetTarget(_newTarget+(a3 + b) * (Mathf.Max(_distance, 0.04f) * 0.01f));
            }
        }

        // Cambiar focus al pulsar la F (TODO: cuando este mejor programado poder clickar en edificios y que haga zoom en ellos)
        else if (Input.GetKey(KeyCode.F))
        {
            SetTarget(focusItem.transform.position);
        }

        // Actualiza la posicion de la camara interpolando posicion vieja y nueva
        if(Vector3.Distance(_target, _newTarget)>0.1f)
            _target = Vector3.Lerp(_target, _newTarget, _lerpSpeed * Time.deltaTime);

        // Cambia la posición de la camara a la nueva posicion
        var rotation = Quaternion.Euler(_y, _x, 0f);
        var position = rotation * new Vector3(0f, 0f, -_distance) + _target;
        transform.SetPositionAndRotation(position, rotation);
    }

    /*
        Establecer el target al que la camara se va a mover poco a poco.
        Valores clampeados minFocus maxFocus
    */
    public void SetTarget(Vector3 target)
    {
        _newTarget.x = Mathf.Clamp(target.x, minFocus.x, maxFocus.x);
        _newTarget.y = Mathf.Clamp(target.y, minFocus.y, maxFocus.y);
        _newTarget.z = Mathf.Clamp(target.z, minFocus.z, maxFocus.z);

    }
}