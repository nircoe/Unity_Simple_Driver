using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{
    [SerializeField] private float speed = 0f;
    [SerializeField] private float acceleration = 1f;
    [SerializeField] private float turnSpeed = 200f;

    //private Camera mainCamera;
    private int steerValue;

    // Start is called before the first frame update
    void Start() 
    {
        //mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);
            if(worldPosition.y < 0)
            {
                Steer(-1);
            }
            else if(worldPosition.y > 0)
            {
                Steer(1);
            }
        }*/

        speed += (acceleration * Time.deltaTime);

        transform.Rotate(0f, steerValue * turnSpeed * Time.deltaTime, 0f);

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Obstacle"))
        {
            SceneManager.LoadScene("Scene_MainMenu");
        }
    }

    public void Steer(int value)
    {
        steerValue = value;
    }
}
