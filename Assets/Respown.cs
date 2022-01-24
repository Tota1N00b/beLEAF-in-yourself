using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respown : MonoBehaviour
{
    void Update()
    {
        if (transform.position.y < -5)
            transform.position = new Vector3(1.82999992f, 15f, -6.90000057f);
    }
}
