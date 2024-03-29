using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField] private float zSpeed;
    [SerializeField] private float _currentRunningSpeed=0f; 
    [SerializeField] private float limit_Z;
    public Vector3 offSet;

  
     
    public Rigidbody rb;

    Camera cam;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    public void SetSpeed(float val)
    {
        _currentRunningSpeed+= val; 
    }
    public float getSpeed()
    {
        return _currentRunningSpeed;
    }

    private void Start()
    {
        cam = Camera.main; 
    }

    private void FixedUpdate()
    {
        if (LevelSystem.instance==null || !LevelSystem.instance.gameActive)
            return;

        float newZ = 0;
        float touchXDelta = 0;
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            touchXDelta = -Input.GetTouch(0).deltaPosition.x / Screen.width; // parmak kaydırması
        }
        else if (Input.GetMouseButton(0))
        {
            touchXDelta = -Input.GetAxis("Mouse X"); // mouse kaydırması
        }
        newZ = transform.position.z + zSpeed * touchXDelta * Time.deltaTime;
        newZ = Mathf.Clamp(newZ, -limit_Z, limit_Z); // Sınırlandırma

        Vector3 newPosition = new Vector3(transform.position.x + _currentRunningSpeed * Time.deltaTime, transform.position.y, newZ);

        rb.MovePosition(newPosition); 

    }

    void Update()
    {
        Vector3 followPlayer = new Vector3(transform.position.x, 0, 0);
        cam.transform.localPosition = followPlayer + offSet;
    }



}
