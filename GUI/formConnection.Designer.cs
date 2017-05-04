namespace GUI
{
    partial class FormConnection
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtBoxConnectionStr = new System.Windows.Forms.TextBox();
            this.grpBoxConnectionType = new System.Windows.Forms.GroupBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxDbName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxServerName = new System.Windows.Forms.TextBox();
            this.rdoBtnConnectionType1 = new System.Windows.Forms.RadioButton();
            this.rdoBtnConnectionType0 = new System.Windows.Forms.RadioButton();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpBoxConnectionType.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnConnect.Location = new System.Drawing.Point(536, 197);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(95, 25);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Подключиться";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtBoxConnectionStr
            // 
            this.txtBoxConnectionStr.Location = new System.Drawing.Point(26, 42);
            this.txtBoxConnectionStr.Name = "txtBoxConnectionStr";
            this.txtBoxConnectionStr.Size = new System.Drawing.Size(688, 20);
            this.txtBoxConnectionStr.TabIndex = 3;
            this.txtBoxConnectionStr.Text = "Data Source=RFlarson-PC\\SQLEXPRESS;Initial Catalog=Integr_lab;Integrated Security" +
    "=SSPI;";
            this.txtBoxConnectionStr.TextChanged += new System.EventHandler(this.txtBoxConnectionStr_TextChanged);
            this.txtBoxConnectionStr.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBoxConnectionStr_KeyDown);
            // 
            // grpBoxConnectionType
            // 
            this.grpBoxConnectionType.Controls.Add(this.textBoxPassword);
            this.grpBoxConnectionType.Controls.Add(this.label4);
            this.grpBoxConnectionType.Controls.Add(this.textBoxUserName);
            this.grpBoxConnectionType.Controls.Add(this.label3);
            this.grpBoxConnectionType.Controls.Add(this.textBoxDbName);
            this.grpBoxConnectionType.Controls.Add(this.label2);
            this.grpBoxConnectionType.Controls.Add(this.label1);
            this.grpBoxConnectionType.Controls.Add(this.textBoxServerName);
            this.grpBoxConnectionType.Controls.Add(this.rdoBtnConnectionType1);
            this.grpBoxConnectionType.Controls.Add(this.txtBoxConnectionStr);
            this.grpBoxConnectionType.Controls.Add(this.rdoBtnConnectionType0);
            this.grpBoxConnectionType.Location = new System.Drawing.Point(12, 12);
            this.grpBoxConnectionType.Name = "grpBoxConnectionType";
            this.grpBoxConnectionType.Size = new System.Drawing.Size(720, 179);
            this.grpBoxConnectionType.TabIndex = 4;
            this.grpBoxConnectionType.TabStop = false;
            this.grpBoxConnectionType.Text = "Способ подключения";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(135, 149);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(227, 20);
            this.textBoxPassword.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 152);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Пароль:";
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.Location = new System.Drawing.Point(135, 122);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(227, 20);
            this.textBoxUserName.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Имя пользователя:";
            // 
            // textBoxDbName
            // 
            this.textBoxDbName.Location = new System.Drawing.Point(458, 93);
            this.textBoxDbName.Name = "textBoxDbName";
            this.textBoxDbName.Size = new System.Drawing.Size(256, 20);
            this.textBoxDbName.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(378, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "База данных:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Имя сервера:";
            // 
            // textBoxServerName
            // 
            this.textBoxServerName.Location = new System.Drawing.Point(106, 93);
            this.textBoxServerName.Name = "textBoxServerName";
            this.textBoxServerName.Size = new System.Drawing.Size(256, 20);
            this.textBoxServerName.TabIndex = 5;
            // 
            // rdoBtnConnectionType1
            // 
            this.rdoBtnConnectionType1.AutoSize = true;
            this.rdoBtnConnectionType1.Location = new System.Drawing.Point(6, 68);
            this.rdoBtnConnectionType1.Name = "rdoBtnConnectionType1";
            this.rdoBtnConnectionType1.Size = new System.Drawing.Size(149, 17);
            this.rdoBtnConnectionType1.TabIndex = 4;
            this.rdoBtnConnectionType1.Text = "Авторизация SQL Server";
            this.rdoBtnConnectionType1.UseVisualStyleBackColor = true;
            // 
            // rdoBtnConnectionType0
            // 
            this.rdoBtnConnectionType0.AutoSize = true;
            this.rdoBtnConnectionType0.Checked = true;
            this.rdoBtnConnectionType0.Location = new System.Drawing.Point(6, 19);
            this.rdoBtnConnectionType0.Name = "rdoBtnConnectionType0";
            this.rdoBtnConnectionType0.Size = new System.Drawing.Size(131, 17);
            this.rdoBtnConnectionType0.TabIndex = 3;
            this.rdoBtnConnectionType0.TabStop = true;
            this.rdoBtnConnectionType0.Text = "Строка подключения";
            this.rdoBtnConnectionType0.UseVisualStyleBackColor = true;
            this.rdoBtnConnectionType0.CheckedChanged += new System.EventHandler(this.rdoBtnConnectionType0_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(637, 197);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(95, 25);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FormConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(740, 231);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.grpBoxConnectionType);
            this.Controls.Add(this.btnConnect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormConnection";
            this.Text = "Подключение к базе данных";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formConnection_FormClosing);
            this.Load += new System.EventHandler(this.FormConnection_Load);
            this.grpBoxConnectionType.ResumeLayout(false);
            this.grpBoxConnectionType.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtBoxConnectionStr;
        private System.Windows.Forms.GroupBox grpBoxConnectionType;
        private System.Windows.Forms.RadioButton rdoBtnConnectionType1;
        private System.Windows.Forms.RadioButton rdoBtnConnectionType0;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxDbName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxServerName;
    }
}

