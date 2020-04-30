using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleClass : MonoBehaviour
{
    int enemyAtual = 0;
    public ClasseBase golem = new Golem(300, 300, 30, 70);
    public ClasseBase goblin = new Goblin(150, 150, 30, 10);
    public ClasseBase dragao = new Dragao(1000, 1000, 80, 50);
    public ClasseBase necromancer = new Necromancer(150, 150, 80, 36);
    public ClasseBase ogro = new Ogro(200,200, 40, 26);
    public static ClasseBase Enemy { get; set; }
    private ClasseBase[] Enemys = new ClasseBase[5];

    public GameObject ogroP;
    public GameObject dragaoP;
    public GameObject goblinP;
    public GameObject necromancerP;
    public GameObject golemP;
    private GameObject enemyPrefeb;

    public GameObject PainelDeDerrota,panelDeDerrotaD;
    public GameObject PainelDeVitoria,PainelDeVitoriaD;

    public static int valorD;
    public static int valorF;

    public Button especialB, especialF;
    private int acurice, opcao, dano, valorPlayer, poderE;
    private bool enemyA, playerA,efeitosobreI;
    public bool turnoPlayer;
    private int i;

    private void Awake()
    {
        //SETA QUAIS SÃO OS PREFABS DOS INIMIGOS 
        goblin.prefab = goblinP;
        golem.prefab = golemP;
        ogro.prefab = ogroP;
        necromancer.prefab = necromancerP;
        dragao.prefab = dragaoP;
        //CRIA UM ARRAY DE INIMIGOS CONTENDO TODOS OS INIMIGOS NA ORDEM DO MAIS FRACO AO MAIS FORTE
        Enemys[0] = goblin; ;
        Enemys[1] = ogro;
        Enemys[2] = golem;
        Enemys[3] = necromancer;
        Enemys[4] = dragao;
        Enemy = Enemys[0];//INICIA COM VETOR IGUAL A 0

        enemyPrefeb = Instantiate(Enemy.prefab) as GameObject;//ISTANCIA OS INIMIGOS
        var ClassePrefab = Instantiate(PlayerScript.singleton.classe.prefab) as GameObject;//ISTANCIA O PLAYER
        OnDestroyButton(especialB);//DESATIVA A FUNÇÃO PARA O ATAQUE BASICO
        OnDestroyButton(especialF);//DESATIVA A FUNÇÃO PARA O ATAQUE FORTE
        poderE = 0;//PODER ESPECIAL IGUAL A 0 ISSO É UM CONTADOR PARA QUE POSTERIORMENTE QUANDO ESSE PODER CHEGAR A 3 E 6 O PLAYER PODER USAR O ESPECIAL
    }
    private void Start()
    {
        BattleClass.valorD = PlayerScript.singleton.classe.defesa;
        BattleClass.valorF = PlayerScript.singleton.classe.forca;
       //USAR COMO DEBUG POREM APARECE NO HUD
    }
    private void Update()
    {
        Especial();//CHAMA A VERIFICAÇÃO DO ESPECIAL
    }
    public int AcureceValue(int valor)//ACURECE VALUE RECEBE POR PARAMETRO UM VALOR ESSE VALOR É OU O ATAQUE OU A DEFESA DO PLAYER
    {
        acurice = Random.Range(0, 10);//FAZ UM RANDOM PARA VER SE ESSE VALOR VAI SAIR ZERADO BAIXO MEDIO OU ALTO
        if (acurice == 0)
        {
            valor = 0;//BAIXO
        }
        else if (acurice > 1 && acurice <= 5)
        {
            valor = valor - valor / 2;//BAIXO
        }
        else if (acurice > 5 && acurice == 9)
        {
            //NORMAL
        }
        else
        {
            valor = valor + valor;//ALMENTADO
        }

        return valor;
    }
    public void AcureceValueModif(int valor)//RECEBE POR PARAMETRO O VALOR
    {
        valorPlayer = valor;//VALORPLAYER É A VARIAVEL GLOBAL PARA DEFESA E ATAQUE ETC ELA RECEBE UM PARAMETRO QUE É DEFINIDO NOS BOTOES 
        //E POSTERIORMENTE É MODIFICADA NO ACURECEVALUE
    }
    public void OpAtaque(int op)//TURNO DO PLAYER
    {
        opcao = op;
        Turnos(false);
        //VERIFICA SE É O TURNO DO PLAYER, ESTANDO TRUE É O TURNO DO PLAYER
    }
    public void RandomAtaq()
    {
        int randomAtaq = Random.Range(0, 10);

        if (randomAtaq <= 5)
        {
            enemyA = true;
        }
        else
        {
            enemyA = false;
        }
    }
    public void OnDestroyButton(Button especial)
    {
        especial.enabled = false;
        especial.image.color = Color.red;
    }
    public void OnCreateButton(Button especial)
    {
        especial.enabled = true;
        especial.image.color = Color.white;
    }   
    public void Especial() 
    {
        if (poderE < 3) 
        {
            OnDestroyButton(especialB);
            OnDestroyButton(especialF);
        }

        if(poderE > 3 &&  poderE < 6) 
        {
            OnCreateButton(especialB);
            OnDestroyButton(especialF);
        }
       
        if (poderE == 6)
        {
            OnCreateButton(especialF);
        }

        if (poderE > 6) 
        {
            poderE = 6;
        }
    }
    public void JogarNovamente()
    {
        Turnos(false);
        PlayerScript.singleton.classe.vida = PlayerScript.singleton.classe.vidaMax / 2;
        PlayerScript.singleton.tentativas--;

    }
    public void Death()
    {
        if(Enemy.vida <= 0)
        {
            GameObject.Destroy(enemyPrefeb);
            PainelDeVitoriaD.SetActive(true);
            i = 0;
            TrocarDeInimigo();
        }

        if (PlayerScript.singleton.classe.vida <= 0)
        {
            if (PlayerScript.singleton.tentativas <= 1)
            {
                panelDeDerrotaD.SetActive(true);
                PlayerScript.singleton.tentativas = 3;
            }
            else
            {
                if (PlayerScript.singleton.tentativas > 1)
                 PainelDeDerrota.SetActive(true);
            }
        }
    }
    public void TurnoInimigo()//TURNO DO INIMIGO
    {
        Debug.Log("TURNO DO INIMIGO");
        RandomAtaq();
        if (enemyA == true)
        {
            Debug.Log("INIMIGO ATACOU");
            if (Enemy.forca > valorPlayer && playerA == true)
            {
                dano = Enemy.forca + 5;
                PlayerScript.singleton.classe.vida = PlayerScript.singleton.classe.vida - dano;
                dano = 0;
                Debug.Log("INIMIGO ACERTOU UM ATAQUE CRITICO A VIDA DO PLAYER FOI PARA " + PlayerScript.singleton.classe.vida);
            }
            else if (Enemy.forca < valorPlayer && playerA == true)
            {
                dano = valorPlayer + 5;
                Enemy.vida = Enemy.vida - dano;
                dano = 0;
                Debug.Log("INIMIGO TOMOU UM CONTRA ATAQUE CRITICO A VIDA DO INIMIGO FOI PARA " + Enemy.vida);

            }
            if (Enemy.forca > valorPlayer && playerA == false)
            {//PLAYER TA ATACANDO E O GOLEM DEFENDENDO RESULTADO GOLEM TOMA DANO
                Debug.Log(valorPlayer);
                dano = Enemy.forca - valorPlayer;
                PlayerScript.singleton.classe.vida = PlayerScript.singleton.classe.vida - dano;
                dano = 0;
                Debug.Log("INIMIGO ACERTOU ATAQUE PODEREM O PLAYER ESTAVA EM MODO DE DEFESA, MESMO ASSIM A DEFESA DO PLAYER N FOI SUFICIENTE " + PlayerScript.singleton.classe.vida);
            }
            else if (Enemy.forca <= valorPlayer && playerA != true)
            {//PLAYER TA ATACANDO E O GOLEM DEFENDENDO RESULDADO GOLEM N TOMA DONO
                dano = 0;
                Debug.Log("MISS " + PlayerScript.singleton.classe.vida);
            }

            if (i > 0)
            {
                i -= 1;
                Enemy.vida -= 20;
            }

        }
        else
        {
            Debug.Log("INIMIGO ENTROU EM MODO DE DEFESA");
            enemyA = false;
        }
        Death();
    }
    public void TurnoDoPlayer()
    {
        // SE A OPCAO DO PLAYER FOR 1 ELE ATACA 
        if (opcao == 1)
        {
            Debug.Log("OPÇÃO FOI ATACAR");
            playerA = true;//VARIAVEL BOOL PARA DEFINIR SE O PLAYER ESTÁ ATACANDO OU N 
            AcureceValueModif(PlayerScript.singleton.classe.forca);//PEGA OS VALORES DO PLAYER, TANTO DEFESA E MODIFICA ADICIONANDO NO PARAMENTRO

            if (valorPlayer > Enemy.forca && enemyA == true)//FAZ UMA VERIFICACAO SE O ATAQUE DO PLAYER É MAIOR OU IGUAL A FORÇA DO INIMIGO E SE O INMIGO ESTÁ ATACANDO SE ISSO TUDO ESTIVER CORRETO 
            {
                //ATAQUE CRITICO DO PLAYER SOBRE O INIMGO
                dano = valorPlayer + 10;//O PLAYER VAI CRITAR DANDO UM VALOR ADICIONAL AO ATAQUE
                Enemy.vida = Enemy.vida - dano;//SUBTRAI O A VIDA PELO DANO
                dano = 0;//RESETA O VALOR DO DANO
            }
            else if (valorPlayer < Enemy.forca && enemyA == true)//VERIFICA SE O ATAQUE DO INIMIGO É MAIOR QUE O DO PLAYER E SE O INIMIGO ESTÁ ATACANDO SE TUDO ISSO FOR VERDADE
            {
                //CRITICO DO INIMIGO
                dano = Enemy.forca + 5;//ACRESCENTA UM VALOR AO DANO DO INIMIGO
                PlayerScript.singleton.classe.vida = PlayerScript.singleton.classe.vida - dano;//SUBTRAI A VIDA DO PLAYER PELO DANO
                dano = 0;
            }
            if (valorPlayer > Enemy.defesa && enemyA == false)// VERIFICA SE O ATAQUE DO PLAYER É MAIOR DO QUE A DEFESA DO INIMGO E SE O INIMGO NÃO ESTA ATACANDO, SE ISSO FOR VERDADE
            {
                // DANO MENOR 
                dano = valorPlayer - Enemy.defesa;// SUBTRAI O DANO DO PLAYER PELA DEFESA DO INIMIGO
                Enemy.vida = Enemy.vida - dano;//SUBTRAI A VIDA DO INMIGO PELA VIDA DO PLAYER
                dano = 0;
            }
            else if (valorPlayer <= Enemy.defesa && enemyA != true)// VERIFICA SE A FORÇA DO PLAYER É MENOR QUE A DEFESA DO INIMIGO E VERIFICA SE O INIMIGO NÃO ESTÁ ATACANDO, SE ISSO FOR VERDADE 
            {
                //INIMIGO DEFENDEU
                dano = 0;
            }
            playerA = true;//TERMINA A AÇÃO DO PLAYER DEIXANDO ELE EM MODO DE ATAQUE
        }
        else if (opcao == 2)//DEFESA
        {
            AcureceValueModif(AcureceValue(PlayerScript.singleton.classe.defesa));//ADICIONA O ACURECEVALUE NA DEFESA PARA VER SE O PLAYER VAI SAIR COM UMA DEFESA MAIS FORTE, NORMAL, FRACA OU ZERADA
            playerA = false;//DEIXA O PLAYER NO MODO DE DEFESA
        }
        else if (opcao == 3)//ESPECIAL NORMAL
        {
            if (UiScript.playerClass == 1) //VERIFICA SE OPÇÃO NO UISCRIPT FOI UM NINJA PARA ESPECIFICAR O ESPECIAL DO NINJA
            {
                efeitosobreI = PlayerScript.singleton.classe.EspecialB(Enemy);//CHAMA O ESPECIAL  NOMAL QUE VEM DA CLASSE BASE
            }
           
            if (UiScript.playerClass == 2)//VERIFICA SE OPÇÃO NO UISCRIPT FOI UM NINJA PARA ESPECIFICAR O ESPECIAL DO MAGO
            {
                efeitosobreI = PlayerScript.singleton.classe.EspecialB(Enemy);// CHAMA O ESPECIAL  NOMAL QUE VEM DA CLASSE BASE
            }

            if (UiScript.playerClass == 3) //VERIFICA SE OPÇÃO NO UISCRIPT FOI UM NINJA PARA ESPECIFICAR O ESPECIAL DO BERSERKER
            {
                efeitosobreI = PlayerScript.singleton.classe.EspecialB(Enemy);// CHAMA O ESPECIAL  NOMAL QUE VEM DA CLASSE BASE

            }


            poderE -= 4;//DIMINUI O PODER USADO PELO ESPECIAL 
            playerA = true;//DEIXA O PLAYER EM MODO DE ATAQUE
        }
        else if (opcao == 4)
        {
            if (UiScript.playerClass == 1)
            {
                efeitosobreI = false;
            }

            if (UiScript.playerClass == 2)
            {
                efeitosobreI = false;
                i = 3;
            }

            if (UiScript.playerClass == 3)
            {
                efeitosobreI = false;
            }

            PlayerScript.singleton.classe.EspecialF(Enemy);
            poderE -= 7;
            playerA = true;
        }
        poderE += 1;

        Death();
        Turnos(true);
       
    }
    public void Turnos(bool turno) 
    {
        if (turno == true) 
        {
            if (efeitosobreI == true) //CASO O PLAYER ATIVE O ESPECIAL O INIMIGO VAI VERIFICAR SE O EFEITOSOBREOINIMIGO É TRUE
            {
                if (opcao == 3)
                {
                    if (UiScript.playerClass == 1)
                    {
                        Debug.Log("O INIMIGO DEU MISS");
                        efeitosobreI = false;
                        return;
                    }
                    else if (UiScript.playerClass == 2)
                    {
                        Debug.Log("O INIMIGO FICOU CONGELADO");
                        efeitosobreI = false;
                        return;
                    }
                }
            }
            else
            {
                TurnoInimigo();
            }
        }
        else 
        {
            if (UiScript.playerClass == 3)
            {
                if (PlayerScript.singleton.classe.vidaMax > 200)
                {
                    PlayerScript.singleton.classe.vidaMax -= 50;
                    PlayerScript.singleton.classe.vida -= 50;
                    PlayerScript.singleton.classe.forca -= 30;
                    PlayerScript.singleton.classe.defesa -= 30;

                    if (valorF > PlayerScript.singleton.classe.forca)
                    {
                        PlayerScript.singleton.classe.forca = valorF;
                    }

                    if (valorF > PlayerScript.singleton.classe.defesa)
                    {
                        PlayerScript.singleton.classe.defesa = valorD;
                    }
                }
            }
            TurnoDoPlayer();
        }
    }
    public void TrocarDeInimigo()
    {
        if (enemyAtual < Enemys.Length)
        {
            if (enemyAtual > 4)
            {
                PainelDeVitoriaD.SetActive(true);
            }
            else
            {
                enemyAtual++;
                Enemy = Enemys[enemyAtual];
                enemyPrefeb = Instantiate(Enemy.prefab);
                Debug.Log("UM NOVO INIMIGO SURGIU, SUA VIDA É " + Enemy.vida);
            }
        }
    }
}
