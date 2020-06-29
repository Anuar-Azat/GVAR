using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasPlayer : MonoBehaviour
{
    Camera playerCamera;
    public PlayerGun gun;
    [SerializeField]
    private RectTransform aimTexture;

    void Start()
    {
        playerCamera = Camera.main;
        transform.parent = null;
    }

    void FixedUpdate()
    {
        Rect aim = gun.FindGunAimPositionOnScreen();
        aimTexture.sizeDelta = new Vector2(aim.width, aim.height);
        aimTexture.anchoredPosition = new Vector2(aim.x, -aim.y);
    }
}
