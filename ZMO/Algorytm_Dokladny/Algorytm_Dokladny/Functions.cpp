#include <iostream>
#include <string>
#include <filesystem>
#include <fstream>
#include <list>
#include <sstream>
#include "Nag³ówek.h"
using namespace std;

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

}
