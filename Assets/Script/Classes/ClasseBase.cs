using UnityEngine;
using System.Collections;

public abstract class ClasseBase
{
    public int vida;
    public int forca;
    public int defesa;
    public int vidaMax;
    public int forcaMax;
    public int defesaMax;
    public GameObject prefab;
    public abstract bool EspecialB(ClasseBase inimigo);
    public virtual bool Roll(ClasseBase inimigo) //FAZ UM ROLL PARA VERIFICAR SE O INIMIGO VAI DAR MIRR NO TURNO SEGUINTE 
    {
        int acurice = Random.Range(0, 10);
        if (acurice >= 0 && acurice <= 7)
        {
            //ACONTECE O MISS
        }
        else if (acurice >= 8 && acurice == 10)
        {
            inimigo.forca += inimigo.forca;

            Debug.Log(vida);
        }
        return true;
    }
    abstract public void EspecialF(ClasseBase inimigo);

}

public class Ninja : ClasseBase
{
    public Ninja(int vidaMax, int defesaMax, int forcaMax, int vida, int forca, int defesa)
    {
        this.vida = vida;
        this.forca = forca;
        this.defesa = defesa;
        this.vidaMax = vidaMax;
        this.defesaMax = defesaMax;
        this.forcaMax = forcaMax;
    }
<<<<<<< Updated upstream
    public int Especial()
    {
        vida += 60;
        int forcaE = forca + 35;
        Debug.Log("VIDA RECUPERADA " + vida);
        return forcaE;

    }
=======
    
>>>>>>> Stashed changes
    public override void EspecialF(ClasseBase inimigo)
    {
        inimigo.vida -= forca;
        inimigo.vida -= forca + forca/2;
        inimigo.vida -= forca + forca;
    }
    public override bool EspecialB(ClasseBase inimigo)
    {
        vida += 50;
        if (vida > vidaMax)
        {
            vida = vidaMax;
        }
        return Roll(inimigo);
    }
    public override bool Roll(ClasseBase inimigo)
    {
        return base.Roll(inimigo);
    }
}

public class Berserker : ClasseBase
{
    public Berserker(int vidaMax, int defesaMax, int forcaMax, int vida, int forca, int defesa)
    {
        this.vida = vida;
        this.forca = forca;
        this.defesa = defesa;
        this.vidaMax = vidaMax;
        this.defesaMax = defesaMax;
        this.forcaMax = forcaMax;
    }

    public override bool EspecialB(ClasseBase inimigo)
    {
        vida = 300;
        vidaMax = 300;
        forca = 70;
        defesa = 70;
        return false;
    }
    public override void EspecialF(ClasseBase inimigo)
    {
        int dano;
        dano = forca * 5;
        inimigo.vida -= dano;
    }
}

public class Mago : ClasseBase
{
    public Mago(int vidaMax, int defesaMax, int forcaMax, int vida, int forca, int defesa)
    {
        this.vida = vida;
        this.forca = forca;
        this.defesa = defesa;
        this.vidaMax = vidaMax;
        this.defesaMax = defesaMax;
        this.forcaMax = forcaMax;
    }
    public override bool EspecialB(ClasseBase inimigo)
    {
        return true;
    }
    public override void EspecialF(ClasseBase inimigo)
    {
        inimigo.vida -= 150;
    }

}


public class Golem : ClasseBase
{

    public Golem(int vidaMax, int vida, int forca, int defesa)
    {
        this.vida = vida;
        this.forca = forca;
        this.defesa = defesa;
        this.vidaMax = vidaMax;
    }
    public override bool EspecialB(ClasseBase inimigo)
    {
        throw new System.NotImplementedException();
    }
    public override void EspecialF(ClasseBase inimigo)
    {
        throw new System.NotImplementedException();
    }
}

public class Goblin : ClasseBase
{

    public Goblin(int vidaMax, int vida, int forca, int defesa)
    {
        this.vida = vida;
        this.forca = forca;
        this.defesa = defesa;
        this.vidaMax = vidaMax;
    }
    public override bool EspecialB(ClasseBase inimigo)
    {
        throw new System.NotImplementedException();
    }
    public override void EspecialF(ClasseBase inimigo)
    {
        throw new System.NotImplementedException();
    }
}

public class Dragao : ClasseBase
{

    public Dragao(int vidaMax, int vida, int forca, int defesa)
    {
        this.vida = vida;
        this.forca = forca;
        this.defesa = defesa;
        this.vidaMax = vidaMax;
    }
    public override bool EspecialB(ClasseBase inimigo)
    {
        throw new System.NotImplementedException();
    }
    public override void EspecialF(ClasseBase inimigo)
    {
        throw new System.NotImplementedException();
    }
}

public class Ogro : ClasseBase
{

    public Ogro(int vidaMax, int vida, int forca, int defesa)
    {
        this.vida = vida;
        this.forca = forca;
        this.defesa = defesa;
        this.vidaMax = vidaMax;
    }
    public override bool EspecialB(ClasseBase inimigo)
    {
        throw new System.NotImplementedException();
    }
    public override void EspecialF(ClasseBase inimigo)
    {
        throw new System.NotImplementedException();
    }
}

public class Necromancer : ClasseBase
{

    public Necromancer(int vidaMax, int vida, int forca, int defesa)
    {
        this.vida = vida;
        this.forca = forca;
        this.defesa = defesa;
        this.vidaMax = vidaMax;
    }
    public override bool EspecialB(ClasseBase inimigo)
    {
        throw new System.NotImplementedException();
    }
    public override void EspecialF(ClasseBase inimigo)
    {
        throw new System.NotImplementedException();
    }
}


