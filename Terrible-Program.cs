using System;
using System.Threading;

namespace Producer_Consumer;
class Program {
    static Queue<int> items = new Queue<int>();
    
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
            Random random = new Random();
            if(random.Next(0, 5) == 0) {
                Thread.Sleep(100/15);
            }
            
            for(int i = 0; i < 3; i++) {
                if(10 < items.Count) {
                    Console.WriteLine($"Producer could not produce {i}");
                } else {
                    items.Enqueue(i);
                    Console.WriteLine($"Producer has produced {i}");
                }
            }
        }
    }

    public static void Consume() {
        while(true) {
            Random random = new Random();
            if(random.Next(0, 5) == 0) {
                Thread.Sleep(100/15);
            }

            for(int i = 0; i < 3; i++) {
                if(0 >= items.Count) {
                    Console.WriteLine($"Consumer could not consume {i}");
                } else { 
                    items.Dequeue();
                    Console.WriteLine($"Consumer has consumed {i}");
                }
            }
        }
    }
}
