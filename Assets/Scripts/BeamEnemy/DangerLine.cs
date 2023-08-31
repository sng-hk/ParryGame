using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerLine : MonoBehaviour
{
    public Vector3 start_point;
    public Vector3 end_point;

    public LineRenderer lr;
    public Vector3 start_position;
    public Vector3 end_position;
    public Color lineColor = new Color(1f, 0f, 0f, 0.2f);

    public float speed;
    public float lifetime;

    private BeamEnemy shooter;

    public void MemoryShooter(BeamEnemy enemy)
    {
        shooter = enemy;
    }

    // Start is called before the first frame update
    void Start()
    {
        speed = 5.0f;
        lifetime = 3.0f;

        lr.startColor = lineColor;
        lr.endColor = lineColor;

        start_point = transform.position;
        end_point = PlayerController.instance.transform.position;
        end_point.y += 0.5f;

        start_position = transform.position;
        lr.SetPosition(0, start_position);

        StartCoroutine(DestroyLine());
    }

    void Update()
    {
        transform.Translate((end_point - start_point) * speed * Time.deltaTime);
        end_position = transform.position;
        lr.SetPosition(1, end_position);
    }

    IEnumerator DestroyLine()
    {
        yield return new WaitForSeconds(1.8f);
        shooter.line_destroy_point = end_point;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            speed = 0;
        }
    }
}
