using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class YellowCircle : MonoBehaviour
{
    //1 take the position of where the pipe needs to go and draw a circle around it.
    //2 display a text "The yellow circle indicates where the pipes go trough.
    //3 change position of the yellow circle to next target.
    //4 go back to one

    public static Transform[] lines;
    public int livesBeforeHint = 3;
    public int vertexCount = 100;
    public float linewidth = 0.2f;
    public float radius;
    public bool circleFillScreen;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material.color = Color.yellow;
        //SetupCircle();
        lines = new Transform[transform.childCount]; // size of the array is the amount of children
        for (int i = 0; i < lines.Length; i++)
        { // get the data from the children and put it in the array
            lines[i] = transform.GetChild(i);
        }
    }

    private void Update()
    {
        SetupCircle();
    }

    private void SetupCircle()
    {
        lineRenderer.widthMultiplier = linewidth;

        //testing purpose only no need to use
        if (circleFillScreen)
        {
            radius = Vector3.Distance(Camera.main.ScreenToWorldPoint(new Vector3(0f, Camera.main.pixelRect.yMax, 0f)),
            Camera.main.ScreenToWorldPoint(new Vector3(0f, Camera.main.pixelRect.yMin, 0f))) * 0.5f - linewidth;
        }

        float deltaTheta = (2f * Mathf.PI) / vertexCount;
        float theta = 0f;

        lineRenderer.positionCount = vertexCount;
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            Vector3 pos = new Vector3(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta), 0f);
            lineRenderer.SetPosition(i, pos);
            theta += deltaTheta;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        float deltaTheta = (2f * Mathf.PI) / vertexCount;
        float theta = 0f;

        Vector3 oldPos = Vector3.zero;
        for (int i = 0; i < vertexCount + 1; i++)
        {
            Vector3 pos = new Vector3(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta), 0f);
            Gizmos.DrawLine(oldPos, transform.position + pos);
            oldPos = transform.position + pos;
         
            theta += deltaTheta;
        }
    }
#endif

    //switch the circle to new position after switching to next line
    void ChangeCirclePos()
    {
        for (int i = 0; i < lines.Length; i++)
        {
            if (Waypoints.waypoints[i].gameObject)
            {

            }
        }
    }
}
