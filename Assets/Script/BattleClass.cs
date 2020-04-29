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
    public static ClasseBase enemy { get; set; }
    private ClasseBase[] enemys = new ClasseBase[5];

    public GameObject ogroP;
    public GameObject dragaoP;
    public GameObject goblinP;
    public GameObject necromancerP;
    public GameObject golemP;

    private GameObject enemyPrefeb;
    public static UiFeedBack Mensagem;

    public GameObject PainelDeDerrota,Passarturno,panelDeDerrotaD;
    public GameObject PainelDeVitoria;
    public GameObject PainelDeVitoriaD;


    public Button especialB, especialF;
    private int acurice, opcao, dano, valorPlayer, poderE;
    private bool enemyA, playerA,efeitosobreI;
    public bool turnoPlayer = true;
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
        enemys[0] = goblin; ;
        enemys[1] = ogro;
        enemys[2] = golem;
        enemys[3] = necromancer;
        enemys[4] = dragao;
        enemy = enemys[0];//INICIA COM VETOR IGUAL A 0

        enemyPrefeb = Instantiate(enemy.prefab) as GameObject;//ISTANCIA OS INIMIGOS
        var ClassePrefab = Instantiate(PlayerScript.singleton.classe.prefab) as GameObject;//ISTANCIA O PLAYER
        OnDestroyButton(especialB);//DESATIVA A FUNÇÃO PARA O ATAQUE BASICO
        OnDestroyButton(especialF);//DESATIVA A FUNÇÃO PARA O ATAQUE FORTE
        Passarturno.SetActive(false);//PASSA O TURNO PARA O INIMIGO
        poderE = 0;//PODER ESPECIAL IGUAL A 0 ISSO É UM CONTADOR PARA QUE POSTERIORMENTE QUANDO ESSE PODER CHEGAR A 3 E 6 O PLAYER PODER USAR O ESPECIAL
    }

    private void Start()
    {
        Mensagem = GameObject.Find("UIManager").GetComponent<UiFeedBack>();//PROCURA REFENCIA DO SCRIPT PELO UIMANAGER
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
    public void TurnoPlayer(int op)//TURNO DO PLAYER
    {
       
        opcao = op;
        //VERIFICA SE É O TURNO DO PLAYER, ESTANDO TRUE É O TURNO DO PLAYER
        if (turnoPlayer == true)
        {
            // SE A OPCAO DO PLAYER FOR 1 ELE ATACA 
            if (opcao == 1)
            {
                Debug.Log("OPÇÃO FOI ATACAR");
                playerA = true;//VARIAVEL BOOL PARA DEFINIR SE O PLAYER ESTÁ ATACANDO OU N 
                AcureceValueModif(PlayerScript.singleton.classe.forca);//PEGA OS VALORES DO PLAYER, TANTO DEFESA E MODIFICA ADICIONANDO NO PARAMENTRO
               
                if (valorPlayer > enemy.forca && enemyA == true)//FAZ UMA VERIFICACAO SE O ATAQUE DO PLAYER É MAIOR OU IGUAL A FORÇA DO INIMIGO E SE O INMIGO ESTÁ ATACANDO SE ISSO TUDO ESTIVER CORRETO 
                {
                    //ATAQUE CRITICO DO PLAYER SOBRE O INIMGO
                    dano = valorPlayer + 10;//O PLAYER VAI CRITAR DANDO UM VALOR ADICIONAL AO ATAQUE
                    enemy.vida = enemy.vida - dano;//SUBTRAI O A VIDA PELO DANO
                    dano = 0;//RESETA O VALOR DO DANO
                }
                else if (valorPlayer < enemy.forca && enemyA == true)//VERIFICA SE O ATAQUE DO INIMIGO É MAIOR QUE O DO PLAYER E SE O INIMIGO ESTÁ ATACANDO SE TUDO ISSO FOR VERDADE
                {
                    //CRITICO DO INIMIGO
                    dano = enemy.forca + 5;//ACRESCENTA UM VALOR AO DANO DO INIMIGO
                    PlayerScript.singleton.classe.vida = PlayerScript.singleton.classe.vida - dano;//SUBTRAI A VIDA DO PLAYER PELO DANO
                    dano = 0;
                }
                if (valorPlayer > enemy.defesa && enemyA == false)// VERIFICA SE O ATAQUE DO PLAYER É MAIOR DO QUE A DEFESA DO INIMGO E SE O INIMGO NÃO ESTA ATACANDO, SE ISSO FOR VERDADE
                {
                    // DANO MENOR 
                    dano = valorPlayer - enemy.defesa;// SUBTRAI O DANO DO PLAYER PELA DEFESA DO INIMIGO
                    enemy.vida = enemy.vida - dano;//SUBTRAI A VIDA DO INMIGO PELA VIDA DO PLAYER
                    dano = 0;
                }
                else if (valorPlayer <= enemy.defesa && enemyA != true)// VERIFICA SE A FORÇA DO PLAYER É MENOR QUE A DEFESA DO INIMIGO E VERIFICA SE O INIMIGO NÃO ESTÁ ATACANDO, SE ISSO FOR VERDADE 
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
                    efeitosobreI = PlayerScript.singleton.classe.EspecialB(enemy);//CHAMA O ESPECIAL  NOMAL QUE VEM DA CLASSE BASE
                    Mensagem.Mensagens = PlayerScript.singleton.nomePlayer + " MINHA ALMA BUSCA O EQUILIBRIO.";

                }

                if (UiScript.playerClass == 3) //VERIFICA SE OPÇÃO NO UISCRIPT FOI UM NINJA PARA ESPECIFICAR O ESPECIAL DO BERSERKER
                {
                    efeitosobreI = PlayerScript.singleton.classe.EspecialB(enemy);// CHAMA O ESPECIAL  NOMAL QUE VEM DA CLASSE BASE
                    Mensagem.Mensagens = PlayerScript.singleton.nomePlayer + " VENHAMM!";

                }

                if (UiScript.playerClass == 2)//VERIFICA SE OPÇÃO NO UISCRIPT FOI UM NINJA PARA ESPECIFICAR O ESPECIAL DO MAGO
                {
                    efeitosobreI = PlayerScript.singleton.classe.EspecialB(enemy);// CHAMA O ESPECIAL  NOMAL QUE VEM DA CLASSE BASE
                    Mensagem.Mensagens = PlayerScript.singleton.nomePlayer + " O MEU ZERO É ABSOLUTO...";
                }

                poderE -= 3;//DIMINUI O PODER USADO PELO ESPECIAL 
                playerA = true;//DEIXA O PLAYER EM MODO DE ATAQUE
            }
            else if (opcao == 4)
            {
                if (UiScript.playerClass == 1)
                {
                    efeitosobreI = false;
                    Mensagem.Mensagens =  PlayerScript.singleton.nomePlayer + " UMA LAMINA FIRME EQUILIBRA A ALMA...";
                }

                if (UiScript.playerClass == 3)
                {
                    efeitosobreI = false;
                    Mensagem.Mensagens = PlayerScript.singleton.nomePlayer + " SINTA O PESO DO MARTELOO!!";
                }

                if (UiScript.playerClass == 2)
                {
                    efeitosobreI = false;
                    Mensagem.Mensagens = PlayerScript.singleton.nomePlayer + " OBESERVE E APRENDA.";
                    i = 3;
                }

                PlayerScript.singleton.classe.EspecialF(enemy);
                poderE -= 6;
                playerA = true;
            }

            poderE += 1;
            turnoPlayer = false;
            Passarturno.SetActive(true);
            Death();
        }
        else 
        {
            Passarturno.SetActive(true);
            if (opcao == 5)
            {
                TurnoInimigo();
            }
        }
    }
    public void TurnoInimigo()//TURNO DO INIMIGO
    {
        Debug.Log("TURNO DO INIMIGO");
        if (efeitosobreI == true) //CASO O PLAYER ATIVE O ESPECIAL O INIMIGO VAI VERIFICAR SE O EFEITOSOBREOINIMIGO É TRUE
        {
            if (UiScript.playerClass == 1)//VERIFICA SE A CLASSE EM QUESTÃO É UM NINJA
            { 
                //O INIMIGO GANHA UM BUFF DE EVA, AQUI O INIMIGO ESTÁ ERRANDO O ATAQUE
                Debug.Log("MISS");
                efeitosobreI = false;//DESATIVA O EFEITOSOBREOINIMIGO
            }

            if (UiScript.playerClass == 2)
            {
                Debug.Log("NESSA RODADA O INIMIGO NÃO ATACOU POR ESTAR CONGELADO");
                efeitosobreI = false;//SE SIM ELE VOLTA AO NORMAL 
            }

            Passarturno.SetActive(false);
            turnoPlayer = true;

        }
        else 
        {
            RandomAtaq();
            if (enemyA == true)
            {
                Debug.Log("INIMIGO ATACOU");
                if (enemy.forca > valorPlayer && playerA == true)
                {
                    dano = enemy.forca + 5;
                    PlayerScript.singleton.classe.vida = PlayerScript.singleton.classe.vida - dano;
                    dano = 0;
                    Debug.Log("INIMIGO ACERTOU UM ATAQUE CRITICO A VIDA DO PLAYER FOI PARA " + PlayerScript.singleton.classe.vida);
                }
                else if (enemy.forca < valorPlayer && playerA == true)
                {
                    dano = valorPlayer + 5;
                    enemy.vida = enemy.vida - dano;
                    dano = 0;
                    Debug.Log("INIMIGO TOMOU UM CONTRA ATAQUE CRITICO A VIDA DO INIMIGO FOI PARA " + enemy.vida);

                }

                if (enemy.forca > valorPlayer && playerA == false)
                {//PLAYER TA ATACANDO E O GOLEM DEFENDENDO RESULTADO GOLEM TOMA DANO
                    Debug.Log(valorPlayer);
                    dano =  enemy.forca - valorPlayer;
                    PlayerScript.singleton.classe.vida = PlayerScript.singleton.classe.vida - dano;
                    dano = 0;
                    Debug.Log("INIMIGO ACERTOU ATAQUE PODEREM O PLAYER ESTAVA EM MODO DE DEFESA, MESMO ASSIM A DEFESA DO PLAYER N FOI SUFICIENTE " + PlayerScript.singleton.classe.vida);
                }
                else if (enemy.forca <= valorPlayer && playerA != true)
                {//PLAYER TA ATACANDO E O GOLEM DEFENDENDO RESULDADO GOLEM N TOMA DONO
                    dano = 0;
                    Debug.Log("MISS " + PlayerScript.singleton.classe.vida);
                }
                Death();
                Passarturno.SetActive(false);
                turnoPlayer = true;
            }
            else
            {
                Death();
                Debug.Log("INIMIGO ENTROU EM MODO DE DEFESA");
                //PODEMOS ADICIONAR UM ROLL AQUI TB PARA QUE O INIMIGO TENHA CHANCE DE TOMAR DANO QUANDO TIVER EM MODO DE DEFESA
                enemyA = false;
                Passarturno.SetActive(false);
                turnoPlayer = true;
            }

            if (UiScript.playerClass == 2)
            {
                if (i>0)
                {
                    i -= 1;
                    enemy.vida -= 20;
                    Debug.Log("INIMIGO TOMOU TORNS " + enemy.vida);
                }
            }

            if (UiScript.playerClass == 3) 
            {
                if (PlayerScript.singleton.classe.vida > 200) 
                {
                    PlayerScript.singleton.classe.vidaMax -= 50;
                    PlayerScript.singleton.classe.vida -= 50;
                    PlayerScript.singleton.classe.forca -= 30;
                    PlayerScript.singleton.classe.defesa -= 30;
                }
                Debug.Log("vida " + PlayerScript.singleton.classe.vida + "forca " + PlayerScript.singleton.classe.forca + "defesa " + PlayerScript.singleton.classe.defesa);
            }

        }
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
    public void Duelo(int op)
    {     
        TurnoPlayer(op);  
    }
    public void JogarNovamente()
    {
        turnoPlayer = true;
        PlayerScript.singleton.classe.vida = PlayerScript.singleton.classe.vidaMax / 2;
        PlayerScript.singleton.tentativas--;

    }
    public void Death()
    {
        if(enemy.vida <= 0)
        {
            Debug.Log("VOCE VENCEU BLUAHAHAHAH");
            GameObject.Destroy(enemyPrefeb);
            turnoPlayer = true;
            PainelDeVitoriaD.SetActive(true);
            TrocarDeInimigo();           
        }

        if (PlayerScript.singleton.classe.vida <= 0)
        {
            if (PlayerScript.singleton.tentativas <= 1)
            {
                panelDeDerrotaD.SetActive(true);
            }
            else
            {
                if (PlayerScript.singleton.tentativas > 1)
                    PainelDeDerrota.SetActive(true);
            }
        }

        return;
    }
    public void TrocarDeInimigo()
    {
        if (enemyAtual < enemys.Length)
        {
            if(enemyAtual > 4)
            {
                PainelDeVitoriaD.SetActive(true);
            }
            else
            {
                enemyAtual++;
                enemy = enemys[enemyAtual];
                enemyPrefeb = Instantiate(enemy.prefab);
                Debug.Log("UM NOVO INIMIGO SURGIU, SUA VIDA É " + enemy.vida);
            }         
        }

    }
    public void PlayerAtaque(bool seila)
    {
        turnoPlayer = seila;
    }
}
