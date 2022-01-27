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
bool cmp(pair<int, int>& a,pair<int, int>& b); //Funkcja por�wnuj�ca potrzebna jest do sortuj�cej
void sort(map<int, int>& M); //Funkcja sortuj�ca s�ownik sk�adaj�cy si� z dw�ch int�w
class Instancja
{
	public:vector<int> dl_m1, dl_m2; //wektory zawieraj�ce d�ugo�ci zada� na pierwszej i drugiej maszynie
public:vector<int> kolej1;//kolejnos� zada� na maszynie 1
public:vector<int> kolej2;//kolejno�� zada� na maszynie 2
	public:int Tau;
	public:int cn; //czas niedost�pno�ci
	public:int t1;//czas na pierwszej maszynie
public:int t2;//czas na drugiej maszynie
public:int okres_nied;//czas nast�pnego czasu niedost�pno�ci
public:map<int, int> maszyna1_dlugosci; //s�ownik zawieraj�cy d�ugo�ci zada� na pierwszej maszynie {nr zadania, d�ugo��}
public:map<int, int> maszyna2_dlugosci; //s�ownik zawieraj�cy d�ugo�ci zada� na drugiej maszynie {nr zadania, d�ugo��}
	public:Instancja(string filename);
	
};