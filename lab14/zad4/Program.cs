using System;
using System.Collections.Generic;
using ASD;

namespace Teksty
{

public partial class Huffman
    {

    private bool checkHuffmanTree(Node node)
        {
        if ( node==null || ( node.left!=null && node.right==null ) || node.left==null && node.right!=null )
            return false;
        if ( node.left==null && node.right==null )
            return node.character>0;
        return node.character==0 && node.freq==node.left.freq+node.right.freq && checkHuffmanTree(node.left) && checkHuffmanTree(node.right);
        }

    public bool isValid()
        {
        return root!=null && checkHuffmanTree(root);
        }

    }

class Program
    {

    static void Main(string[] args)
        {
        var testStrings = new List<string>();
        var testCompressed = new List<BitList>();
        var testTrees = new List<Huffman>();
        var outputs = new List<Byte[]>();

        testStrings.Add("ab");
        testCompressed.Add(new BitList("01"));
        testStrings.Add("aba");
        testCompressed.Add(new BitList("101"));
        testStrings.Add("aabaa");
        testCompressed.Add(new BitList("11011"));
        testStrings.Add("aaabbababb");
        testCompressed.Add(new BitList("0001101011"));
        testStrings.Add("aacabccbcacbcabb");
        testCompressed.Add(new BitList("10100101100110100110101111"));
        testStrings.Add("ala ma kota");
        testCompressed.Add(new BitList("01100011110100111101110011010"));
        testStrings.Add("stol z powylamywanymi nogami");
        testCompressed.Add(new BitList("11100110100111010000110110001111001110110011010100010001101110011000010101111100011000111110110001011111"));
        testStrings.Add("litwo ojczyzno moja, ty jestes jak zdrowie");
        testCompressed.Add(new BitList("1110100011101101001001101000000010010100101101011110110011001100100000111110110111010110101110000011111100101101111110011000011111001011101010111100111011100010000110111"));
        testStrings.Add("aaa");
        testCompressed.Add(new BitList("000"));

        // ETAP 1
        Console.WriteLine("\n*** ETAP 1 (2p.) ***\n");
        bool treesOK = true;
        foreach (string str in testStrings)
            {
            Huffman testTree = new Huffman(str);
            if (testTree == null || !testTree.isValid())
                {
                Console.WriteLine(" bledne drzewo Huffmana napisu: " + str + "\n");
                treesOK = false;
                }
            else
                {
                Console.WriteLine(" drzewo napisu " + str + "\n prawdopodobnie OK\n");
                }
            testTrees.Add(testTree);
            }
        Console.WriteLine("\nETAP 1: {0}\n",treesOK?"prawdopodobnie OK":"niepoprawny");

        // ETAP 2
        Console.WriteLine("\n*** ETAP 2 (1p.) ***\n");
        bool compressOK = true;
        for (int i = 0; i < testStrings.Count; ++i)
            {
            BitList compressed = testTrees[i].Compress(testStrings[i]);

            if (compressed != null && compressed.Count == testCompressed[i].Count )
                {
                Console.WriteLine(" kompresja napisu  : " + testStrings[i]);
                if ( compressed.Equals(testCompressed[i]) )
                    Console.WriteLine(" skompresowane dane: " + compressed + "\n OK\n");
                else
                    {
                    Console.WriteLine(" skompresowane dane: " + compressed + "\n prawdopodobnie OK\n");
                    testCompressed[i] = compressed; // do dalszego wykorzystania w sprawdzaniu Decompress
                    }
                }
            else
                {
                Console.WriteLine(" blad podczas kompresji napisu: " + testStrings[i]);
                if (compressed == null)
                    Console.WriteLine(" dane niepoprawne: null");
                else
                    Console.WriteLine(" jest dlugosc: " + compressed.Count);
                Console.WriteLine(" powinna być dlugosc: " + testCompressed[i].Count);
                compressOK=false;
                }
            }
        Console.WriteLine("\nETAP 2: {0}\n",compressOK?"prawdopodobnie OK":"niepoprawny");

        // ETAP 3
        Console.WriteLine("\n*** ETAP 3 (1p.) ***\n");
        bool decompressOK = true;
        for (int i = 0; i < testCompressed.Count; ++i)
        {
            string decompressed = testTrees[i].Decompress(testCompressed[i]);
            if (decompressed != null && testStrings[i] == decompressed)
            {
                Console.WriteLine(" dekompresja napisu  : " + testStrings[i] + "\n OK\n");
            }
            else
            {
                Console.WriteLine(" blad podczas dekompresji napisu: " + testStrings[i]);
                Console.WriteLine(" otrzymano: " + decompressed);
                decompressOK=false;
            }
        }
        Console.WriteLine("\nETAP 3: {0}\n",decompressOK?"OK":"niepoprawny");
        }

    }

}
