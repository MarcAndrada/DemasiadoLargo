using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerarLineas : MonoBehaviour
{
    public GameObject generarLineas;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 lineapos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
            Instantiate(generarLineas, lineapos, transform.rotation);
        }
    }
}
