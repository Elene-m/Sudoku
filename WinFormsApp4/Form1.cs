using WinFormsApp2;
namespace Sudoku
{
    public partial class Form1 : Form
    {
        int[,] rowCombinations = new int[4, 4]{
            { 1,2,3,4 }, // 0
            { 5,6,7,8 },
            { 9,10,11,12 }, // 12 - [2,3]  i = 2  j = 3 
            { 13,14,15,16 }
        };
        int[,] colCombinations = new int[4, 4]
        {
            { 1,5,9,13 }, // 5 - [0,1]
            { 2,6,10,14 },
            { 3,7,11,15 }, // indexRow = 2, 
            { 4,8,12,16 }
        };
        int[,] groupsCombinations = new int[4, 4]
        {
            { 1,2,5,6 },
            { 3,4,7,8 }, // 1
            { 9,10,13,14 },
            { 11,12,15,16 }
        };
        string[] puzzle = new string[16];
        
        public Form1()
        {
            InitializeComponent();
            puzzle = Puzzle.GetPuzzle();
            SetPuzzle(puzzle);
        }

        public void SetPuzzle(string[] puzzle)
        {
            List<TextBox> textBoxlist = new List<TextBox>();
            foreach (Control control in this.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBoxlist.Add(textBox);
                }
            }
            for (int i = 0; i < textBoxlist.Count; i++)
            {
                var enabled = puzzle[i] == "";
                textBoxlist[i].Enabled = enabled;
                textBoxlist[i].Text = puzzle[i];
            }
        }

        private void OnChanged(object sender, EventArgs e)
        {
            SetTextBoxDefaultColor();

            TextBox textBox = sender as TextBox;
            if (!textBox.Enabled) return;

            int textBoxNum = int.Parse(textBox.Name.Substring(7));

            bool alreadyUsed = false;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (rowCombinations[i, j] == textBoxNum)
                    {
                        alreadyUsed = CheckCombination(rowCombinations, i, textBox);
                        if (alreadyUsed)
                        {
                            textBox.ForeColor = Color.Red;
                            SetTextBoxesDisabled(textBox);
                            return;
                        }
                    }
                }
            }
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (colCombinations[i, j] == textBoxNum)
                    {
                        alreadyUsed = CheckCombination(colCombinations, i, textBox);  //4
                        if (alreadyUsed)
                        {
                            textBox.ForeColor = Color.Red;
                            SetTextBoxesDisabled(textBox);
                            return;
                        }
                    }
                }
            }
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (groupsCombinations[i, j] == textBoxNum)
                    {
                        alreadyUsed = CheckCombination(groupsCombinations, i, textBox);
                        if (alreadyUsed)
                        {
                            textBox.ForeColor = Color.Red;
                            SetTextBoxesDisabled(textBox);
                            return;
                        }
                    }
                }
            }
            SetTextBoxesEnabled(puzzle);
            GameOverCheck();
        }

        private bool CheckCombination(int[,] combinations, int rowIndex, TextBox textBox)
        {
            int count = 0;
            int[] tocheckArray = new int[4];

            for (int i = 0; i < 4; i++)
            {
                tocheckArray[i] = combinations[rowIndex, i];
            }
            for (int i = 0; i < 4; i++)
            {
                string textBoxName = "textBox" + tocheckArray[i].ToString();
                TextBox currentTextBox = this.Controls[textBoxName] as TextBox;
                if (currentTextBox.Text == textBox.Text) count++;
            }
            return count > 1;
        }

        private void SetTextBoxDefaultColor()
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox textBox)
                {
                    control.ForeColor = Color.Black;
                }
            }
        }

        private void SetTextBoxesDisabled(TextBox textBox)
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox textBox1)
                {
                    if (textBox1 != textBox)
                    {
                        textBox1.Enabled = false;
                    }
                }
            }
        }

        private void GameOverCheck()
        {
            bool allTextBoxesFilled = true;
            foreach (Control control in this.Controls)
            {
                if (control is TextBox textBoxControl)
                {
                    if (textBoxControl.Text == "")
                    {
                        allTextBoxesFilled = false;
                        break;
                    }
                }
            }
            
            if (allTextBoxesFilled)
            {
                foreach (Control control in this.Controls)
                {
                    if (control is TextBox textBoxControl)
                    {
                        textBoxControl.Enabled = false;
                    }
                }
                Winner.Text = "Congratulations!";
                Winner.Visible = true;
            }
        }

        private void SetTextBoxesEnabled(string[] puzzle)
        {
            List<TextBox> textBoxlist = new List<TextBox>();

            foreach (Control control in this.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBoxlist.Add(textBox);
                }
            }

            for (int i = 0; i < textBoxlist.Count; i++)
            {
                textBoxlist[i].Enabled = puzzle[i] == "";
            }
        }

        private void Restartbutton_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
