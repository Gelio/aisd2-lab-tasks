using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ASD15
{
    class Lab15_main
    {
        static void Main(string[] args)
        {
            TestPalindroms();

            if (Debugger.IsAttached)
                Console.ReadKey();
        }


        static void TestPalindroms()
        {
            Random r = new Random(116);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 100; i++)
                sb.Append((char)('a' + r.Next(4)));

            StringBuilder sb2 = new StringBuilder();
            for (int i = 0; i < 1000000; i++)
                sb2.Append((char)('a' + r.Next(6)));

            var tests = new[]{
                new {Text="aaba", Even=new string[]{"aa"},Odd=new string[]{"aba"}, TotalCount=2, Answer="aa aba"},
                new {Text="aabbaa", Even=new string[]{"aa","aabbaa"},Odd=new string[]{}, TotalCount=3,Answer="aa aabbaa aa"},
                new {Text="kobyłamamałybok wasitacatisaw", Even=new string[]{},Odd=new string[]{"ama","kobyłamamałybok","wasitacatisaw"}, TotalCount=4, Answer="" },
                new {Text="ababababaa", Even=new string[]{"aa"},Odd=new string[]{"aba","ababa","abababa"}, TotalCount=8, Answer="" },
                new {Text="abcaaddbabaababddaacadabcaddaabdabacabcd", Even=new string[]{"aa","dd","caaddbabaababddaac","adda"},Odd=new string[]{"bab","aba","aca","bacab"}, TotalCount=15, Answer="" },                
                new {Text=sb.ToString(), Even = new string[]{"bddb", "bcddcb", "dd", "aa", "cc", "ccaacc"},Odd=new string[]{"badab", "ccc"}, TotalCount=44 , Answer=""},
                new {Text=sb2.ToString(), Even = new string[]{"aa","cc"}, Odd = new string[]{"beb", "aba", "aea", "bab"}, TotalCount=333439, Answer="" },
            };

            for (int i = 0; i < tests.Length; i++)
            {
                try
                {
                    Console.WriteLine("Test {0} :", i + 1);

                    var result = Lab15.FindPalindroms(tests[i].Text);

                    if (i == 0 || i == 1)
                    {
                        Console.WriteLine(tests[i].Text);
                        Console.Write("Palindromy: {0}", tests[i].Answer);
                        Console.WriteLine();
                        Console.Write("Znalezione: ");
                        foreach (var p in result)
                            Console.Write("{0} ", p);
                        Console.WriteLine();
                    }



                    bool evenOk = true, oddOk = true;
                    foreach (var item in tests[i].Even)
                    {
                        if (!result.Contains(item))
                        {
                            if (evenOk)
                            {
                                Console.Write("Parzyste brak palindromu:");
                                evenOk = false;
                            }
                            Console.Write(" {0}", item);

                        }

                    }
                    if (!evenOk)
                        Console.WriteLine();
                    foreach (var item in tests[i].Odd)
                    {
                        if (!result.Contains(item))
                        {
                            if (oddOk)
                            {
                                Console.Write("Nieparzyste brak palindromu:");
                                oddOk = false;
                            }
                            Console.Write(" {0}", item);
                        }
                    }
                    if (!oddOk)
                        Console.WriteLine();
                    if (result.Count() != tests[i].TotalCount)
                        Console.WriteLine("Nieprawidłowa liczba znalezionych palindromów, powinno być {0}, znaleziono {1}", tests[i].TotalCount, result.Count());

                    Console.WriteLine("Parzyste {0}", evenOk ? "SUKCES" : "BŁĄD");
                    Console.WriteLine("Nieparzyste {0}", oddOk ? "SUKCES" : "BŁĄD");
                    Console.WriteLine();
                }
                catch (Exception e)
                {
                    Console.WriteLine("BŁĄD - wyjątek", i + 1);
                    Console.WriteLine(e.StackTrace);
                }
            }

        }

    }
}
