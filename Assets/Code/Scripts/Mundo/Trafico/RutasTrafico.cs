using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RutasTrafico : MonoBehaviour
{
    public List<Transform> wayPoints; // Lista de waypoints sin SerializeField
    List<Vector3> deviatedPositions = new List<Vector3>();
    [SerializeField]
    float moveSpeed = 10f,
          rotationSpeed = 10f;
    [SerializeField] float deviationDistance = 1f; // Distancia fija de desviación
    [SerializeField] bool deviateRight = true; // Elige si desviarse a la derecha o izquierda desde el Inspector
    [SerializeField] float busStopTime = 10f; // Tiempo que el autobús se queda en la marquesina
    [SerializeField] Transform entryWaypoint; // Waypoint de entrada al edificio

    int waypointIndex = 0;
    bool isStopped = false;
    bool isEnteringBuilding = false;

    void Start()
    {
        if (wayPoints != null && wayPoints.Count > 0)
        {
            ComputeDeviatedPositions();
            if (deviatedPositions.Count > 0)
            {
                waypointIndex = Random.Range(0, deviatedPositions.Count - 1); // Seleccionar un índice aleatorio de waypoint, evitando el último índice
                Vector3 spawnPosition = GetRandomPointBetweenWaypoints(waypointIndex, waypointIndex + 1);
                transform.position = spawnPosition;
            }
        }
    }

    void Update()
    {
        if (!isStopped && !isEnteringBuilding && waypointIndex < deviatedPositions.Count)
        {
            MoveTowardsWaypoint();
        }
    }

    void ComputeDeviatedPositions()
    {
        for (int i = 0; i < wayPoints.Count; i++)
        {
            Vector3 currentWaypointPosition = wayPoints[i].position;
            Vector3 directionToNext = Vector3.zero;
            if (i < wayPoints.Count - 1)
            {
                directionToNext = (wayPoints[i + 1].position - currentWaypointPosition).normalized;
            }
            else
            {
                directionToNext = (currentWaypointPosition - wayPoints[i - 1].position).normalized;
            }
            Vector3 perpendicular = Vector3.Cross(directionToNext, Vector3.up).normalized;

            if (!deviateRight)
            {
                perpendicular = -perpendicular;
            }

            Vector3 deviatedPosition = currentWaypointPosition + perpendicular * deviationDistance;
            deviatedPositions.Add(deviatedPosition);
        }
    }

    Vector3 GetRandomPointBetweenWaypoints(int index1, int index2)
    {
        Vector3 point1 = deviatedPositions[index1];
        Vector3 point2 = deviatedPositions[index2];
        float randomFactor = Random.Range(0f, 1f);
        return Vector3.Lerp(point1, point2, randomFactor);
    }

    void MoveTowardsWaypoint()
    {
        Vector3 targetPosition = deviatedPositions[waypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        Vector3 directionToTarget = targetPosition - transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, directionToTarget, rotationSpeed * Time.deltaTime, 0.0f));

        if (Vector3.Distance(transform.position, targetPosition) < 1)
        {
            // Verificar si el waypoint actual es una marquesina
            if (wayPoints[waypointIndex].CompareTag("ParadaBus"))
            {
                StartCoroutine(StopAtBusStop());
            }

            waypointIndex += 1;
            if (waypointIndex >= deviatedPositions.Count)
                waypointIndex = 0;
        }
    }

    IEnumerator StopAtBusStop()
    {
        isStopped = true;
        yield return new WaitForSeconds(busStopTime); // Esperar el tiempo definido en la marquesina
        isStopped = false;
    }

    public void SetWayPoints(List<Transform> wayPoints)
    {
        this.wayPoints = wayPoints;
    }

    void OnDrawGizmosSelected()
    {
        if (wayPoints != null)
        {
            for (int i = 0; i < wayPoints.Count; i++)
            {
                Gizmos.color = Color.blue;

                // Conectar el waypoint actual con el siguiente
                Gizmos.DrawLine(wayPoints[i].transform.position, wayPoints[(i + 1) % wayPoints.Count].transform.position);
            }
        }
    }

    void OnMouseDown()
    {
        Camara camara = Camera.main.GetComponent<Camara>();
        if (camara != null)
        {
            camara.SeleccionarElemento(gameObject);
        }
    }

    public void ReverseWayPoints()
    {
        if (wayPoints != null)
        {
            wayPoints.Reverse();
        }
    }

    public void EnterBuilding(Transform targetWaypoint)
    {
        if (!isEnteringBuilding)
        {
            StartCoroutine(MoveToWaypointAndEnter(targetWaypoint));
        }
    }

    IEnumerator MoveToWaypointAndEnter(Transform targetWaypoint)
    {
        isEnteringBuilding = true;
        while (Vector3.Distance(transform.position, targetWaypoint.position) > 0.1f)
        {
            Vector3 directionToTarget = targetWaypoint.position - transform.position;
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, directionToTarget, rotationSpeed * Time.deltaTime, 0.0f));
            yield return null;
        }
        Destroy(gameObject); // Eliminar el objeto después de llegar al waypoint
    }
}
