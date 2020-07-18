using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CanvasPlayer : MonoBehaviour
{
    [Header("HUD")]
    public Texture2D textureHUD;
    [Header("Настройки главного прицела")]
    public GameObject MainAim;
    public float widthMainAim = 100f;
    public float heightMainAim = 100f;
    [Header("Настройки орудийного прицела")]
    public GameObject GunAim;
    public float widthGunAim = 25f;
    public float heightGunAim = 25f;
    [Space]
    public PlayerGun gun;

    RectTransform rectMainAim;
    RectTransform rectGunAim;

    void Start()
    {
        /* Инициализация прицелов */
        rectMainAim = MainAim.GetComponent<RectTransform>();
        rectGunAim = GunAim.GetComponent<RectTransform>();

        rectMainAim.sizeDelta = new Vector2(widthMainAim, heightMainAim);
        rectMainAim.anchorMin = new Vector2(0.5f, 0.5f);
        rectMainAim.anchorMax = new Vector2(0.5f, 0.5f);
        rectMainAim.pivot = new Vector2(0.5f, 0.5f);

        rectGunAim.sizeDelta = new Vector2(widthGunAim, heightGunAim);
        rectGunAim.anchorMin = new Vector2(0f, 0f);
        rectGunAim.anchorMax = new Vector2(0f, 0f);
        rectGunAim.pivot = new Vector2(0.5f, 0.5f);
    }

    void FixedUpdate()
    {
        MovingGunAim();
    }

    void MovingGunAim()
    {
        Vector3 aim = gun.GetGunAimOnScreen();
        rectGunAim.transform.position = aim;
    }
}
