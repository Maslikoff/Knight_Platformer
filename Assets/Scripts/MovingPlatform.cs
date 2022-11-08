using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Поинты")]
    [SerializeField] private Transform[] points;

    [Header("Скорость платформы")]
    [Range(0, 10f)][SerializeField] private float SpeedPlatform;

    [Header("Начальный поинт")]
    [SerializeField] private int startingPoint;

    private int i;

    void Start()
    {
        transform.position = points[startingPoint].position;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;

            if (i == points.Length)
                i = 0;
        }

        transform.position = Vector2.MoveTowards(transform.position, points[i].position, SpeedPlatform * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
