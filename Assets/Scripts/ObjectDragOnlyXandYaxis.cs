using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDragOnlyXandYaxis : MonoBehaviour
{
    private const float planeY = 0f;

    Plane plane = new Plane(Vector3.up, Vector3.up * planeY); // ground plane
    
    void OnMouseDrag()
    {
        if (API.moveToDrag == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float distance; // the distance from the ray origin to the ray intersection of the plane
            if(plane.Raycast(ray, out distance))
            {
                transform.position = ray.GetPoint(distance); // distance along the ray
            }
        }
        
    }
}
