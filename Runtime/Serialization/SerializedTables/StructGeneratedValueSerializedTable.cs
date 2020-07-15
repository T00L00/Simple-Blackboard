// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using System.Collections.Generic;
using UnityEngine;
using Zor.SimpleBlackboard.Core;
using Zor.SimpleBlackboard.Helpers;

namespace Zor.SimpleBlackboard.Serialization
{
	/// <summary>
	/// <para>Serialized table that has set keys and generates struct values at runtime.</para>
	/// <para>Inherit this class and override <see cref="GetValue"/> to get this functionality.</para>
	/// </summary>
	/// <typeparam name="T">Struct type of the generated value.</typeparam>
	public abstract class StructGeneratedValueSerializedTable<T> : GeneratedValueSerializedTable_Base where T : struct
	{
#pragma warning disable CS0649
		[SerializeField] private string[] m_Keys;
#pragma warning restore CS0649

		/// <inheritdoc/>
		public sealed override Type valueType => typeof(T);

		/// <inheritdoc/>
		public sealed override void Apply(Blackboard blackboard)
		{
			for (int i = 0, count = m_Keys.Length; i < count; ++i)
			{
				blackboard.SetStructValue(new BlackboardPropertyName(m_Keys[i]), GetValue());
			}
		}

		/// <inheritdoc/>
		public sealed override void GetKeys(List<(string, Type)> keys)
		{
			int count = m_Keys.Length;
			ListHelper.EnsureCapacity(keys, count);

			for (int i = 0; i < count; ++i)
			{
				keys.Add((m_Keys[i], valueType));
			}
		}

		/// <summary>
		/// Gets a generated value.
		/// </summary>
		/// <returns>Generated value.</returns>
		protected abstract T GetValue();
	}
}
