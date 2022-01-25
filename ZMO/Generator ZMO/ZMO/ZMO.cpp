#include <iostream>
#include <iostream>
#include <stdlib.h>
#include <fstream>
#include <list>
#include <string>
#include <algorithm>

using namespace std;


int main()
{
	int liczba_instancji, niedostepnosc, tau, ilosc_zadan, dlugosc_zadan;
	list<int> maszyna1;
	list<int> maszyna2;
	srand(time(0));

	cout << "Liczba instancji: ";
	cin >> liczba_instancji;

	cout << "Dlugosc tau: ";
	cin >> tau;

	cout << "Dlugosc okresu niedostepnosci: ";
	cin >> niedostepnosc;

	cout << "Ilosc zadan ";
	cin >> ilosc_zadan;

	fstream plik;
	string ścieżka1 = "C:\\Users\\User\\Desktop\\ZMO";
	string ścieżka2 = ".txt";

	for (int instancja = 0; instancja < liczba_instancji; instancja++)
	{
		string ścieżka = ścieżka1 + to_string(instancja) + ścieżka2;
		plik.open(ścieżka.c_str(), ios::out | ios::app);

		maszyna1.clear();
		maszyna2.clear();

		auto it = maszyna1.begin();
		for (int i = 0; i < ilosc_zadan; i++)
		{
			dlugosc_zadan = rand() % 10 + 1;
			maszyna1.insert(it, dlugosc_zadan);
		}

		auto jt = maszyna2.begin();
		for (int i = 0; i < ilosc_zadan; i++)
		{
			dlugosc_zadan = rand() % 10 + 1;
			maszyna2.insert(jt, dlugosc_zadan);
		}

		for (list<int>::iterator i = maszyna1.begin();
			i != maszyna1.end();
			i++)
			plik << to_string(*i) + " ";

		plik << '\n';
		for (list<int>::iterator i = maszyna2.begin();
			i != maszyna2.end();
			i++)
			plik << to_string(*i) + " ";
		plik << '\n';
		plik << tau << endl;
		plik << niedostepnosc << endl;

		plik.close();
	}
	return 0;
}