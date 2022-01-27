#include <iostream>
#include <string>
#include <filesystem>
#include <fstream>
#include <list>
#include <sstream>
#include "Header.h"
using namespace std;

bool cmp(pair<int, int>& a, pair<int, int>& b)
{
        return a.second < b.second;
}
void sort(map<int, int>& M)
{
    // Declare vector of pairs
    vector<pair<int, int> > A;

    // Copy key-value pair from Map
    // to vector of pairs
    for (auto& it : M) {
        A.push_back(it);
    }

    // Sort using comparator function
    sort(A.begin(), A.end(), cmp);

    // Print the sorted value
    for (auto& it : A) {

        cout << it.first << ' '
            << it.second << endl;
    }
}


vector<int> Konwersja_String(string text)
{
    vector<int> wektor;
    
    stringstream ss;
    ss << text;

    string temp;
    int found;

    while (!ss.eof()) {

        /* extracting word by word from stream */
        ss >> temp;

        /* Checking the given word is integer or not */
        if (stringstream(temp) >> found)
            wektor.push_back(found);

        /* To save from space at the end of string */
        temp = "";
    }
    vector<int>::iterator it;
    /*for (it = wektor.begin(); it != wektor.end(); ++it)
        cout << '\t' << *it;
    cout << '\n';
    */

    return wektor;
}

Instancja::Instancja(string filename)
{
    filesystem::path p = filesystem::current_path();
    string path = { p.parent_path().parent_path().u8string() };
    string text;
    ifstream File(path + "\\" + filename);

    getline(File, text);
    dl_m1 = Konwersja_String(text);

    getline(File, text);
    dl_m2= Konwersja_String(text);

    getline(File, text);
    Tau = stoi(text);
    

    getline(File, text);
    cn = stoi(text);
    
    File.close();
    
    //tworzenie s³owników
    for (int i = 0; i < dl_m1.size();i++)
    {
        maszyna1_dlugosci.insert({ i+1, dl_m1[i] });
    }
    for (int i = 0; i < dl_m1.size();i++)
    {
        maszyna2_dlugosci.insert({ i+1, dl_m2[i] });
    }

    sort(maszyna1_dlugosci);
    sort(maszyna2_dlugosci);
}
void Instancja::pierwsze()
{
    int zadanie_m1 = maszyna1_dlugosci.begin()->first;//zadanie 1 to zadanie najkrotsze
    kolej1.push_back(zadanie_m1);//doajemy je do kolejki zadań na maszynie 1
    t1 = maszyna1_dlugosci.begin()->second;//czas na maszynie pierwszej to czas zakończenia 1 zadania
    maszyna1_dlugosci.erase(maszyna1_dlugosci.begin()); //usuwamy ,,zużyte'' zadanie
    int zadanie_m2=zadanie_m1; //zadanie na maszynie 2 ma taki sam numer jak zadanie 1 
    int zadanie_m2_czas=dl_m2[zadanie_m1-1]; //czas zadania na maszynie 2 ponierany z vectora długości

    map<int, int>::iterator it;
    it = maszyna2_dlugosci.find(zadanie_m2);
    
    if (t1 >= cn) //jeżeli czas zakończenia zad1 na maszynie 1 jest większy niż czas niedostęności
    {
        kolej2.push_back(0); //na maszynie 2 pierwszy jest okres niedostępności
        okres_nied = t1 + Tau; //czas nastepnego okresu niedostęności to czas na maszynie 1 +Tau
        t2 = t1; //czas na maszynie2 jest taki jak na 1, ponieważ chcemy aby okres niedostępności skończył się wtedy kiedy zadanie na maszynie1
        kolej2.push_back(zadanie_m2);
        t2 += zadanie_m2_czas;

        maszyna2_dlugosci.erase(it); //usuwamy ,,zużyte zadanie''


    }
    else
    {
        kolej2.push_back(zadanie_m2);
        t2 = t1 + zadanie_m2_czas;
        maszyna2_dlugosci.erase(it); //usuwamy ,,zużyte zadanie''
    }
  
}
void Instancja::nastepne_zadanie()
{
    pair<int, int> zadanie_m1 = { maszyna1_dlugosci.begin()->first, maszyna1_dlugosci.begin()->second };
    pair<int, int> zadanie_m2 = { maszyna2_dlugosci.begin()->first, maszyna2_dlugosci.begin()->second };


}
