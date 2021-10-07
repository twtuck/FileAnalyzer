using System.Windows.Forms;

namespace FileAnalyzer
{
    partial class StatisticsBox : Form
    {
        public StatisticsBox(IStatistics statistics, string filename)
        {
            InitializeComponent();
            this.Text = $"\"{filename}\"";
            textBoxLinesCount.Text = $@"{statistics.NumberOfLines:n0}"; 
            textBoxWordsCount.Text = $@"{statistics.NumberOfWords:n0}"; 
            textBoxCharsNoSpaceCount.Text = $@"{statistics.NumberOfCharsWithoutSpace:n0}";
            textBoxCharsWithSpaceCount.Text = $@"{statistics.NumberOfCharsWithSpace:n0}";
            textBoxProcessingTime.Text = statistics.ProcessingTime.ToString(@"hh\:mm\:ss\.fff");
        }
    }
}
