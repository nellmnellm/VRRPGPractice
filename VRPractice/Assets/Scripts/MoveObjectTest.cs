using UnityEngine;

public class MoveObjectTest : MonoBehaviour
{
    public Vector3 axis = Vector3.forward; // 왕복할 축
    public float distance = 3f;
    public float speed = 1f;
    private Vector3 _start;

    void Start() => _start = transform.position;

    void Update()
    {
        float t = Mathf.PingPong(Time.time * speed, 1f) * 2f - 1f; // -1~1 왕복
        transform.position = _start + axis.normalized * (t * distance);
        transform.Rotate(0f, 60f * Time.deltaTime, 0f, Space.World);
    }
}
