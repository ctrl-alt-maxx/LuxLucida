using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private Transform Point1;
    [SerializeField]
    private Transform Point2;
    [SerializeField]
    private Transform Point3;
    private LineRenderer linerenderer;
    private float vertexCount = 12;
    [SerializeField]
    private float Point2Ypositio = 0, _DistanceIndicateur = 4.0f, _PredictionAtterissageDelta = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        Point1 = transform;
        linerenderer = GetComponent<LineRenderer>();
       
        
    }

    // Update is called once per frame
    void Update()
    {
        Point2.transform.position = new Vector3((Point1.transform.position.x + Point3.transform.position.x) / 2, ((Point1.transform.position.y + Point3.transform.position.y) / 2) + Point2Ypositio, (Point1.transform.position.z + Point3.transform.position.z) / 2);
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = transform.position.z - Camera.main.transform.position.z;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        worldMousePos.y -= _PredictionAtterissageDelta;
        Vector3 direction = (worldMousePos - Point1.transform.position).normalized;
        Point3.position = direction * _DistanceIndicateur + Point1.transform.position;

        var pointList = new List<Vector3>();

        for (float ratio = 0; ratio <= 1; ratio += 1 / vertexCount)
        {
            var tangent1 = Vector3.Lerp(Point1.position, Point2.position, ratio);
            var tangent2 = Vector3.Lerp(Point2.position, Point3.position, ratio);
            var curve = Vector3.Lerp(tangent1, tangent2, ratio);

            pointList.Add(curve);
        }

        linerenderer.positionCount = pointList.Count;
        linerenderer.SetPositions(pointList.ToArray());
    }
}
