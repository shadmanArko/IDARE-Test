using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderBetweenTwoPoints : MonoBehaviour {

    #region Fields

    [SerializeField] [Tooltip("Give a 3D object by which the connection will be created")]
    private Transform cylinderPrefab;
    private GameObject leftSphere;
    private GameObject rightSphere;
    private GameObject cylinder;
    private RaycastHit hitInfo;
    private Vector3 tempPosition;
    private int pointCount;

    #endregion


    #region Methods

      private void Start ()
    {
        pointCount = 0;
        leftSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        rightSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        leftSphere.transform.position = new Vector3(-1, -5, 0);
        rightSphere.transform.position = new Vector3(1, -5, 0);

        InstantiateCylinder(cylinderPrefab, leftSphere.transform.position, rightSphere.transform.position);
    }

    private void Update ()
    {
        CreateConnection();
    }

    private void CreateConnection()
    {
        UpdateCylinderPosition(cylinder, leftSphere.transform.position, rightSphere.transform.position);

        if (Input.GetMouseButtonDown(0) && pointCount < 2)
        {
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(myRay, out hitInfo, 100))
            {
                if (hitInfo.collider.CompareTag("Box"))
                {
                    tempPosition = hitInfo.collider.transform.position;
                    pointCount = pointCount + 1;
                }
            }
        }

        if (pointCount == 1)
        {
            leftSphere.transform.position = tempPosition;
            rightSphere.transform.position = tempPosition;
        }

        if (pointCount == 2)
        {
            rightSphere.transform.position = tempPosition;
        }
    }

    private void InstantiateCylinder(Transform cylinderPrefab, Vector3 beginPoint, Vector3 endPoint)
    {
        cylinder = Instantiate<GameObject>(cylinderPrefab.gameObject, Vector3.zero, Quaternion.identity);
        UpdateCylinderPosition(cylinder, beginPoint, endPoint);
    }

    private void UpdateCylinderPosition(GameObject cylinder, Vector3 beginPoint, Vector3 endPoint)
    {
        Vector3 offset = endPoint - beginPoint;
        Vector3 position = beginPoint + (offset / 2.0f);

        cylinder.transform.position = position;
        cylinder.transform.LookAt(beginPoint);
        Vector3 localScale = cylinder.transform.localScale;
        localScale.z = (endPoint - beginPoint).magnitude;
        cylinder.transform.localScale = localScale;
    }
    
    #endregion
  
}
