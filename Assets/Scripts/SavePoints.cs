using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoints : MonoBehaviour
{
    public int totalPontos;

    //Cria um método estático para dar acesso à esta classe
    public static SavePoints acesso;

    // Start is called before the first frame update
    void Start()
    {
        acesso = this;
        
    }


}

