public class CInputBox : System.Windows.Forms.Form
{
	private System.Windows.Forms.TextBox textBox1;
	private System.Windows.Forms.Label label1;
	private System.ComponentModel.Container components = null;

	private CInputBox()
	{
		InitializeComponent();
	}

	protected override void Dispose( bool disposing )
	{
		if( disposing )
		{
			if(components != null)
			{
				components.Dispose();
			}
		}
		base.Dispose( disposing );
	}

	private void InitializeComponent()
	{
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.SuspendLayout();
		// 
		// textBox1
		// 
		this.textBox1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
		this.textBox1.Location = new System.Drawing.Point(22, 50);
		this.textBox1.Name = "textBox1";
		this.textBox1.Size = new System.Drawing.Size(256, 21);
		this.textBox1.TabIndex = 0;
		this.textBox1.Text = "";
		this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
		this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
		// 
		// label1
		// 
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
		this.label1.ForeColor = System.Drawing.Color.Blue;
		this.label1.Location = new System.Drawing.Point(21, 17);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(258, 17);
		this.label1.TabIndex = 1;
		this.label1.Text = "COMPLETE EL TEXTO PARA EL RECTANGULO";
		// 
		// CInputBox
		// 
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
		this.ClientSize = new System.Drawing.Size(301, 88);
		this.Controls.Add(this.label1);
		this.Controls.Add(this.textBox1);
		this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		this.MaximizeBox = false;
		this.MinimizeBox = false;
		this.Name = "CInputBox";
		this.ShowInTaskbar = false;
		this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.ResumeLayout(false);

	}

	private void textBox1_KeyDown(object sender,
		System.Windows.Forms.KeyEventArgs e)
	{
		if(e.KeyCode == System.Windows.Forms.Keys.Enter)
			this.Close();
	}

	public static string ShowInputBox(string Texto)
	{
		CInputBox box = new CInputBox();
		box.textBox1.Text=Texto;
		box.ShowDialog();
		return box.textBox1.Text;
	}

	private void textBox1_TextChanged(object sender, System.EventArgs e)
	{
	
	}    
}
