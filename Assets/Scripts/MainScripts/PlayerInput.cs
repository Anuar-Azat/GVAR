using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{

    public Movement _tankController;
    private Player _player;


    public GameObject Shell;
    public Transform piv;
    //задержка между выстрелами
    private float TimeShot;
    public float StartTime;
    //--------------------
    private void Start()
    {
        _player = GetComponent<Player>();
    }
    private void FixedUpdate()
    {
        if (_player.hp != 0)
        {
            _tankController.FixedUpdateTwo(Input.GetAxis("Ver"), Input.GetAxis("Hor"));
            _tankController.CmdFixedUpdateTwo(Input.GetAxis("Ver"), Input.GetAxis("Hor"));
        }
        else
        {
            _tankController.FixedUpdateTwo(0, 0);
            _tankController.CmdFixedUpdateTwo(0, 0);

        }
        if (TimeShot <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                Instantiate(Shell, piv.position, piv.rotation);
                TimeShot = StartTime;
            }
        }
        else 
        {
            TimeShot -= Time.deltaTime;
        }
    }
}
