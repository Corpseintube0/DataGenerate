namespace GUI
{
    partial class formConnection
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
            this.rdoBtnConnectionType1 = new System.Windows.Forms.RadioButton();
            this.rdoBtnConnectionType0 = new System.Windows.Forms.RadioButton();
            this.grpBoxConnectionType.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(628, 179);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(104, 23);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Подключение";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtBoxConnectionStr
            // 
            this.txtBoxConnectionStr.Location = new System.Drawing.Point(97, 18);
            this.txtBoxConnectionStr.Name = "txtBoxConnectionStr";
            this.txtBoxConnectionStr.Size = new System.Drawing.Size(617, 20);
            this.txtBoxConnectionStr.TabIndex = 3;
            this.txtBoxConnectionStr.Text = "Data Source=CORPSEPC\\SQLEXPRESS;Initial Catalog=testGenBD;Integrated Security=SSP" +
    "I;";
            // 
            // grpBoxConnectionType
            // 
            this.grpBoxConnectionType.Controls.Add(this.rdoBtnConnectionType1);
            this.grpBoxConnectionType.Controls.Add(this.txtBoxConnectionStr);
            this.grpBoxConnectionType.Controls.Add(this.rdoBtnConnectionType0);
            this.grpBoxConnectionType.Location = new System.Drawing.Point(12, 38);
            this.grpBoxConnectionType.Name = "grpBoxConnectionType";
            this.grpBoxConnectionType.Size = new System.Drawing.Size(720, 135);
            this.grpBoxConnectionType.TabIndex = 4;
            this.grpBoxConnectionType.TabStop = false;
            this.grpBoxConnectionType.Text = "Тип подключения";
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
            this.ClientSize = new System.Drawing.Size(744, 241);
            this.Controls.Add(this.grpBoxConnectionType);
            this.Controls.Add(this.btnConnect);
            this.Name = "formConnection";
            this.Text = "Подключение";
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
    }
}

