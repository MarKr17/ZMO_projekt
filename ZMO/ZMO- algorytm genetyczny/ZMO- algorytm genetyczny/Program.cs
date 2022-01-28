using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ZMO__algorytm_genetyczny
{
    class Program
    {
        static List<int> m1 = new List<int>();
        static List<int> m2 = new List<int>();
        static int tau;
        static int okres_niedostępności;
        static List<List<int>> populacja = new List<List<int>>();
        private static int najlepsza_funkcja_celu;
        static List<int> najlepszy_osobnik = new List<int>();
        static List<List<int>> rodzice = new List<List<int>>();
        static List<int> Para = new List<int>();
        static List<List<int>> kolejne_pokolenie = new List<List<int>>();
        static List<int> CjOsobników = new List<int>();
        static List<int> CjPotomków = new List<int>();
        static int czas_m1;
        static int czas_m2;
        static int czas_Tau;
        private static FileStream fs;
        private static double sekundy;
        private static int Cj;

        static void Main(string[] args)
        {
            MetaheurystykaStart();
        }

        #region Wartości z pliku

        static void Pobieranie()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\User\Desktop\ZMO0.txt");
            for (int i = 0; i < lines.Length; i++)
            {

                string[] liczba = lines[i].Split(' ');
                foreach (var licz in liczba)
                {
                    if (licz != "")
                    {
                        if (i == 0)
                        {
                            m1.Add(Convert.ToInt32(licz));
                        }
                        if (i == 1)
                        {
                            m2.Add(Convert.ToInt32(licz));
                        }
                        if (i == 2)
                        {
                            tau = Convert.ToInt32(licz);
                        }
                        if (i == 3)
                        {
                            okres_niedostępności = Convert.ToInt32(licz);
                        }
                    }
                }
            }
        }

        #endregion

        #region Lista z numerami zadań i słowniki 
        static List<int> Numery_zadan()
        {
            List<int> zadania = new List<int>();
            for (int i = 0; i < m1.Count; i++)
            {
                if (!zadania.Contains(i))
                {
                    zadania.Add(i);
                }
            }
            return zadania;
        }

        static Dictionary<int, int> Zadania_długości1()
        {

            Dictionary<int, int> DługościZadań1 = new Dictionary<int, int>();
            for (int i = 0; i < m1.Count; i++)
            {
                int z = m1[i];
                DługościZadań1.Add(i, z);
            }
            return DługościZadań1;
        }
        static Dictionary<int, int> Zadania_długości2()
        {

            Dictionary<int, int> DługościZadań2 = new Dictionary<int, int>();
            for (int i = 0; i < m2.Count; i++)
            {
                int z = m2[i];
                DługościZadań2.Add(i, z);
            }
            return DługościZadań2;

        }


        #endregion

        #region Tworzenie Osobników i Populacji
        static void TworzeniePopulacji(int wielkość_populacji)
        {
            populacja.Clear();
            for (int i = 0; i < wielkość_populacji; i++)
            {
                populacja.Add(TworzenieOsobnika());
            }
        }
       
        static List<int> TworzenieOsobnika()
        {
            Random r = new Random();
            List<int> Osobnik = new List<int>();

            var zadania = Numery_zadan();


            while (zadania.Count != 0)
            {
                int WylosowaneZadanie = zadania[r.Next(zadania.Count)];
                if (!Osobnik.Contains(WylosowaneZadanie))
                {
                    Osobnik.Add(WylosowaneZadanie);
                    zadania.Remove(WylosowaneZadanie);
                }
            }
            return Osobnik;
        }

     
        #endregion

        #region Obliczenie funkcji celu

        static void FunkcjaCelu()
        {
            var Słownik_m1 = Zadania_długości1();
            var Słownik_m2 = Zadania_długości2();
            CjOsobników.Clear();

            for (int i = 0; i < populacja.Count; i++)
            {
                int Cj = 0;

                for (int j = 0; j < populacja[i].Count; j++)
                {
                    int zadanie = populacja[i][j];
                    if (j == 0)
                    {
                        if (Słownik_m1[zadanie] >= okres_niedostępności) //długość zadania na m1
                        {
                            czas_m1 = Słownik_m1[zadanie];
                            czas_m2 = Słownik_m1[zadanie] + Słownik_m2[zadanie]; //okres niedo --> kończy się w czasie m1
                            czas_Tau = Słownik_m1[zadanie] + tau;
                            Cj = czas_m2;
                        }
                        else
                        {
                            czas_m1 = Słownik_m1[zadanie];
                            czas_m2 = okres_niedostępności + Słownik_m2[zadanie];
                            czas_Tau = okres_niedostępności + tau;
                            Cj = czas_m2;
                        }
                    }
                    else
                    {
                        czas_m1 += Słownik_m1[zadanie];
                        if (czas_m2 < czas_Tau)
                        {
                            if (czas_m1 <= czas_m2)
                            {
                                if ((czas_m2 += Słownik_m2[zadanie]) < czas_Tau)
                                {
                                    czas_m2 += Słownik_m2[zadanie];
                                    Cj += czas_m2;
                                }
                                else
                                {
                                    czas_m2 += okres_niedostępności;
                                    czas_Tau = czas_m2 + tau;
                                    czas_m2 += Słownik_m2[zadanie];
                                    Cj += czas_m2;
                                }

                            }
                            else
                            {
                                if (czas_m1 - czas_m2 >= okres_niedostępności)
                                {
                                    if(czas_m1 < czas_Tau && czas_m1 + czas_m2 < czas_Tau)
                                    {
                                        czas_m2 = czas_m1 + Słownik_m2[zadanie];
                                        czas_Tau = czas_m1 + tau;
                                        Cj += czas_m2;
                                    }
                                    else
                                    {
                                        czas_Tau = czas_m2 + okres_niedostępności + tau;
                                        czas_m2 = czas_m1 + Słownik_m2[zadanie];
                                    }
                                    
                                }
                                else
                                {
                                    if (czas_m1 + Słownik_m2[zadanie] < czas_Tau)
                                    {
                                        czas_m2 = czas_m1 + Słownik_m2[zadanie];
                                        Cj += czas_m2;
                                    }
                                    else
                                    {
                                        czas_m2 = okres_niedostępności + Słownik_m2[zadanie];
                                        Cj += czas_m2;
                                    }

                                }
                            }
                        }
                        else
                        {
                            czas_m2 += okres_niedostępności;
                            czas_Tau = czas_m2 + tau;
                            czas_m2 += Słownik_m2[zadanie];
                            Cj += czas_m2;
                        }
                    }
                }
                CjOsobników.Add(Cj);
            }

        }
        #endregion

        #region Wybór Rodziców
        static void WybórRodziców(int wielkość_populacji)
        {
            rodzice.Clear();
            Para.Clear();
            Random r = new Random();

            int suma = 0;
            int funkcja_celu = 0;
            for (int i = 0; i < wielkość_populacji; i++)
            {
                funkcja_celu = CjOsobników[i];
                suma = suma + funkcja_celu;
            }

            List<int> osobniki_dostosowanie = new List<int>();
            int dostosowanie = 0;
            int ruletka = 0;
            for (int i = 0; i < populacja.Count; i++)
            {
                dostosowanie = suma - funkcja_celu;
                osobniki_dostosowanie.Add(ruletka + dostosowanie);
                ruletka = ruletka + dostosowanie;
            }

            for (int i = 0; i < osobniki_dostosowanie.Count; i++)
            {
                int rodzic1 = 0;
                int rodzic2 = 0;
                Para = new List<int>();

                while (rodzic1 == rodzic2)
                {
                    int los1 = r.Next(ruletka);
                    int los2 = r.Next(ruletka);
                    for (int j = 0; j < osobniki_dostosowanie.Count; j++)
                    {
                        if (los1 > osobniki_dostosowanie[j])
                        {
                            rodzic1 = j + 1;
                            break;
                        }
                    }
                    if (los1 < osobniki_dostosowanie[0])
                    {
                        rodzic1 = 0;
                    }
                    for (int j = 0; j < osobniki_dostosowanie.Count; j++)
                    {
                        if (los2 <= osobniki_dostosowanie[j])
                        {
                            rodzic2 = j;
                            break;
                        }
                    }
                    if (los2 < osobniki_dostosowanie[0])
                    {
                        rodzic2 = 0;
                    }
                }
                Para.Add(rodzic1);
                Para.Add(rodzic2);
                rodzice.Add(Para);
            }
        }
        #endregion

        #region Krzyżowanie
        static void Krzyżowanie(int wielkość_populacji, int krzyżowanie)
        {
            Random r = new Random();
            kolejne_pokolenie.Clear();
            List<int> potomek = new List<int>();


            int prawdopodobieństwo_krzyżowania = krzyżowanie * rodzice.Count / 100;
            int osobniki_większy_zakres = prawdopodobieństwo_krzyżowania * 10 / 100;

            List<int> indeksy_rodziców = new List<int>();
            while (indeksy_rodziców.Count() < osobniki_większy_zakres)
            {
                int indeks = r.Next(osobniki_większy_zakres);
                if (!indeksy_rodziców.Contains(indeks))
                {
                    indeksy_rodziców.Add(indeks);
                }
            }

            for (int i = 0; i < rodzice.Count; i++)
            {
                var Zadania = Numery_zadan();

                int rodzic1 = rodzice[i][0];
                int rodzic2 = rodzice[i][1];
                int miejsce_krzyżowania_40_60 = r.Next((40 * (populacja[rodzic1].Count()) / 100), (60 * (populacja[rodzic1].Count()) / 100));
                int miejsce_krzyżowania_0_100 = r.Next(1, populacja[rodzic1].Count() - 1);

                if (i < prawdopodobieństwo_krzyżowania)
                {
                    if (indeksy_rodziców.Contains(i))
                    {
                        for (int p = 0; p < miejsce_krzyżowania_0_100; p++)
                        {
                            potomek.Add(populacja[rodzic1][p]);
                            Zadania.Remove(populacja[rodzic1][p]);
                        }
                        for (int s = 0; s < populacja[rodzic2].Count; s++)
                        {
                            int z = populacja[rodzic2][s];
                            if (Zadania.Contains(z))
                            {
                                potomek.Add(z);
                                Zadania.Remove(z);
                            }
                            else
                            {
                                continue;
                            }

                        }
                        while (Zadania.Count != 0)
                        {
                            int WylosowaneZadanie100 = Zadania[r.Next(Zadania.Count)];
                            potomek.Add(WylosowaneZadanie100);
                            Zadania.Remove(WylosowaneZadanie100);
                        }
                    }
                    else
                    {
                        for (int m = 0; m < miejsce_krzyżowania_40_60; m++)
                        {

                            potomek.Add(populacja[rodzic1][m]);
                            Zadania.Remove(populacja[rodzic1][m]);
                        }

                        for (int t = 0; t < populacja[rodzic2].Count; t++)
                        {
                            int x = populacja[rodzic2][t];
                            if (Zadania.Contains(x))
                            {
                                potomek.Add(x);
                                Zadania.Remove(x);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        while (Zadania.Count != 0)
                        {
                            int WylosowaneZadanie50 = Zadania[r.Next(Zadania.Count)];
                            potomek.Add(WylosowaneZadanie50);
                            Zadania.Remove(WylosowaneZadanie50);
                        }
                    }
                }
                else
                {
                    int losowanie = r.Next(1);
                    if (losowanie == 0)
                    {
                        potomek = new List<int>(populacja[rodzic1]);
                    }
                    else
                    {
                        potomek = new List<int>(populacja[rodzic2]);
                    }
                }
                kolejne_pokolenie.Add(new List<int>(potomek));
                potomek.Clear();
            }
            for (int i = 0; i < wielkość_populacji; i++)
            {
                populacja[i] = new List<int>(kolejne_pokolenie[i]);
            }
        }
        #endregion

        #region Wprowadzenie mutacji
        static void WprowadzenieMutacji(int mutacja)
        {
            Random r = new Random();
            int prawdopodobieństwo_mutacji = mutacja * kolejne_pokolenie.Count / 100;

            for (int i = 0; i < prawdopodobieństwo_mutacji; i++)
            {
                int wylosowany_osobnik_do_mutacji = r.Next(kolejne_pokolenie.Count);

                int wylosowane_miejsce_mutacji1 = 0;
                int wylosowane_miejsce_mutacji2 = 0;

                while (wylosowane_miejsce_mutacji1 == wylosowane_miejsce_mutacji2)
                {
                    wylosowane_miejsce_mutacji1 = r.Next(kolejne_pokolenie[wylosowany_osobnik_do_mutacji].Count);
                    wylosowane_miejsce_mutacji2 = r.Next(kolejne_pokolenie[wylosowany_osobnik_do_mutacji].Count);
                }
                int x = kolejne_pokolenie[wylosowany_osobnik_do_mutacji][wylosowane_miejsce_mutacji2];
                int y = kolejne_pokolenie[wylosowany_osobnik_do_mutacji][wylosowane_miejsce_mutacji1];
                kolejne_pokolenie[wylosowany_osobnik_do_mutacji][wylosowane_miejsce_mutacji1] = x;
                kolejne_pokolenie[wylosowany_osobnik_do_mutacji][wylosowane_miejsce_mutacji2] = y;
            }
        }
        #endregion

        #region Funkcja celu - kolejne pokolenie

        static void FunkcjaCeluPokolenie()
        {
            var Słownik_m1 = Zadania_długości1();
            var Słownik_m2 = Zadania_długości2();
            CjPotomków.Clear();


            for (int i = 0; i < kolejne_pokolenie.Count; i++)
            {
                int Cj = 0;
                for (int j = 0; j < kolejne_pokolenie[i].Count; j++)
                {
                    int zadanie = kolejne_pokolenie[i][j];
                    if (j == 0)
                    {
                        if (Słownik_m1[zadanie] >= okres_niedostępności) //długość zadania na m1
                        {
                            czas_m1 = Słownik_m1[zadanie];
                            czas_m2 = Słownik_m1[zadanie] + Słownik_m2[zadanie]; //okres niedo --> kończy się w czasie m1
                            czas_Tau = Słownik_m1[zadanie] + tau;
                            Cj = czas_m2;
                        }
                        else
                        {
                            czas_m1 = Słownik_m1[zadanie];
                            czas_m2 = okres_niedostępności + Słownik_m2[zadanie];
                            czas_Tau = okres_niedostępności + tau;
                            Cj = czas_m2;
                        }
                    }
                    else
                    {
                        czas_m1 += Słownik_m1[zadanie];
                        if (czas_m2 < czas_Tau)
                        {
                            if (czas_m1 <= czas_m2)
                            {
                                if ((czas_m2 += Słownik_m2[zadanie]) < czas_Tau)
                                {
                                    czas_m2 += Słownik_m2[zadanie];
                                    Cj += czas_m2;
                                }
                                else
                                {
                                    czas_m2 += okres_niedostępności;
                                    czas_Tau = czas_m2 + tau;
                                    czas_m2 += Słownik_m2[zadanie];
                                    Cj += czas_m2;
                                }

                            }
                            else
                            {
                                if (czas_m1 - czas_m2 >= okres_niedostępności)
                                {
                                    czas_m2 = czas_m1 + Słownik_m2[zadanie];
                                    czas_Tau = czas_m1 + tau;
                                    Cj += czas_m2;
                                }
                                else
                                {
                                    if (czas_m1 + Słownik_m2[zadanie] < czas_Tau)
                                    {
                                        czas_m2 = czas_m1 + Słownik_m2[zadanie];
                                        Cj += czas_m2;
                                    }
                                    else
                                    {
                                        czas_m2 = okres_niedostępności + Słownik_m2[zadanie];
                                        Cj += czas_m2;
                                    }
                                }
                            }
                        }
                        else
                        {
                            czas_m2 += okres_niedostępności;
                            czas_Tau = czas_m2 + tau;
                            czas_m2 += Słownik_m2[zadanie];
                            Cj += czas_m2;
                        }
                    }
                }
                CjPotomków.Add(Cj);
            }
        }


        #endregion

        #region Pobranie do pliku

        static void DoPliku()
        {
            string ścieżka3 = @"C:\Users\User\Desktop\najlepszyCj.txt";
            fs = new FileStream(ścieżka3, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);

            sw.Write("Najlepsza kolejność zadań: ");
            for (int i = 0; i < najlepszy_osobnik.Count; i++)
            {
                sw.Write(najlepszy_osobnik[i]);
            }
            sw.WriteLine();
            sw.WriteLine("Wartość funkcji celu: " + najlepsza_funkcja_celu);
            sw.WriteLine("Czas działania programu: " + Convert.ToString(sekundy));
            sw.Close();
            fs.Close();
        }
        #endregion

        #region Metaheurystyka

        static void MetaheurystykaStart()
        {
            populacja.Clear();
            najlepszy_osobnik.Clear();

            int Counter = 0;
            DateTime start_czas = DateTime.Now;
            DateTime stop_czas;

            Console.Write("Wpisz wielkość populacji: ");
            string wielkość_pop = Console.ReadLine();
            int wielkość_populacji = Convert.ToInt32(wielkość_pop);
            Console.Write("Wpisz prawdopodobieństwo krzyżowania [%]: ");
            string p_krzyżowanie = Console.ReadLine();
            int krzyżowanie = Convert.ToInt32(p_krzyżowanie);
            Console.Write("Wpisz prawdopodobieństwo mutacji [%]: ");
            string p_mutacja = Console.ReadLine();
            int mutacja = Convert.ToInt32(p_mutacja);
            Console.Write("Wpisz liczbę iteracji bez poprawy, po której metaheurystyka ma zakończyć działanie: ");
            string p_iteracje = Console.ReadLine();
            int iteracje_bez_poprawy = Convert.ToInt32(p_iteracje);

            bool czy_petla_pierwsza = true;

            while (Counter < iteracje_bez_poprawy)
            {
                
                Pobieranie();
                TworzeniePopulacji(wielkość_populacji);
                FunkcjaCelu();
                WybórRodziców(wielkość_populacji);
                Krzyżowanie(wielkość_populacji, krzyżowanie);
                WprowadzenieMutacji(mutacja);
                FunkcjaCeluPokolenie();

                if (czy_petla_pierwsza)
                {
                    int najkrótszyCj = CjPotomków[0];
                    int indeks_najlepszego_osobnika = 0;

                    for (int i = 0; i < kolejne_pokolenie.Count; i++)
                    {
                        if (CjPotomków[i] < najkrótszyCj)
                        {
                            najkrótszyCj = CjPotomków[i];
                            indeks_najlepszego_osobnika = i;
                        }
                    }
                    najlepsza_funkcja_celu = najkrótszyCj;
                    najlepszy_osobnik = new List<int>(kolejne_pokolenie[indeks_najlepszego_osobnika]);
                    czy_petla_pierwsza = false;
                }
                else
                {
                    int najkrótszyCj = CjPotomków[0];
                    int indeks_najlepszego_osobnika = 0;

                    for (int i = 0; i < kolejne_pokolenie.Count; i++)
                    {
                        if (CjPotomków[i] < najkrótszyCj)
                        {
                            najkrótszyCj = CjPotomków[i];
                            indeks_najlepszego_osobnika = i;
                        }
                    }
                    if (najkrótszyCj < najlepsza_funkcja_celu)
                    {
                        najlepsza_funkcja_celu = najkrótszyCj;
                        najlepszy_osobnik = new List<int>(kolejne_pokolenie[indeks_najlepszego_osobnika]);
                    }
                    if (najkrótszyCj >= najlepsza_funkcja_celu)
                    {
                        Counter++;
                    }
                }
            }
            DoPliku();
            stop_czas = DateTime.Now;
            sekundy = (stop_czas - start_czas).TotalSeconds;
        }
        #endregion


    }
}
