using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class YellowCircle : MonoBehaviour
{
    //1 Take the position of where the pipe needs to go and draw a circle around it.
    //2 Display a text "The yellow circle indicates where the pipes go trough.
    //3 Change position of the yellow circle to next target.
    //4 Go back to one

    public static Transform[] lines;
    public int vertexCount = 100;
    public float linewidth = 0.2f;
    public float radius;
    public bool circleFillScreen;
    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material.color = Color.yellow;
        // Size of the array is the amount of children
        lines = new Transform[transform.childCount];
        // Get the data from the children and put it in the array
        for (int i = 0; i < lines.Length; i++)
        {
            lines[i] = transform.GetChild(i);
        }
    }

    private void Update()
    {
        SetupCircle();
        ChangeCirclePos();
    }

    private void SetupCircle()
    {
        lineRenderer.widthMultiplier = linewidth;

        // Testing purpose only no need to use we enlarge the circle to fill the screen
        if (circleFillScreen)
        {
            radius = Vector3.Distance(Camera.main.ScreenToWorldPoint(new Vector3(0f, Camera.main.pixelRect.yMax, 0f)),
            Camera.main.ScreenToWorldPoint(new Vector3(0f, Camera.main.pixelRect.yMin, 0f))) * 0.5f - linewidth;
        }

        // We use PI to calculate the diametre of a circle
        float deltaTheta = (2f * Mathf.PI) / vertexCount;
        float theta = 0f;

        // The vertexCount is the amount of polygons used to draw the circle, the more you use the smoother the circle is.
        lineRenderer.positionCount = vertexCount;
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            // We create a new vector 3 to determine where our next vertex point should go 
            // So from the middle of the cirlce to the first vertexpoint what angle do we need?
            Vector3 pos = new Vector3(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta), 0f);
            // Insert the value and go trough the next point
            lineRenderer.SetPosition(i, pos);
            theta += deltaTheta;
        }
    }

// This is only used for debugging/scene purposes it makes it better to use so we can see it both in game and in the project
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

    // Switch the circle to new position after switching to next line
    void ChangeCirclePos()
    {
        // We refer back to the ActiveLineChecker script to get the position of the waypoints
        this.transform.position = ActiveLineChecker.GetTargetPosition();
    }
}
