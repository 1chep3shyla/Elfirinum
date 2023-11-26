using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target; // Целевой объект, за которым следует камера
    public float smoothSpeed = 0.125f; // Скорость, с которой камера следует за объектом
    public Vector3 offset; // Смещение камеры относительно объекта

    private void FixedUpdate()
    {
        // Вычисляем позицию, к которой должна переместиться камера
        Vector3 desiredPosition = target.position + offset;

        // Интерполируем текущую позицию камеры к желаемой позиции с заданной скоростью
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Обновляем позицию камеры
        transform.position = smoothedPosition;

        // Направляем камеру в целевой объект
    }
}