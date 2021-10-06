using System.Windows.Forms;

namespace FileAnalyzer
{
    partial class StatisticsBox : Form
    {
        public StatisticsBox(IStatistics statistics, string filename)
        {
            InitializeComponent();
            this.Text = $"\"{filename}\"";
            textBoxLinesCount.Text = statistics.NumberOfLines.ToString();
            textBoxWordsCount.Text = statistics.NumberOfWords.ToString();
            textBoxCharsNoSpaceCount.Text = statistics.NumberOfCharsWithoutSpace.ToString();
            textBoxCharsWithSpaceCount.Text = statistics.NumberOfCharsWithSpace.ToString();
        }
    }
}
