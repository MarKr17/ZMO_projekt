#pragma once
#include <iostream>
#include <string>
#include <filesystem>
#include <fstream>
#include<list>
using namespace std;

void Wczytaj_Instancje(string filename);
list<int> Konwersja_String(string text);
class Instancja
{
public:list<int> dl_m1, dl_m2; //listy zawierające długości zadań na pierwszej i drugiej maszynie
public:int Tau;
public:int cn; //czas niedostępności

public:Instancja(string filename);
};