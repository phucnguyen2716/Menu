using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;

namespace Menu
{
    class ArtanTextBox: Control
    {
        private int borderSize = 0;
        private int radius = 50;
        private Color borderColor = Color.Transparent;
        private Color backColor;
        private Color waterMarkColor = Color.Gray;
        private Color mColor = Color.Transparent;
        private string waterMark;
        private GraphicsPath shape;
        private GraphicsPath innerRect;
        public TextBox textBox = new TextBox();
        public ArtanTextBox()
        {
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.ResizeRedraw, true);

            base.Controls.Add(textBox);
            base.Size = new Size(200, 50);
            textBox.Parent = this;
            textBox.BorderStyle = BorderStyle.None;
            textBox.BackColor = backColor;
            BackColor = Color.Transparent;
            backColor = Color.White;
            Font = new Font("Segoe UI", 12F);
            DoubleBuffered = true;
            textBox.KeyDown += new KeyEventHandler(textBox_KeyDown);
            textBox.TextChanged += new EventHandler(textBox_TextChanged);
            textBox.MouseDoubleClick += new MouseEventHandler(textBox_MouseDoubleClick);
            textBox.MouseClick += new MouseEventHandler(textBox_MouseClick);
            textBox.Leave += new EventHandler(textBox_Leave);
            textBox.KeyPress += new KeyPressEventHandler(textBox_KeyPress);
        }
        
        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
        }

        private void textBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (Text == waterMark || Text == string.Empty)
            {
                Text = "";
                textBox.ForeColor = mColor;
            }
        }

        private void textBox_Leave(object sender, EventArgs e)
        {
            if (Text == waterMark || Text == string.Empty)
            {
                Text = waterMark;
                textBox.ForeColor = waterMarkColor;
            }
            else
                textBox.ForeColor = mColor;
        }

        private void textBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                textBox.SelectAll();
            }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            Text = textBox.Text;
            base.OnTextChanged(e);
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.A))
            {
                textBox.SelectionStart = 0;
                textBox.SelectionLength = textBox.Text.Length;
            }
        }

        public char PasswordChar
        {
            get { return textBox.PasswordChar; }
            set { textBox.PasswordChar = value; base.Invalidate(); }
        }

        public bool UseSystemPasswordChar
        {
            get { return textBox.UseSystemPasswordChar; }
            set { textBox.UseSystemPasswordChar = value; base.Invalidate(); }
        }

        public Color BackColorArtan
        {
            get { return backColor; }
            set {
                backColor = value;
                if(backColor!= Color.Transparent)
                {
                    textBox.BackColor = backColor;
                }
                base.Invalidate();
            } 
        }
        public override Color BackColor
        {
            get => base.BackColor;
            set => base.BackColor = value;
        }

        public Color BorderColor
        {
            get => borderColor;
            set { borderColor = value; Invalidate(); }
        }
        public Color WaterMarkColor
        {
            get => waterMarkColor;
            set { waterMarkColor = value; Invalidate(); }
        }

        public int Radius
        {
            get => radius;
            set { radius = value; Invalidate(); }
        }

        public int BorderSize
        {
            get => borderSize;
            set { borderSize = value; Invalidate(); }
        }
        public string WaterMark
        {
            get => waterMark;
            set
            {
                waterMark = value;
                if (Text == waterMark || Text == string.Empty)
                {
                    Text = waterMark;
                    textBox.ForeColor = waterMarkColor;
                }
                else textBox.ForeColor = mColor;
                Invalidate();
            }
        }
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            textBox.Font = Font;
            base.Invalidate();
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            textBox.ForeColor = mColor = ForeColor;
            base.Invalidate();
        }
        public override string Text
        {
            get => textBox.Text;
            set
            {
                textBox.Text = value;
                base.Invalidate();
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            shape = new MyRectangle((float)base.Width, (float)base.Height, (float)radius / 2, 0F, 0F).path;
            innerRect = new MyRectangle(base.Width - 0.5F, base.Height - 0.5F, (float)radius / 2, 0.5F, 0.5F).path;
            Pen pen = new Pen(BorderColor, BorderSize);

            if (textBox.Height > (base.Height - 4))
                base.Height = textBox.Height + 4;

            textBox.Location = new Point(Radius - 5, (base.Height / 2) - (textBox.Font.Height / 2));
            textBox.Width = base.Width - ((int)(Radius * 1.5));
            e.Graphics.SmoothingMode = (SmoothingMode.HighQuality);
            e.Graphics.DrawPath(pen, shape);

            using (SolidBrush brush = new SolidBrush(backColor))
            {
                e.Graphics.FillPath((Brush)brush, innerRect);
            }

            Trans.MakeTransparent(this, e.Graphics);
            base.OnPaint(e);
        }

    }

}
