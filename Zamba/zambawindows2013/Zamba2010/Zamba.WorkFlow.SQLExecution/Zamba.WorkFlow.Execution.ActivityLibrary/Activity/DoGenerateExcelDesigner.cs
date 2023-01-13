using System;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Design;

using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using System.Windows.Forms;

namespace Zamba.WFActivity.Xoml
{
    class GenerateExcelDesigner : ActivityDesigner
    {
        DoGenerateExcel parentActivity;

        protected override void Initialize(System.Workflow.ComponentModel.Activity activity)
        {
            base.Initialize(activity);
            parentActivity = (DoGenerateExcel) activity;
        }

        protected override Size OnLayoutSize(ActivityDesignerLayoutEventArgs e)
        {
            return new Size(150, 50);
        }

        protected override void OnPaint(ActivityDesignerPaintEventArgs e)
        {
            Rectangle frameRect = new Rectangle(this.Location.X, this.Location.Y, this.Size.Width - 5, this.Size.Height - 5);
            Rectangle shadowRect = new Rectangle(frameRect.X + 5, frameRect.Y + 5, frameRect.Width, frameRect.Height);
            Rectangle pageRect = new Rectangle(frameRect.X + 4, frameRect.Y + 20, frameRect.Width - 8, frameRect.Height - 24);
            Rectangle titleRect = new Rectangle(frameRect.X + 25, frameRect.Y + 4, frameRect.Width - 20, 20);
            Rectangle iconRect = new Rectangle(frameRect.X , frameRect.Y , 20,20);

            Brush frameBrush = new LinearGradientBrush(frameRect, Color.Green, Color.LightGreen, 45);

            e.Graphics.FillPath(Brushes.LightGray, RoundedRect(shadowRect));
            e.Graphics.FillPath(frameBrush, RoundedRect(frameRect));
            e.Graphics.FillPath(new LinearGradientBrush(pageRect, Color.CornflowerBlue, Color.WhiteSmoke, 45), RoundedRect(pageRect));
            e.Graphics.DrawString(parentActivity.MaskName, new Font("Segoe UI", 8), Brushes.Black, pageRect.X + 2, pageRect.Y + 5);
            e.Graphics.DrawString("Generar Excel", new Font("Segoe UI", 8), Brushes.White, titleRect);
            frameRect.Inflate(20, 20);

            //Icon icon = new Icon(Application.StartupPath + "\\about.ico");
            //if (icon != null)
            //    e.Graphics.DrawIcon(icon, iconRect);
            }

        private GraphicsPath RoundedRect(Rectangle frame)
        {
            GraphicsPath path = new GraphicsPath();
            int radius = 7;
            int diameter = radius * 2;

            Rectangle arc = new Rectangle(frame.Left, frame.Top, diameter, diameter);

            path.AddArc(arc, 180, 90);

            arc.X = frame.Right - diameter;
            path.AddArc(arc, 270, 90);

            arc.Y = frame.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            arc.X = frame.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();

            return path;
        }
    }
}
