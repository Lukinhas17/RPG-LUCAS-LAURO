using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassManager : MonoBehaviour
{
    public ClasseBase ninja = new Ninja(100,65,55,100, 40, 30);//CRIA UM NINJA
    public ClasseBase berserker = new Berserker(200,60,40,200, 30, 40);//CRIA UM BERSERKER
    public ClasseBase mago = new Mago(120,70,70, 120, 35, 30);//CRIA UM MAGO
    //REFERENCIAS DE GAMEOBJECT
    public GameObject ninjaPrefab;
    public GameObject berserkerPrefab;
    public GameObject magoPrefab;

    private void Start()
    {
        //COLOCA O PREFAB DE CADA CLASS
        ninja.prefab = ninjaPrefab;
        mago.prefab = magoPrefab;
        berserker.prefab = berserkerPrefab;
    }
 
    public void CriarClasse(ClasseBase classe)
    {
        PlayerScript.singleton.classe = classe;   //CRIA UM SINGLETON CLASS     
    }
}
