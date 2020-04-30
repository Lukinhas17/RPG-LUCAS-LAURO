using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIImagem : MonoBehaviour
{
    private Image SpriteAtual; 
    public Sprite Atacar;
    public Sprite Defender;

    private Animator Acao;


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
        Acao = GetComponent<Animator>();
        SpriteAtual = GetComponent<Image>();

        if (UiScript.playerClass == 1)
        {
            AtualEspecial1 = EspecialNinja1;
            AtualEspecial2 = EspecialNinja2;
        }
        if (UiScript.playerClass == 2)
        {
            AtualEspecial1 = EspecialMago1;
            AtualEspecial2 = EspecialMago2;
        }
        if (UiScript.playerClass == 3)
        {
            AtualEspecial1 = EspecialBerserker1;
            AtualEspecial2 = EspecialBerserker2;
        }

        SpriteAtual.enabled = false;
    }


    public void TrocarSprite(int op)
    {
        //Acao.SetBool("TrocaDeCard", true);
        SpriteAtual.enabled = true;
        Acao.Play("CardAnimation", -1, 0f);

        if (op == 1)
        {
            SpriteAtual.sprite = Atacar;
        }
        if (op == 2)
        {
            SpriteAtual.sprite = Defender;
        }
        if (op == 3)
        {
            SpriteAtual.sprite = AtualEspecial1;
        }
        if (op == 4)
        {
            SpriteAtual.sprite = AtualEspecial2;
        }

    }


}
