
using System;

namespace ASD
{

public interface IContainer
    {
    void Put(int x);      //  dodaje element do kontenera

    int  Get();           //  zwraca pierwszy element kontenera i usuwa go z kontenera
                          //  w przypadku pustego kontenera zg³asza wyj¹tek typu EmptyException (zdefiniowany w Lab01_Main.cs)

    int  Peek();          //  zwraca pierwszy element kontenera (ten, który bêdzie pobrany jako pierwszy),
                          //  ale pozostawia go w kontenerze (czyli nie zmienia zawartoœci kontenera)
                          //  w przypadku pustego kontenera zg³asza wyj¹tek typu EmptyException (zdefiniowany w Lab01_Main.cs)

    int  Count { get; }   //  zwraca liczbê elementów w kontenerze

    int  Size  { get; }   //  zwraca rozmiar kontenera (rozmiar wewnêtznej tablicy)
    }

public class Stack : IContainer
    {
    private int[] tab;      // wewnêtrzna tablica do pamiêtania elementów
    private int count = 0;  // liczba elementów kontenera - metody Put i Get powinny (musz¹) to aktualizowaæ
    // nie wolno dodawaæ ¿adnych pól ani innych sk³adowych

    public Stack(int n=2)
        {
        tab = new int[n>2?n:2];
        }

    public void Put(int x)
        {
        // uzupe³niæ
        }

    public int Get()
        {
        return 0; // zmieniæ
        }

    public int Peek()
        {
        return 0; // zmieniæ
        }

    public int Count => count;

    public int Size => tab.Length;

    } // class Stack


public class Queue : IContainer
    {
    private int[] tab;      // wewnêtrzna tablica do pamiêtania elementów
    private int count = 0;  // liczba elementów kontenera - metody Put i Get powinny (musz¹) to aktualizowaæ
    // mo¿na dodaæ jedno pole (wiêcej nie potrzeba)

    public Queue(int n=2)
        {
        tab = new int[n>2?n:2];
        }

    public void Put(int x)
        {
        // uzupe³niæ
        }

    public int Get()
        {
        return 0; // zmieniæ
        }

    public int Peek()
        {
        return 0; // zmieniæ
        }

    public int Count => count;

    public int Size => tab.Length;

    } // class Queue


public class LazyPriorityQueue : IContainer
    {
    private int[] tab;      // wewnêtrzna tablica do pamiêtania elementów
    private int count = 0;  // liczba elementów kontenera - metody Put i Get powinny (musz¹) to aktualizowaæ
    // nie wolno dodawaæ ¿adnych pól ani innych sk³adowych

    public LazyPriorityQueue(int n=2)
        {
        tab = new int[n>2?n:2];
        }

    public void Put(int x)
        {
        // uzupe³niæ
        }

    public int Get()
        {
        return 0; // zmieniæ
        }

    public int Peek()
        {
        return 0; // zmieniæ
        }

    public int Count => count;

    public int Size => tab.Length;

    } // class LazyPriorityQueue


public class HeapPriorityQueue : IContainer
    {
    private int[] tab;      // wewnêtrzna tablica do pamiêtania elementów
    private int count = 0;  // liczba elementów kontenera - metody Put i Get powinny (musz¹) to aktualizowaæ
    // nie wolno dodawaæ ¿adnych pól ani innych sk³adowych

    public HeapPriorityQueue(int n=2)
        {
        tab = new int[n>2?n:2];
        }

    public void Put(int x)
        {
        // uzupe³niæ
        }

    public int Get()
        {
        return 0; // zmieniæ
        }

    public int Peek()
        {
        return 0; // zmieniæ
        }

    public int Count => count;

    public int Size => tab.Length;

    } // class HeapPriorityQueue

} // namespace ASD
