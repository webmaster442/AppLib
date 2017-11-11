using System;
using System.Collections.Generic;

namespace AppLib.Common.IOC
{
	/// <summary>
	/// IoC container
	/// </summary>
	public class Container : IContainer
	{
        /// <summary>
        /// Singleton instance name
        /// </summary>
        public const string Singleton = "_Singleton_";

		/// <summary>
		/// Key: object containing the type of the object to resolve and the name of the instance (if any);
		/// Value: delegate that creates the instance of the object mapped
		/// </summary>
		private readonly Dictionary<MappingKey, Func<object>> _mappings;


		/// <summary>
		/// Creates a new instance of <see cref="Container"/>
		/// </summary>
		public Container()
		{
			_mappings = new Dictionary<MappingKey, Func<object>>();
		}


		/// <summary>
		/// Register a type mapping
		/// </summary>
		/// <param name="from">Type that will be requested</param>
		/// <param name="to">Type that will actually be returned</param>
		/// <param name="instanceName">Instance name (optional)</param>
		public void Register(Type from, Type to, string instanceName = null)
		{
			//if (from == null)
			//	throw new ArgumentNullException("from");

			if (to == null)
				throw new ArgumentNullException("to");

			if(!from.IsAssignableFrom(to))
			{
				const string errorMessageFormat = "Error trying to register the instance: '{0}' is not assignable from '{1}'";
				throw new InvalidOperationException(string.Format(errorMessageFormat, from.FullName, to.FullName));
			}


			Register(from, () => Activator.CreateInstance(to), instanceName);
		}


		/// <summary>
		/// Register a type mapping
		/// </summary>
		/// <typeparam name="TFrom">Type that will be requested</typeparam>
		/// <typeparam name="TTo">Type that will actually be returned</typeparam>
		/// <param name="instanceName">Instance name (optional)</param>
		public void Register<TFrom, TTo>(string instanceName = null) where TTo : TFrom
		{
			Register(typeof(TFrom), typeof(TTo), instanceName);
		}


		/// <summary>
		/// Register a type mapping
		/// </summary>
		/// <param name="type">Type that will be requested</param>
		/// <param name="createInstanceDelegate">A delegate that will be used to 
		/// create an instance of the requested object</param>
		/// <param name="instanceName">Instance name (optional)</param>
		public void Register(Type type, Func<object> createInstanceDelegate, string instanceName = null)
		{
			if (type == null)
				throw new ArgumentNullException("type");

			if (createInstanceDelegate == null)
				throw new ArgumentNullException("createInstanceDelegate");


			var key = new MappingKey(type, instanceName);

			if (_mappings.ContainsKey(key))
			{
				const string errorMessageFormat = "The requested mapping already exists - {0}";
				throw new InvalidOperationException(string.Format(errorMessageFormat, key.ToTraceString()));
			}


			_mappings.Add(key, createInstanceDelegate);
		}


		/// <summary>
		/// Register a type mapping
		/// </summary>
		/// <typeparam name="T">Type that will be requested</typeparam>
		/// <param name="createInstanceDelegate">A delegate that will be used to 
		/// create an instance of the requested object</param>
		/// <param name="instanceName">Instance name (optional)</param>
		public void Register<T>(Func<T> createInstanceDelegate, string instanceName = null)
		{
			if (createInstanceDelegate == null)
				throw new ArgumentNullException("createInstanceDelegate");


			Register(typeof(T), createInstanceDelegate as Func<object>, instanceName);
		}


		/// <summary>
		/// Check if a particular type/instance name has been registered with the container
		/// </summary>
		/// <param name="type">Type to check registration for</param>
		/// <param name="instanceName">Instance name (optional)</param>
		/// <returns><c>true</c>if the type/instance name has been registered 
		/// with the container; otherwise <c>false</c></returns>
		public bool IsRegistered(Type type, string instanceName = null)
		{
			if (type == null)
				throw new ArgumentNullException("type");


			var key = new MappingKey(type, instanceName);

			return _mappings.ContainsKey(key);
		}


		/// <summary>
		/// Check if a particular type/instance name has been registered with the container
		/// </summary>
		/// <typeparam name="T">Type to check registration for</typeparam>
		/// <param name="instanceName">Instance name (optional)</param>
		/// <returns><c>true</c>if the type/instance name has been registered 
		/// with the container; otherwise <c>false</c></returns>
		public bool IsRegistered<T>(string instanceName = null)
		{
			return IsRegistered(typeof(T), instanceName);
		}


		/// <summary>
		/// Resolve an instance of the requested type from the container.
		/// </summary>
		/// <param name="type">Requested type</param>
		/// <param name="instanceName">Instance name (optional)</param>
		/// <returns>The retrieved object</returns>
		public object Resolve(Type type, string instanceName = null)
		{
			var key = new MappingKey(type, instanceName);
			Func<object> createInstance;

			if (_mappings.TryGetValue(key, out createInstance))
			{
				var instance = createInstance();
				return instance;
			}

			const string errorMessageFormat = "Could not find mapping for type '{0}'";
			throw new InvalidOperationException(string.Format(errorMessageFormat, type.FullName));
		}


		/// <summary>
		/// Resolve an instance of the requested type from the container.
		/// </summary>
		/// <typeparam name="T">Requested type</typeparam>
		/// <param name="instanceName">Instance name (optional)</param>
		/// <returns>The retrieved object</returns>
		public T Resolve<T>(string instanceName = null)
		{
			object instance = Resolve(typeof(T), instanceName);

			return (T) instance;
		}


		/// <summary>
		/// For debugging purposes only
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			if (_mappings == null)
				return "No mappings";

			return string.Join(Environment.NewLine, _mappings.Keys);
		}

	}
}
