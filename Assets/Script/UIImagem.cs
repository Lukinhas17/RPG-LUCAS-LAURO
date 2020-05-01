using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIImagem : MonoBehaviour
{
    private Image SpriteAtual; //sprite atual de hablidade do player que vai abrigar os demais sprites

    private Animator Acao;

    //Sprites de hablidades do player

    public Sprite Atacar; 
    public Sprite Defender;

    public Sprite EspecialNinja1;
    public Sprite EspecialNinja2;

    public Sprite EspecialMago1;
    public Sprite EspecialMago2;

    public Sprite EspecialBerserker1;
    public Sprite EspecialBerserker2;

    private Sprite AtualEspecial1;
    private Sprite AtualEspecial2;


    private void Awake()
    {
        Acao = GetComponent<Animator>(); //pegando componentes do objeto
        SpriteAtual = GetComponent<Image>();

        if (UiScript.playerClass == 1) //se for ninja
        {
            AtualEspecial1 = EspecialNinja1; //troca de sprites
            AtualEspecial2 = EspecialNinja2;
        }
        if (UiScript.playerClass == 2) //se for mago
        {
            AtualEspecial1 = EspecialMago1;//troca de sprites
            AtualEspecial2 = EspecialMago2;
        }
        if (UiScript.playerClass == 3)// se for um berserker
        {
            AtualEspecial1 = EspecialBerserker1;//troca de sprites
            AtualEspecial2 = EspecialBerserker2;
        }

        SpriteAtual.enabled = false;// deixa o sprite off no inicio 
    }


    public void TrocarSprite(int op) //troca dos sprites 
    {
        //Acao.SetBool("TrocaDeCard", true);
        SpriteAtual.enabled = true;
        Acao.Play("CardAnimation", -1, 0f);

        if (op == 1) //se atacar
        {
            SpriteAtual.sprite = Atacar;
        }
        if (op == 2)//se defender
        {
            SpriteAtual.sprite = Defender;
        }
        if (op == 3)//se usar a hablidade um
        {
            SpriteAtual.sprite = AtualEspecial1;
        }
        if (op == 4)//se usar a hablidade dois
        {
            SpriteAtual.sprite = AtualEspecial2;
        }

    }


}
