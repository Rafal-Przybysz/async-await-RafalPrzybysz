namespace Company.HardwareProgrammer
{
	// DO NOT CHANGE THIS FILE!
	public class HardwareProgrammer
	{
		private readonly string _deviceName;
		private readonly IHardwareWriter _writer;

		public HardwareProgrammer(string deviceName, IHardwareWriter writer)
		{
			_deviceName = deviceName ?? throw new ArgumentNullException(nameof(deviceName));
			_writer = writer ?? throw new ArgumentNullException(nameof(writer));
		}

		public Task PrepareConfiguration(CancellationToken token)
		{
			return Task.Delay(1_000, token);
		}

		public Task ConfigureDevice(CancellationToken token)
		{
			return _writer.Write(_deviceName, token);

		}
	}
}
