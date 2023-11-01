using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Common
{
	public class OneThreadSynchronizationContext : SynchronizationContext
	{
		/// <summary>
		/// 不需要加入SingletonList管理
		/// </summary>
		public static OneThreadSynchronizationContext Instance { get; } = new OneThreadSynchronizationContext();

		private readonly int mainThreadId = Thread.CurrentThread.ManagedThreadId;

		// 线程同步队列,发送接收socket回调都放到该队列,由poll线程统一执行
		private readonly ConcurrentQueue<(SendOrPostCallback, object)> queue = new ConcurrentQueue<(SendOrPostCallback, object)>();

		public void Update()
		{
			while (true)
			{
				if (!this.queue.TryDequeue(out var a))
				{
					return;
				}
				a.Item1(a.Item2);
			}
		}

		public override void Post(SendOrPostCallback callback, object state)
		{
			if (Thread.CurrentThread.ManagedThreadId == this.mainThreadId)
			{
				callback(state);
				return;
			}
			
			this.queue.Enqueue((callback, state));
		}
	}
}
