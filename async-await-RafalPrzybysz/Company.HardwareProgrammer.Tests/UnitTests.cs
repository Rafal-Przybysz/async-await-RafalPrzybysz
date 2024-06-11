using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Company.HardwareProgrammer.Tests;

public class HardwareWriterMock : IHardwareWriter
{
	public ConcurrentBag<string> WrittenNames { get; } = new();
	public Task Write(string text, CancellationToken cancellationToken)
	{
		WrittenNames.Add(text);
		return Task.Delay(1_000, cancellationToken);

	}
}

public class UnitTests
{
	readonly string[] _names = Enumerable.Range(0, 10).Select(x => "device" + x).ToArray();
	[Fact]
	public async Task Test1()
	{
		var updater = new HardwareUpdater();
		var writer = new HardwareWriterMock();
		await updater.Update(_names, writer, CancellationToken.None);
		Assert.All(_names, x => Assert.Contains(x, writer.WrittenNames));
	}

	[Fact]
	public async Task Test2()
	{
		var updater = new HardwareUpdater();
		var writer = new HardwareWriterMock();
		var tokenSource = new CancellationTokenSource();
		var updaterTask = updater.Update(_names, writer, tokenSource.Token);
		await Task.Delay(2_000);
		tokenSource.Cancel();
		var ex = await Assert.ThrowsAsync<TaskCanceledException>(async () => await updaterTask);
		Assert.All(_names, x => Assert.Contains(x, writer.WrittenNames));
	}
}