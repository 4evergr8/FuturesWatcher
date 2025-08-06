using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class FloatingNumberForm : Form
{
    private Label numberLabel;
    private Point mouseOffset;
    private bool isMouseDown = false;

    public FloatingNumberForm(string numberText)
    {
        this.FormBorderStyle = FormBorderStyle.None;
        this.TopMost = true;
        this.StartPosition = FormStartPosition.Manual;
        this.ShowInTaskbar = false;

        this.BackColor = Color.Magenta;               // 背景色
        this.TransparencyKey = Color.Magenta;         // 设置为透明
        this.Size = new Size(140, 50);
        this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width,
                                  Screen.PrimaryScreen.WorkingArea.Height - this.Height);

        this.Region = new Region(GetRoundedRectPath(new Rectangle(0, 0, this.Width, this.Height), 12));

        numberLabel = new Label();
        numberLabel.Text = numberText;
        numberLabel.ForeColor = Color.White;
        numberLabel.Font = new Font("Segoe UI", 14, FontStyle.Bold);
        numberLabel.TextAlign = ContentAlignment.MiddleCenter;
        numberLabel.Dock = DockStyle.Fill;
        numberLabel.BackColor = Color.Transparent; // 文字背景透明

        this.Controls.Add(numberLabel);

        // 拖动事件绑定
        this.MouseDown += Form_MouseDown;
        this.MouseMove += Form_MouseMove;
        this.MouseUp += Form_MouseUp;
        numberLabel.MouseDown += Form_MouseDown;
        numberLabel.MouseMove += Form_MouseMove;
        numberLabel.MouseUp += Form_MouseUp;
    }

    public void UpdateNumber(string text, Color color)
    {
        numberLabel.Text = text;
        numberLabel.ForeColor = color;
    }

    private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
    {
        GraphicsPath path = new GraphicsPath();
        int d = radius * 2;

        path.AddArc(rect.X, rect.Y, d, d, 180, 90);
        path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
        path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
        path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
        path.CloseFigure();

        return path;
    }

    private void Form_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            mouseOffset = new Point(-e.X, -e.Y);
            isMouseDown = true;
        }
    }

    private void Form_MouseMove(object sender, MouseEventArgs e)
    {
        if (isMouseDown)
        {
            Point mousePos = Control.MousePosition;
            mousePos.Offset(mouseOffset.X, mouseOffset.Y);
            this.Location = mousePos;
        }
    }

    private void Form_MouseUp(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            isMouseDown = false;
        }
    }
}
