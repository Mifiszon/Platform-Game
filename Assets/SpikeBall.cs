using UnityEngine;

public class OrbitingObject : MonoBehaviour
{
    public Transform pointA; // Obiekt A
    public float orbitSpeed = 100f; // Prędkość obrotu w stopniach na sekundę
    public float startAngle = 200; // Początkowy kąt obrotu w stopniach
    public float endAngle =  340; // Końcowy kąt obrotu w stopniach

    public LineRenderer orbitLineRenderer; // Komponent do rysowania linii
   
    private float currentAngle; // Aktualny kąt obrotu
    private float distanceFromPointA; // Odległość między A i B
    private bool reverseDirection = false;
    void Start()
    {
        // Automatycznie ustaw odległość na podstawie promienia okręgu
        distanceFromPointA = Vector2.Distance(transform.position, pointA.position);

        // Inicjalizuj LineRenderer
        if (orbitLineRenderer != null)
        {
            orbitLineRenderer.positionCount = 2;
            orbitLineRenderer.useWorldSpace = true;
            orbitLineRenderer.material.color = new Color(88f / 255f, 57f / 255f, 39f / 255f);
        }

       
    }

    void Update()
    {
        currentAngle += (reverseDirection ? -1 : 1) * orbitSpeed * Time.deltaTime;

        // Sprawdź, czy osiągnięto maksymalny lub minimalny kąt obrotu
        if (currentAngle > endAngle || currentAngle < startAngle)
        {
            reverseDirection = !reverseDirection;
        }

        float radianAngle = Mathf.Deg2Rad * currentAngle;
        Vector3 orbitPosition = new Vector3(
            pointA.position.x + distanceFromPointA * Mathf.Cos(radianAngle),
            pointA.position.y + distanceFromPointA * Mathf.Sin(radianAngle),
            transform.position.z
        );

        transform.position = orbitPosition;

        if (orbitLineRenderer != null)
        {
            Vector3 lineStart = pointA.position;
            Vector3 lineEnd = transform.position;
            orbitLineRenderer.SetPosition(0, lineStart);
            orbitLineRenderer.SetPosition(1, lineEnd);
        }
    }
}