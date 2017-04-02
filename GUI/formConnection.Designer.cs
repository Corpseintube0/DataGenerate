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
            this.btnCancel = new System.Windows.Forms.Button();
            this.rdoBtnConnectionType1 = new System.Windows.Forms.RadioButton();
            this.rdoBtnConnectionType0 = new System.Windows.Forms.RadioButton();
            this.grpBoxConnectionType.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(487, 104);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(95, 25);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Подключиться";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtBoxConnectionStr
            // 
            this.txtBoxConnectionStr.Location = new System.Drawing.Point(97, 18);
            this.txtBoxConnectionStr.Name = "txtBoxConnectionStr";
            this.txtBoxConnectionStr.Size = new System.Drawing.Size(617, 20);
            this.txtBoxConnectionStr.TabIndex = 3;
            this.txtBoxConnectionStr.Text = "Data Source=RFlarson-PC\\SQLEXPRESS;Initial Catalog=integr_lab;Integrated Security" +
    "=SSPI;";
            // 
            // grpBoxConnectionType
            // 
            this.grpBoxConnectionType.Controls.Add(this.btnCancel);
            this.grpBoxConnectionType.Controls.Add(this.rdoBtnConnectionType1);
            this.grpBoxConnectionType.Controls.Add(this.btnConnect);
            this.grpBoxConnectionType.Controls.Add(this.txtBoxConnectionStr);
            this.grpBoxConnectionType.Controls.Add(this.rdoBtnConnectionType0);
            this.grpBoxConnectionType.Location = new System.Drawing.Point(12, 12);
            this.grpBoxConnectionType.Name = "grpBoxConnectionType";
            this.grpBoxConnectionType.Size = new System.Drawing.Size(720, 154);
            this.grpBoxConnectionType.TabIndex = 4;
            this.grpBoxConnectionType.TabStop = false;
            this.grpBoxConnectionType.Text = "Тип подключения";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(588, 104);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(95, 25);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // rdoBtnConnectionType1
            // 
            this.rdoBtnConnectionType1.AutoSize = true;
            this.rdoBtnConnectionType1.Location = new System.Drawing.Point(6, 52);
            this.rdoBtnConnectionType1.Name = "rdoBtnConnectionType1";
            this.rdoBtnConnectionType1.Size = new System.Drawing.Size(81, 17);
            this.rdoBtnConnectionType1.TabIndex = 4;
            this.rdoBtnConnectionType1.Text = "Удаленное";
            this.rdoBtnConnectionType1.UseVisualStyleBackColor = true;
            // 
            // rdoBtnConnectionType0
            // 
            this.rdoBtnConnectionType0.AutoSize = true;
            this.rdoBtnConnectionType0.Checked = true;
            this.rdoBtnConnectionType0.Location = new System.Drawing.Point(6, 19);
            this.rdoBtnConnectionType0.Name = "rdoBtnConnectionType0";
            this.rdoBtnConnectionType0.Size = new System.Drawing.Size(81, 17);
            this.rdoBtnConnectionType0.TabIndex = 3;
            this.rdoBtnConnectionType0.TabStop = true;
            this.rdoBtnConnectionType0.Text = "Локальное";
            this.rdoBtnConnectionType0.UseVisualStyleBackColor = true;
            // 
            // formConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 170);
            this.Controls.Add(this.grpBoxConnectionType);
            this.Name = "formConnection";
            this.Text = "Подключение к базе данных";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formConnection_FormClosing);
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
    }
}

