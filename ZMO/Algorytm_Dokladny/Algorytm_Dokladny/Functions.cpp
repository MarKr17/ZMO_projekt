#include <iostream>
#include <string>
#include <filesystem>
#include <fstream>
#include <list>
#include <sstream>
#include "Nag³ówek.h"
using namespace std;

list<int> Konwersja_String(string text)
{
    list<int> lista;
    
    stringstream ss;
    ss << text;

    string temp;
    int found;

    while (!ss.eof()) {

        /* extracting word by word from stream */
        ss >> temp;

        /* Checking the given word is integer or not */
        if (stringstream(temp) >> found)
            lista.push_back(found);

        /* To save from space at the end of string */
        temp = "";
    }
    list<int>::iterator it;
    for (it = lista.begin(); it != lista.end(); ++it)
        cout << '\t' << *it;
    cout << '\n';

    return lista;
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
    
}
