using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;

namespace Circo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> lions = new List<string>() { "Mufasa", "Scar", "Simba" };
            List<string> wolves = new List<string>() { "Zanna", "Giallo" };
            List<string> listaAnimali = new List<string>();

            #region EntraAddestratore

            Persona Igor = new Addestratore();
            Igor.nome = "Igor";
            ((Addestratore)Igor).salutaPubblico();
            #endregion

            #region InizioShow

            //Fa entrare i leoni
            foreach (var leone in lions)
            {
                Animale leo = new Leone();
                leo.nome = leone;
                leo.ascolta((Addestratore)Igor);
                leo.salutaEntrando();
                listaAnimali.Add(leo.nome);
            }
            //Fa entrare i lupi
            foreach (var lupo in wolves)
            {
                Animale wolf = new Leone();
                wolf.nome = lupo;
                wolf.ascolta((Addestratore)Igor);
                wolf.salutaEntrando();
                listaAnimali.Add(wolf.nome);
            }

            /*
            foreach (var animale in listaAnimali)
            {
                Console.WriteLine(animale + " entra in pista!");
            }*/
            
            Task<Byte[]> novita = Igor.leggiGiornale("http://www.repubblica.it");
           
            ((Addestratore)Igor).RiceviElenco(listaAnimali);
            ((Addestratore)Igor).FaiAppello();

            #endregion
        }
    }
}



public abstract class Persona
{
    public string razza = "Umana";
    public string nome { get; set; }

    public async Task<Byte[]> leggiGiornale(string url)
    {
        Console.WriteLine("Apro il Giornale");
        WebClient client = new WebClient();
        var html = await client.DownloadDataTaskAsync(url);
        Console.WriteLine("Leggo il Giornale");
        return html;
    }

}

public class Addestratore : Persona
{
    private List<string> ListaAnimali;
    public event EventHandler haChiamato;

    public void salutaPubblico()
    {
        System.Console.WriteLine("L'addestratore " + nome + " saluta il pubblico");  
    }

    public void RiceviElenco(List<string> lista)
    {
        ListaAnimali = lista;
    }   

    public void FaiAppello()
    {
        foreach (string nome in ListaAnimali)
        {
            System.Console.WriteLine("L'addestratore " + this.nome + " chiama: " + nome);
            if (haChiamato != null)
            {
                haChiamato(this, EventArgs.Empty);
            }
        }
    }

   
}

public abstract class Animale
{
    public string nome { get; set; }
    public string verso = null;
    public string esercizio = null;
    public Addestratore MyMaster;

    public void ascolta(Addestratore MyNewMaster)
    {
        MyMaster = MyNewMaster;
        MyMaster.haChiamato += MyMaster_haChiamato; 
    }

    private void MyMaster_haChiamato(object sender, EventArgs e)
    {
        this.faiVerso();
       
    }

    public void faiVerso()
    {
        Console.WriteLine(">" + nome + " fai il suo verso:" + verso);
    }

    public void salutaEntrando()
    {
        Console.WriteLine(">" + nome + " entra e saluta "+MyMaster.nome + " ed il pubblico");
    }

    public void faiEsercizio()
    {
        Console.WriteLine(">" + nome + " ubbidisce a "+MyMaster.nome+" e fa il suo esercizio:" + esercizio);
    }

   
}

public class Leone : Animale
{
    public bool criniera = true;
    public Leone()
    {
        verso = "Roar";
   
    }
}

public class Lupo : Animale
{
    public bool criniera = false;
    public Lupo()
    {
        verso = "Grrrrr";

    }
}



