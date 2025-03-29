namespace Sudoku
{
    public partial class Form1 : Form
    {

        int[,] rowCombinations = new int[4, 4]
        {
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
        string[] puzzle1demo = new string[16]
        {
            "2", "", "", "",
            "",  "1", "3",  "",
            "3",  "",  "", "1",
            "",  "2", "4",  ""
        };
        public Form1()
        {
            InitializeComponent();
            SetPuzzle(puzzle1demo);
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
                textBoxlist[i].Enabled = puzzle[i] == "";
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
                            CheckWinner();
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
                            CheckWinner();
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
                            CheckWinner();
                            return;
                        }
                    }
                }
            }
            SetTextBoxesEnabled(puzzle1demo);
            CheckWinner();
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
        // parametrad mowodebuli textBoxes garda yvela
        // textbox unda iyos disabled
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
            CheckWinner();
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            CheckWinner();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Winner.Visible = false;

            foreach (Control control in this.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.TextChanged += textBox_TextChanged;
                }
            }
        }

        private void CheckWinner()
        {
            bool allFilled = true;
            bool allNotRed = true;

            foreach (Control control in this.Controls)
            {
                if (control is TextBox textBox)
                {

                    if (string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        allFilled = false;
                    }

                    if (textBox.BackColor == Color.Red)
                    {
                        allNotRed = false;
                    }
                }
            }
            if (allFilled && allNotRed)
            {
                Winner.Visible = true;
            }

        }
    }
}