using UnityEngine;

public class Camara : MonoBehaviour
{
    // Target de la camara para rotar alrededor
    public Vector3 _target = Vector3.zero;
    private Vector3 _newTarget = Vector3.zero;
    public GameObject focusItem;
    public UIEdificios UIEdificios;

    // Valores de velocidad  
    [SerializeField]
    private float
        _distance = 10f,
        _xSpeed = 250f,
        _ySpeed = 120f,
        _zoomSpeed = 90f,
        _yMinLimit = -20f,
        _yMaxLimit = 80f,
        _lerpSpeed = 5f,
        _distanceLerpSpeed = 5f; // Velocidad de interpolación de la distancia

    // Valores Clamp movimiento
    public Vector3 minFocus = new Vector3(-5f, -5f, -5f);
    public Vector3 maxFocus = new Vector3(5f, 5f, 5f);
    public Vector3 minmaxDistance = new Vector2(200f, 2500f);

    private float _x, _y;
    private float lastClickTime = 0f;
    private float doubleClickTime = 0.3f; // Tiempo máximo para detectar un doble clic
    private float targetDistance; // Distancia objetivo para la interpolación

    private GameObject elementoSeleccionado;
    private bool isFollowingElemento = false;

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
        targetDistance = _distance;
        if (focusItem != null)
        {
            SetTarget(focusItem.transform.position);
        }
        _x = _xSpeed * 0.03f;
        _y = ClampAngle(_y, _yMinLimit, _yMaxLimit);
    }

    private void LateUpdate()
    {
        if (isFollowingElemento && elementoSeleccionado != null)
        {
            SetTarget(elementoSeleccionado.transform.position);
            //targetDistance = 20f; // Ajusta esta distancia según sea necesario para el zoom
        }

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
                targetDistance = _distance; // Ignorar interpolacion
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

        // Cambiar focus al pulsar la F
        else if (Input.GetKey(KeyCode.F))
        {
            SeleccionarElemento(null); // Detener el seguimiento del coche cuando se usa otro input de la cámara
            if (focusItem != null)
            {
                SetTarget(focusItem.transform.position);
            }
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            SeleccionarElemento(null);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time - lastClickTime < doubleClickTime)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    // Si es edificio o papelera
                    if (hit.transform.CompareTag("Selectable"))
                    {
                        SeleccionarElemento(null);
                        // TODO: que haga zoom al objeto, pero tiene 0,0,0 dentro del padre, asi que ya vere
                        ZoomToHitPoint(hit.point, hit.transform);

                        Edificio edificio = hit.transform.GetComponent<Edificio>();
                        if(edificio != null)
                        {
                            UIEdificios.MostrarInformacionEdificio(edificio);
                        }

                        Papelera papelera = hit.transform.GetComponent<Papelera>();
                        if(papelera != null)
                        {
                            papelera.VaciarPapelera();
                        }
                    }
                    else if (hit.transform.GetComponent<Coche>() != null)
                    {
                        SeleccionarElemento(hit.transform.gameObject);
                        
                        ZoomToHitPoint(hit.point, hit.transform);
                    }
                    else if (hit.transform.GetComponent<Peaton>() != null)
                    {
                        SeleccionarElemento(hit.transform.gameObject);
                        
                        ZoomToHitPoint(hit.point, hit.transform);
                    }
                }
            }
            lastClickTime = Time.time;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            targetDistance = Mathf.Clamp(_distance - scroll * _zoomSpeed*10000 * Time.deltaTime, minmaxDistance.x, minmaxDistance.y);
        }

        // Actualizar la distancia de la cámara usando Lerp
        UpdateCameraDistance();

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

    public void ZoomToHitPoint(Vector3 hitPoint, Transform target)
    {
        focusItem = target.gameObject;
        SetTarget(hitPoint);
        targetDistance = Mathf.Clamp(20f, minmaxDistance.x, minmaxDistance.y); // Distancia de zoom deseada, ajusta según tu necesidad
    }

    private void UpdateCameraDistance()
    {
        _distance = Mathf.Lerp(_distance, targetDistance, _distanceLerpSpeed * Time.deltaTime);
    }

    public void SeleccionarElemento(GameObject elemento)
    {
        // Verificar si hay un elemento previamente seleccionado
        if (elementoSeleccionado != null)
        {
            Coche coche = elementoSeleccionado.GetComponent<Coche>();
            Peaton peaton = elementoSeleccionado.GetComponent<Peaton>();

            if (coche != null) coche.SetFocus(false);
            else if (peaton != null) peaton.SetFocus(false);
        }

        elementoSeleccionado = elemento;

        // Verificar si vamos a elemnto no seleccionable
        if (elementoSeleccionado != null)
        {
            Coche coche = elementoSeleccionado.GetComponent<Coche>();
            Peaton peaton = elementoSeleccionado.GetComponent<Peaton>();

            if (coche != null) coche.SetFocus(true);
            else if (peaton != null) peaton.SetFocus(true);

            isFollowingElemento = true;
        }
        else
        {
            isFollowingElemento = false;
        }
    }
}