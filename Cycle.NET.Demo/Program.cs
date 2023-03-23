using System;
using System.Reactive;
using System.Reactive.Linq;

namespace Cycle.NET.Demo
{
    class Program
    {
        private static IObservable<int> LogDriver(IObservable<int> sinks)
        {
            sinks.Subscribe(i => Console.WriteLine("Log " + i.ToString()));

            return Observable.Empty<int>();
        }

        private static IObservable<int> KeyInputDriver(IObservable<int> sinks)
        {
            return Observable.Range(20, 5);
        }

        private static Streams<int, int, Unit> CycleMain(Streams<int, int, Unit> sources)
        {
            var sinks = new Streams<int, int, Unit>(
                sources.Second,
                Observable.Empty<int>(),
                Observable.Empty<Unit>());

            return sinks;
        }
        static void Main(string[] args)
        {
            var component = new Component<int, int, int, int, Unit, Unit>(
                CycleMain);
            var drivers = new Drivers<int, int, int, int, Unit, Unit>(
                onFirst: LogDriver,
                onSecond: KeyInputDriver,
                onThird: _ => _);
            Runner<int, int, int, int, Unit, Unit>.Run(component, drivers);
            Console.ReadLine();
        }
    }
}
