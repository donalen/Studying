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
        /// <summary>
        /// Создание Формы
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Обработка нажатия на клавишу
        /// Создание матрицы и поиск ее наибольшего собственного значения по модулю
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToString(comboBox1.SelectedItem) == "2")
                {
                    ComplexNumbers[,] matrix = new ComplexNumbers[2, 2];
                    matrix[0, 0] = CheckingTheValue(textBox1.Text);
                    matrix[0, 1] = CheckingTheValue(textBox2.Text);
                    matrix[1, 0] = CheckingTheValue(textBox5.Text);
                    matrix[1, 1] = CheckingTheValue(textBox6.Text);

                    MatrixOperations matrix_ = new MatrixOperations(2, matrix);

                    IterativeMethod method = new IterativeMethod(2, matrix_);
                    textBox17.Text = method.Method_().Print();

                }
                if (Convert.ToString(comboBox1.SelectedItem) == "3")
                {
                    ComplexNumbers[,] matrix = new ComplexNumbers[3, 3];

                    matrix[0, 0] = CheckingTheValue(textBox1.Text);
                    matrix[0, 1] = CheckingTheValue(textBox2.Text);
                    matrix[0, 2] = CheckingTheValue(textBox3.Text);
                    matrix[1, 0] = CheckingTheValue(textBox5.Text);
                    matrix[1, 1] = CheckingTheValue(textBox6.Text);
                    matrix[1, 2] = CheckingTheValue(textBox7.Text);
                    matrix[2, 0] = CheckingTheValue(textBox9.Text);
                    matrix[2, 1] = CheckingTheValue(textBox10.Text);
                    matrix[2, 2] = CheckingTheValue(textBox11.Text);

                    MatrixOperations matrix_ = new MatrixOperations(3, matrix);

                    IterativeMethod method = new IterativeMethod(3, matrix_);
                    textBox17.Text = method.Method_().Print();


                }
                if (Convert.ToString(comboBox1.SelectedItem) == "4")
                {
                    ComplexNumbers[,] matrix = new ComplexNumbers[4, 4];

                    matrix[0, 0] = CheckingTheValue(textBox1.Text);
                    matrix[0, 1] = CheckingTheValue(textBox2.Text);
                    matrix[0, 2] = CheckingTheValue(textBox3.Text);
                    matrix[0, 3] = CheckingTheValue(textBox4.Text);
                    matrix[1, 0] = CheckingTheValue(textBox5.Text);
                    matrix[1, 1] = CheckingTheValue(textBox6.Text);
                    matrix[1, 2] = CheckingTheValue(textBox7.Text);
                    matrix[1, 3] = CheckingTheValue(textBox8.Text);
                    matrix[2, 0] = CheckingTheValue(textBox9.Text);
                    matrix[2, 1] = CheckingTheValue(textBox10.Text);
                    matrix[2, 2] = CheckingTheValue(textBox11.Text);
                    matrix[2, 3] = CheckingTheValue(textBox12.Text);
                    matrix[3, 0] = CheckingTheValue(textBox13.Text);
                    matrix[3, 1] = CheckingTheValue(textBox14.Text);
                    matrix[3, 2] = CheckingTheValue(textBox15.Text);
                    matrix[3, 3] = CheckingTheValue(textBox16.Text);

                    MatrixOperations matrix_ = new MatrixOperations(4, matrix);

                    IterativeMethod method = new IterativeMethod(4, matrix_);
                    textBox17.Text = method.Method_().Print();


                }
            }
            catch
            {
                textBox17.Text = "Неверные данные";
            }
        }
        /// <summary>
        /// Выбор значения из списка
        /// Активирует нужные окна ввода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToString(comboBox1.SelectedItem) == "2")
            {
                button1.Enabled = true;
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = true;
                textBox6.Enabled = true;
                textBox7.Enabled = false;
                textBox8.Enabled = false;
                textBox9.Enabled = false;
                textBox10.Enabled = false;
                textBox11.Enabled = false;
                textBox12.Enabled = false;
                textBox13.Enabled = false;
                textBox14.Enabled = false;
                textBox15.Enabled = false;
                textBox16.Enabled = false;

            }

            if (Convert.ToString(comboBox1.SelectedItem) == "3")
            {
                button1.Enabled = true;
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = false;
                textBox5.Enabled = true;
                textBox6.Enabled = true;
                textBox7.Enabled = true;
                textBox8.Enabled = false;
                textBox9.Enabled = true;
                textBox10.Enabled = true;
                textBox11.Enabled = true;
                textBox12.Enabled = false;
                textBox13.Enabled = false;
                textBox14.Enabled = false;
                textBox15.Enabled = false;
                textBox16.Enabled = false;

            }

            if (Convert.ToString(comboBox1.SelectedItem) == "4")
            {
                button1.Enabled = true;
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
                textBox6.Enabled = true;
                textBox7.Enabled = true;
                textBox8.Enabled = true;
                textBox9.Enabled = true;
                textBox10.Enabled = true;
                textBox11.Enabled = true;
                textBox12.Enabled = true;
                textBox13.Enabled = true;
                textBox14.Enabled = true;
                textBox15.Enabled = true;
                textBox16.Enabled = true;

            }
        }
        /// <summary>
        /// Метод CheckingTheValue
        /// Обрабатывает введенную строку
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private ComplexNumbers CheckingTheValue(string value)
        {

            value = value.Replace(" ", "");
            string realPart = "";
            string imagPart = "";
            double result;
            if ((Double.TryParse(value, out result)))
                return new ComplexNumbers(result, 0);
            if (value[value.Length - 1] == 'i')
            {
                if (value.Contains('+'))
                    realPart = value.Substring(0, value.Length - value.IndexOf("+") - 2);
                imagPart = value.Substring(value.IndexOf("+") + 1).Trim(new char[] { 'i' });
            }
            if ((Double.TryParse(realPart, out result)))
                return new ComplexNumbers(result, Double.Parse(imagPart));
            else
                return new ComplexNumbers(0, Double.Parse(imagPart));
        }
    }
}