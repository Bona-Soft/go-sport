using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Lifestyle;

namespace MYB.BaseApplication.Infrastructure.Windsor
{
	public abstract class HybridLifestyleManager<M1, M2> : AbstractLifestyleManager
		where M1 : ILifestyleManager, new()
		where M2 : ILifestyleManager, new()
	{
		protected readonly M1 lifestyle1 = new M1();
		protected readonly M2 lifestyle2 = new M2();

		public override void Dispose()
		{
			lifestyle1.Dispose();
			lifestyle2.Dispose();
		}

		public override void Init(IComponentActivator componentActivator, IKernel kernel, ComponentModel model)
		{
			lifestyle1.Init(componentActivator, kernel, model);
			lifestyle2.Init(componentActivator, kernel, model);
		}

		public override bool Release(object instance)
		{
			var r1 = lifestyle1.Release(instance);
			var r2 = lifestyle2.Release(instance);
			return r1 || r2;
		}

		public abstract override object Resolve(CreationContext context, IReleasePolicy releasePolicy);
	}
}