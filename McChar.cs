using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace minecraft_nickname_color {
    public partial class McChar {
        public char lettera { get; set; }
        public char colore { get; set; }
        public string stile { get; set; }

        public bool resetBefore { get; set; }

        private Dictionary<char, Color> ColorTable = new(){
            { '\0', Color.FromRgb(0, 0, 0) },
            { '0', Color.FromRgb(0, 0, 0) },
            { '1', Color.FromRgb(0, 0, 170) },
            { '2', Color.FromRgb(0, 170, 0) },
            { '3', Color.FromRgb(0, 170, 170) },
            { '4', Color.FromRgb(170, 0, 0) },
            { '5', Color.FromRgb(170, 0, 170) },
            { '6', Color.FromRgb(255, 170, 0) },
            { '7', Color.FromRgb(170, 170, 170) },
            { '8', Color.FromRgb(85, 85, 85) },
            { '9', Color.FromRgb(85, 85, 255) },
            { 'a', Color.FromRgb(85, 255, 85) },
            { 'b', Color.FromRgb(85, 255, 255) },
            { 'c', Color.FromRgb(255, 85, 85) },
            { 'd', Color.FromRgb(255, 85, 255) },
            { 'e', Color.FromRgb(255, 255, 85) },
            { 'f', Color.FromRgb(255, 255, 255) }
        };


        public McChar(char chr, char clr = '\0', string stl = "") {
            this.resetBefore = false;
            this.lettera = chr;
            this.colore = clr;
            this.stile = stl;
        }

        public Color charToColor(char chr) {
            return ColorTable[chr];
        }

        public TextBlock ToTextBlock() {
            // <TextBlock Text="{Binding Testo}" Foreground="{Binding Colore}" TextDecorations="{Binding Stile1}" FontStyle="{Binding Stile2}" />
            
            SolidColorBrush colore = new SolidColorBrush();
            colore.Color = charToColor(this.colore);

            TextBlock a = new TextBlock();
            a.FontSize = 16;
            a.Text = lettera.ToString();
            a.Foreground = colore;
            

            foreach (char chr in this.stile) {
                switch (chr) {
                    case 'l':
                        a.FontWeight = FontWeights.Bold;
                        break;
                    case 'm':
                        a.TextDecorations.Add(TextDecorations.Strikethrough);
                        break;
                    case 'n':
                        a.TextDecorations.Add(TextDecorations.Underline);
                        break;
                    case 'o':
                        a.FontStyle = FontStyles.Italic;
                        break;
                    case 'k':
                        a.Background = Brushes.Black;
                        break;
                    default:
                        a.FontWeight = FontWeights.Normal;
                        a.TextDecorations.Clear();
                        a.FontStyle = FontStyles.Normal;
                        a.Background = null;
                        a.Foreground = Brushes.Black;


                        this.colore = '\0';
                        this.stile = "";
                        //this.resetBefore = true;
                        break;
                }
            }

            return a;
        }

        public override string ToString() {
            return (resetBefore ? "&r" : "") + (colore != '\0' ? "&" + colore : "") + (stile != "" ? "&" + String.Join("&", stile.ToArray()) : "") + lettera;
        }
        
    }
}
