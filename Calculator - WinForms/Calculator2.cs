using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practice2_WindowsFormsApp
{
    internal partial  class Calculator2: Form
    {
        public Calculator2()
        {
            InitializeComponent();
        }

        public string Operator;
        //public static double Num;
        //public int total1;
        //public int N1;
        static double res;
        public ArrayList NumbersArray = new ArrayList();
        public ArrayList SymbolsArray = new ArrayList();
        string pnt = @"^(\d,)$";
        string neg = @"^(-\d)$";
        string negNull = @"^(-\d{0})$";
        string negFloat = @"^(-[0-9]*(?:\,[0-9]*)?)$";

        private void nums_Click(object sender, EventArgs e)
        {
            Button B = (Button)sender;
            if (textBox1.Text == null)
            {
                textBox1.Text = B.Text;
            }
            else
            {
                textBox1.Text = textBox1.Text + B.Text;
            }

        }

        private void delete_Click(object sender, EventArgs e)
        {
                textBox1.Text = null;
        }

        private void full_del_Click(object sender, EventArgs e)
        {
            textBox1.Text = null;
            NumbersArray.Clear();
            SymbolsArray.Clear();
            process.Text = "";
        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                if(Regex.IsMatch(textBox1.Text, pnt) == true)
                {
                    textBox1.Text = textBox1.Text + "0";
                }
                
                if (process.Text != "" && SymbolsArray[SymbolsArray.Count - 1] == null)
                {
                    Button B = (Button)sender;
                    Operator = B.Text;
                    //Num = Convert.ToDouble(textBox1.Text);
                    NumbersArray.RemoveRange(0, NumbersArray.Count);
                    SymbolsArray.RemoveRange(0, SymbolsArray.Count);

                    if (Regex.IsMatch(textBox1.Text, neg) == true)
                        process.Text = " (" + textBox1.Text + ") " + B.Text;
                    else
                        process.Text = textBox1.Text + B.Text;

                    NumbersArray.Add(textBox1.Text);
                    SymbolsArray.Add(Operator);
                    textBox1.Text = null;
                }
                else
                {
                    Button B = (Button)sender;
                    Operator = B.Text;
                    //Num = Convert.ToDouble(textBox1.Text);

                    if (Regex.IsMatch(textBox1.Text, neg) == true)
                        process.Text = process.Text + " (" + textBox1.Text + ") " + B.Text;
                    else
                        process.Text = process.Text + textBox1.Text + B.Text;

                    SymbolsArray.Add(Operator);
                    NumbersArray.Add(textBox1.Text);
                    textBox1.Text = null;
                }
            }
            catch
            {
                MessageBox.Show("Incorrect INPUT!");
            }
        }

        private void equally_Click(object sender, EventArgs e)
        {
            try
            {
                if (Regex.IsMatch(textBox1.Text, pnt) == true)
                {
                    textBox1.Text = textBox1.Text + "0";
                }

                //Num = textBox1.Text;
                NumbersArray.Add(textBox1.Text);
                SymbolsArray.Add(null);

                if (Regex.IsMatch(textBox1.Text, neg) == true)
                    process.Text = process.Text + " (" + textBox1.Text + ") " + " = ";
                else
                    process.Text = process.Text + textBox1.Text + " = ";

                //textBox1.Text = null;
                var process_num = NumbersArray;
                var process_symb = SymbolsArray;

                if (process_symb.Contains("*") || process_symb.Contains("/"))
                {
                    for (int j = 0; j < process_num.Count - 1; j++)
                    {
                        string op = (string)process_symb[j];
                        double x = Convert.ToDouble(process_num[j]);
                        double y = Convert.ToDouble(process_num[j + 1]);
                        double z = 0;

                        try
                        {

                            if (op == "*")
                            {
                                z = x * y;
                                process_num.RemoveAt(j);
                                process_num.RemoveAt(j);
                                process_num.Insert(j, z.ToString());
                                process_symb.RemoveAt(j);
                                j = j - 1;
                            }
                            else if (op == "/")
                            {
                                z = x / y;
                                if (double.IsInfinity(z))
                                {
                                    MessageBox.Show("You cannot divide by ZERO!");
                                }else
                                {
                                    process_num.RemoveAt(j);
                                    process_num.RemoveAt(j);
                                    process_num.Insert(j, z.ToString());
                                    process_symb.RemoveAt(j);
                                    j = j - 1;
                                }
                                
                            }

                        }catch (DivideByZeroException a)
                        {
                            MessageBox.Show("Exception caught: " + a.Message);
                        }
                        /*catch
                        {
                            MessageBox.Show("You cannot divide by ZERO!");
                        }*/
                    }
                };

                res = Convert.ToDouble(process_num[0]);
                for (int i = 0; i < process_num.Count - 1; i++)
                {
                    double N2 = Convert.ToDouble(process_num[i + 1]);
                    string D1 = (string)process_symb[i];

                    if (D1 == "+")
                        res = res + N2;
                    else if (D1 == "-")
                        res = res - N2;
                }

                textBox1.Text = res.ToString();
                memory.Text = memory.Text + "\r\n" + process.Text + "\r\n" + textBox1.Text + "\r\n";
            }
            catch
            {
                MessageBox.Show("Incorrect INPUT!");
            }
        }

        private void point_Click(object sender, EventArgs e)
        {
            Button B = (Button)sender;
            if (textBox1.Text == null || textBox1.Text == "")
            {
                textBox1.Text = "0,";
            }
            else
            {
                textBox1.Text = textBox1.Text + ",";
            }
        }

        private void negative_Click(object sender, EventArgs e)
        {

            Button B = (Button)sender;

            if (textBox1.Text == null || textBox1.Text == "")
                textBox1.Text = "-";
            else if (Regex.IsMatch(textBox1.Text, negNull) == true)
                textBox1.Text = null;
            else if (Regex.IsMatch(textBox1.Text, neg) == true || Regex.IsMatch(textBox1.Text, negFloat) == true)
                textBox1.Text = textBox1.Text.Substring(1);
            else
                textBox1.Text = "-" + textBox1.Text;
        }

    }
    
}
