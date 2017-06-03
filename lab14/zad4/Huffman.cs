using System.Collections.Generic;
using System.Text;
using ASD;

namespace Teksty
{

    public partial class Huffman
    {

        private class Node
        {

            public char character;
            public long freq;
            public Node left, right;

            public Node(char character, long freq)
            {
                this.character = character;
                this.freq = freq;
            }

            public Node(long freq, Node left, Node right)
            {
                this.freq = freq;
                this.left = left;
                this.right = right;
            }

        }

        private Node root;
        private Dictionary<char, BitList> codesMap;

        public Huffman(string content)
        {

            // ETAP 1 - tu należy zaimplementować tworzenie drzewa Huffmana
            Dictionary<char, long> characterFrequency = new Dictionary<char, long>();
            foreach (char c in content)
            {
                if (characterFrequency.ContainsKey(c))
                    characterFrequency[c]++;
                else
                    characterFrequency[c] = 1;
            }
            PriorityQueue<Node> queue = new PriorityQueue<Node>((n1, n2) => n1.freq < n2.freq);
            foreach (var characterWithFrequency in characterFrequency)
                queue.Put(new Node(characterWithFrequency.Key, characterWithFrequency.Value));

            while (queue.Count > 1)
            {
                Node n1 = queue.Get(),
                    n2 = queue.Get();

                Node result = new Node(n1.freq + n2.freq, n1, n2);
                queue.Put(result);
            }

            root = queue.Get();


            codesMap = new Dictionary<char, BitList>();
            buildCodesMap(root, new BitList());
        }

        private void buildCodesMap(Node node, BitList code)
        {

            // ETAP 2  - tu należy zaimplementować generowanie kodów poszczególnych znaków oraz wypełnianie mapy codesMap

        }

        public BitList Compress(string content)
        {

            // ETAP 2 - wykorzystując dane w codesMap należy zakodować napis przekazany jako parametr content

            return null;
        }

        public string Decompress(BitList compressed)
        {

            // ETAP 3 - należy zwrócić zdekodowane dane

            return null;
        }

    }

}
