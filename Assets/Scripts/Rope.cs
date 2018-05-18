using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{

    public GameObject ropeShoot;
    private SpringJoint2D rope;
    public int maxRopeCount;
    private int ropeCount;

    public LineRenderer lineRenderer;
    private DistanceJoint2D hookJoint;

    void Update()
    {

        // если ЛКМ нажата...
        if (Input.GetButtonDown("Fire1"))
        {

            // проверка объектов находящихся под куссором мыши
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            // если есть пересечение объектов и один из объектов помечен тегом "Wall"...
            if (hit.collider != null && hit.collider.gameObject.CompareTag("deathZone"))
            {

                // добавляем соединение к переменной thePlayer
                hookJoint = ropeShoot.AddComponent<DistanceJoint2D>() as DistanceJoint2D;

                // соединяем расстояние между hookJoin и объектом находящимся под курсором мыши
                hookJoint.connectedBody = hit.collider.GetComponent<Rigidbody2D>();

                // расчёт дистанции между игроком и объектом под курсором мыши
                float distance = Vector2.Distance(hit.transform.position, ropeShoot.transform.position);

                // устанавливаем  hookJoint.distance равным distance соответственно
                hookJoint.distance = distance;

                // связанные объекты могут столкнуться
                hookJoint.enableCollision = true;

                // этот LineRenderer имеет 2 точки, помеченые как "0" и "1". точка "0" указывает на позицию стены
                lineRenderer.SetPosition(0, hit.transform.position);
            }
        }

        // если ЛКМ не  нажата...
        if (Input.GetButtonUp("Fire1"))
        {

            // если есть сохранёная связь...
            if (hookJoint)
            {

                // унечтожаем связь
                Destroy(hookJoint);

                // устанавливаем значение переменной hookJoint в null
                hookJoint = null;

                // устанавливаем точки "0" и "1" в одно и тоже положение, из за этого линия не будет рисоваться
                lineRenderer.SetPosition(0, new Vector3(0, 0, 0));
                lineRenderer.SetPosition(1, new Vector3(0, 0, 0));
            }
        }

        // если есть связь...
        if (hookJoint)
        {

            // сокращаем до 0.05%
            hookJoint.distance = hookJoint.distance * 0.995f;

            // устанавливаем точку "1" в LineRenderer на позицию игрока, в результате линия будет отображаться
            lineRenderer.SetPosition(1, ropeShoot.transform.position);
        }
    }


private void LateUpdate()
{
    if (rope != null)
    {
        lineRenderer.enabled = true;
        lineRenderer.SetVertexCount(2);
        lineRenderer.SetPosition(0, ropeShoot.transform.position);
        lineRenderer.SetPosition(1, rope.connectedAnchor);
    }
    else
    {
        lineRenderer.enabled = false;
    }
}

private void FixedUpdate()
{
    if (rope != null)
    {
        ropeCount++;
        if (ropeCount > maxRopeCount)
        {
            GameObject.DestroyImmediate(rope);
            ropeCount = 0;
        }
    }
}

void Fire()
{
    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    Vector3 position = ropeShoot.transform.position;
    Vector3 direction = mousePosition - position;

    RaycastHit2D hit = Physics2D.Raycast(position, direction, Mathf.Infinity);

    if (hit.collider != null)
    {
        SpringJoint2D newRope = ropeShoot.AddComponent<SpringJoint2D>();
        newRope.enableCollision = false;
        newRope.frequency = 0.2f;
        newRope.connectedAnchor = hit.point;
        newRope.enabled = true;

        GameObject.DestroyImmediate(rope);
        rope = newRope;
        ropeCount = 0;
    }
}
}
