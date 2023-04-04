using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Linq;

namespace Cycle.NET
{
    public class Streams : Dictionary<string, IObservable<object>>
    {
        public Streams() : base() { }
        public Streams(Dictionary<string, IObservable<object>> value) : base(value) { }
    }

    public partial class Drivers : Dictionary<string, Func<IObservable<object>, IObservable<object>>>
    {
        public Drivers() : base() { }
    }

    public static class Runner
    {
        public static void Run(Func<Streams, Streams> main, Drivers drivers)
        {
            // Create fake sinks to use to call the drivers to get around the interdependency between
            // main and the drivers.
            var fakeSinks = drivers.ToDictionary(d => d.Key, d => new ReplaySubject<object>());

            // Call the drivers and collate the returned sources.
            var sources = drivers.ToDictionary(d => d.Key, d => d.Value(fakeSinks[d.Key]));

            Streams sinks = main(new Streams(sources));

            // Update the sinks returned from main with the sinks used by the drivers.
            foreach (var sink in sinks)
            {
                sink.Value.Subscribe(s => fakeSinks[sink.Key].OnNext(s));
            }
        }
    }
}
