using System.Collections.Concurrent;
using System.Threading.Channels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_SharpExamplesLib.Language.ConcurrentDataTypes
{
    // Channel is the async counterpart of BlockingCollection: a waiting consumer
    // does not block a thread, it just does not continue until something arrives.
    public abstract class MyChannel
    {
        #region unbounded channel
        // #Channel #producer #consumer
        public static async Task Test_UnboundedChannel()
        {
            Channel<int> channel = Channel.CreateUnbounded<int>();

            Task producer = Task.Run(async () =>
            {
                for (int i = 1; i <= 5; i++)
                    await channel.Writer.WriteAsync(i);

                // Without Complete() the consumer below would wait forever.
                channel.Writer.Complete();
            });

            List<int> consumed = [];

            // ReadAllAsync ends on its own once the writer is complete.
            await foreach (int item in channel.Reader.ReadAllAsync())
                consumed.Add(item);

            await producer;

            Assert.AreEqual(5, consumed.Count);
            Assert.AreEqual(15, consumed.Sum());
        }
        #endregion

        #region bounded channel, back pressure
        // #Channel #bounded #backpressure
        public static async Task Test_BoundedChannelBackPressure()
        {
            // Capacity 1: a fast producer is slowed down by a slow consumer.
            Channel<int> channel = Channel.CreateBounded<int>(1);

            await channel.Writer.WriteAsync(1);

            // The channel is full, so this write cannot finish yet.
            Task pending = channel.Writer.WriteAsync(2).AsTask();
            Assert.IsFalse(pending.IsCompleted);

            // Reading makes room and the waiting write goes through.
            Assert.AreEqual(1, await channel.Reader.ReadAsync());
            await pending;

            Assert.AreEqual(2, await channel.Reader.ReadAsync());
        }
        #endregion

        #region several consumers on one channel
        // #Channel #consumers
        public static async Task Test_MultipleConsumers()
        {
            Channel<int> channel = Channel.CreateUnbounded<int>();

            for (int i = 1; i <= 100; i++)
                await channel.Writer.WriteAsync(i);

            channel.Writer.Complete();

            // Three consumers share one channel. Every item goes to exactly one
            // of them, nobody has to lock anything.
            ConcurrentBag<int> consumed = new ConcurrentBag<int>();
            Task[] consumers = Enumerable.Range(0, 3)
                .Select(_ => Task.Run(async () =>
                {
                    await foreach (int item in channel.Reader.ReadAllAsync())
                        consumed.Add(item);
                }))
                .ToArray();

            await Task.WhenAll(consumers);

            Assert.AreEqual(100, consumed.Count);
            Assert.AreEqual(5050, consumed.Sum());
        }
        #endregion

        #region full channel drops instead of waiting
        // #Channel #DropOldest
        public static void Test_BoundedChannelDropsOldest()
        {
            // The other answer to a full channel: throw the oldest item away
            // instead of slowing the producer down.
            Channel<int> channel = Channel.CreateBounded<int>(
                new BoundedChannelOptions(2) { FullMode = BoundedChannelFullMode.DropOldest });

            for (int i = 1; i <= 5; i++)
                Assert.IsTrue(channel.Writer.TryWrite(i));

            channel.Writer.Complete();

            Assert.IsTrue(channel.Reader.TryRead(out int first));
            Assert.IsTrue(channel.Reader.TryRead(out int second));
            Assert.IsFalse(channel.Reader.TryRead(out _));

            // 1, 2 and 3 were dropped along the way.
            Assert.AreEqual(4, first);
            Assert.AreEqual(5, second);
        }
        #endregion
    }
}
