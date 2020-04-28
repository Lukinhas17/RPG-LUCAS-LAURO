using UnityEngine;
using System.Collections;

public abstract class ClasseBase
{
    public int vida;
    public int forca;
    public int defesa;
    public GameObject prefab;
    public abstract bool EspecialB(ClasseBase inimigo);
    public virtual bool Roll(ClasseBase inimigo) 
    {
        int acurice = Random.Range(0, 10);
        if (acurice >= 0 && acurice <= 7)
        {
            Debug.Log("O PROXIMO ATAQUE DO INIMIGO TERÁ UMA GRANDE CHANDE DE DAR MISS");
        }
        else if (acurice >= 8 && acurice == 10) 
        {
            inimigo.forca += inimigo.forca;
            Debug.Log("VALOR DA FORÇA DO INIMIGO COM O MISS ALTO" + inimigo.forca);
        }
        return true;
    }
    abstract public void EspecialF(ClasseBase inimigo);
}

public class Ninja : ClasseBase
{
    public Ninja(int vida, int forca, int defesa)
    {
        this.vida = vida;
        this.forca = forca;
        this.defesa = defesa;
    }
    public int Especial()
    {
        vida += 55;
        int forcaE = forca + 40;
        Debug.Log("VIDA RECUPERADA " + vida);
        return forcaE;
    }
    public override void EspecialF(ClasseBase inimigo)
    {
        int dano;
        dano = Especial();
        inimigo.vida -= dano;
    }
    public override bool EspecialB(ClasseBase inimigo)
    {
        return Roll(inimigo);
    }

    public override bool Roll(ClasseBase inimigo)
    {
        return base.Roll(inimigo);
    }

}

public class Berserker : ClasseBase
{
    public Berserker(int vida, int forca, int defesa)
    {
        this.vida = vida;
        this.forca = forca;
        this.defesa = defesa;
    }

    public override bool EspecialB(ClasseBase inimigo)
    {
        vida += 50;
        forca += 10;
        defesa += 20;
        Debug.Log("ESPECIAL DO BERSERKER");
        return true;
    }
    public override void EspecialF(ClasseBase inimigo)
    {
        throw new System.NotImplementedException();
    }
}

public class Mago : ClasseBase
{
    public Mago(int vida, int forca, int defesa)
    {
        this.vida = vida;
        this.forca = forca;
        this.defesa = defesa;
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


public class Golem : ClasseBase
{

    public Golem(int vida, int forca, int defesa)
    {
        this.vida = vida;
        this.forca = forca;
        this.defesa = defesa;
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

    public Goblin(int vida, int forca, int defesa)
    {
        this.vida = vida;
        this.forca = forca;
        this.defesa = defesa;
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

    public Dragao(int vida, int forca, int defesa)
    {
        this.vida = vida;
        this.forca = forca;
        this.defesa = defesa;
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

    public Ogro(int vida, int forca, int defesa)
    {
        this.vida = vida;
        this.forca = forca;
        this.defesa = defesa;
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

    public Necromancer(int vida, int forca, int defesa)
    {
        this.vida = vida;
        this.forca = forca;
        this.defesa = defesa;
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


