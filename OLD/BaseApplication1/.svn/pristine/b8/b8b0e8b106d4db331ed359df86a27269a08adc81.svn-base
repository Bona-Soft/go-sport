using Castle.MicroKernel.Context;
using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using MYB.BaseApplication.Infrastructure.Windsor.LifeStylesCustom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MYB.BaseApplication.Infrastructure.Windsor
{
	#region " ParentLifestyleManager Classes "

	public class ParentLifestyleManager<Parent> : IParentManager<Parent>
	{
		private Dictionary<object, object> _Objects { get; set; }

		public Dictionary<object, object> Objects
		{
			get
			{
				if (_Objects == null)
					_Objects = new Dictionary<object, object>();
				return _Objects;
			}
			set
			{
				_Objects = value;
			}
		}
	}

	public class ParentLifestyleManager<Parent, Parent2> : IParentManager<Parent, Parent2>
	{
		private Dictionary<object, object> _Objects { get; set; }

		public Dictionary<object, object> Objects
		{
			get
			{
				if (_Objects == null)
					_Objects = new Dictionary<object, object>();
				return _Objects;
			}
			set
			{
				_Objects = value;
			}
		}
	}

	public class ParentLifestyleManager<Parent, Parent2, Parent3> : IParentManager<Parent, Parent2, Parent3>
	{
		private Dictionary<object, object> _Objects { get; set; }

		public Dictionary<object, object> Objects
		{
			get
			{
				if (_Objects == null)
					_Objects = new Dictionary<object, object>();
				return _Objects;
			}
			set
			{
				_Objects = value;
			}
		}
	}

	public class ParentLifestyleManager<Parent, Parent2, Parent3, Parent4> : IParentManager<Parent, Parent2, Parent3, Parent4>
	{
		private Dictionary<object, object> _Objects { get; set; }

		public Dictionary<object, object> Objects
		{
			get
			{
				if (_Objects == null)
					_Objects = new Dictionary<object, object>();
				return _Objects;
			}
			set
			{
				_Objects = value;
			}
		}
	}

	public class ParentLifestyleManager<Parent, Parent2, Parent3, Parent4, Parent5> : IParentManager<Parent, Parent2, Parent3, Parent4, Parent5>
	{
		private Dictionary<object, object> _Objects { get; set; }

		public Dictionary<object, object> Objects
		{
			get
			{
				if (_Objects == null)
					_Objects = new Dictionary<object, object>();
				return _Objects;
			}
			set
			{
				_Objects = value;
			}
		}
	}

	public class ParentLifestyleManager<Parent, Parent2, Parent3, Parent4, Parent5, Parent6> : IParentManager<Parent, Parent2, Parent3, Parent4, Parent5, Parent6>
	{
		private Dictionary<object, object> _Objects { get; set; }

		public Dictionary<object, object> Objects
		{
			get
			{
				if (_Objects == null)
					_Objects = new Dictionary<object, object>();
				return _Objects;
			}
			set
			{
				_Objects = value;
			}
		}
	}

	#endregion " ParentLifestyleManager Classes "

	#region " ParentLifeStyleManager Interfaces "

	public interface IParentManager<Parent> : ILifeStyle
	{
		Dictionary<object, object> Objects { get; set; }
	}

	public interface IParentManager<Parent, Parent2> : ILifeStyle
	{
		Dictionary<object, object> Objects { get; set; }
	}

	public interface IParentManager<Parent, Parent2, Parent3> : ILifeStyle
	{
		Dictionary<object, object> Objects { get; set; }
	}

	public interface IParentManager<Parent, Parent2, Parent3, Parent4> : ILifeStyle
	{
		Dictionary<object, object> Objects { get; set; }
	}

	public interface IParentManager<Parent, Parent2, Parent3, Parent4, Parent5> : ILifeStyle
	{
		Dictionary<object, object> Objects { get; set; }
	}

	public interface IParentManager<Parent, Parent2, Parent3, Parent4, Parent5, Parent6> : ILifeStyle
	{
		Dictionary<object, object> Objects { get; set; }
	}

	#endregion " ParentLifeStyleManager Interfaces "

	public class ParentLifestyleManager
	{
		public static bool HasParent(CreationContext context)
		{
			List<Type> Types = context.RequestedType.ParentTypes().Distinct().ToList();
			Types.RemoveAll(item => item == typeof(ILifeStyle) || !(item.GetInterfaces().Count() == 1 && item.GetInterface("ILifeStyle") != null));
			return Types.Count() > 1;
		}

		public static Dictionary<T, object> GetObjects<T>(CreationContext context)
		{
			List<Type> typeList = context.RequestedType.ParentTypes().Distinct().ToList();
			List<Type> endTypeList = new List<Type>();
			Type firstType = typeList.ElementAt(0);
			MethodInfo method;
			MethodInfo generic;
			dynamic parentManager = null;
			Dictionary<T, object> resultObjects;

			typeList.RemoveAt(0);
			typeList.RemoveAll(item => item == typeof(ILifeStyle) || !(item.GetInterfaces().Count() == 1 && item.GetInterface("ILifeStyle") != null));

			switch (typeList.Count)
			{
				case 1: method = typeof(ParentLifestyleManager).GetMethod("ResolveParent1"); break;
				case 2: method = typeof(ParentLifestyleManager).GetMethod("ResolveParent2"); break;
				case 3: method = typeof(ParentLifestyleManager).GetMethod("ResolveParent3"); break;
				case 4: method = typeof(ParentLifestyleManager).GetMethod("ResolveParent4"); break;
				case 5: method = typeof(ParentLifestyleManager).GetMethod("ResolveParent5"); break;
				case 6: method = typeof(ParentLifestyleManager).GetMethod("ResolveParent6"); break;
				default: method = null; break;
			}
			if (method != null)
			{
				int implementationID = LifestylesMethods.GetContextKey<int>(context, "implementationID");
				long userID = LifestylesMethods.GetContextKey<int>(context, "userID");
				object[] constructor = LifestylesMethods.GetAllContextKey(context);
				Dictionary<string, object> constructParameters = new Dictionary<string, object>();
				if (implementationID != 0)
					constructParameters.Add("implementationID", implementationID);
				if (userID != 0)
					constructParameters.Add("userID", userID);
				if (constructor != null && constructor.Count() > 0)
					constructParameters.Add("constructor", constructor);
				generic = method.MakeGenericMethod(typeList.ToArray());
				object[] argumentParent = { constructParameters };
				parentManager = generic.Invoke(null, argumentParent);
			}

			endTypeList.Add(typeof(T));
			endTypeList.Add(firstType);
			method = typeof(ParentLifestyleManager).GetMethod("GetObjectsFromParentManager");
			generic = method.MakeGenericMethod(endTypeList.ToArray());
			object[] argument = { parentManager };
			resultObjects = (Dictionary<T, object>)generic.Invoke(null, argument);

			return resultObjects;
		}

		public static Dictionary<TResult, object> GetObjectsFromParentManager<TResult, TFirstLifeStyle>(dynamic parentManager)
		{
			if (parentManager == null)
				return null;
			if (!parentManager.Objects.ContainsKey(typeof(TFirstLifeStyle)))
				parentManager.Objects[typeof(TFirstLifeStyle)] = new Dictionary<TResult, object>();
			return parentManager.Objects[typeof(TFirstLifeStyle)];
		}

		#region " Resolve Parent Methods "

		public static IParentManager<Parent> ResolveParent1<Parent>(Dictionary<string, object> arguments)
		{
			return ContainerManager.WindsorContainer.Resolve<IParentManager<Parent>>(arguments);
		}

		public static IParentManager<Parent1, Parent2> ResolveParent2<Parent1, Parent2>(Dictionary<string, object> arguments)
		{
			return ContainerManager.WindsorContainer.Resolve<IParentManager<Parent1, Parent2>>(arguments);
		}

		public static IParentManager<Parent1, Parent2, Parent3> ResolveParent3<Parent1, Parent2, Parent3>(Dictionary<string, object> arguments)
		{
			return ContainerManager.WindsorContainer.Resolve<IParentManager<Parent1, Parent2, Parent3>>(arguments);
		}

		public static IParentManager<Parent1, Parent2, Parent3, Parent4> ResolveParent4<Parent1, Parent2, Parent3, Parent4>(Dictionary<string, object> arguments)
		{
			return ContainerManager.WindsorContainer.Resolve<IParentManager<Parent1, Parent2, Parent3, Parent4>>(arguments);
		}

		public static IParentManager<Parent1, Parent2, Parent3, Parent4, Parent5> ResolveParent5<Parent1, Parent2, Parent3, Parent4, Parent5>(Dictionary<string, object> arguments)
		{
			return ContainerManager.WindsorContainer.Resolve<IParentManager<Parent1, Parent2, Parent3, Parent4, Parent5>>(arguments);
		}

		public static IParentManager<Parent1, Parent2, Parent3, Parent4, Parent5, Parent6> ResolveParent6<Parent1, Parent2, Parent3, Parent4, Parent5, Parent6>(Dictionary<string, object> arguments)
		{
			return ContainerManager.WindsorContainer.Resolve<IParentManager<Parent1, Parent2, Parent3, Parent4, Parent5, Parent6>>(arguments);
		}

		#endregion " Resolve Parent Methods "
	}
}