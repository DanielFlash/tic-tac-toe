using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using NeuroLibrary;

namespace tic_tac_toe
{
    public partial class MainWindow : Form
    {
        Manager manager = new Manager();
        public Button[] Buttons;
        Color color;
        public bool IsGame;

        public MainWindow()
        {
            InitializeComponent();
            Buttons = new Button[] { button1, button2, button3, button4, button5, button6, button7, button8, button9};
            color = button1.BackColor;
            IsGame = false;
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }

        public void CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                tableLayoutPanel1.Enabled = false;
                button10.Enabled = true;
            }
            else if (!((RadioButton)sender).Checked)
            {
                tableLayoutPanel1.Enabled = true;
                button10.Enabled = false;
            }
        }

        public async void StartClick(object sender, EventArgs e)
        {
            IsGame = true;
            groupBox1.Enabled = false;
            await StartClickAsync(sender, e);
        }

        public async Task StartClickAsync(object sender, EventArgs e)
        {
            int[,] field = new int[,] { { 0, 0, 0}, { 0, 0, 0}, { 0, 0, 0} };
            await NeuronStep(field);
        }

        public async void ButtonClick(object sender, EventArgs e)
        {
            if (!IsGame)
            {
                IsGame = true;
                groupBox1.Enabled = false;
            }
            await ButtonClickAsync(sender, e);
        }

        public async Task ButtonClickAsync(object sender, EventArgs e)
        {
            await Task.Run(async () =>
            {
                try
                {
                    ((Button)sender).BackColor = Color.Green;
                    ((Button)sender).Enabled = false;
                    await NeuronStep(await CheckWin());
                }
                catch (Exception ex) when (ex.Message == "Game over")
                {
                    MessageBox.Show("Amazing, you win!");
                    groupBox1.Enabled = true;
                    if(radioButton2.Checked)
                    {
                        tableLayoutPanel1.Enabled = false;
                        button10.Enabled = true;
                    }
                    else if (!radioButton2.Checked)
                    {
                        tableLayoutPanel1.Enabled = true;
                        button10.Enabled = false;
                    }
                    for(int i = 0; i < 9; i++)
                    {
                        Buttons[i].BackColor = color;
                        Buttons[i].Enabled = true;
                    }
                }
                catch (Exception ex) when (ex.Message == "Draw")
                {
                    MessageBox.Show("Hmmm, lucky u!");
                    groupBox1.Enabled = true;
                    if (radioButton2.Checked)
                    {
                        tableLayoutPanel1.Enabled = false;
                        button10.Enabled = true;
                    }
                    else if (!radioButton2.Checked)
                    {
                        tableLayoutPanel1.Enabled = true;
                        button10.Enabled = false;
                    }
                    for (int i = 0; i < 9; i++)
                    {
                        Buttons[i].BackColor = color;
                        Buttons[i].Enabled = true;
                    }
                }
            });
        }

        public async Task NeuronStep(int[,] field)
        {
            await Task.Run(async () =>
             {
                 tableLayoutPanel1.Enabled = false;
                 int step;
                 step = manager.Step(field);
                 if (step >= 0)
                 {
                     Buttons[step].BackColor = Color.Red;
                     Buttons[step].Enabled = false;
                 }
                 try
                 {
                    field = await CheckWin();
                 }
                 catch(Exception ex) when (ex.Message == "Comp")
                 {
                     MessageBox.Show("Easy! Terminator win!");
                     groupBox1.Enabled = true;
                     if (radioButton2.Checked)
                     {
                         tableLayoutPanel1.Enabled = false;
                         button10.Enabled = true;
                     }
                     else if (!radioButton2.Checked)
                     {
                         tableLayoutPanel1.Enabled = true;
                         button10.Enabled = false;
                     }
                     for (int i = 0; i < 9; i++)
                     {
                         Buttons[i].BackColor = color;
                         Buttons[i].Enabled = true;
                     }
                 }
                 tableLayoutPanel1.Enabled = true;
             });
        }

        public async Task<int[,]> CheckWin()
        {
            return await Task<int[,]>.Run(() => {
                int[,] field = new int[3,3];
                for (int i = 0, k = 0; i < 3; i++)
                    for(int j = 0; j < 3; j++, k++)
                    {
                        if (Buttons[k].BackColor == color)
                            field[i,j] = 0;
                        else if (Buttons[k].BackColor == Color.Green)
                            field[i,j] = 1;
                        else if (Buttons[k].BackColor == Color.Red)
                            field[i,j] = 2;
                    }
                bool flag = false;
                bool comp = false;
                for(int i = 0; i < 3; i++)
                {
                    if ((field[i, 0] != 0) && (field[i, 0] == field[i, 1]) && (field[i, 0] == field[i, 2]))
                    {
                        IsGame = false;
                        if (field[i, 0] == 2)
                            comp = true;
                        break;
                    }
                    if ((field[0, i] != 0) && (field[0, i] == field[1, i]) && (field[0, i] == field[2, i]))
                    {
                        IsGame = false;
                        if (field[0, i] == 2)
                            comp = true;
                        break;
                    }
                    if ((i == 0) && (field[i, i] != 0) &&(field[i, i] == field[i + 1, i + 1]) && (field[i, i] == field[i + 2, i + 2]))
                    {
                        IsGame = false;
                        if (field[i, i] == 2)
                            comp = true;
                        break;
                    }
                    if ((i == 2) && (field[0, i] != 0) && (field[0, i] == field[i - 1, i - 1]) && (field[0, i] == field[i, 0]))
                    {
                        IsGame = false;
                        if (field[0, i] == 2)
                            comp = true;
                        break;
                    }
                    if(!flag)
                        for (int j = 0; j < 3; j++)
                            if (field[i, j] == 0)
                            {
                                flag = true;
                                break;
                            }
                }
                if (comp)
                    throw new Exception("Comp");
                else if (!IsGame)
                    throw new Exception("Game over");
                else if (!flag)
                    throw new Exception("Draw");
                else
                    return field;
            });
        }
    }
}
