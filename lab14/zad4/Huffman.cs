using System.Collections.Generic;
using System.Data;
using System.Linq;
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
        private Dictionary<BitList, char> reverseCodesMap;

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
            reverseCodesMap = new Dictionary<BitList, char>();
            foreach (var entry in codesMap)
                reverseCodesMap.Add(entry.Value, entry.Key);
        }

        private void buildCodesMap(Node node, BitList code)
        {

            // ETAP 2  - tu należy zaimplementować generowanie kodów poszczególnych znaków oraz wypełnianie mapy codesMap

            if (node.character != default(char))
            {
                if (node == root) // Special case, there is only one character, so we should add a 0 to the code
                    code.Append(false);

                codesMap.Add(node.character, code);
            }
            else
            {
                // Not a leaf, continue down the path
                BitList rightSubtreeBitList = new BitList(code);
                code.Append(false);
                buildCodesMap(node.left, code);
                rightSubtreeBitList.Append(true);
                buildCodesMap(node.right, rightSubtreeBitList);
            }

        }

        public BitList Compress(string content)
        {

            // ETAP 2 - wykorzystując dane w codesMap należy zakodować napis przekazany jako parametr content

            BitList compressedContent = new BitList();
            foreach (char c in content)
                compressedContent.Append(codesMap[c]);

            return compressedContent;
        }

        public string Decompress(BitList compressed)
        {

            // ETAP 3 - należy zwrócić zdekodowane dane

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < compressed.Count; )
            {
                bool matchFound = false;

                foreach (BitList currentBitList in reverseCodesMap.Keys.Reverse())
                {
                    int j = 0;
                    while (j < currentBitList.Count && j + i < compressed.Count && currentBitList[j] == compressed[i + j])
                        j++;

                    if (j == currentBitList.Count)
                    {
                        sb.Append(reverseCodesMap[currentBitList]);
                        i += j;
                        matchFound = true;
                        break;
                    }
                }

                if (!matchFound)
                    break;
            }

            return sb.ToString();
        }

    }

}
