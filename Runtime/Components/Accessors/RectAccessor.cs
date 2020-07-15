// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using UnityEngine;
using UnityEngine.Events;

namespace Zor.SimpleBlackboard.Components.Accessors
{
	[AddComponentMenu(AddComponentConstants.AccessorsFolder + "Rect Accessor")]
	public sealed class RectAccessor : StructAccessor<Rect, RectAccessor.RectEvent>
	{
		[Serializable]
		public sealed class RectEvent : UnityEvent<Rect>
		{
		}
	}
}
