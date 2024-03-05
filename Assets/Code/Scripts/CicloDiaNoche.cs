using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CicloDiaNoche : MonoBehaviour
{
    private Vector3 rot;
    [SerializeField]private float gradosseg = 1f;
    // Start is called before the first frame update
    void Start()
    {
        rot = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        rot.x=gradosseg*Time.deltaTime;
        transform.Rotate(rot,Space.World);
    }
}
