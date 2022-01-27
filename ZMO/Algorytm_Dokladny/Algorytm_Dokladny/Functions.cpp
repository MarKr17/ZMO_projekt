#include <iostream>
#include <string>
#include <filesystem>
#include <fstream>
#include <list>
#include <sstream>
#include "Nag³ówek.h"
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
    for (it = wektor.begin(); it != wektor.end(); ++it)
        cout << '\t' << *it;
    cout << '\n';

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
    cout << Tau << endl;

    getline(File, text);
    cn = stoi(text);
    cout << cn << endl;
    File.close();
    
    //tworzenie s³owników
    for (int i = 0; i < dl_m1.size();i++)
    {
        maszyna1_dlugosci.insert({ i, dl_m1[i] });
    }
    for (int i = 0; i < dl_m1.size();i++)
    {
        maszyna2_dlugosci.insert({ i, dl_m2[i] });
    }

    sort(maszyna1_dlugosci);
    sort(maszyna2_dlugosci);
}
void pierwsze(int Tau, int okres_nied, int cn, int t1, int t2, vector<int> kolej1, vector<int> kolej2, sort(maszyna1_dlugosci), sort(maszyna2_dlugosci))
{
    int zadanie_m1 = sort(maszyna1_dlugosci).begin()->first;
    kolej1.push_back(zadanie_m1);
    t1 = sort(maszyna1_dlugosci).begin()->second;
    int zadanie_m2;
    int zadanie_m2_czas;
    if (t1 > cn) 
    {
        kolej2.push_back(cn);
        okres_nied = t1 + Tau;
        t2 = t1;

        for (auto &e1 : sort(maszyna2_dlugosci))
        {
            if (e1 == zadanie_m1)
            {
                zadanie_m2 = e1->first;
                zadanie_m2_czas = e1->second;
                kolej2.push_back(zadanie_m2);
                t2 = t2 + e1->second;
            }
            else
            {
                continue;
            }
        }
    }
    else
    {
        if (zadanie_m1 + zadanie_m2 <= Tau)
        {
            kolej2.push_back(zadanie_m2);
            t2 = t1 + zadanie_m2_czas;
        }
        else
        {
            kolej2.push_back(cn);
            kolej2.push_back(zadanie_m2);
            t2 = cn + zadanie_m2_czas;
        }
    }
  

}
