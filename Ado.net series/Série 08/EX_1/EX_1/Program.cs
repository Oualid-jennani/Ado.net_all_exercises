using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX_1
{
    class Client
    {
        public int numC { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }

        // S08 => EX 02 => 03
        public override string ToString()
        {
            return this.nom + " " + this.prenom;
        }
    }

    class Produit
    {
        public int numP { get; set; }
        public string libelle { get; set; }
        public double pu { get; set; }
    }

    class Commande
    {
        public int numCom { get; set; }
        public DateTime dateCom { get; set; }
        public Client client { get; set; }
    }

    class LigneCommande
    {
        public Commande commande { get; set; }
        public Produit produit { get; set; }
        public int qt { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("EX1  ********************************************************************************** ");
            Console.WriteLine("*************************** 1 *************************** ");
            int[] words = { 1, 15, 7, 8, 19, 66, 56 };

            Console.WriteLine("---A");
            //var query = from word in words where word % 2 == 0 select word;
            var query = words.Where(x => x % 2 == 0);

            foreach (var n in query)
                Console.WriteLine(n);


            Console.WriteLine(" \n---B \n");
            //query = from word in words where word > 5 && word < 15 select word;
            query = words.Where(x => x > 5 && x < 15);

            foreach (var n in query)
                Console.WriteLine(n);



            Console.WriteLine(" \n---C \n");
            query = from word in words orderby word select word;
            query = words.OrderBy(x => x);

            foreach (var n in query)
                Console.WriteLine(n);



            Console.WriteLine(" \n---D \n");
            int query2 = words.Max();
            Console.WriteLine(query2);

            Console.WriteLine(" \n---E \n");
            int query3 = words.Min();
            Console.WriteLine(query3);


            Console.WriteLine("*************************** 2 *************************** ");
            string[] Tab2 = { "OUJDA", "RABAT", "CASA", "TANGER", "MARRAKECH", "AGADIR", "DAKHLA" };
       
            Console.WriteLine("\n---A \n");
            //var shortWords = from word in Tab2 where word.Length >= 6 select word;
            var shortWords = Tab2.Where(x => x.Length >= 6);

            foreach (var word in shortWords)
            {
                Console.WriteLine(word);
            }


            //Console.WriteLine("\n---B \n");
            //shortWords = from word in Tab2 where word.Contains("D") select word;
            //foreach (var word in shortWords)
            //{
            //    Console.WriteLine(word);
            //}

            Console.WriteLine("\n---C \n");
            int nbc = Tab2.Length;
            Console.WriteLine("nombre de chaînes : " + nbc);
        


            Console.WriteLine("EX2  **********************************************************************************\n\n ");

            List<Client> clients = new List<Client>();
            clients.Add(new Client { numC = 1, nom = "Alami", prenom = "Mohammed" });
            clients.Add(new Client { numC = 2, nom = "Berrichi", prenom = "Karim" });
            clients.Add(new Client { numC = 3, nom = "Taj", prenom = "Fatima" });

            List<Produit> produits = new List<Produit>();
            produits.Add(new Produit { numP = 1, libelle = "Stylo", pu = 16.7 });
            produits.Add(new Produit { numP = 2, libelle = "Brosse", pu = 18 });
            produits.Add(new Produit { numP = 3, libelle = "Marqueur", pu = 8 });

            List<Commande> commandes = new List<Commande>();
            commandes.Add(new Commande { numCom = 1, dateCom = new DateTime(2017, 5, 16), client = clients.ElementAt(0) });
            commandes.Add(new Commande { numCom = 2, dateCom = new DateTime(2017, 11, 12), client = clients.ElementAt(1) });
            commandes.Add(new Commande { numCom = 3, dateCom = new DateTime(2017, 5, 18), client = clients.ElementAt(2) });

            List<LigneCommande> ligneCommandes = new List<LigneCommande>();
            ligneCommandes.Add(new LigneCommande { commande = commandes.ElementAt(0), produit = produits.ElementAt(0), qt = 2 });
            ligneCommandes.Add(new LigneCommande { commande = commandes.ElementAt(2), produit = produits.ElementAt(2), qt = 1 });
            ligneCommandes.Add(new LigneCommande { commande = commandes.ElementAt(2), produit = produits.ElementAt(2), qt = 3 });

            // Mettre vos requêtes ici...
            Console.WriteLine("\n---4 \n");
            //var Cl = from c in clients  select c;
            var Cl = clients;
            foreach (var c in Cl)
            {
                Console.WriteLine(c.ToString());
            }

            Console.WriteLine("\n---5 \n");
            //var C2 = from c in clients join cm in commandes on c equals (cm.client) select c;
            //var C2 = from cm in commandes join c in clients on cm.client equals (c) select c;
            //var C2 = commandes.Join(clients, cm => cm.client, c => c, (cm, c) => c);

            var C2 = clients.Join(commandes, c => c, cm => cm.client, (c, cm) => c);

            foreach (var c in C2)
            {
                Console.WriteLine(c.ToString());
            }

            C2 = clients.Join(commandes, c => c, cm => cm.client, (c, cm) => c).Distinct();
            Console.WriteLine("\n---6 \n");
            foreach (var c in C2)
            {
                Console.WriteLine(c.ToString());
            }

            Console.WriteLine("\n---7 \n");
            //var C3 = from c in clients join cm in commandes on c equals (cm.client) where cm.numCom == 2 select c;
            //var C3 = from cm in commandes join c in clients on cm.client equals (c) where cm.numCom == 2 select c;
            var C3 = clients.Join(commandes.Where(cm => cm.numCom == 2), c => c, cm => cm.client, (c, cm) => c);

            foreach (var c in C3.Distinct())
            {
                Console.WriteLine(c.ToString());
            }

            Console.WriteLine("\n---8 \n");
            //var C4 = from c in clients join cm in commandes on c equals (cm.client) where cm.dateCom > DateTime.Parse("10/11/2017") && cm.dateCom < DateTime.Parse("2017/11/15") select c;
            var C4 = commandes.Where(cm => cm.dateCom > DateTime.Parse("10/11/2017") && cm.dateCom > DateTime.Parse("2017/11/15") ).Join(clients, cm => cm.client, c => c, (cm, c) => c);
            foreach (var c in C4.Distinct())
            {
                Console.WriteLine(c.ToString());
            }

            //--------------------------------------------------------------------------------------
            Console.WriteLine("\n---9 et 10 \n");
            var COM = from lc in ligneCommandes join c in commandes on lc.commande equals (c) select c;

            float prix = 0;
            foreach (var CM in COM.Distinct())
            {
                prix = 0;
                Console.WriteLine("Commande Num : " + CM.numCom + " | Date : " + CM.dateCom.ToShortDateString() + " | Client : " + CM.client.nom + " " + CM.client.prenom);
                var ligneC = from pr in produits join LC in ligneCommandes on pr equals (LC.produit) where LC.commande.numCom == CM.numCom select LC;
                foreach (var lc in ligneC.Distinct())
                {
                    Console.WriteLine("         ==>  Produit : " + lc.produit.libelle + " | Prix unitaire : " + lc.produit.pu + " | Quantité : " + lc.qt);

                    prix = prix + float.Parse(lc.produit.pu.ToString());
                }
                Console.WriteLine("         ==> Total PX :" + prix);
                Console.WriteLine("\n");
            }

            //--------------------------------------------------------------------------------------
            Console.WriteLine("\n---9 et 10 \n");
            var com5 = from cm in commandes join lc in ligneCommandes on cm equals lc.commande into g select new
                       {
                           cmd = cm,
                           lco = g,
                           total = g.Sum(x => x.qt * x.produit.pu)
                       };
            var testc = commandes.GroupJoin(ligneCommandes, cm => cm, lc => lc.commande, (cm, lc) => new {cmd = cm , lco = lc , total = lc.Sum(s=> s.qt * s.produit.pu) });

            foreach (var n in com5.Distinct())
            {
                Console.WriteLine("Commande Num : {0} | Date :{1} | Client : {2} ", n.cmd.numCom, n.cmd.dateCom, n.cmd.client.nom);

                foreach (var c in n.lco)
                {
                    Console.WriteLine("         ==>  Produit : {0} | Prix unitaire : {1} | Quantité : {2}  ", c.produit.libelle, c.produit.pu, c.qt);
                }
                Console.WriteLine("         ==> Total PX :"+n.total);
            }
            //--------------------------------------------------------------------------------------

            Console.WriteLine("\n\n\n");
            foreach (Commande c in commandes)
            {
                var T = (from LC in ligneCommandes where LC.commande == c select LC).Count();
                Console.WriteLine("total comande nombre : " + c.numCom + " est : " + T);
            }


            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();

            Console.ReadLine();
        }
    }
}
