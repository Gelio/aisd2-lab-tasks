
using System;

namespace ASD
{

partial class Lab02
    {

    // zwraca dominujacy element (gdy takiego nie ma zwraca null)
    // w parametrze max ustawia liczbe wystapien dominujacego elementu (gdy takiego nie ma ustawia 0)
    // wymagana zlozonosc o(n*n)
    static public Elem FindWinnerSimply(Elem[] el, out int max)
        {

            foreach (Elem s in el)
            {
                int k = 0;
                foreach (Elem m in el)
                    if (s.Compare(m)) k++;
                if (k > el.Length / 2)
                {
                    max = k;
                    return s;
                }
            }

            max = 0;
            return null;
        }

    // zwraca dominujacy element (gdy takiego nie ma zwraca null)
    // w parametrze max ustawia liczbe wystapien dominujacego elementu (gdy takiego nie ma ustawia 0)
    // wymagana zlozonosc o(n*log(n))
    static public Elem FindWinnerFast(Elem[] el, out int max)
        {
            Elem t = FindWinnerRec(el, out max, 0, el.Length - 1);        
            return t;
        }

    static private Elem FindWinnerRec(Elem[] el, out int max, int begin, int end)
    {
        if (end != begin)
        {
            Elem First, Second;
            int FirstCount = 0, SecondCount = 0, med = (begin + end)/2;

            First = FindWinnerRec(el, out FirstCount, begin, med);
            Second = FindWinnerRec(el, out SecondCount, med + 1, end);

            int temp = 0;
            if (First != null)
            {
                temp = CountElement(el, med + 1, end, First);
                if (temp + FirstCount > (end - begin + 1) / 2)
                {
                    max = temp + FirstCount;
                    return First;
                }
            }

            if (Second != null)
            {
                temp = CountElement(el, begin, med, Second);
                if (temp + SecondCount > (end - begin + 1) / 2)
                {
                    max = temp + SecondCount;
                    return Second;
                }
            }

            max = 0;
            return null;
        }

        max = 1;
        return el[begin];
    }

    private static int CountElement(Elem[] el, int begin, int end, Elem Element)
    {
        int k = 0;
        for (int i = begin; i <= end; i++)
            if (el[i].Compare(Element)) k++;
        return k;
    }


    //
    // tu mozna dopisac prywatne funkcje pomocnicze
    //

    }

}  // namespace ASD
