namespace ArxEcs
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class Entity
	{
		public delegate void EntityComponentHandler(Entity e, IComponent c);
		public event EntityComponentHandler ComponentAdded;
		public event EventHandler ComponentRemoved;

		public ComponentProcessingSystem ProcessingSystem { get; set; }

		readonly Dictionary<Type, IComponent> _components;

		public Entity()
		{
			_components = new Dictionary<Type, IComponent>();
			ProcessingSystem = new ComponentProcessingSystem(this);
		}

		/// <summary>
		/// Get a component
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns>The component (or null if not present)</returns>
		public T GetComponent<T>() where T : class, IComponent
			=> (T)_components.FirstOrDefault(c => c.Key == typeof(T)).Value;

		/// <summary>
		/// Checks if entity has a certain component
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns>True if component is present on the entity</returns>
		public bool HasComponent<T>() where T : class, IComponent
			=> _components.FirstOrDefault(c => c.Key == typeof(T)).Value != null;

		/// <summary>
		/// Checks if entity has a certain component
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns>True if component is present on the entity</returns>
		public bool HasComponent(Type comp)
			=> _components.FirstOrDefault(c => c.Key == comp).Value != null;

		/// <summary>
		/// Adds a given component
		/// </summary>
		/// <param name="component"></param>
		/// <returns></returns>
		public Entity AddComponent(IComponent component)
		{
			if (_components.ContainsKey(component.GetType()))
			{
				throw new ArgumentException("Component already exists");
			}

			_components.Add(component.GetType(), component);

			ComponentAdded?.Invoke(this, component);

			return this;
		}

		/// <summary>
		/// Remove a given component
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public Entity RemoveComponent<T>() where T : class, IComponent
		{
			if (_components.ContainsKey(typeof(T)))
			{
				_components.Remove(typeof(T));
			}

			ComponentRemoved?.Invoke(this, EventArgs.Empty);

			return this;
		}

		/// <summary>
		/// Removes a given component
		/// </summary>
		/// <param name="component"></param>
		/// <returns></returns>
		public Entity RemoveComponent(IComponent component)
		{
			if (_components.ContainsKey(component.GetType()))
			{
				_components.Remove(component.GetType());
			}

			ComponentRemoved?.Invoke(this, null);

			return this;
		}

		/// <summary>
		/// Replaces a given component (of the same type)
		/// </summary>
		/// <param name="newComponent">The component that should be replaced</param>
		/// <returns></returns>
		public Entity ReplaceComponent(IComponent newComponent)
		{
			RemoveComponent(newComponent);
			AddComponent(newComponent);

			return this;
		}
	}
}