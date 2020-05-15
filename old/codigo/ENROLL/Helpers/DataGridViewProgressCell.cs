using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ENROLL.Helpers.ComplementoDatagrid
{
	internal class DataGridViewProgressCell : DataGridViewImageCell
	{
		private static Image emptyImage;

		private static Color _ProgressBarColor;

		public Color ProgressBarColor
		{
			get
			{
				return DataGridViewProgressCell._ProgressBarColor;
			}
			set
			{
				DataGridViewProgressCell._ProgressBarColor = value;
			}
		}

		static DataGridViewProgressCell()
		{
			DataGridViewProgressCell.emptyImage = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
		}

		public DataGridViewProgressCell()
		{
			this.ValueType = typeof(int);
		}

		public override object Clone()
		{
			DataGridViewProgressCell dataGridViewCell = base.Clone() as DataGridViewProgressCell;
			if (dataGridViewCell != null)
			{
				dataGridViewCell.ProgressBarColor = this.ProgressBarColor;
			}
			return dataGridViewCell;
		}

		protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
		{
			return DataGridViewProgressCell.emptyImage;
		}

		protected override void Paint(Graphics g, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			if ((Convert.ToInt16(value) == 0 ? true : value == null))
			{
				value = 0;
			}
			int progressVal = Convert.ToInt32(value);
			float percentage = (float)progressVal / 100f;
			Brush backColorBrush = new SolidBrush(cellStyle.BackColor);
			Brush foreColorBrush = new SolidBrush(cellStyle.ForeColor);
			base.Paint(g, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts & (DataGridViewPaintParts.Background | DataGridViewPaintParts.Border | DataGridViewPaintParts.ContentBackground | DataGridViewPaintParts.ErrorIcon | DataGridViewPaintParts.Focus | DataGridViewPaintParts.SelectionBackground));
			float posX = (float)cellBounds.X;
			float posY = (float)cellBounds.Y;
			System.Drawing.Size size = TextRenderer.MeasureText(string.Concat(progressVal.ToString(), "%"), cellStyle.Font);
			float textWidth = (float)size.Width;
			size = TextRenderer.MeasureText(string.Concat(progressVal.ToString(), "%"), cellStyle.Font);
			float textHeight = (float)size.Height;
			DataGridViewContentAlignment alignment = cellStyle.Alignment;
			if (alignment <= DataGridViewContentAlignment.MiddleCenter)
			{
				switch (alignment)
				{
					case DataGridViewContentAlignment.TopLeft:
					{
						posX = (float)cellBounds.X;
						posY = (float)cellBounds.Y;
						break;
					}
					case DataGridViewContentAlignment.TopCenter:
					{
						posX = (float)(cellBounds.X + cellBounds.Width / 2) - textWidth / 2f;
						posY = (float)cellBounds.Y;
						break;
					}
					case DataGridViewContentAlignment.TopLeft | DataGridViewContentAlignment.TopCenter:
					{
						break;
					}
					case DataGridViewContentAlignment.TopRight:
					{
						posX = (float)(cellBounds.X + cellBounds.Width) - textWidth;
						posY = (float)cellBounds.Y;
						break;
					}
					default:
					{
						if (alignment == DataGridViewContentAlignment.MiddleLeft)
						{
							posX = (float)cellBounds.X;
							posY = (float)(cellBounds.Y + cellBounds.Height / 2) - textHeight / 2f;
							break;
						}
						else if (alignment == DataGridViewContentAlignment.MiddleCenter)
						{
							posX = (float)(cellBounds.X + cellBounds.Width / 2) - textWidth / 2f;
							posY = (float)(cellBounds.Y + cellBounds.Height / 2) - textHeight / 2f;
							break;
						}
						else
						{
							break;
						}
					}
				}
			}
			else if (alignment <= DataGridViewContentAlignment.BottomLeft)
			{
				if (alignment == DataGridViewContentAlignment.MiddleRight)
				{
					posX = (float)(cellBounds.X + cellBounds.Width) - textWidth;
					posY = (float)(cellBounds.Y + cellBounds.Height / 2) - textHeight / 2f;
				}
				else if (alignment == DataGridViewContentAlignment.BottomLeft)
				{
					posX = (float)cellBounds.X;
					posY = (float)(cellBounds.Y + cellBounds.Height) - textHeight;
				}
			}
			else if (alignment == DataGridViewContentAlignment.BottomCenter)
			{
				posX = (float)(cellBounds.X + cellBounds.Width / 2) - textWidth / 2f;
				posY = (float)(cellBounds.Y + cellBounds.Height) - textHeight;
			}
			else if (alignment == DataGridViewContentAlignment.BottomRight)
			{
				posX = (float)(cellBounds.X + cellBounds.Width) - textWidth;
				posY = (float)(cellBounds.Y + cellBounds.Height) - textHeight;
			}
			if ((double)percentage >= 0)
			{
				g.FillRectangle(new SolidBrush(DataGridViewProgressCell._ProgressBarColor), cellBounds.X + 2, cellBounds.Y + 2, Convert.ToInt32((double)(percentage * (float)cellBounds.Width) * 0.8), cellBounds.Height / 1 - 5);
				g.DrawString(string.Concat(progressVal.ToString(), "%"), cellStyle.Font, foreColorBrush, posX, posY);
			}
			else if (base.DataGridView.CurrentRow.Index != rowIndex)
			{
				g.DrawString(string.Concat(progressVal.ToString(), "%"), cellStyle.Font, foreColorBrush, posX, posY);
			}
			else
			{
				g.DrawString(string.Concat(progressVal.ToString(), "%"), cellStyle.Font, new SolidBrush(cellStyle.SelectionForeColor), posX, posX);
			}
		}

		internal void SetProgressBarColor(int rowIndex, Color value)
		{
			this.ProgressBarColor = value;
		}
	}
}