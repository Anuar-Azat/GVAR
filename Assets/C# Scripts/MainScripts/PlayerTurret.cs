using UnityEngine;
using System.Collections;

public class PlayerTurret : MonoBehaviour
{
    //обьект камера. Добавляется в инспекторе. Камера и башня дочерние к танку
    public Transform camera_transform;
    //обьект танк. Добавляется в инспекторе. Камера и башня дочерние к танку
    public Transform tank_transform;

    //сглаживание(скорость) вращения башни
    public float speedRotateTurret = 0.5f;
    //ограничение поворота башни
    public float rangeAngleTurret = 45;
    //Расстояние от башни до прицела
    public float distanceToHitTurret;
    //вектор, куда нужно вращать башню
    Quaternion directionTurret;
    public float GunDamage;

    void Update()
    {
        directionTurret = Quaternion.LookRotation(camera_transform.GetComponent<PlayerCamera>().camera_targetPosition - transform.position);
        //если угол между башней и корпусом меньше ограничения угла
        if (Vector3.Angle(transform.forward, tank_transform.forward) < rangeAngleTurret)
        {//Поворачиваем башню в нужный вектор
            transform.rotation = Quaternion.RotateTowards(transform.rotation, directionTurret, speedRotateTurret);
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
        }
        else
        {//Если угол больше ограничения но угол между векторами камеры и танка меньше ограничения
            if (Vector3.Angle(camera_transform.forward, tank_transform.forward) < rangeAngleTurret)
            {//Поворачиваем башню в нужный вектор
                transform.rotation = Quaternion.RotateTowards(transform.rotation, directionTurret, speedRotateTurret);
                transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
            }
        }
        //расстояние до прицела башни
        distanceToHitTurret = (Vector3.Distance(transform.position, camera_transform.GetComponent<PlayerCamera>().camera_targetPosition));

        Debug.DrawLine(transform.position, transform.TransformPoint(Vector3.forward * distanceToHitTurret), Color.blue);

    }
}
