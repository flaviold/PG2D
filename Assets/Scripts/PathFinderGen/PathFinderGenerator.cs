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

    public void DrawArrows()
    {
        var gObjs = new List<PathPoint>();
        Handles.color = Color.blue;
        foreach (Transform t in transform)
        {
            if (t != transform)
            {
                var point = t.gameObject.GetComponent<PathPoint>();
                var guiS = new GUIStyle()
                {
                    fontSize = 15,
                };
                guiS.richText = true;
                gObjs.Add(point);
                Handles.DrawSolidDisc(t.position, Vector3.back, .3f);
                Handles.Label(point.transform.position, "<color=white>" + point.id.ToString() + "</color>", guiS);
            }
        }
        
        foreach (var gObj in gObjs)
        {
            foreach (var p in gObj.siblings)
            {
                var sObj = gObjs.Where(o => o.id == p).Single();
                var angle = Quaternion.Angle(gObj.transform.rotation, sObj.transform.rotation);
                Handles.DrawLine(gObj.transform.position, sObj.transform.position);
            }
        }
    }

    public float GetDistance(Vector3 a, Vector3 b)
    {
        var dX = a.x - b.x;
        var dY = a.y - b.y;
        var dZ = a.z - b.z;
        return Mathf.Sqrt((dX * dX) + (dY * dY) + (dZ * dZ));
    }
}
