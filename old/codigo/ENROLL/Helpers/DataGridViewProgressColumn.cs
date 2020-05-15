using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ENROLL.Helpers.ComplementoDatagrid
{
    public class DataGridViewProgressColumn : DataGridViewImageColumn
    {
        public static Color _ProgressBarColor;

        public DataGridViewProgressColumn()
        {
            this.CellTemplate = (DataGridViewCell)new DataGridViewProgressCell();
        }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                if (value != null && !value.GetType().IsAssignableFrom(typeof(DataGridViewProgressCell)))
                    throw new InvalidCastException("Must be a DataGridViewProgressCell");
                base.CellTemplate = value;
            }
        }

       
        public Color ProgressBarColor
        {
            get
            {
                if (this.ProgressBarCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                return this.ProgressBarCellTemplate.ProgressBarColor;
            }
            set
            {
                if (this.ProgressBarCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                this.ProgressBarCellTemplate.ProgressBarColor = value;
                if (this.DataGridView == null)
                    return;
                DataGridViewRowCollection rows = this.DataGridView.Rows;
                int count = rows.Count;
                for (int rowIndex = 0; rowIndex < count; ++rowIndex)
                {
                    if (rows.SharedRow(rowIndex).Cells[this.Index] is DataGridViewProgressCell cell)
                        cell.SetProgressBarColor(rowIndex, value);
                }
                this.DataGridView.InvalidateColumn(this.Index);
            }
        }

        private DataGridViewProgressCell ProgressBarCellTemplate
        {
            get
            {
                return (DataGridViewProgressCell)this.CellTemplate;
            }
        }
    }
}