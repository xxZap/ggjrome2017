using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthMana : MonoBehaviour {

    public Vector4 HP_POS;
    public Vector4 MANA_POS;
    public Vector4 Icon;

   public Personaggio p;
    float Health;
    float Mana_;

    public GUISkin SkinHP;
    public GUISkin SKinMana;
    public GUISkin SkinPG;

    void Start()
    {
        Health = p.HP;
        Mana_ = p.Magia;
        
    }

    void Update()
    {
       
    }

    void OnGUI()
    {
       
        GUI.skin = SkinPG;
        GUI.Box(new Rect(100, 10, Screen.width / 4, 30), "Health " + Health);
        GUI.Box(new Rect(100, 50, Screen.width / 4, 30), "Waves Power " + Mana_);
        GUI.Box(new Rect(10, 10, 85,70 ), "");
           
        if(Health > 0) 
        {
            GUI.skin = SkinHP;
            GUI.Box(new Rect(100, 10, Screen.width / 4 * (Health / 3), 30),"");
        }
        if(Mana_ > 0)
        {
            GUI.skin = SKinMana;
            GUI.Box(new Rect(100, 50, Screen.width / 4 * (Health / 10), 30), "");
        }

        
    }


}
