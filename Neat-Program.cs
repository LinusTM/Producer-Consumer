using System;
using System.Threading;

namespace Producer_Consumer_Good;
class Program {
    static Queue<int> items = new Queue<int>();
    // static object _lock = new object();
    
    static void Main(string[] args) {
        Thread producer = new Thread(Produce);
        Thread consumer = new Thread(Consume);

        producer.Start();
        consumer.Start();

        producer.Join();
        consumer.Join();
    }

    public static void Produce() {
        while(true) {
            Monitor.Enter(items);
                for(int i = 0; i < 3; i++) {
                    Console.WriteLine($"Producer has produced {i}");
                    items.Enqueue(i); 
                }

            Monitor.PulseAll(items);
            
            Thread.Sleep(500);
            Monitor.Exit(items);
        }
    }

    public static void Consume() {
        while(true) {
            Monitor.Enter(items);
            if(items.Count == 0) {
                Monitor.Wait(items);
            }
            
            for(int i = 0; i < 3; i++) {
                Console.WriteLine($"Consumer has consumed {i}");
                items.Dequeue();
            }
            
            Thread.Sleep(1000);
            Monitor.Exit(items);
        }
    }
}
