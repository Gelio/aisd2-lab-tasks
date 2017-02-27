
using System;
using System.Linq;

namespace ASD
{

class EmptyException : ApplicationException
    {
    public EmptyException(bool rec=true) { }
    public EmptyException(string msg) : base(msg) { }
    public EmptyException(string msg, Exception ex) : base(msg,ex) { }
    }

public struct Result
    {
    public int? value;
    public int count;
    public int size;
    public Result(int? v, int c, int s)
        {
        value=v;
        count=c;
        size=s;
        }
    }

public class Lab01
    {

    private const int put  = 1;
    private const int get  = 2;
    private const int peek = 3;

    private static string[] operName = { null, "Put ", "Get ", "Peek" };

    public static void Main()
        {
        bool verboseStack             = false;
        bool verboseQueue             = false;
        bool verboseLazyPriorityQueue = false;
        bool verboseHeapPriorityQueue = false;
        bool maskExceptions           = true;

        int[] oper1, oper2;
        int[] input1, input2;
        Result[] output1_s, output2_s;
        Result[] output1_q, output2_q;
        Result[] output1_pq, output2_pq;
        bool res;

        PrepareTest1(out oper1, out input1, out output1_s, out output1_q, out output1_pq);
        PrepareTest2(out oper2, out input2, out output2_s, out output2_q, out output2_pq);

        Console.WriteLine("\nStack\n");
        Console.WriteLine("Test 1");
        res = Test(new Stack(), input1, output1_s, oper1, verboseStack,maskExceptions);
        Console.WriteLine("    {0}\n",res?"Passed":"Failed");
        Console.WriteLine("Test 2");
        res = Test(new Stack(), input2, output2_s, oper2, verboseStack,maskExceptions);
        Console.WriteLine("    {0}\n",res?"Passed":"Failed");

        Console.WriteLine("\nQueue\n");
        Console.WriteLine("Test 1");
        res = Test(new Queue(), input1, output1_q, oper1, verboseQueue,maskExceptions);
        Console.WriteLine("    {0}\n",res?"Passed":"Failed");
        Console.WriteLine("Test 2");
        res = Test(new Queue(), input2, output2_q, oper2, verboseQueue,maskExceptions);
        Console.WriteLine("    {0}\n",res?"Passed":"Failed");

        Console.WriteLine("\nLazyPriorityQueue\n");
        Console.WriteLine("Test 1");
        res = Test(new LazyPriorityQueue(), input1, output1_pq, oper1, verboseLazyPriorityQueue,maskExceptions);
        Console.WriteLine("    {0}\n",res?"Passed":"Failed");
        Console.WriteLine("Test 2");
        res = Test(new LazyPriorityQueue(), input2, output2_pq, oper2, verboseLazyPriorityQueue,maskExceptions);
        Console.WriteLine("    {0}\n",res?"Passed":"Failed");

        Console.WriteLine("\nHeapPriorityQueue\n");
        Console.WriteLine("Test 1");
        res = Test(new HeapPriorityQueue(), input1, output1_pq, oper1, verboseHeapPriorityQueue,maskExceptions);
        Console.WriteLine("    {0}\n",res?"Passed":"Failed");
        Console.WriteLine("Test 2");
        res = Test(new HeapPriorityQueue(), input2, output2_pq, oper2, verboseHeapPriorityQueue,maskExceptions);
        Console.WriteLine("    {0}\n",res?"Passed":"Failed");
        }

    private static bool Test(IContainer cont, int[] input, Result[] output, int[] oper, bool verbose, bool mask)
        {
        bool res = true;
        int? x=0;
        for ( int i=0, inp=0, outp=0 ; i<oper.Length ; ++i, ++outp )
            {
            try
                {
                switch ( oper[i] )
                    {
                    case put:
                        x=input[inp++];
                        cont.Put(x.Value);
                        break;
                    case get:
                        x=cont.Get();
                        break;
                    case peek:
                        x=cont.Peek();
                        break;
                    }
                }
            catch ( EmptyException )
                {
                x=null;
                }
            catch ( Exception e ) when ( mask )
                {
                if ( verbose )
                    {
                    Console.WriteLine("Incorrect exception: "+e.Message);
                    res = false;
                    continue;
                    }
                else
                    return false;
                }
            if ( verbose )
                {
                Console.Write("{0,2}: {1} -  v:{2,3}, c:{3,2}, s:{4,2}  - powinno byc -  v:{5,3}, c:{6,2}, s:{7,2}",
                              i, operName[oper[i]], x!=null?x.ToString():"Ex ", cont.Count, cont.Size,
                              output[outp].value!=null?output[outp].value.ToString():"Ex ", output[outp].count, output[outp].size);
                if ( x!=output[outp].value || cont.Count!=output[outp].count || cont.Size!=output[outp].size )
                    {
                    Console.WriteLine("  -  Failed");
                    res = false;
                    }
                else
                    Console.WriteLine();
                }
            else
                if ( x!=output[outp].value || cont.Count!=output[outp].count || cont.Size!=output[outp].size )
                    return false;
            }
        return res;
        }

    private static void PrepareTest1(out int[] oper, out int[] input, out Result[] output_s, out Result[] output_q, out Result[] output_pq)
        {
        oper=new int[28];
        for ( int i=0 ; i<14 ; i+=2 )
            {
            oper[i]=put;
            oper[i+1]=peek;
            }
        for ( int i=14 ; i<28 ; i+=2 )
            {
            oper[i]=get;
            oper[i+1]=peek;
            }

        input=Enumerable.Range(1,7).ToArray();

        output_s = new Result[] {
            new Result(1,1,2),
            new Result(1,1,2),
            new Result(2,2,2),
            new Result(2,2,2),
            new Result(3,3,4),
            new Result(3,3,4),
            new Result(4,4,4),
            new Result(4,4,4),
            new Result(5,5,8),
            new Result(5,5,8),
            new Result(6,6,8),
            new Result(6,6,8),
            new Result(7,7,8),
            new Result(7,7,8),
            new Result(7,6,8),
            new Result(6,6,8),
            new Result(6,5,8),
            new Result(5,5,8),
            new Result(5,4,8),
            new Result(4,4,8),
            new Result(4,3,8),
            new Result(3,3,8),
            new Result(3,2,8),
            new Result(2,2,8),
            new Result(2,1,8),
            new Result(1,1,8),
            new Result(1,0,8),
            new Result(null,0,8),
            };

        output_q = new Result[] {
            new Result(1,1,2),
            new Result(1,1,2),
            new Result(2,2,2),
            new Result(1,2,2),
            new Result(3,3,4),
            new Result(1,3,4),
            new Result(4,4,4),
            new Result(1,4,4),
            new Result(5,5,8),
            new Result(1,5,8),
            new Result(6,6,8),
            new Result(1,6,8),
            new Result(7,7,8),
            new Result(1,7,8),
            new Result(1,6,8),
            new Result(2,6,8),
            new Result(2,5,8),
            new Result(3,5,8),
            new Result(3,4,8),
            new Result(4,4,8),
            new Result(4,3,8),
            new Result(5,3,8),
            new Result(5,2,8),
            new Result(6,2,8),
            new Result(6,1,8),
            new Result(7,1,8),
            new Result(7,0,8),
            new Result(null,0,8),
            };

        output_pq = (Result[])output_s.Clone();
        }

    private static void PrepareTest2(out int[] oper, out int[] input, out Result[] output_s, out Result[] output_q, out Result[] output_pq)
        {
        input = new int[] { 15, 20, 10, 12, -3, 5, 11, 9, 7 }; 
        oper = new int[] { 1, 1, 1, 3, 2, 2, 1, 1, 3, 2, 2, 3, 2, 2, 1, 1, 1, 1, 3 };

        output_s = new Result[] {
            new Result(  15,1,2), // 1
            new Result(  20,2,2), // 1
            new Result(  10,3,4), // 1
            new Result(  10,3,4), // 3
            new Result(  10,2,4), // 2
            new Result(  20,1,4), // 2
            new Result(  12,2,4), // 1
            new Result(  -3,3,4), // 1
            new Result(  -3,3,4), // 3
            new Result(  -3,2,4), // 2
            new Result(  12,1,4), // 2
            new Result(  15,1,4), // 3
            new Result(  15,0,4), // 2
            new Result(null,0,4), // 2
            new Result(   5,1,4), // 1
            new Result(  11,2,4), // 1
            new Result(   9,3,4), // 1
            new Result(   7,4,4), // 1
            new Result(   7,4,4), // 3
            };

        output_q = new Result[] {
            new Result(  15,1,2), // 1
            new Result(  20,2,2), // 1
            new Result(  10,3,4), // 1
            new Result(  15,3,4), // 3
            new Result(  15,2,4), // 2
            new Result(  20,1,4), // 2
            new Result(  12,2,4), // 1
            new Result(  -3,3,4), // 1
            new Result(  10,3,4), // 3
            new Result(  10,2,4), // 2
            new Result(  12,1,4), // 2
            new Result(  -3,1,4), // 3
            new Result(  -3,0,4), // 2
            new Result(null,0,4), // 2
            new Result(   5,1,4), // 1
            new Result(  11,2,4), // 1
            new Result(   9,3,4), // 1
            new Result(   7,4,4), // 1
            new Result(   5,4,4), // 3
            };

        output_pq = new Result[] {
            new Result(  15,1,2), // 1
            new Result(  20,2,2), // 1
            new Result(  10,3,4), // 1
            new Result(  20,3,4), // 3
            new Result(  20,2,4), // 2
            new Result(  15,1,4), // 2
            new Result(  12,2,4), // 1
            new Result(  -3,3,4), // 1
            new Result(  12,3,4), // 3
            new Result(  12,2,4), // 2
            new Result(  10,1,4), // 2
            new Result(  -3,1,4), // 3
            new Result(  -3,0,4), // 2
            new Result(null,0,4), // 2
            new Result(   5,1,4), // 1
            new Result(  11,2,4), // 1
            new Result(   9,3,4), // 1
            new Result(   7,4,4), // 1
            new Result(  11,4,4), // 3
            };
        }

    }

}