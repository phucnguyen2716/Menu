using Menu.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Menu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            lbTitle.Font = new Font(lbTitle.Font.FontFamily, 12);
            lb1.Font = new Font(lb1.Font.FontFamily, 12);
            lb2.Font = new Font(lb2.Font.FontFamily, 12);
            lb3.Font = new Font(lb3.Font.FontFamily, 12);
            lb4.Font = new Font(lb4.Font.FontFamily, 12);
            btnBack.Font = new Font(btnBack.Font.FontFamily, 20);
        }

        bool IsLengthValid(ArtanTextBox text)
        {
            if (text == null || string.IsNullOrEmpty(text.Text?.Trim()) ) return false;
            return text.Text.Trim().Length >= 8;
        }
        bool HasLowercase(ArtanTextBox text)
        {
            if (text == null || string.IsNullOrEmpty(text.Text?.Trim())) return false;
            return Regex.IsMatch(text.Text.Trim(), "[a-z]");
        }
        bool HasSpecialSymbol(ArtanTextBox text)
        {
            if (text == null || string.IsNullOrEmpty(text.Text?.Trim())) return false;
            return Regex.IsMatch(text.Text.Trim(), "[^a-zA-Z0-9]");
        }
        bool HasNumber(ArtanTextBox text)
        {
            if (text == null || string.IsNullOrEmpty(text.Text?.Trim())) return false;
            return Regex.IsMatch(text.Text.Trim(), "[0-9]");
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

            if (IsLengthValid(txtPassword))
            {
                lb1.ForeColor = Color.FromArgb(0, 189, 83);
                pb1.Image = GetImageFromBytes(Resource1.Yes);
            }
            else
            {
                lb1.ForeColor = Color.Red;
                pb1.Image = GetImageFromBytes(Resource1.No);
            }

            if (HasLowercase(txtPassword))
            {
                lb2.ForeColor = Color.FromArgb(0, 189, 83);
                pb2.Image = GetImageFromBytes(Resource1.Yes);
            }
            else
            {
                lb2.ForeColor = Color.Red;
                pb2.Image = GetImageFromBytes(Resource1.No);
            }
            if (HasSpecialSymbol(txtPassword))
            {
                lb3.ForeColor = Color.FromArgb(0, 189, 83);
                pb3.Image = GetImageFromBytes(Resource1.Yes);
            }
            else
            {
                lb3.ForeColor = Color.Red;
                pb3.Image = GetImageFromBytes(Resource1.No);
            }
            if (HasNumber(txtPassword))
            {
                lb4.ForeColor = Color.FromArgb(0, 189, 83);
                pb4.Image = GetImageFromBytes(Resource1.Yes);
            }
            else
            {
                lb4.ForeColor = Color.Red;
                pb4.Image = GetImageFromBytes(Resource1.No);
            }
        }

        public Image GetImageFromBytes(byte[] imageData)
        {
            if (imageData != null && imageData.Length > 0)
            {
                using (var ms = new MemoryStream(imageData))
                {
                    return Image.FromStream(ms);
                }
            }
            return null;
        }

        private void artanPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
    }
