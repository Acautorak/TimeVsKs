using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   public Camera cam;
    [SerializeField] private float _maxZoom =5;
    [SerializeField] private float _minZoom =20;
    [SerializeField] private float _sensitivity = 1;
    [SerializeField] private float _mapBoundry = 50;
    [SerializeField] private AnimationCurve _movementCurve;
    
    public float speed = 30;
    float targetZoom;
    public Rigidbody2D rb;
    private Vector2 _moveDirection;

    
    void Update()
    {
       Zoom();
       TakeInput();
    }

    void FixedUpdate()
    {
        Move();
    }

    void TakeInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        _moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move()
    {
        rb.velocity = new Vector2(_moveDirection.x * speed * _movementCurve.Evaluate(Time.deltaTime), _moveDirection.y*speed*_movementCurve.Evaluate(Time.deltaTime));
        transform.position = new Vector3(
				Mathf.Clamp(transform.position.x, -_mapBoundry, _mapBoundry),
				Mathf.Clamp(transform.position.y, -_mapBoundry, _mapBoundry),
				transform.localPosition.z); 
    }

    void Zoom()
    {
        targetZoom -= Input.mouseScrollDelta.y * _sensitivity;
        targetZoom = Mathf.Clamp(targetZoom, _maxZoom, _minZoom);
        float newSize = Mathf.MoveTowards(cam.orthographicSize, targetZoom, speed * Time.deltaTime);
        cam.orthographicSize = newSize;
    }
}