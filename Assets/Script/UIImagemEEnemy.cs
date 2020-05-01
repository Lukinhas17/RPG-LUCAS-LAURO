using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIImagemEEnemy : MonoBehaviour
{
    private Image SpriteAtual; //sprite atual de hablidade do player que vai abrigar os demais sprites

    private Animator Acao; //componente de animacao da card

    public Sprite Atacar; //sprite de ataque
    public Sprite Defender; //sprite de defesa

    public static int EnemyAtacando; //utilizado para saber qual a acao atual do inimigo

    private void Awake()
    {
        Acao = GetComponent<Animator>(); //pegar componente do object
        SpriteAtual = GetComponent<Image>(); 
        SpriteAtual.enabled = false; //deixar o sprite off no inicio
    }

    public void TrocarSprite() //troca as imagens das habilidades
    {

        if (EnemyAtacando == 1) //se o inimigo atacar
        {
            SpriteAtual.enabled = true; //aparecer sprite
            SpriteAtual.sprite = Atacar; //atualizar sprite
            Acao.Play("CardEnemyAnimation", -1, 0f); //fazer animacao
        }
        if (EnemyAtacando == 2) //se o inimigo defender
        {
            SpriteAtual.enabled = true;
            SpriteAtual.sprite = Defender;
            Acao.Play("CardEnemyAnimation", -1, 0f);
        }
    }


}
