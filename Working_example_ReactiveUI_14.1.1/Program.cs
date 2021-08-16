using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using ReactiveUI;

namespace Working_example_ReactiveUI_14_1_1
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var semaphore = new SemaphoreSlim(0);
			var command = ReactiveCommand.Create(() =>
			{
				semaphore.Release();
			});

			Observable.Return(Unit.Default)
					  .InvokeCommand(command);

			var task = semaphore.WaitAsync();
			if (await Task.WhenAny(Task.Delay(TimeSpan.FromMilliseconds(100)), task) == task)
			{
				System.Diagnostics.Debug.WriteLine("Command executed");
			}
			else
			{
				System.Diagnostics.Debug.WriteLine("Command not executed");
			}
		}
	}
}