#pragma once
#include <iostream>
#include <string>
#include <filesystem>
#include <fstream>
#include<list>
#include<map>
using namespace std;

vector<int> Konwersja_String(string text);
bool cmp(pair<int, int>& a,pair<int, int>& b); //Funkcja porównująca potrzebna jest do sortującej
void sort(map<int, int>& M); //Funkcja sortująca słownik składający się z dwóch intów
class Instancja
{
	public:vector<int> dl_m1, dl_m2; //wektory zawierające długości zadań na pierwszej i drugiej maszynie
public:vector<int> kolej1;//kolejnosć zadań na maszynie 1
public:vector<int> kolej2;//kolejność zadań na maszynie 2
	public:int Tau;
	public:int cn; //czas niedostępności
	public:int t1;//czas na pierwszej maszynie
public:int t2;//czas na drugiej maszynie
public:int okres_nied;//czas następnego czasu niedostępności
public:map<int, int> maszyna1_dlugosci; //słownik zawierający długości zadań na pierwszej maszynie {nr zadania, długość}
public:map<int, int> maszyna2_dlugosci; //słownik zawierający długości zadań na drugiej maszynie {nr zadania, długość}
public:Instancja(string filename) ;
void pierwsze();
void nastepne_zadanie();
	
};
