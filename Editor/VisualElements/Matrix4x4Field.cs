﻿// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Zor.SimpleBlackboard.VisualElements
{
	public sealed class Matrix4x4Field : BaseField<Matrix4x4>
	{
		private static readonly EventCallback<ChangeEvent<Vector4>, RowInfo> s_onRowChanged = (c, info) =>
		{
			Matrix4x4Field field = info.field;
			Matrix4x4 matrix = field.value;
			matrix.SetRow(info.index, c.newValue);
			field.value = matrix;
		};

		private readonly Vector4Field[] m_rows = new Vector4Field[4];

		public Matrix4x4Field() : this(null)
		{
		}

		public Matrix4x4Field([CanBeNull] string label) : this(label, new VisualElement())
		{
		}

		private Matrix4x4Field([CanBeNull] string label, [NotNull] VisualElement visualInput) : base(label, visualInput)
		{
			for (int i = 0; i < 4; ++i)
			{
				Vector4Field row = m_rows[i] = new Vector4Field();
				row.RegisterCallback(s_onRowChanged, new RowInfo {field = this, index = i});
				visualInput.Add(row);
			}
		}

		public override void SetValueWithoutNotify(Matrix4x4 newValue)
		{
			base.SetValueWithoutNotify(newValue);

			for (int i = 0; i < 4; ++i)
			{
				m_rows[i].SetValueWithoutNotify(newValue.GetRow(i));
			}
		}

		private sealed class RowInfo
		{
			public Matrix4x4Field field;
			public int index;
		}
	}
}
