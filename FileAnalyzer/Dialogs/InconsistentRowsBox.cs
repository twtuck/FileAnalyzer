using System.Collections.Generic;
using System.Windows.Forms;

namespace FileAnalyzer
{
    partial class InconsistentRowsBox : Form
    {
        public InconsistentRowsBox(IEnumerable<InconsistentRow> inconsistentRows, string filename)
        {
            InitializeComponent();
            this.Text = $"Inconsistent Rows - \"{filename}\"";
            foreach (var inconsistentRow in inconsistentRows)
            {
                listViewInconsistentRows.Items.Add(inconsistentRow.RowId.ToString())
                    .SubItems.Add(inconsistentRow.Value);
            }
        }
    }
}
