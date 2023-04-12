namespace CourseWork
{
    partial class main_form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main_form));
            this.w_button = new System.Windows.Forms.Button();
            this.g_button = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // w_button
            // 
            this.w_button.BackColor = System.Drawing.Color.White;
            this.w_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.w_button.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.w_button.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.w_button.Location = new System.Drawing.Point(19, 83);
            this.w_button.Name = "w_button";
            this.w_button.Size = new System.Drawing.Size(191, 55);
            this.w_button.TabIndex = 0;
            this.w_button.Text = "Работники склада";
            this.w_button.UseVisualStyleBackColor = false;
            this.w_button.Click += new System.EventHandler(this.w_button_Click);
            // 
            // g_button
            // 
            this.g_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.g_button.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.g_button.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.g_button.Location = new System.Drawing.Point(19, 22);
            this.g_button.Name = "g_button";
            this.g_button.Size = new System.Drawing.Size(191, 55);
            this.g_button.TabIndex = 1;
            this.g_button.Text = "Учёт товаров на складе";
            this.g_button.UseVisualStyleBackColor = true;
            this.g_button.Click += new System.EventHandler(this.g_button_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(269, 31);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(262, 263);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // main_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OldLace;
            this.ClientSize = new System.Drawing.Size(572, 323);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.g_button);
            this.Controls.Add(this.w_button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "main_form";
            this.Text = "InventoryManager";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button w_button;
        private System.Windows.Forms.Button g_button;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

