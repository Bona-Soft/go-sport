using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.BaseApplication.Framework.Helpers.TypesExt
{
	public static class ListExt
	{
		public static void Replace<T>(this List<T> list, T oldValue, T newValue)
		{
			int index = list.FindIndex(ind => ind.Equals(oldValue));
			if(index >= 0)
			{
				list[index] = newValue;
			}	
		}

		public static void TryAdd<T>(this List<T> list, T value)
		{
			int index = list.FindIndex(ind => ind.Equals(value));
			if (index < 0)
			{
				list.Add(value);
			}
		}
	}

	public interface IList<T1,T2> : IList<Tuple<T1, T2>>
	{
		void Add(T1 item1, T2 item2);
		int IndexOf(T1 item1);
		int IndexOf(T2 item2);
		bool Contain(T1 item1);
		bool Contain(T2 item2);
		bool Remove(T1 item1, T2 item2);
		bool Remove(T1 item1);
		bool Remove(T2 item2);
	}
	public class List<T1,T2> : List<Tuple<T1, T2>>, IList<T1,T2>
	{
		public void Add(T1 item1, T2 item2)
		{
			base.Add(Tuple.Create(item1, item2));
		}


		public int IndexOf(T1 item1)
		{
			foreach (Tuple<T1, T2> t in this)
			{
				if (item1.Equals(t.Item1))
				{
					return this.IndexOf(t);
				}
			}
			return -1;
		}

		public int IndexOf(T2 item2)
		{
			foreach (Tuple<T1, T2> t in this)
			{
				if (item2.Equals(t.Item2))
				{
					return this.IndexOf(t);
				}
			}
			return -1;
		}

		public bool Contain(T1 item1)
		{
			foreach (Tuple<T1, T2> t in this)
			{
				if (item1.Equals(t.Item1))
				{
					return true;
				}
			}
			return false;
		}

		public bool Contain(T2 item2)
		{
			foreach (Tuple<T1, T2> t in this)
			{
				if (item2.Equals(t.Item2))
				{
					return true;
				}
			}
			return false;
		}

		public bool Remove(T1 item1, T2 item2)
		{
			return base.Remove(Tuple.Create(item1, item2));
		}

		public bool Remove(T1 item1)
		{
			bool result = false;
			for(int i = 0; i < this.Count; i++)
			{
				if (this[i].Item1.Equals(item1))
				{
					result = true;
					Remove(this[i].Item1, this[i].Item2);
				}
			}
			return result;
		}

		public bool Remove(T2 item2)
		{
			bool result = false;
			for (int i = 0; i < this.Count; i++)
			{
				if (this[i].Item2.Equals(item2))
				{
					result = true;
					Remove(this[i].Item1, this[i].Item2);
				}
			}
			return result;
		}
	}

	public interface IList<T1, T2, T3> : IList<Tuple<T1, T2, T3>>
	{
		void Add(T1 item1, T2 item2, T3 item3);
		bool Remove(T1 item1, T2 item2, T3 item3);
		int IndexOf(T1 item1);
		int IndexOf(T2 item2);
		int IndexOf(T3 item3);
		bool Contain(T1 item1);
		bool Contain(T2 item2);
		bool Contain(T3 item3);
		bool Remove(T1 item1);
		bool Remove(T2 item2);
		bool Remove(T3 item3);
	}

	public class List<T1, T2, T3> : List<Tuple<T1, T2, T3>>
	{
		
		public void Add(T1 item1, T2 item2, T3 item3)
		{
			base.Add(Tuple.Create(item1, item2, item3));
		}

		public bool Remove(T1 item1, T2 item2, T3 item3)
		{
			return base.Remove(Tuple.Create(item1, item2, item3));
		}

		public int IndexOf(T1 item1)
		{
			foreach (Tuple<T1, T2, T3> t in this)
			{
				if (item1.Equals(t.Item1))
				{
					return this.IndexOf(t);
				}
			}
			return -1;
		}

		public int IndexOf(T2 item2)
		{
			foreach (Tuple<T1, T2, T3> t in this)
			{
				if (item2.Equals(t.Item2))
				{
					return this.IndexOf(t);
				}
			}
			return -1;
		}

		public int IndexOf(T3 item3)
		{
			foreach (Tuple<T1, T2, T3> t in this)
			{
				if (item3.Equals(t.Item3))
				{
					return this.IndexOf(t);
				}
			}
			return -1;
		}

		public bool Contain(T1 item1)
		{
			foreach(Tuple<T1,T2,T3> t in this)
			{
				if (item1.Equals(t.Item1))
				{
					return true;
				}
			}
			return false;
		}

		public bool Contain(T2 item2)
		{
			foreach (Tuple<T1, T2, T3> t in this)
			{
				if (item2.Equals(t.Item2))
				{
					return true;
				}
			}
			return false;
		}

		public bool Contain(T3 item3)
		{
			foreach (Tuple<T1, T2, T3> t in this)
			{
				if (item3.Equals(t.Item3))
				{
					return true;
				}
			}
			return false;
		}

		public bool Remove(T1 item1)
		{
			bool result = false;
			for (int i = 0; i < this.Count; i++)
			{
				if (this[i].Item1.Equals(item1))
				{
					result = true;
					Remove(this[i].Item1, this[i].Item2, this[i].Item3);
				}
			}
			return result;
		}

		public bool Remove(T2 item2)
		{
			bool result = false;
			for (int i = 0; i < this.Count; i++)
			{
				if (this[i].Item2.Equals(item2))
				{
					result = true;
					Remove(this[i].Item1, this[i].Item2, this[i].Item3);
				}
			}
			return result;
		}

		public bool Remove(T3 item3)
		{
			bool result = false;
			for (int i = 0; i < this.Count; i++)
			{
				if (this[i].Item3.Equals(item3))
				{
					result = true;
					Remove(this[i].Item1, this[i].Item2, this[i].Item3);
				}
			}
			return result;
		}
	}
}
