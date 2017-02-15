
using System;

namespace ASD
{

class Lab01
    {

    public static void Main()
        {
        Random r = new Random(123);
        int[] tab = new int[30];
        int[] tab2 = new int[30] { 96, 88, 82, 81, 69, 62, 48, 48, 26, 13, 
                                   79, 56, 51, 37, 27,  4, -1, -8,-49,-61,
                                  -62,-68,-70,-72,-84,-90,-95,-96,-98,-99};
        int p;
        for ( int i=0 ; i<30 ; ++i )
            tab[i]=r.Next(-99,100);
        IPriorityQueue[] pq = new IPriorityQueue[3];
        pq[0] = new LazyPriorityQueue();
        pq[1] = new EagerPriorityQueue();
        pq[2] = new HeapPriorityQueue();
        string[] msg = new string[3] { "LazyPriorityQueue", "EagerPriorityQueue", "HeapPriorityQueue" };

        for ( int k = 0 ; k<3 ; ++k )
            {
            Console.WriteLine("\n  {0}\n",msg[k]);

            for ( int i=0 ; i<20 ; ++i )
                pq[k].Put(tab[i]);
            p = pq[k].ShowMax();
            Console.WriteLine("Lazy  - ShowMax:  {0,3}  -  {1}",p,p==tab2[0]);
            for ( int i=0 ; i<10 ; ++i )
                {
                p = pq[k].GetMax();
                Console.WriteLine("Lazy  - GetMax:   {0,3}  -  {1}",p,p==tab2[i]);
                }
            p = pq[k].ShowMax();
            Console.WriteLine("Lazy  - ShowMax:  {0,3}  -  {1}",p,p==tab2[16]);
            for ( int i=20 ; i<30 ; ++i )
                pq[k].Put(tab[i]);
            p = pq[k].ShowMax();
            Console.WriteLine("Lazy  - ShowMax:  {0,3}  -  {1}",p,p==tab2[10]);
            for ( int i=10 ; i<29 ; ++i )
                {
                p = pq[k].GetMax();
                Console.WriteLine("Lazy  - GetMax:   {0,3}  -  {1}",p,p==tab2[i]);
                }
            p = pq[k].ShowMax();
            Console.WriteLine("Lazy  - ShowMax:  {0,3}  -  {1}",p,p==tab2[29]);
            p = pq[k].GetMax();
            Console.WriteLine("Lazy  - GetMax:   {0,3}  -  {1}",p,p==tab2[29]);
            try
                {
                p = pq[k].ShowMax();
                Console.WriteLine("Lazy  - ShowMax:  {0,3}  -  should be exception",p);
                }
            catch ( InvalidOperationException e )
                {
                Console.WriteLine(e.Message);
                }
            try
                {
                p = pq[k].GetMax();
                Console.WriteLine("Lazy  - GetMax:   {0,3}  -  should be exception",p);
                }
            catch ( Exception e )
                {
                Console.WriteLine(e.Message);
                }
            }

        Console.WriteLine("\n");
        }
    }

}