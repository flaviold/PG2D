using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

public class PathFinderGenerator : MonoBehaviour {

    public int startNumber;

    public float x0;
    public float xN;
    public float y;

    public float spaceBetweenPoints = 3f;

    public GameObject obj;
	
	public void BuildPoints ()
    {
        if (x0 >= xN) return;
        if (Mathf.Abs((xN - x0) / spaceBetweenPoints) < 1f) return;

        //Arredondar Espaço entre pontos
        var delta = (xN - x0);
        var pNumber = (int)(delta / spaceBetweenPoints);
        Debug.Log("Number of points: " + pNumber);
        spaceBetweenPoints = delta / pNumber;

        PathPoint previousPoint = null;
        for (var n = x0; n <= xN; n += spaceBetweenPoints)
        {
            var childObj = Instantiate(obj, new Vector3(n, y), Quaternion.identity) as GameObject;
            childObj.transform.parent = this.transform;
            childObj.name = obj.name + " (" + (startNumber) + ")";

            PathPoint point = childObj.GetComponent<PathPoint>();
            point.id = startNumber;

            if (previousPoint != null)
            {
                previousPoint.siblings.Add(point.id);
                point.siblings.Add(previousPoint.id);
            }

            previousPoint = point;

            Debug.Log(obj.name + " (" + (startNumber++) + "): " + n);
        }
	}

    //public void DrawArrows()
    //{
    //    var gObjs = new List<PathPoint>();
    //    foreach (Transform t in transform)
    //    {
    //        if (t != transform)
    //        {
    //            gObjs.Add(t.gameObject.GetComponent<PathPoint>());
    //        }
    //    }
        
    //    foreach (var gObj in gObjs)
    //    {
    //        foreach (var p in gObj.siblings)
    //        {
    //            var sObj = gObjs.Where(o => o.id == p).Single();
    //            Handles.color = Color.blue;
    //            Handles.ArrowCap(
    //                0,
    //                gObj.transform.position,
    //                Quaternion. Vector3.Dot(gObj.transform.position, sObj.transform.position),
    //                GetDistance(gObj.transform.position, sObj.transform.position)
    //        }
    //        Handles.ArrowCap(0,)
    //    }
    //    var teste = GetDistance(v1, v2);
    //    var rotation = ;
    //    Handles.color = Color.blue;
    //    Handles.ArrowCap(0,)
    //    Handles.ArrowCap(0,
    //            target.transform.position + Vector3(0, 0, 5),
    //            target.transform.rotation,
    //            arrowSize);
    //}

    public float GetDistance(Vector3 a, Vector3 b)
    {
        var dX = a.x - b.x;
        var dY = a.y - b.y;
        var dZ = a.z - b.z;
        return Mathf.Sqrt((dX * dX) + (dY * dY) + (dZ * dZ));
    }
}
