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

using System.Diagnostics;
using System.Text.RegularExpressions;

namespace minecraft_nickname_color {

    public partial class MainWindow : Window {

        private McChar[] caratteri;

        private void updateOut() {
            //preview.Text = String.Format("<TextBlock Text='{0}' Foreground='#ffff0000' />", "lorem ipsum");
            preview.Children.Clear();
            foreach ( TextBlock elem in caratteri.Select(clss => clss.ToTextBlock())) {
                preview.Children.Add(elem);
            }
            previewRaw.Text = String.Join("", caratteri.Select(clss => clss.ToString()));
        }

        public MainWindow() {
            InitializeComponent();
        }

        private void username_field_TextChanged(object sender, TextChangedEventArgs e) {
            
            string original = (sender as TextBox).Text;
            string modified = Regex.Replace(original, @"[^0-9a-z_]", "", RegexOptions.IgnoreCase);
            //Trace.WriteLine(original);
            if ((e.Handled = original != modified) == true) {
                (sender as TextBox).Text = modified;
            }

            caratteri = (sender as TextBox).Text.ToCharArray().Select(chr => new McChar(chr)).ToArray();

            this.updateOut();
        }

        private void color_Click(object sender, RoutedEventArgs e) {
            int start = username_field.SelectionStart;
            int len = username_field.SelectionLength;

            for(int i = 0; i< len; i++) {
                caratteri[start + i].colore = (sender as Button).Tag.ToString()[0];
            }
            //caratteri[start].colore = (sender as Button).Tag.ToString()[0];
            if (start + len < username_field.Text.Length) {
                caratteri[start + len].resetBefore = true;
            }

            updateOut();
        }

        private void style_Click(object sender, RoutedEventArgs e) {

            int start = username_field.SelectionStart;
            int len = username_field.SelectionLength;

            for (int i = 0; i < len; i++) {
                caratteri[start + i].stile += (sender as Button).Tag.ToString();
                if((sender as Button).Tag.ToString() == "r") {
                    caratteri[start+i].resetBefore = false;
                }
            }
            if((sender as Button).Tag.ToString() == "r") {
                caratteri[start].resetBefore = true;
            }
            

            updateOut();

        }

        private void copyBtn_Click(object sender, RoutedEventArgs e) {
            Clipboard.SetText(previewRaw.Text);
        }
    }
}
