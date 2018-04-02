namespace ArxEcs
{
	public interface IProcessingSystem
	{
		string SystemId { get; set; }
		Entity Entity { get; set; }
		void Process();
	}
}