using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sample
{
	public class DataGridViewProgressColumn : DataGridViewImageColumn
	{
		public static Color _ProgressBarColor;

		public override DataGridViewCell CellTemplate
		{
			get
			{
				return base.CellTemplate;
			}
			set
			{
				if ((value == null ? false : !value.GetType().IsAssignableFrom(typeof(DataGridViewProgressCell))))
				{
					throw new InvalidCastException("Must be a DataGridViewProgressCell");
				}
				base.CellTemplate = value;
			}
		}

		private DataGridViewProgressCell ProgressBarCellTemplate
		{
			get
			{
				return (DataGridViewProgressCell)this.CellTemplate;
			}
		}

		[Browsable(true)]
		public Color ProgressBarColor
		{
			get
			{
				if (this.ProgressBarCellTemplate == null)
				{
					throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
				}
				return this.ProgressBarCellTemplate.ProgressBarColor;
			}
			set
			{
				if (this.ProgressBarCellTemplate == null)
				{
					throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
				}
				this.ProgressBarCellTemplate.ProgressBarColor = value;
				if (base.DataGridView != null)
				{
					DataGridViewRowCollection dataGridViewRows = base.DataGridView.Rows;
					int rowCount = dataGridViewRows.Count;
					for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
					{
						DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
						DataGridViewProgressCell dataGridViewCell = dataGridViewRow.Cells[base.Index] as DataGridViewProgressCell;
						if (dataGridViewCell != null)
						{
							dataGridViewCell.SetProgressBarColor(rowIndex, value);
						}
					}
					base.DataGridView.InvalidateColumn(base.Index);
				}
			}
		}

		public DataGridViewProgressColumn()
		{
			this.CellTemplate = new DataGridViewProgressCell();
		}
	}
}