namespace WebDavMail
{
    partial class frmWebDavMailExample
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnGetAllUnread = new System.Windows.Forms.Button();
            this.btnGetAllUnreadAttachment = new System.Windows.Forms.Button();
            this.btnAllMailboxInfo = new System.Windows.Forms.Button();
            this.btnGetMailboxSize = new System.Windows.Forms.Button();
            this.btnGetAttachmentList = new System.Windows.Forms.Button();
            this.btnGetAttachment = new System.Windows.Forms.Button();
            this.gbSettings = new System.Windows.Forms.GroupBox();
            this.btnHelpDrafts = new System.Windows.Forms.Button();
            this.btnHelpInbox = new System.Windows.Forms.Button();
            this.btnHelpPassword = new System.Windows.Forms.Button();
            this.btnHelpAlias = new System.Windows.Forms.Button();
            this.btnHelpUserName = new System.Windows.Forms.Button();
            this.btnHelpExchange = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtExchange = new System.Windows.Forms.TextBox();
            this.txtAlias = new System.Windows.Forms.TextBox();
            this.txtDrafts = new System.Windows.Forms.TextBox();
            this.txtInbox = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.gbResult = new System.Windows.Forms.GroupBox();
            this.lblResult = new System.Windows.Forms.Label();
            this.tvResult = new System.Windows.Forms.TreeView();
            this.txtUrlMail = new System.Windows.Forms.TextBox();
            this.txtUrlAttachment = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtUrlMailRead = new System.Windows.Forms.TextBox();
            this.btnMarkAsRead = new System.Windows.Forms.Button();
            this.gbSendMail = new System.Windows.Forms.GroupBox();
            this.btnSendMail = new System.Windows.Forms.Button();
            this.txtBody = new System.Windows.Forms.TextBox();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.txtMailTo = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.btnCopyClipboard = new System.Windows.Forms.Button();
            this.btnGetContacts = new System.Windows.Forms.Button();
            this.txtContactsLike = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.TxtDownloadAttach = new System.Windows.Forms.TextBox();
            this.BtnDownload = new System.Windows.Forms.Button();
            this.gbSettings.SuspendLayout();
            this.gbResult.SuspendLayout();
            this.gbSendMail.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Exchange URL:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "User name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "User name Alias:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Password:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 137);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Inbox name:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 163);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Drafts name:";
            // 
            // btnGetAllUnread
            // 
            this.btnGetAllUnread.Location = new System.Drawing.Point(333, 43);
            this.btnGetAllUnread.Name = "btnGetAllUnread";
            this.btnGetAllUnread.Size = new System.Drawing.Size(276, 23);
            this.btnGetAllUnread.TabIndex = 13;
            this.btnGetAllUnread.Text = "Get All unread mail";
            this.btnGetAllUnread.UseVisualStyleBackColor = true;
            this.btnGetAllUnread.Click += new System.EventHandler(this.btnGetAllUnread_Click);
            // 
            // btnGetAllUnreadAttachment
            // 
            this.btnGetAllUnreadAttachment.Location = new System.Drawing.Point(333, 72);
            this.btnGetAllUnreadAttachment.Name = "btnGetAllUnreadAttachment";
            this.btnGetAllUnreadAttachment.Size = new System.Drawing.Size(276, 23);
            this.btnGetAllUnreadAttachment.TabIndex = 15;
            this.btnGetAllUnreadAttachment.Text = "Get All unread mail With attachments";
            this.btnGetAllUnreadAttachment.UseVisualStyleBackColor = true;
            this.btnGetAllUnreadAttachment.Click += new System.EventHandler(this.btnGetAllUnreadAttachment_Click);
            // 
            // btnAllMailboxInfo
            // 
            this.btnAllMailboxInfo.Location = new System.Drawing.Point(333, 101);
            this.btnAllMailboxInfo.Name = "btnAllMailboxInfo";
            this.btnAllMailboxInfo.Size = new System.Drawing.Size(276, 23);
            this.btnAllMailboxInfo.TabIndex = 16;
            this.btnAllMailboxInfo.Text = "Get Info on All mailboxes";
            this.btnAllMailboxInfo.UseVisualStyleBackColor = true;
            this.btnAllMailboxInfo.Click += new System.EventHandler(this.btnAllMailboxInfo_Click);
            // 
            // btnGetMailboxSize
            // 
            this.btnGetMailboxSize.Location = new System.Drawing.Point(333, 133);
            this.btnGetMailboxSize.Name = "btnGetMailboxSize";
            this.btnGetMailboxSize.Size = new System.Drawing.Size(276, 23);
            this.btnGetMailboxSize.TabIndex = 17;
            this.btnGetMailboxSize.Text = "Get Total Size of the Mailbox";
            this.btnGetMailboxSize.UseVisualStyleBackColor = true;
            this.btnGetMailboxSize.Click += new System.EventHandler(this.btnGetMailboxSize_Click);
            // 
            // btnGetAttachmentList
            // 
            this.btnGetAttachmentList.Location = new System.Drawing.Point(333, 164);
            this.btnGetAttachmentList.Name = "btnGetAttachmentList";
            this.btnGetAttachmentList.Size = new System.Drawing.Size(276, 23);
            this.btnGetAttachmentList.TabIndex = 18;
            this.btnGetAttachmentList.Text = "Get the list of attachments from an email";
            this.btnGetAttachmentList.UseVisualStyleBackColor = true;
            this.btnGetAttachmentList.Click += new System.EventHandler(this.btnGetAttachmentList_Click);
            // 
            // btnGetAttachment
            // 
            this.btnGetAttachment.Location = new System.Drawing.Point(333, 220);
            this.btnGetAttachment.Name = "btnGetAttachment";
            this.btnGetAttachment.Size = new System.Drawing.Size(276, 23);
            this.btnGetAttachment.TabIndex = 19;
            this.btnGetAttachment.Text = "Get one attachment from an email";
            this.btnGetAttachment.UseVisualStyleBackColor = true;
            this.btnGetAttachment.Click += new System.EventHandler(this.btnGetAttachment_Click);
            // 
            // gbSettings
            // 
            this.gbSettings.Controls.Add(this.btnHelpDrafts);
            this.gbSettings.Controls.Add(this.btnHelpInbox);
            this.gbSettings.Controls.Add(this.btnHelpPassword);
            this.gbSettings.Controls.Add(this.btnHelpAlias);
            this.gbSettings.Controls.Add(this.btnHelpUserName);
            this.gbSettings.Controls.Add(this.btnHelpExchange);
            this.gbSettings.Controls.Add(this.txtPassword);
            this.gbSettings.Controls.Add(this.label1);
            this.gbSettings.Controls.Add(this.txtExchange);
            this.gbSettings.Controls.Add(this.label4);
            this.gbSettings.Controls.Add(this.label5);
            this.gbSettings.Controls.Add(this.label2);
            this.gbSettings.Controls.Add(this.txtAlias);
            this.gbSettings.Controls.Add(this.txtDrafts);
            this.gbSettings.Controls.Add(this.txtInbox);
            this.gbSettings.Controls.Add(this.txtUserName);
            this.gbSettings.Controls.Add(this.label3);
            this.gbSettings.Controls.Add(this.label6);
            this.gbSettings.Location = new System.Drawing.Point(12, 43);
            this.gbSettings.Name = "gbSettings";
            this.gbSettings.Size = new System.Drawing.Size(298, 188);
            this.gbSettings.TabIndex = 20;
            this.gbSettings.TabStop = false;
            this.gbSettings.Text = "Exchange Settings";
            // 
            // btnHelpDrafts
            // 
            this.btnHelpDrafts.Location = new System.Drawing.Point(264, 158);
            this.btnHelpDrafts.Name = "btnHelpDrafts";
            this.btnHelpDrafts.Size = new System.Drawing.Size(22, 23);
            this.btnHelpDrafts.TabIndex = 18;
            this.btnHelpDrafts.Tag = "Drafts";
            this.btnHelpDrafts.Text = "?";
            this.btnHelpDrafts.UseVisualStyleBackColor = true;
            this.btnHelpDrafts.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnHelpInbox
            // 
            this.btnHelpInbox.Location = new System.Drawing.Point(264, 132);
            this.btnHelpInbox.Name = "btnHelpInbox";
            this.btnHelpInbox.Size = new System.Drawing.Size(22, 23);
            this.btnHelpInbox.TabIndex = 17;
            this.btnHelpInbox.Tag = "Inbox";
            this.btnHelpInbox.Text = "?";
            this.btnHelpInbox.UseVisualStyleBackColor = true;
            this.btnHelpInbox.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnHelpPassword
            // 
            this.btnHelpPassword.Enabled = false;
            this.btnHelpPassword.Location = new System.Drawing.Point(263, 106);
            this.btnHelpPassword.Name = "btnHelpPassword";
            this.btnHelpPassword.Size = new System.Drawing.Size(22, 23);
            this.btnHelpPassword.TabIndex = 16;
            this.btnHelpPassword.Tag = "Password";
            this.btnHelpPassword.Text = "?";
            this.btnHelpPassword.UseVisualStyleBackColor = true;
            this.btnHelpPassword.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnHelpAlias
            // 
            this.btnHelpAlias.Location = new System.Drawing.Point(264, 80);
            this.btnHelpAlias.Name = "btnHelpAlias";
            this.btnHelpAlias.Size = new System.Drawing.Size(22, 23);
            this.btnHelpAlias.TabIndex = 15;
            this.btnHelpAlias.Tag = "Alias";
            this.btnHelpAlias.Text = "?";
            this.btnHelpAlias.UseVisualStyleBackColor = true;
            this.btnHelpAlias.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnHelpUserName
            // 
            this.btnHelpUserName.Location = new System.Drawing.Point(264, 54);
            this.btnHelpUserName.Name = "btnHelpUserName";
            this.btnHelpUserName.Size = new System.Drawing.Size(22, 23);
            this.btnHelpUserName.TabIndex = 14;
            this.btnHelpUserName.Tag = "UserName";
            this.btnHelpUserName.Text = "?";
            this.btnHelpUserName.UseVisualStyleBackColor = true;
            this.btnHelpUserName.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnHelpExchange
            // 
            this.btnHelpExchange.Location = new System.Drawing.Point(264, 28);
            this.btnHelpExchange.Name = "btnHelpExchange";
            this.btnHelpExchange.Size = new System.Drawing.Size(22, 23);
            this.btnHelpExchange.TabIndex = 13;
            this.btnHelpExchange.Tag = "Exchange";
            this.btnHelpExchange.Text = "?";
            this.btnHelpExchange.UseVisualStyleBackColor = true;
            this.btnHelpExchange.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::WebDavMail.Properties.Settings.Default, "Password", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtPassword.Location = new System.Drawing.Point(103, 108);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(154, 20);
            this.txtPassword.TabIndex = 7;
            this.txtPassword.Text = global::WebDavMail.Properties.Settings.Default.Password;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // txtExchange
            // 
            this.txtExchange.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::WebDavMail.Properties.Settings.Default, "ExchangeServer", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtExchange.Location = new System.Drawing.Point(103, 30);
            this.txtExchange.Name = "txtExchange";
            this.txtExchange.Size = new System.Drawing.Size(154, 20);
            this.txtExchange.TabIndex = 1;
            this.txtExchange.Text = global::WebDavMail.Properties.Settings.Default.ExchangeServer;
            // 
            // txtAlias
            // 
            this.txtAlias.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::WebDavMail.Properties.Settings.Default, "UserNameAlias", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtAlias.Location = new System.Drawing.Point(103, 82);
            this.txtAlias.Name = "txtAlias";
            this.txtAlias.Size = new System.Drawing.Size(154, 20);
            this.txtAlias.TabIndex = 5;
            this.txtAlias.Text = global::WebDavMail.Properties.Settings.Default.UserNameAlias;
            // 
            // txtDrafts
            // 
            this.txtDrafts.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::WebDavMail.Properties.Settings.Default, "DraftsName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtDrafts.Location = new System.Drawing.Point(103, 160);
            this.txtDrafts.Name = "txtDrafts";
            this.txtDrafts.Size = new System.Drawing.Size(154, 20);
            this.txtDrafts.TabIndex = 11;
            this.txtDrafts.Text = global::WebDavMail.Properties.Settings.Default.DraftsName;
            // 
            // txtInbox
            // 
            this.txtInbox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::WebDavMail.Properties.Settings.Default, "InboxName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtInbox.Location = new System.Drawing.Point(103, 134);
            this.txtInbox.Name = "txtInbox";
            this.txtInbox.Size = new System.Drawing.Size(154, 20);
            this.txtInbox.TabIndex = 9;
            this.txtInbox.Text = global::WebDavMail.Properties.Settings.Default.InboxName;
            // 
            // txtUserName
            // 
            this.txtUserName.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::WebDavMail.Properties.Settings.Default, "UserName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtUserName.Location = new System.Drawing.Point(103, 56);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(154, 20);
            this.txtUserName.TabIndex = 3;
            this.txtUserName.Text = global::WebDavMail.Properties.Settings.Default.UserName;
            // 
            // gbResult
            // 
            this.gbResult.Controls.Add(this.lblResult);
            this.gbResult.Controls.Add(this.tvResult);
            this.gbResult.Location = new System.Drawing.Point(12, 412);
            this.gbResult.Name = "gbResult";
            this.gbResult.Size = new System.Drawing.Size(597, 334);
            this.gbResult.TabIndex = 21;
            this.gbResult.TabStop = false;
            this.gbResult.Text = "Result";
            // 
            // lblResult
            // 
            this.lblResult.Location = new System.Drawing.Point(13, 30);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(567, 301);
            this.lblResult.TabIndex = 22;
            this.lblResult.Text = "lblResult";
            this.lblResult.Visible = false;
            // 
            // tvResult
            // 
            this.tvResult.Location = new System.Drawing.Point(7, 20);
            this.tvResult.Name = "tvResult";
            this.tvResult.Size = new System.Drawing.Size(584, 348);
            this.tvResult.TabIndex = 0;
            this.tvResult.Click += new System.EventHandler(this.tvResult_Click);
            // 
            // txtUrlMail
            // 
            this.txtUrlMail.Location = new System.Drawing.Point(354, 194);
            this.txtUrlMail.Name = "txtUrlMail";
            this.txtUrlMail.Size = new System.Drawing.Size(255, 20);
            this.txtUrlMail.TabIndex = 22;
            this.txtUrlMail.Text = "<Enter URL (a:href) of one email with attachment(s)>";
            this.txtUrlMail.DoubleClick += new System.EventHandler(this.textbox_Enter);
            this.txtUrlMail.Click += new System.EventHandler(this.textbox_Enter);
            this.txtUrlMail.Enter += new System.EventHandler(this.textbox_Enter);
            // 
            // txtUrlAttachment
            // 
            this.txtUrlAttachment.Location = new System.Drawing.Point(354, 249);
            this.txtUrlAttachment.Name = "txtUrlAttachment";
            this.txtUrlAttachment.Size = new System.Drawing.Size(255, 20);
            this.txtUrlAttachment.TabIndex = 23;
            this.txtUrlAttachment.Text = "<Enter URL (a:href) of one attachment>";
            this.txtUrlAttachment.DoubleClick += new System.EventHandler(this.textbox_Enter);
            this.txtUrlAttachment.Click += new System.EventHandler(this.textbox_Enter);
            this.txtUrlAttachment.Enter += new System.EventHandler(this.textbox_Enter);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(330, 194);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(21, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "|__";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(330, 246);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(21, 13);
            this.label8.TabIndex = 25;
            this.label8.Text = "|__";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(330, 356);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(21, 13);
            this.label9.TabIndex = 28;
            this.label9.Text = "|__";
            // 
            // txtUrlMailRead
            // 
            this.txtUrlMailRead.Location = new System.Drawing.Point(354, 359);
            this.txtUrlMailRead.Name = "txtUrlMailRead";
            this.txtUrlMailRead.Size = new System.Drawing.Size(255, 20);
            this.txtUrlMailRead.TabIndex = 27;
            this.txtUrlMailRead.Text = "<Enter URL (a:href) of one email>";
            this.txtUrlMailRead.DoubleClick += new System.EventHandler(this.textbox_Enter);
            this.txtUrlMailRead.Click += new System.EventHandler(this.textbox_Enter);
            this.txtUrlMailRead.Enter += new System.EventHandler(this.textbox_Enter);
            // 
            // btnMarkAsRead
            // 
            this.btnMarkAsRead.Location = new System.Drawing.Point(333, 330);
            this.btnMarkAsRead.Name = "btnMarkAsRead";
            this.btnMarkAsRead.Size = new System.Drawing.Size(276, 23);
            this.btnMarkAsRead.TabIndex = 26;
            this.btnMarkAsRead.Text = "Mark email as read";
            this.btnMarkAsRead.UseVisualStyleBackColor = true;
            this.btnMarkAsRead.Click += new System.EventHandler(this.btnMarkAsRead_Click);
            // 
            // gbSendMail
            // 
            this.gbSendMail.Controls.Add(this.btnSendMail);
            this.gbSendMail.Controls.Add(this.txtBody);
            this.gbSendMail.Controls.Add(this.txtSubject);
            this.gbSendMail.Controls.Add(this.txtMailTo);
            this.gbSendMail.Controls.Add(this.label12);
            this.gbSendMail.Controls.Add(this.label11);
            this.gbSendMail.Controls.Add(this.label10);
            this.gbSendMail.Location = new System.Drawing.Point(12, 237);
            this.gbSendMail.Name = "gbSendMail";
            this.gbSendMail.Size = new System.Drawing.Size(298, 87);
            this.gbSendMail.TabIndex = 29;
            this.gbSendMail.TabStop = false;
            this.gbSendMail.Text = "Send Mail";
            // 
            // btnSendMail
            // 
            this.btnSendMail.Location = new System.Drawing.Point(204, 19);
            this.btnSendMail.Name = "btnSendMail";
            this.btnSendMail.Size = new System.Drawing.Size(75, 23);
            this.btnSendMail.TabIndex = 7;
            this.btnSendMail.Text = "Send";
            this.btnSendMail.UseVisualStyleBackColor = true;
            this.btnSendMail.Click += new System.EventHandler(this.btnSendMail_Click);
            // 
            // txtBody
            // 
            this.txtBody.Location = new System.Drawing.Point(67, 55);
            this.txtBody.Multiline = true;
            this.txtBody.Name = "txtBody";
            this.txtBody.Size = new System.Drawing.Size(212, 26);
            this.txtBody.TabIndex = 6;
            // 
            // txtSubject
            // 
            this.txtSubject.Location = new System.Drawing.Point(67, 35);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(100, 20);
            this.txtSubject.TabIndex = 4;
            // 
            // txtMailTo
            // 
            this.txtMailTo.Location = new System.Drawing.Point(67, 15);
            this.txtMailTo.Name = "txtMailTo";
            this.txtMailTo.Size = new System.Drawing.Size(100, 20);
            this.txtMailTo.TabIndex = 3;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(16, 58);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(34, 13);
            this.label12.TabIndex = 2;
            this.label12.Text = "Body:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(16, 38);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(46, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Subject:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 18);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Mail To:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(55, 2);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(514, 26);
            this.label13.TabIndex = 30;
            this.label13.Text = "WebDav Example using Exchange Server 2003";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCopyClipboard
            // 
            this.btnCopyClipboard.Enabled = false;
            this.btnCopyClipboard.Location = new System.Drawing.Point(12, 752);
            this.btnCopyClipboard.Name = "btnCopyClipboard";
            this.btnCopyClipboard.Size = new System.Drawing.Size(117, 23);
            this.btnCopyClipboard.TabIndex = 31;
            this.btnCopyClipboard.Text = "Copy to Clipboard";
            this.btnCopyClipboard.UseVisualStyleBackColor = true;
            this.btnCopyClipboard.Click += new System.EventHandler(this.btnCopyClipboard_Click);
            // 
            // btnGetContacts
            // 
            this.btnGetContacts.Location = new System.Drawing.Point(333, 385);
            this.btnGetContacts.Name = "btnGetContacts";
            this.btnGetContacts.Size = new System.Drawing.Size(126, 23);
            this.btnGetContacts.TabIndex = 32;
            this.btnGetContacts.Text = "Get contact(s) like...";
            this.btnGetContacts.UseVisualStyleBackColor = true;
            this.btnGetContacts.Click += new System.EventHandler(this.btnGetContacts_Click);
            // 
            // txtContactsLike
            // 
            this.txtContactsLike.Location = new System.Drawing.Point(466, 386);
            this.txtContactsLike.Name = "txtContactsLike";
            this.txtContactsLike.Size = new System.Drawing.Size(100, 20);
            this.txtContactsLike.TabIndex = 33;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(330, 301);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(21, 13);
            this.label14.TabIndex = 36;
            this.label14.Text = "|__";
            // 
            // TxtDownloadAttach
            // 
            this.TxtDownloadAttach.Location = new System.Drawing.Point(354, 304);
            this.TxtDownloadAttach.Name = "TxtDownloadAttach";
            this.TxtDownloadAttach.Size = new System.Drawing.Size(255, 20);
            this.TxtDownloadAttach.TabIndex = 35;
            this.TxtDownloadAttach.Text = "<Enter URL (a:href) of one attachment>";
            this.TxtDownloadAttach.DoubleClick += new System.EventHandler(this.textbox_Enter);
            this.TxtDownloadAttach.Click += new System.EventHandler(this.textbox_Enter);
            this.TxtDownloadAttach.Enter += new System.EventHandler(this.textbox_Enter);
            // 
            // BtnDownload
            // 
            this.BtnDownload.Location = new System.Drawing.Point(333, 275);
            this.BtnDownload.Name = "BtnDownload";
            this.BtnDownload.Size = new System.Drawing.Size(276, 23);
            this.BtnDownload.TabIndex = 34;
            this.BtnDownload.Text = "Download attachments from an email";
            this.BtnDownload.UseVisualStyleBackColor = true;
            this.BtnDownload.Click += new System.EventHandler(this.BtnDownload_Click);
            // 
            // frmWebDavMailExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 782);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.TxtDownloadAttach);
            this.Controls.Add(this.BtnDownload);
            this.Controls.Add(this.txtContactsLike);
            this.Controls.Add(this.btnGetContacts);
            this.Controls.Add(this.btnCopyClipboard);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.gbSendMail);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtUrlMailRead);
            this.Controls.Add(this.btnMarkAsRead);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtUrlAttachment);
            this.Controls.Add(this.txtUrlMail);
            this.Controls.Add(this.gbResult);
            this.Controls.Add(this.gbSettings);
            this.Controls.Add(this.btnGetAttachment);
            this.Controls.Add(this.btnGetAttachmentList);
            this.Controls.Add(this.btnGetMailboxSize);
            this.Controls.Add(this.btnAllMailboxInfo);
            this.Controls.Add(this.btnGetAllUnreadAttachment);
            this.Controls.Add(this.btnGetAllUnread);
            this.Name = "frmWebDavMailExample";
            this.Text = "WebDav Mail Example";
            this.gbSettings.ResumeLayout(false);
            this.gbSettings.PerformLayout();
            this.gbResult.ResumeLayout(false);
            this.gbSendMail.ResumeLayout(false);
            this.gbSendMail.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtExchange;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAlias;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtInbox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDrafts;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnGetAllUnread;
        private System.Windows.Forms.Button btnGetAllUnreadAttachment;
        private System.Windows.Forms.Button btnAllMailboxInfo;
        private System.Windows.Forms.Button btnGetMailboxSize;
        private System.Windows.Forms.Button btnGetAttachmentList;
        private System.Windows.Forms.Button btnGetAttachment;
        private System.Windows.Forms.GroupBox gbSettings;
        private System.Windows.Forms.GroupBox gbResult;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TreeView tvResult;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.TextBox txtUrlMail;
        private System.Windows.Forms.TextBox txtUrlAttachment;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtUrlMailRead;
        private System.Windows.Forms.Button btnMarkAsRead;
        private System.Windows.Forms.GroupBox gbSendMail;
        private System.Windows.Forms.Button btnSendMail;
        private System.Windows.Forms.TextBox txtBody;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.TextBox txtMailTo;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnHelpDrafts;
        private System.Windows.Forms.Button btnHelpInbox;
        private System.Windows.Forms.Button btnHelpPassword;
        private System.Windows.Forms.Button btnHelpAlias;
        private System.Windows.Forms.Button btnHelpUserName;
        private System.Windows.Forms.Button btnHelpExchange;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnCopyClipboard;
        private System.Windows.Forms.Button btnGetContacts;
        private System.Windows.Forms.TextBox txtContactsLike;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox TxtDownloadAttach;
        private System.Windows.Forms.Button BtnDownload;
    }
}

