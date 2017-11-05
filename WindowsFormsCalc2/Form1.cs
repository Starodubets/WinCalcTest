using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsCalc2
{

    public partial class Form1 : Form
    {
        private bool last_number = false;
        private Calculator calc;

        public Form1()
        {
            InitializeComponent();
            calc = new Calculator();
        }

        public class Calculator
        {
            private int result = 0;
            private char last_operator = '+';

            //public static int funct_calc(int a, int b, char op)
            //{
            //    switch (op)
            //    {
            //        case '+':
            //            return a + b;
            //        case '-':
            //            return a - b;
            //        case '/':
            //            return a / b;
            //        case '*':
            //            return a * b;
            //    }
            //    throw new ArgumentException("Illegal operator");
            //}

            public void set_last_operator(char op)
            {
                last_operator = op;
            }

            public void calculate(int num)
            {
                result = Calc(result, num, last_operator);
            }

            public int get_result()
            {
                return result;
            }

        }
      

        private void btnNumClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (last_number)
                txtres.Text = txtres.Text + button.Text;
            else
                txtres.Text = button.Text;

            last_number = true;
        }

        private void opClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            char op = System.Convert.ToChar(button.Text);
            calc.calculate(Convert.ToInt32(txtres.Text));
            calc.set_last_operator(op);
            last_number = false;
        }

        private void btnEqual(object sender, EventArgs e)
        {
            calc.calculate(Convert.ToInt32(txtres.Text));
            txtres.Text = "" + calc.get_result();
            last_number = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        public static int CalcPHP(int a, int b, char op)
        {
            string Url = "http://localhost/POST/func.php";
            string oper = "";
            switch (op)
            {
                case '+':
                    oper = "plus";
                    break;
                case '-':
                    oper = "minus";
                    break;
                case '*':
                    oper = "multiply";
                    break;
                case '/':
                    oper = "div";
                    break;

                default:
                    break;
            }
            string Data = "num1=" + a + "&num2=" + b + "&act=" + oper;
            WebRequest req = WebRequest.Create(Url);
            req.Method = "POST";
            req.Timeout = 100000;
            req.ContentType = "application/x-www-form-urlencoded";
            byte[] sentData = Encoding.GetEncoding(1251).GetBytes(Data);
            req.ContentLength = sentData.Length;
            Stream sendStream = req.GetRequestStream();
            sendStream.Write(sentData, 0, sentData.Length);
            sendStream.Close();
            WebResponse res = req.GetResponse();
            Stream ReceiveStream = res.GetResponseStream();
            StreamReader sr = new StreamReader(ReceiveStream, Encoding.UTF8);
            Char[] read = new Char[256];
            int count = sr.Read(read, 0, 256);
            string Out = String.Empty;
            while (count > 0)
            {
                String str = new String(read, 0, count);
                Out += str;
                count = sr.Read(read, 0, 256);
            }
            return Convert.ToInt32(Out);
        }
        public static int Calc(int a, int b, char op)
        {
            int res = 0;
            switch (op)
            {
                case '+':
                    res = a + b;
                    break;
                case '-':
                    res = a - b;
                    break;
                case '/':
                    res = a / b;
                    break;
                case '*':
                    res = a * b;
                    break;
            }
            return res;
        }
    }
}
