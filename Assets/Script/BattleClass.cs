using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleClass : MonoBehaviour
{
    //CRIANDO O STATUS DOS INIMIGOS
    int enemyAtual = 0;//INICIALIZANDO O VETOR NA POSIÇÃO 0, ESSE VETOR É O VETOR QUE CONTA A QUANTIDADE DE INIMIGOS.
    public ClasseBase golem = new Golem(300, 300, 33, 70);//CRIA UM GOLEM 
    public ClasseBase goblin = new Goblin(150, 150, 30, 10);//CRIA UM GOBLIN
    public ClasseBase dragao = new Dragao(500, 500, 70, 50);//CRIA UM DRAGÃO
    public ClasseBase necromancer = new Necromancer(150, 150, 60, 36);//CRIA UM NECROMANCER
    public ClasseBase ogro = new Ogro(200,200, 40, 26);//CRIANDO UM OGRO
    public static ClasseBase Enemy { get; set; }//METODO GET SET PARA ARMAZENAR OS INIMIGOS
    private ClasseBase[] Enemys = new ClasseBase[5];//VETOR QUE ARMAZENA OS INIMIGOS É IGUAL A 5
    //REFERENCIAS PARA GAMEOBJECTS
    public GameObject ogroP;
    public GameObject dragaoP;
    public GameObject goblinP;
    public GameObject necromancerP;
    public GameObject golemP;
    public GameObject PainelDeDerrota,panelDeDerrotaD;
    public GameObject PainelDeVitoria,PainelDeVitoriaD;
    private GameObject enemyPrefeb;
    public Button especialB, especialF;
    //VALORES STATICOS PARA ARMAZENAR A FORÇA E A DEFESA ATUAL, USADO PARA O BERSERKER
    public static int valorD;
    public static int valorF;
    //VARIAVEIS TIPO INT USADAS PARA BATALHA
    private int acurice, opcao, dano, valorPlayer, poderE;
    //VARIAVEIS TIPO BOOL USADAS PARA BATALHA
    private bool enemyA, playerA,efeitosobreI;
    
    //VARIAVEL USADA COMO CONTADOR
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
        //DESATIVA A FUNÇÃO PARA O ATAQUE BASICO
        //DESATIVA A FUNÇÃO PARA O ATAQUE FORTE
        OnButton(especialF, false);
        OnButton(especialB,false);
        poderE = 0;//PODER ESPECIAL IGUAL A 0 ISSO É UM CONTADOR PARA QUE POSTERIORMENTE QUANDO ESSE PODER CHEGAR A 3 E 6 O PLAYER PODER USAR O ESPECIAL
    }
    private void Start()
    {   
        //DEFINE QUAL É O VALOR INICIAL DA FORÇA E DA DEFESA DO PLAYER- SERVE SÓ PARA O BERSERKER
        BattleClass.valorD = PlayerScript.singleton.classe.defesa;
        BattleClass.valorF = PlayerScript.singleton.classe.forca;
    }
    private void Update()
    {
        VereficarBotton();//CHAMA A VERIFICAÇÃO DO ESPECIAL
    }
    private int AcureceValue(int valor)//ACURECE VALUE RECEBE POR PARAMETRO UM VALOR ESSE VALOR É OU O ATAQUE OU A DEFESA DO PLAYER
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
    private void AcureceValueModif(int valor)//RECEBE POR PARAMETRO O VALOR
    {
        valorPlayer = valor;//VALORPLAYER É A VARIAVEL GLOBAL PARA DEFESA E ATAQUE ETC ELA RECEBE UM PARAMETRO QUE É DEFINIDO NOS BOTOES 
        //E POSTERIORMENTE É MODIFICADA NO ACURECEVALUE
    }
    public void OpAtaque(int op)//RECEBE POR PARAMENTRO OPCÃO QUE O PLAYER ESCOLHEU
    {
        opcao = op;//RECEBE A OPÇÃO POR PARAMETRO E ARMAZENA EM OPÇÃO
        Turnos(false);//VERIFICA SE É O TURNO DO PLAYER, ESTANDO TRUE É O TURNO DO INIMIGO, FALSE É DO PLAYER
    }
    public void RandomAtaq()//RANDOM PARA FUNCIONALIDADE DO INIMIGO
    {
        //FAZ UM RANDOM DE 0 A 10 E ARMAZENA NO RANDOMATAQ
        int randomAtaq = Random.Range(0, 10);

        //VERIFICA SE A VIDA DO INIMIGO ESTÁ ABAIXA OU MENOR DE 30 SE ISSO FOR VERDADE ENTÃO 
        if (Enemy.vida <= 30) 
        {
            //VAI TER OUTRA VERIFICAÇÃO, DE O RANDOM FOR MENOS QUE 8 ENTÃO O INIMIGO RECUPERA VIDA
            if(randomAtaq < 8) 
            {
                enemyA = false;//DEIXA O INIMIGO EM MODO DE DEFESA 
                UIImagemEEnemy.EnemyAtacando = 2;
                Enemy.vida += 20;//RECUPERA A VIDA
            }
            else 
            {
                enemyA = true;//DEIXA O INIMIGO EM MODO DE ATAQUE
                UIImagemEEnemy.EnemyAtacando = 1;
                Enemy.forca += 3;//AUMENTA A FORÇA DO INIMIGO PARA MAIS 3 (esse valor vai aumentando e não baixa) é um jeito de forçar o player a matar o inimigo de uma vez antes que o bixo esteja full bufado
            }
        }
        if (randomAtaq <= 5)//SE O INIMIGO NÃO ESTIVER COM A VIDA BAIXA ENTÃO VAI SER UM RANDOM 50% 
        {
            enemyA = true;//OU ATACA 
            UIImagemEEnemy.EnemyAtacando = 1;
        }
        else
        {
            enemyA = false;//OU DEFENDE
            UIImagemEEnemy.EnemyAtacando = 2;
        }
    }
    public void OnButton(Button especial,bool status)//FUNÇÃO PARA ATIVAR E DESATIVAR BOTÃO
    {
        if (status == true)//ATIVA O BOTÃO E DEIXA COM A COR BRANCA
        {
            especial.enabled = true;
            especial.image.color = Color.white;
        }
        else //DESATIVA O BOTÃO E DEIXA COM A COR VERMELHA
        {
            especial.enabled = false;
            especial.image.color = Color.red;
        }
        
    }
    public void VereficarBotton() //VERIFICA A QUANTIDADE DE PODER QUE O PLAYER TEM 
    {
        if (poderE < 3) //SE O PLAYER ESTÁ COM MENOS DE 3 DE PODER ENTÃO O ELE ESTÁ SEM PODER USAR AS SUAS HABILIDADES
        {
            OnButton(especialB, false);//CHAMA A FUÇÃO RECEBENDO POR PARAMETRO O BOTÃO E O STATUS EM QUE O BOTÃO VAI SAIR
            OnButton(especialF, false);//CHAMA A FUÇÃO RECEBENDO POR PARAMETRO O BOTÃO E O STATUS EM QUE O BOTÃO VAI SAIR
        }

        if (poderE > 3 &&  poderE < 6) //SE O PLAYER ESTÁ COM MAIS DE 3 E MENOS DE 6 ENTÃO O PLAYER SÓ PODE USAR UMA HABILIDADE QUE É A HABILIDADE 1
        {
            OnButton(especialB, true);//CHAMA A FUÇÃO RECEBENDO POR PARAMETRO O BOTÃO E O STATUS EM QUE O BOTÃO VAI SAIR
            OnButton(especialF, false);//CHAMA A FUÇÃO RECEBENDO POR PARAMETRO O BOTÃO E O STATUS EM QUE O BOTÃO VAI SAIR
        }
       
        if (poderE == 6)//SE O PODER DO PLAYER ESTÁ EM 6 ENTÃO LIBERA O BOTÃO DO ESPECIAL 2
        {
            OnButton(especialF, true);//CHAMA A FUÇÃO RECEBENDO POR PARAMETRO O BOTÃO E O STATUS EM QUE O BOTÃO VAI SAIR
        }

        if (poderE > 6) //SE PASSAR DE 6 ENTÃO TRAVA O PODER EM 6
        {
            poderE = 6;
        }
    }
    public void JogarNovamente()//JOGAR NOVAMENTE
    {
        PlayerScript.singleton.classe.vida = PlayerScript.singleton.classe.vidaMax / 2;//DEIXA A VIDA DO PLAYER NA METADE
        PlayerScript.singleton.tentativas--;//DIMINUI UMA TENTATIVA 
    }
    public void Death()//VERIFICADOR DE MORTE DO PLAYER E DO INIMIGO
    {
        if (Enemy.vida <= 0)//SE A VIDA DO INIMIGO FOR MENOR OU IGUAL A 0
        {
            GameObject.Destroy(enemyPrefeb);//DESTROY O INIMIGO
            PainelDeVitoria.SetActive(true);//ATIVA O PRIMEIRO PAINEL DE VITORIA 
            i = 0;//ESSA VARIAVEL É A VARIAVEL PARA O DEBUF DO MAGO, É NESCESSARIO POR AQUI PARA QUE NÃO HAJA BUGS COMO O DEBUF CONTINUAR NO OUTRO INIMIGO
            TrocarDeInimigo();//CHAMA PROXIMO INIMIGO
           
        }

        if (PlayerScript.singleton.classe.vida <= 0)//VERIFICA SE A VIDA DO PLAYER CHEGOU A 0 OU MENOS 
        {
            if (PlayerScript.singleton.tentativas <= 1)//VERIFICA SE AS TENTATIVAS DE MORTE ESTÁ MENOR OU IGUAL A 1
            {
                panelDeDerrotaD.SetActive(true);//ATIVA O PAINEL DE DERROTA 
                PainelDeDerrota.SetActive(false);//DESATIVA O PAINEL DE DERROTA
                PainelDeVitoria.SetActive(false);//DESATIVA O PAINEL DE VITORIA
             
            }
            else
            {
                if (PlayerScript.singleton.tentativas > 1)//SE AS TENTATIVAS AINDA NÃO ACABARAM ENTÃO
                PainelDeDerrota.SetActive(false);//DESATIVA O PAINEL DE DERROTA
                PainelDeVitoria.SetActive(false);//PAINEL DE VITORIA FICA DESATIVADO PARA NÃO HAVER BUGS COMO O INIMIGO MATOU E O PLAYER MATOU SE ISSO ACONTECER O PAINEL SIMPLESMENTE NÃO APARECE E O PLAYER NÃO PODE USAR O BUF
                PainelDeDerrota.SetActive(true);//DESATIVA O PAINEL DE DERROTA
            }
        }
    }
    public void TurnoInimigo()//TURNO DO INIMIGO
    {
        RandomAtaq();//CHAMA A FUNÇÃO DE RANDOM ATAQUE
        if (enemyA == true)//VERIFICA SE O INIMIGO ESTÁ EM MODO DE ATAQUE
        {
            if (Enemy.forca > valorPlayer && playerA == true)//VERIFICA SE A FORÇA DO INIMIGO É MAIOR QUE A FOÇA DO PLAYER E SE O PLAYER ESTÁ EM MODO DE ATQUE
            {
                int randomAtaq = Random.Range(0, 10);// FAZ UM RANDOM DE 0 A 10
                
                if (randomAtaq <= 6)// SE NESSE RANDOM A VARIAVEL LOCAL SAIR MENOR OU IGUAL A 6
                {
                    dano = Enemy.forca - PlayerScript.singleton.classe.forca;//O DANO DO PLAYER VAI DIMINUIR DO DANO DO INIMIGO E O RESULTADO VAI SER O DANO SOBRE A VIDA DO PLAYER, UM CONTRA ATAQUE FRACO
                    PlayerScript.singleton.classe.vida = PlayerScript.singleton.classe.vida - dano;
                    dano = 0;
                }
                else// SE NÃO ESSE CONTRA ATAQUE VAI SER UM UM CONTRA ATAQUE FORTE 
                {
                    dano = Enemy.forca + 5;
                    PlayerScript.singleton.classe.vida = PlayerScript.singleton.classe.vida - dano;
                    dano = 0;
                }

            }//VERIFICADOR DE CONTRA ATAQUE 
            else if (Enemy.forca < valorPlayer && playerA == true)//VERIFICADOR DE ATAQUE DO PLAYER É MAIOR QUE A DO INIMIGO E SE O PLAYER ESTÁ EM MODO DE ATAQUE SE TUDO ISSO FOR VERDADE ENTÃO O INIMIGO VAI TOMAR UM DANO DE CONTRA ATAQUE
            {
                dano = valorPlayer + 5;//DANO 
                Enemy.vida = Enemy.vida - dano;
                dano = 0;
            }//VERIFICADOR DE CONTRA ATAQUE
            if (Enemy.forca > valorPlayer && playerA == false)
            {//PLAYER TA ATACANDO E O INIMIGO DEFENDENDO RESULTADO INIMIGO TOMA DANO
                dano = Enemy.forca - valorPlayer;
                PlayerScript.singleton.classe.vida = PlayerScript.singleton.classe.vida - dano;
                dano = 0;
            }//VERIFICADOR DE ATAQUE
            else if (Enemy.forca <= valorPlayer && playerA != true)
            {//PLAYER TA ATACANDO E O INIMIGO DEFENDENDO RESULDADO INIMIGO N TOMA DONO
                dano = 0;
            }//VERIFICADOR DE ATAQUE
            if (i > 0)//FUNÇÃO PARA DINIMIUIR DBUF DO ESPECIAL DO MAGO 
            {
                i -= 1;//DIMINUI 1 DO CONTADOR 
                Enemy.vida -= 30;//DIMINUI 30 DE VIDA DO INIMIGO
            }
        }//FUNÇÃO NO TURNO DE ATAQUE DO INIMIGO
        else//TURNO DE DEFESA DO INIMIGO
        {
            enemyA = false;
        }
        Death();//CHAMA A FUNÇÃO DE VERIFICAÇÃO DE MORTE 
    }
    public void TurnoDoPlayer()
    {
        // SE A OPCAO DO PLAYER FOR 1 ELE ATACA 
        if (opcao == 1)
        {
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
        }//PLAYER ATACA
        else if (opcao == 2)//DEFESA
        {
            AcureceValueModif(AcureceValue(PlayerScript.singleton.classe.defesa));//ADICIONA O ACURECEVALUE NA DEFESA PARA VER SE O PLAYER VAI SAIR COM UMA DEFESA MAIS FORTE, NORMAL, FRACA OU ZERADA
            playerA = false;//DEIXA O PLAYER NO MODO DE DEFESA
        }//PLAYER DEFENDE
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
        }//PLAYER DA O ESPECIAL DE 3 DE PODER
        else if (opcao == 4)//PLAYER DA O ESPECIAL DE 6 DE PODER
        {
            if (UiScript.playerClass == 1)//VERIFICA SE ELE É UM NINJA
            {
                efeitosobreI = false;//DEIXA O EFEITO SOBRE O INIMIGO DESATIVADO
            }

            if (UiScript.playerClass == 2)//VERIFICA SE O PLAYER É UM MAGO
            {
                efeitosobreI = false;//DEIXA O EFEITO SOBRE O INIMIGO DESATIVADO
                i = 3;//DEIXA O CONTADOR IGUAL A 3 PARA QUE A HABILIDADE DO MAGO DURE POR 3 TURNOS
            }

            if (UiScript.playerClass == 3)//VERIFICA SE É UM BERSERKER
            {
                efeitosobreI = false;//DEIXA O EFEITO SOBRE O INIMIGO DESATIVADO
            }

            PlayerScript.singleton.classe.EspecialF(Enemy);
            poderE -= 7;
            playerA = true;
        }
        poderE += 1;//ADICIONA 1 DE PODER AO FINALIZAR A RODADA
        Death();//CHAMA A FUNÇÃO DE MORTE 
        Turnos(true);//CHAMA O TURNO DO INIMIGO
       
    }//TURNO DO PLAYER
    public void Turnos(bool turno) 
    {
        if (turno == true) //SE O TURNO ESTIVER TRUE ENTÃO É O TURNO DO INIMIGO
        {
            if (efeitosobreI == true) //CASO O PLAYER ATIVE O ESPECIAL O INIMIGO VAI VERIFICAR SE O EFEITOSOBREOINIMIGO É TRUE
            {
                if (opcao == 3)//CASO O ESPECIAL SEJA O ESPECIAL 1 QUE É O MAIS FRACO
                {
                    //ESSE CASE DE IF É IGUAL MAS OPTAMOS POR DEIXAR UM PARA CADA CLASS PARA DEIXAR PADRONIZADO
                    if (UiScript.playerClass == 1)//VERIFICA SE O ESPECIAL É DO NINJA
                    {
                        efeitosobreI = false;//DESATIVA O ESPECIAL
                        return;//RETORNA
                    }
                    else if (UiScript.playerClass == 2)//VERIFICA SE O ESPECIAL É DO MAGO
                    {
                        efeitosobreI = false;//DESATIVA O ESPECIAL
                        return;//RETORNA
                    }
                }
            }
            else// SE NÃO HOUVER UM DEBUF ENTÃO O TURNO SEGUE IGUAL 
            {
                TurnoInimigo();//CHAMA O TURNO DO INIMIGO
            }
        }//TURNO DO INIMIGO
        else 
        {
            if (UiScript.playerClass == 3)//VERIFICA SE É UM BERSERKER 
            {
                if (PlayerScript.singleton.classe.vidaMax > 200)//VERIFICA SE A VIDA MAXIMA DO BERSERKER É MAIOR QUE 200(VIDA MAXIMA DO BERSERKER)
                {
                    PlayerScript.singleton.classe.vidaMax -= 25;//DIMINUI A VIDA MAXIMA DO BERSERKER
                    PlayerScript.singleton.classe.vida -= 25;//DIMINUI A VIDA DO BERSERKER
                    PlayerScript.singleton.classe.forca -= 20;//DIMINUI A FORÇA DO BERSERKER
                    PlayerScript.singleton.classe.defesa -= 20;//DIMINUI A DEFESA DO BERSERKER

                    if (valorF > PlayerScript.singleton.classe.forca)//VERIFICA SE A FORÇA INICIAL DO BERSERKER É MAIOR QUE A FORÇA 
                    {
                        PlayerScript.singleton.classe.forca = valorF;//ENTÃO FORÇA RECEBE FORÇA INICIAL
                    }
                                                                    //MESMA COISA PARA DEFESA
                    if (valorF > PlayerScript.singleton.classe.defesa)
                    {
                        PlayerScript.singleton.classe.defesa = valorD;
                    }
                }
            }
            TurnoDoPlayer();
        }//TURNO DO PLAYER 
    }//FUNÇÃO PARA GERENCIAR OS TURNOS
    public void TrocarDeInimigo()
    {
        if (enemyAtual < Enemys.Length)//VERIFICA SE O ENEMYATUAL É MENOR QUE O TAMANHO DO VETOR 
        {
            if (enemyAtual > 3)// SE ESSE ENEMYATUAL FOR MAIOR QUE 4 ENTÃO
            {
                PainelDeVitoria.SetActive(false);//PAINEL DE VITORIA ATIVADO
                PainelDeVitoriaD.SetActive(true);//PAINEL DE VITORIA ATIVADO
            }
            else
            {
                enemyAtual++;
                //LUGAR NO VETOR DO INIMIGO ATUAL  
                Enemy = Enemys[enemyAtual];//TROCA PARA O PROXIMO INIMIGO DO VETOR
                enemyPrefeb = Instantiate(Enemy.prefab);//INSTACIA O INIMIGO
                Enemy.vida = Enemy.vidaMax;//DEIXA A VIDA DO INIMIGO IGUAL AO INIMIGO VIDA MAX
               
            }
        }
    }//TROCA DE INIMIGO
}
