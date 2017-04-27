using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Eigenvalue
{
    public partial class Form1 : Form
    {
        TextBox[,] matrixTextBox = new TextBox[4, 4];
        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    matrixTextBox[i, j] = new TextBox();
            matrixTextBox[0, 0] = textBox1;
            matrixTextBox[0, 1] = textBox2;
            matrixTextBox[0, 2] = textBox3;
            matrixTextBox[0, 3] = textBox4;
            matrixTextBox[1, 0] = textBox5;
            matrixTextBox[1, 1] = textBox6;
            matrixTextBox[1, 2] = textBox7;
            matrixTextBox[1, 3] = textBox8;
            matrixTextBox[2, 0] = textBox9;
            matrixTextBox[2, 1] = textBox10;
            matrixTextBox[2, 2] = textBox11;
            matrixTextBox[2, 3] = textBox12;
            matrixTextBox[3, 0] = textBox13;
            matrixTextBox[3, 1] = textBox14;
            matrixTextBox[3, 2] = textBox15;
            matrixTextBox[3, 3] = textBox16;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            { 
                int size = Convert.ToInt32(comboBox1.SelectedItem);
                Complex[,] matrix = new Complex[size, size];
                for (int i = 0; i < size; i++)
                    for (int j = 0; j < size; j++)
                        matrix[i, j] = CheckingTheValue(matrixTextBox[i, j].Text);
                Matrix matrix_ = new Matrix(size, matrix);
                Method method = new Method(size, matrix_);
                textBox17.Text = method.MainMethod().ToString();
            }
            catch
            {
                MessageBox.Show("Введены некорректные данные", "Ошибка");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {            
            int size = Convert.ToInt32(comboBox1.SelectedItem);
            if(size <= 4 && size > 1)
                button1.Enabled = true;          
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    matrixTextBox[i, j].Enabled = false;
            for (int i = 0; i<size; i++)
                for(int j = 0; j<size; j++)
                    matrixTextBox[i, j].Enabled = true;
        }

        private Complex CheckingTheValue(string value)//обработка введённых данных
        {
            value = value.Replace(" ", "");
            string realPart = "";
            string imagPart = "";
            double result;
            if ((Double.TryParse(value, out result)))
                return new Complex(result, 0);
            if (value[value.Length - 1] == 'i')
            {
                if (value.Contains('+'))
                    realPart = value.Substring(0, value.Length - value.IndexOf("+") - 2);
                imagPart = value.Substring(value.IndexOf("+") + 1).Trim(new char[] { 'i' });
            }
            if ((Double.TryParse(realPart, out result)))
                return new Complex(result, Double.Parse(imagPart));
            else
                return new Complex(0, Double.Parse(imagPart));
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}