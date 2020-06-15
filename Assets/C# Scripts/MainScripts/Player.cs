using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
public class Player :MonoBehaviour
{
   
    public int maxHp;

   
    public int hp=100;
  
    public string nickname;
   
    public int dead = 0;
 
    public int killing = 0;
    [HideInInspector]
    //public float GetHpToBar;
   
    public bool inBattle;
    [SerializeField]
    public Collider[] colliders;

    [SerializeField]
    private GameObject[] disableGameObjectOnDeat;
    [SerializeField]
    private Behaviour[] disableOnDeath;
  
    public int teamId = -1; // 0 - Red  1 - blue
    [SerializeField]
    private Color red;
    [SerializeField]
    private Color blue;
    [SerializeField]
    private MeshRenderer[] meshRenderersForColor;

   

    public void takeRegeneration(int _regeneration)
    {
        hp = (hp + _regeneration) > maxHp ? maxHp : (hp + _regeneration);
    }
   
    public void takeDamage(int _damage)
    {
        hp = (hp - _damage) < 0 ? 0 : (hp - _damage);
        
    }

   
}
