// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine.Events;
using UnityEngine.Scripting;
using Zor.SimpleBlackboard.Core;

namespace Zor.SimpleBlackboard.Components.Accessors
{
	/// <summary>
	/// <para>Accessor for a property of struct type <typeparamref name="T"/> in a blackboard.</para>
	/// <para>Inherit this to implement a new type support.</para>
	/// </summary>
	/// <typeparam name="T">Value type.</typeparam>
	/// <typeparam name="TEvent">Unity event which is invoked on <see cref="Accessor{T,TEvent}.Flush"/>.</typeparam>
	public abstract class StructAccessor<T, TEvent> : Accessor<T, TEvent> where T : struct where TEvent : UnityEvent<T>
	{
		[Preserve]
		public sealed override T value
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get
			{
				Blackboard blackboard = blackboardPropertyReference.blackboardContainer.blackboard;

#if SIMPLE_BLACKBOARD_MULTITHREADING
				lock (blackboard)
#endif
				{
					blackboard.TryGetStructValue(propertyName, out T answer);
					return answer;
				}
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				Blackboard blackboard = blackboardPropertyReference.blackboardContainer.blackboard;

#if SIMPLE_BLACKBOARD_MULTITHREADING
				lock (blackboard)
#endif
				{
					blackboard.SetStructValue(propertyName, value);
				}
			}
		}
	}
}
