using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaggio : MonoBehaviour
{
    public float HP;
    public float Magia;
    public bool PersonaggioMuore;
    public bool ManaFinito;

    public GameObject OggettoDaDistruggere;
   void Start()
    {
        HP = 3;
        Magia = 10;

        PersonaggioMuore = false;
        ManaFinito = false;
    }
    void Update()
    {
        ControlloPuntiGiocatore();
        if(PersonaggioMuore == true)
        {
            Destroy(OggettoDaDistruggere);
        }

    }

    void ControlloPuntiGiocatore()
    {
        if (HP < 0)
            HP = 0;
        if (HP > 3)
            HP = 3;
        if (HP == 0)
        {
            PersonaggioMuore = true;
        }

       else if (Magia < 0)
            HP = 0;
        if (Magia > 100)
            HP = 100;
        if (Magia == 0)
        {
            ManaFinito = true;
        }

    }//controlla vita e mana
    


}//fine classe
    

   

