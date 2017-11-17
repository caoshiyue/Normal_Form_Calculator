using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace discrete_mathematics1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public string expression;
        string_operate so = new string_operate();
        public MainWindow()
        {
            InitializeComponent();
            this.output1.IsReadOnly = true;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            insert(char_check.NOT);
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            insert(char_check.AND);
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            insert(char_check.OR);
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            insert(char_check.PREDICT);
        }
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            insert(char_check.EQUAL);
        }
        public void insert(string symbol)       //在光标处插入
        {
            string s = this.input.Text;
            int idx = input.SelectionStart;
            s = s.Insert(idx, symbol);
            input.Text = s;
            input.SelectionStart = idx + 1;
            input.Focus();
        }
        private void Button_Click_6(object sender, RoutedEventArgs e)//计算
        {
            input.Text = input.Text.Replace(" ", "");
            expression = input.Text;
            so.get_string(ref expression);
            if (so.syntactic_error())
            {
                output1.Text = so.repalce();
            }
        }
    }
}
