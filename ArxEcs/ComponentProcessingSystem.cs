namespace ArxEcs
{
	using System.Collections.Generic;
	using System.Linq;

	public class ComponentProcessingSystem
	{
		Entity parentEntity;
		List<IProcessingSystem> systems;

		public ComponentProcessingSystem(Entity owner)
		{
			systems = new List<IProcessingSystem>();
			parentEntity = owner;
		}

		public bool ContainsSystem(string id) => systems.Any(s => s.SystemId == id);

		public void AddSystem(IProcessingSystem system)
		{
			if (systems.Any(s => s.SystemId == system.SystemId))
			{
				throw new System.ArgumentException("System with that id already exists");
			}

			system.Entity = parentEntity;
			systems.Add(system);
		}

		public void ProcessSystems()
		{
			systems.ForEach(s => s.Process());
		}
	}
}
