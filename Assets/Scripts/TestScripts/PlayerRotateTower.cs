using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotateTower : MonoBehaviour
{
    public GameObject Camera3rd; // камера от 3лица
    public GameObject CamFPS; // камера от 1ого
    private bool switchCam=false; //переключатель , по умолчанию стоит камера от 3-о лица
    public float rotspeed = 30f;  // скорость поворота башни для LeftArrow и RightArrow стрелки 
  
   
    //public float rotspeedBarrel = 0.25f;
    //public float maxBarrelUp = 20f;
    //public float maxBarrelDown = -10f;
    //float counter = 0f;
    //public float wheel_speed = 10f; // скорость зума
    //public float mouseSensitivity = 100f;
    public float M_FieldOfView;
    public float Max_FieldOfView = 100;
    public float Min_FieldOfView = 20;
    void Start()
    {
        M_FieldOfView = 60;
    }
    void Update()
    {
        RotTowerButtons();
        ChangeCamera();
 //       Camera.main.fieldOfView = M_FieldOfView;
    }

    void RotTowerButtons() //поворот башни 
    {
        
        if (Input.GetKey(KeyCode.LeftArrow)) 
        {
            transform.Rotate(0, 0, -rotspeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow))  
        {
            transform.Rotate(0, 0, rotspeed * Time.deltaTime);
        }

   // ДОП СТРЕЛКИ ВВЕРХ И ВНИЗ
    }
    
    //float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
    //float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;


    void ChangeCamera() // смена камер
    {
        if (Input.GetKeyUp(KeyCode.Q)|| Input.GetMouseButtonDown(1))
        {
            if (switchCam ==true ) switchCam =false;
            else if(switchCam == false) switchCam = true;
        }
        if(switchCam == false)
        {
            Camera3rd.SetActive(true);
            CamFPS.SetActive(false);
        }
        if (switchCam ==true)
        {
           
            Camera3rd.SetActive(false);
            CamFPS.SetActive(true);
        }
       
    }

    void OnGUI()
    {
        //if (Input.GetKeyUp(KeyCode.Q) || Input.GetMouseButtonDown(1))
        //{
        //    // изменяет поле зрения камеры между минимальным и максимальным значениями
        //    M_FieldOfView = GUI.HorizontalSlider(new Rect(20, 20, 100, 40), M_FieldOfView, Min_FieldOfView, Max_FieldOfView);
        //}
    }
}
