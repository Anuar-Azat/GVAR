using UnityEngine;
using System.Collections;

public class PlayerGun : MonoBehaviour
{
    //обьект камера. Добавляется в инспекторе. Камера и башня дочерние к танку
    public Transform camera_transform;
    //текстура прицела орудия
    public Texture2D gun_aim;
    //позиция отривки текстуры прицела камеры
    Rect gun_aim_position;

    //Позиция прицела орудия в пространстве
    Vector3 gun_targetPosition;
    //сглаживание(скорость) вращения орудия
    public float speedRotateGun = 0.3f;
    //ограничение поворота орудия
    public float maximumAngleGun = 10f;
    //ограничение поворота орудия
    public float minimumAngleGun = 5f;
    //Расстояние от орудия до прицела
    public float distanceToHitGun;
    //вектор, куда нужно вращать орудие
    Quaternion directionGun;
    //значение оси x орудия, используется для проверки ограничений вращения
    float tempAngleGunX;

    void Update()
    {
        //сохраняем в переменную вектор, куда нужно повернуть орудие
        directionGun = Quaternion.LookRotation(camera_transform.GetComponent<PlayerCamera>().camera_targetPosition - transform.position);
        //вращаем орудие в сторону нужного вектора с определ. скоростью
        transform.rotation = Quaternion.RotateTowards(transform.rotation, directionGun, speedRotateGun);
        //сохраняем текущий Х во временную переменную
        tempAngleGunX = transform.localEulerAngles.x;
        //
        if (transform.localEulerAngles.x > 180f)
        {//если значение Х больше 180, то используем ограничение между 360 и максимальним ограничением
            tempAngleGunX = Mathf.Clamp(tempAngleGunX, 360f - maximumAngleGun, 360f);
        }
        else
            if (transform.localEulerAngles.x < 180f)
            {//если значение Х меньше 180, то ограничение используем между 0 и минимальным ограничением 
                tempAngleGunX = Mathf.Clamp(tempAngleGunX, 0f, minimumAngleGun);
            }
        //присваиваем орудию новое значение Х
        transform.localEulerAngles = new Vector3(tempAngleGunX, 0, 0);

        //сохраняем в переменную расстояние до прицела орудия
        distanceToHitGun = (Vector3.Distance(camera_transform.GetComponent<PlayerCamera>().camera_targetPosition, transform.position));
        //сохраняем в переменную позицию прицела орудия
        gun_targetPosition = transform.TransformPoint(Vector3.forward * distanceToHitGun);

        Debug.DrawLine(transform.position, transform.TransformPoint(Vector3.forward * distanceToHitGun), Color.green);

        ///////находим позицию прицела орудия////////////////////
        //Переводим трехмерные координаты точки прицела камеры в координаты экрана
        Vector3 screenPos = camera_transform.GetComponent<Camera>().WorldToScreenPoint(gun_targetPosition);
        //Инвертируем координату Y
        screenPos.y = Screen.height - screenPos.y;
        //Сохраняем кординаты отрисовки прицела в переменную
        gun_aim_position = new Rect(screenPos.x - (gun_aim.width / 2), screenPos.y - (gun_aim.height / 2), gun_aim.width, gun_aim.height);
    }

    void OnGUI()
    {
        //рисуем прицелы камеры и орудия
        GUI.DrawTexture(gun_aim_position, gun_aim);
    }
}
