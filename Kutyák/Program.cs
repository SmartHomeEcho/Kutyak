using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Kutyák
{
    class Program
    {
        public static List<KutyaNevek> KutyaNevLista = new List<KutyaNevek>();
        public static List<KutyaFajtak> KutyaFajtaLista = new List<KutyaFajtak>();
        public static List<Kutyak> KutyaLista = new List<Kutyak>();
        static void Main(string[] args)
        {
            KutyaNevBeolvas();
            KutyaFajtaBeolvas();
            KutyaBeolvas();
            KutyakAtlagEletkora();
            LegidosebbKutyaNeve();
            Datum20180110();
            LegjobbanLeterheltNap();
            Statisztika();
            Console.ReadLine();
        }
        static void KutyaNevBeolvas()
        {
            StreamReader Olvas = new StreamReader("KutyaNevek.csv", Encoding.Default);
            string fejlec = Olvas.ReadLine();
            while (!Olvas.EndOfStream)
            {
                KutyaNevLista.Add(new KutyaNevek(Olvas.ReadLine()));
            }
            Console.WriteLine($"3. feladat: Kutyanevek száma: {KutyaNevLista.Count}");
        }
        static void KutyaFajtaBeolvas()
        {
            StreamReader Olvas = new StreamReader("KutyaFajtak.csv", Encoding.Default);
            string fejlec = Olvas.ReadLine();
            while (!Olvas.EndOfStream)
            {
                KutyaFajtaLista.Add(new KutyaFajtak(Olvas.ReadLine()));
            }
        }
        static void KutyaBeolvas()
        {
            StreamReader Olvas = new StreamReader("Kutyak.csv", Encoding.Default);
            string fejlec = Olvas.ReadLine();
            while (!Olvas.EndOfStream)
            {
                KutyaLista.Add(new Kutyak(Olvas.ReadLine()));
            }
        }
        static void KutyakAtlagEletkora() => Console.WriteLine($"6. feladat: A kutyák átlag életkora: {Math.Round(Convert.ToDouble(KutyaLista.Average(x => x.Eletkor)), 2)}");
        static void LegidosebbKutyaNeve()
        {
            int Legidosebb = 0;
            int Keresettj = 0;
            int Keresettz = 0;
            for (int i = 0; i < KutyaLista.Count; i++)
            {
                for (int j = 0; j < KutyaFajtaLista.Count; j++)
                {
                    for (int z = 0; z < KutyaNevLista.Count; z++)
                    {
                        if (KutyaLista[i].Eletkor > KutyaLista[Legidosebb].Eletkor)
                        {
                            if (KutyaLista[i].FajtaId == KutyaFajtaLista[j].FajtaId && KutyaLista[i].KutyaNevId == KutyaNevLista[z].KutyaNevId)
                            {
                                Legidosebb = i;
                                Keresettj = j;
                                Keresettz = z;
                            }
                        }
                    }
                }
            }
            Console.WriteLine($"7. feladat: a legidősebb kutya neve és fajtálya:{KutyaNevLista[Keresettz].KutyaNev}, {KutyaFajtaLista[Keresettj].MagyarNeve}");
        }
        static void Datum20180110()
        {
            int[] Vizsgalt= KutyaLista.Where(x => x.OrvosiEllenorzes == "2018.01.10").Select(y => y.FajtaId).Distinct().ToArray();
            int[] Darab = new int[Vizsgalt.Length];
            for (int i = 0; i < Vizsgalt.Length; i++)
            {
                for (int j = 0; j < KutyaLista.Count; j++)
                {
                    if (KutyaLista[j].FajtaId==Vizsgalt[i]&& KutyaLista[j].OrvosiEllenorzes=="2018.01.10")
                    {
                        Darab[i]++;
                    }
                }
            }
            Console.WriteLine("8. feladat: Január 10.-én vizsgált kutya fajták:");
            for (int i = 0; i < Vizsgalt.Length; i++)
            {
                Console.WriteLine($"\t{KutyaFajtaLista[KutyaFajtaLista.FindIndex(x=>x.FajtaId==Vizsgalt[i])].MagyarNeve},{Darab[i]} kutya");
            }
        }
        static void LegjobbanLeterheltNap()
        {
            List<LegjobbanLeterheltNap> Leterhelt = new List<LegjobbanLeterheltNap>();
            for (int i = 0; i < KutyaLista.Count; i++)
            {
                int NapHelye = 0;
                bool VaneNap = false;
                for (int j = 0; j < Leterhelt.Count; j++)
                {
                    if (Leterhelt[j].Datum == KutyaLista[i].OrvosiEllenorzes)
                    {
                        VaneNap = true;
                        NapHelye = j;
                    }
                }
                if (VaneNap == true)
                {
                    Leterhelt[NapHelye].Kutyaszam++;
                }
                else
                {
                    Leterhelt.Add(new LegjobbanLeterheltNap(KutyaLista[i].OrvosiEllenorzes, 1));
                }
            }
            List<LegjobbanLeterheltNap> TerheltsegLista = Leterhelt.OrderByDescending(item => item.Kutyaszam).ToList();
            Console.WriteLine($"9. feladat: Legjobban leterhelt nap: {TerheltsegLista[0].Datum}.: {TerheltsegLista[0].Kutyaszam} kutya");
        }
        static void Statisztika()
        { 
            Console.WriteLine("10. feladat: névszatisztika.txt");
            List<StatisztikaElem> Stat = new List<StatisztikaElem>();
            int[] NevekSeged = new int[KutyaNevLista.Count];
            for (int i = 0; i < KutyaNevLista.Count; i++)
            {
                for (int j = 0; j < KutyaLista.Count; j++)
                {
                    if (KutyaNevLista[i].KutyaNevId == KutyaLista[j].KutyaNevId)
                    {
                        NevekSeged[i]++;
                    }
                }
            }
            for (int i = 0; i < KutyaNevLista.Count; i++)
            {
                Stat.Add(new StatisztikaElem(KutyaNevLista[i].KutyaNev, NevekSeged[i]));
            }
            List<StatisztikaElem> RendezettStatisztika = Stat.OrderByDescending(item => item.Db).ToList();
            StreamWriter Iro = new StreamWriter("Nevstatisztika.txt", false, Encoding.UTF8);
            for (int i = 0; i < KutyaNevLista.Count; i++)
            {
                Iro.WriteLine($"{RendezettStatisztika[i].Nev};{RendezettStatisztika[i].Db}");
            }
            Iro.Close();
        }
    }
    
    class KutyaNevek
    {
        public KutyaNevek(string sor)
        {
            string[] sorelemek = sor.Split(';');
            this.KutyaNevId = Convert.ToInt32(sorelemek[0]);
            this.KutyaNev = sorelemek[1];
        }
        public int KutyaNevId { get; set; }
        public string KutyaNev { get; set; }
    }
    class KutyaFajtak
    {
        public KutyaFajtak(string sor)
        {
            string[] sorelemek = sor.Split(';');
            this.FajtaId = Convert.ToInt32(sorelemek[0]);
            this.MagyarNeve =sorelemek[1];
            this.EredetiNeve = sorelemek[2];
        }
        public int FajtaId { get; set; }
        public string MagyarNeve { get; set; }
        public string EredetiNeve { get; set; }
    }
    class Kutyak
    {
        public Kutyak(string sor)
        {
            string[] sorelemek = sor.Split(';');
            this.VizsgalatAzonosito = Convert.ToInt32(sorelemek[0]);
            this.FajtaId = Convert.ToInt32(sorelemek[1]);
            this.KutyaNevId = Convert.ToInt32(sorelemek[2]);
            this.Eletkor = Convert.ToInt32(sorelemek[3]);
            this.OrvosiEllenorzes = sorelemek[4];
        }
        public int VizsgalatAzonosito { get; set; }
        public int FajtaId { get; set; }
        public int KutyaNevId { get; set; }
        public int Eletkor { get; set; }
        public string OrvosiEllenorzes { get; set; }
    }
    public class LegjobbanLeterheltNap
    {
        public string Datum;
        public int Kutyaszam;
        public LegjobbanLeterheltNap(string DatumAdd, int KutyaszamAdd)
        {
            Datum = DatumAdd;
            Kutyaszam = KutyaszamAdd;
        }
    }
    public class StatisztikaElem
    {
        public string Nev;
        public int Db;
        public StatisztikaElem(string NevAdd, int DbAdd)
        {
            Nev = NevAdd;
            Db = DbAdd;
        }
    }



}
