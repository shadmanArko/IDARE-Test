using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewRotation : MonoBehaviour
{
    public float speed;
    public GameObject camera;
    
    /// <summary>
    /// Camera rotation controller
    /// </summary>
    void Update()
    {
        camera.transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}
