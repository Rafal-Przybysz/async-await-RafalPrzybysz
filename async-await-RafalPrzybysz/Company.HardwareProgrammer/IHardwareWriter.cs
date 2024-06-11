namespace Company.HardwareProgrammer
{
	public interface IHardwareWriter
	{
		Task Write(string text, CancellationToken cancellationToken);
	}
}
