using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace InheritanceCode
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static IntPtr handler = IntPtr.Zero;
        public MainWindow()
        {
            int[] arrPoint = new int[] { 100, 200, 300, 400, 500, 600, 700 };
            InitializeComponent();
            cbMoveH.ItemsSource= arrPoint;
            cbMoveW.ItemsSource= arrPoint;
            cbMoveX.ItemsSource= arrPoint;
            cbMoveY.ItemsSource= arrPoint;
        }

        private void bClose_Click(object sender, RoutedEventArgs e)
        {
            const int WM_CLOSE = 0x0010;
            WindowFinder.SendMessage(handler, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        }

        private void bFind_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WindowFinder.FindWindow(null, tbNameWin.Text.ToString());
                handler = WindowFinder.FindWindowByCaption(IntPtr.Zero, tbNameWin.Text.ToString());
                if(handler != IntPtr.Zero)
                {
                    WindowFinder.MessageBox(new IntPtr(0), "Window is found!", " ",0);
                }
                else
                {
                    WindowFinder.MessageBox(new IntPtr(0), "Name not found", " ", 0);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bSetText_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WindowFinder.SetWindowText(handler,tbSetText.Text.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bChangeColor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(Char.IsDigit((char)cbMoveX.SelectedValue)&& Char.IsDigit((char)cbMoveY.SelectedValue) &&
                    Char.IsDigit((char)cbMoveH.SelectedValue) && Char.IsDigit((char)cbMoveW.SelectedValue))
                {
                    WindowFinder.MoveWindow(handler, (int)cbMoveX.SelectedValue, (int)cbMoveY.SelectedValue,
                    (int)cbMoveH.SelectedValue, (int)cbMoveW.SelectedValue, true);
                }
                else
                {
                    WindowFinder.MessageBox(new IntPtr(0), "Unccorect value!", " ", 0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    public static class WindowFinder
    {
        
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        public static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        public static IntPtr FindWindow(string caption)
        {
            return FindWindow(String.Empty, caption);
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SetWindowText(IntPtr hWnd, string txt);
        [DllImport("gdi32.dll",  SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool bRepaint);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int MessageBox(IntPtr hWnd, String text, String caption, uint type);

    }
}
