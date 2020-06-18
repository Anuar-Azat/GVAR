using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour
{
   

    //обьект танк. Добавляется в инспекторе. Камера и башня дочерние к танку
    public Transform tank_transform;
    //обьект башня. Добавляется в инспекторе. Камера и башня дочерние к танку
    public Transform turret_transform;
    //текстура прицела камеры
    public Texture2D camera_aim;
    public float maxDistanceToHitGun = 100f;
    Rect camera_aim_position;

    float rotationY = 0f;
    float rotationX = 0f;
    [Header("Размер главного прицела")]
    public float sizeAim = 100f; // проценты от исходного изображения
    private float sizeAimX;
    private float sizeAimY;
    [Header("Ограничния поворота камеры по осям")]
    public float minimumY = -20f;
    public float maximumY = 20f;
    [Header("Ограничния скролла камеры")]
    public float minimumZ = -15f;
    public float maximumZ = 0f;
    [Header("Позиция камеры")]
    public float cameraPositionZ = -15f;
    public float distanceToHitCamera = 25f;
    public float speedRotateCamera = 5f;
    public float speedScrollCamera = 0.02f;
    Vector3 newCamToTank;
    Vector3 newDistance;
    Quaternion newRotation;
    Vector3 newPosition;
    
    //Позиция прицела камеры в пространстве
    public Vector3 camera_targetPosition;
    //Позиция прицела орудия в пространстве
    Vector3 gun_targetPosition;

    RaycastHit hitCollision;
    Ray rayCollision;
    //---------------------------------------------------------
    // [Header("Скорость следования за танком")]
    // private float intens;
    // [SerializeField]
    // private float intensDefault = 0.15f;
    // [SerializeField]
    // private float intensMin = 0.02f;
    // [Header("Скорость следования за вращением башни")]
    // public float speed = 0.13f;
    // void Start()
    // {
    //     transform.parent = null;
    //}

    void Start()
    {
        sizeAimX = camera_aim.width * sizeAim / 100;
        sizeAimY = camera_aim.height * sizeAim / 100;
    }

    void Update()
    {
      
            //Сохраняем значение осей мыши и умножаем на скорость вращения камеры
            // + и - отвечают за инверсию осей мыши
            rotationX += Input.GetAxis("Mouse X") * speedRotateCamera;
            rotationY -= Input.GetAxis("Mouse Y") * speedRotateCamera;
            //Ограничиваем угол повора камеры по оси Y
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
            ////////Приближаем камеру///////////
            if (transform.localPosition.z <= cameraPositionZ && transform.localPosition.z < cameraPositionZ)
            {
                transform.localPosition += new Vector3(0, 0, speedScrollCamera);
            }
            ////////проверка на столкновения камеры с обьектами
            rayCollision = new Ray(turret_transform.position, transform.position - turret_transform.position);
            //Debug.DrawRay(turret_transform.position, transform.position - turret_transform.position, Color.red);
            //если луч длинной Z+1 камеры столкнулся с обьетом имеющим тег
            if (Physics.Raycast(rayCollision, out hitCollision, (cameraPositionZ - (cameraPositionZ * 2f)) + 1f) && hitCollision.collider.tag == "SceneObject")
            {//сохраняем z камеры равный расстоянию до обьекта столкновения +1
                newDistance = new Vector3(0.0f, 0.0f, hitCollision.distance - (hitCollision.distance * 2f) + 1f);
            }
            else
            { //иначе z меняем согласно данных скрола 
              //сохраняем значение скрола с учетом ограничений
                cameraPositionZ = Mathf.Clamp(cameraPositionZ + Input.GetAxis("Mouse ScrollWheel"), minimumZ, maximumZ);
                newDistance = new Vector3(0.0f, 0.0f, cameraPositionZ);
            }

            //Сохраняем местоположение камеры относительно танка
            newCamToTank = new Vector3(0, tank_transform.localPosition.y + 5f, 0);
            //Сохраняем значения поворота в новую переменную (для удобства)
            newRotation = Quaternion.Euler(rotationY, rotationX, 0);
            //Сохраняем новое местоположение в переменную (для удобства)
            newPosition = newRotation * newDistance + newCamToTank;
            //Изменяем позицию и угол поворота камеры 
            transform.localRotation = newRotation;
            transform.localPosition = newPosition;
            //Сохраняем позицию прицела камеры в переменную

            ////////находим позицию прицела камеры
            //Переводим трехмерные координаты точки прицела камеры в координаты экрана
            Vector3 screenPos = GetAimPoint();
            //Инвертируем координату Y
            screenPos.y = Screen.height - screenPos.y;
            //Сохраняем кординаты отрисовки прицела в переменную

            camera_aim_position = new Rect(Screen.width / 2 - sizeAimX / 2, Screen.height / 2 - sizeAimY / 2, sizeAimX, sizeAimY);
    }

    void OnGUI()
    {
        GUI.DrawTexture(camera_aim_position, camera_aim);
    }

    public Vector3 GetAimPoint()
        /// <summary>
        /// Функция поиска точки экранного прицела
        /// </summary>
        /// <returns>Vector3 точки в пространстве</returns>
    {
        Ray ray = new Ray(transform.position, transform.forward);
        float distanceToHit = 0f;
        Vector3 currentCamTarget;
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistanceToHitGun))
        {
            currentCamTarget = hit.point;
        }
        else
        {
            distanceToHit = Vector3.Distance(transform.position, transform.forward * maxDistanceToHitGun);
            currentCamTarget = transform.TransformPoint(Vector3.forward * distanceToHit);
            //print(distanceToHit);
        }
        
        Debug.DrawLine(ray.origin, currentCamTarget, Color.red);

        return currentCamTarget;
    }
}
