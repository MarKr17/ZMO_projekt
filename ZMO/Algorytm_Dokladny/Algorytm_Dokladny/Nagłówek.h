#pragma once
#include <iostream>
#include <string>
#include <filesystem>
#include <fstream>
#include<list>
#include<map>
using namespace std;

void Wczytaj_Instancje(string filename);
vector<int> Konwersja_String(string text);
bool cmp(pair<int, int>& a,pair<int, int>& b); //Funkcja porównuj¹ca potrzebna jest do sortuj¹cej
void sort(map<int, int>& M); //Funkcja sortuj¹ca s³ownik sk³adaj¹cy siê z dwóch intów
class Instancja
{
	public:vector<int> dl_m1, dl_m2; //wektory zawieraj¹ce d³ugoœci zadañ na pierwszej i drugiej maszynie
public:vector<int> kolej1;//kolejnosæ zadañ na maszynie 1
public:vector<int> kolej2;//kolejnoœæ zadañ na maszynie 2
	public:int Tau;
	public:int cn; //czas niedostêpnoœci
	public:int t1;//czas na pierwszej maszynie
public:int t2;//czas na drugiej maszynie
public:int okres_nied;//czas nastêpnego czasu niedostêpnoœci
public:map<int, int> maszyna1_dlugosci; //s³ownik zawieraj¹cy d³ugoœci zadañ na pierwszej maszynie {nr zadania, d³ugoœæ}
public:map<int, int> maszyna2_dlugosci; //s³ownik zawieraj¹cy d³ugoœci zadañ na drugiej maszynie {nr zadania, d³ugoœæ}
	public:Instancja(string filename);
	
};