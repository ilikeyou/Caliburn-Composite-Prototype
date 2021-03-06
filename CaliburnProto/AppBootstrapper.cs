using System.IO;
using System.Reflection;

namespace CaliburnProto
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.Composition;
	using System.ComponentModel.Composition.Hosting;
	using System.ComponentModel.Composition.Primitives;
	using System.Linq;
	using Caliburn.Micro;
    using CaliburnProto.Infrastructure;

	public class AppBootstrapper : Bootstrapper<IShell>
	{
		CompositionContainer container;

        protected override IEnumerable<System.Reflection.Assembly> SelectAssemblies()
        {
            var assemblies =  base.SelectAssemblies();
            var directory = new DirectoryInfo(@"Modules");
            var files = directory.GetFiles("*.dll", SearchOption.TopDirectoryOnly);
            //only load the the dlls that are from this namespace
            var modules = files.Where(f => f.Name.Contains("CaliburnProto"))
                                .Select(f => Assembly.LoadFile(f.FullName));
            return assemblies.Concat(modules);
        }

		/// <summary>
		/// By default, we are configured to use MEF
		/// </summary>
		protected override void Configure() {

		    var catalog = new AggregateCatalog(
		        AssemblySource.Instance.Select(x => new AssemblyCatalog(x)).OfType<ComposablePartCatalog>()
                );
			container = new CompositionContainer(catalog);

			var batch = new CompositionBatch();
            //not needed because we use the DockAwareWindowManager
            //  batch.AddExportedValue<IWindowManager>(new WindowManager());
			batch.AddExportedValue<IEventAggregator>(new EventAggregator());
			batch.AddExportedValue(container);
		    batch.AddExportedValue(catalog);

			container.Compose(batch);
		}

		protected override object GetInstance(Type serviceType, string key)
		{
			string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
			var exports = container.GetExportedValues<object>(contract);

			if (exports.Count() > 0)
				return exports.First();

			throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
		}

		protected override IEnumerable<object> GetAllInstances(Type serviceType)
		{
			return container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
		}

		protected override void BuildUp(object instance)
		{
			container.SatisfyImportsOnce(instance);
		}
	}
}
