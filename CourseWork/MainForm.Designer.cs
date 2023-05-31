namespace CourseWork
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.менюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.инофрмацияОПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GoodsOpenLink = new System.Windows.Forms.LinkLabel();
            this.WorkersOpenLink = new System.Windows.Forms.LinkLabel();
            this.SellLink = new System.Windows.Forms.LinkLabel();
            this.MainMenuGropBox = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.MainMenuGropBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.менюToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(834, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // менюToolStripMenuItem
            // 
            this.менюToolStripMenuItem.BackColor = System.Drawing.SystemColors.MenuBar;
            this.менюToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.инофрмацияОПрограммеToolStripMenuItem,
            this.выходToolStripMenuItem});
            this.менюToolStripMenuItem.Name = "менюToolStripMenuItem";
            this.менюToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.менюToolStripMenuItem.Text = "Меню";
            // 
            // инофрмацияОПрограммеToolStripMenuItem
            // 
            this.инофрмацияОПрограммеToolStripMenuItem.Name = "инофрмацияОПрограммеToolStripMenuItem";
            this.инофрмацияОПрограммеToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.инофрмацияОПрограммеToolStripMenuItem.Text = "Информация";
            this.инофрмацияОПрограммеToolStripMenuItem.Click += new System.EventHandler(this.инофрмацияОПрограммеToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // GoodsOpenLink
            // 
            this.GoodsOpenLink.ActiveLinkColor = System.Drawing.Color.Firebrick;
            this.GoodsOpenLink.AutoSize = true;
            this.GoodsOpenLink.BackColor = System.Drawing.SystemColors.Menu;
            this.GoodsOpenLink.Font = new System.Drawing.Font("Calibri", 18F);
            this.GoodsOpenLink.LinkColor = System.Drawing.SystemColors.InfoText;
            this.GoodsOpenLink.Location = new System.Drawing.Point(5, 49);
            this.GoodsOpenLink.Name = "GoodsOpenLink";
            this.GoodsOpenLink.Size = new System.Drawing.Size(252, 29);
            this.GoodsOpenLink.TabIndex = 8;
            this.GoodsOpenLink.TabStop = true;
            this.GoodsOpenLink.Text = "Учёт товаров на складе";
            this.GoodsOpenLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.GoodsOpenLink_LinkClicked);
            // 
            // WorkersOpenLink
            // 
            this.WorkersOpenLink.ActiveLinkColor = System.Drawing.Color.Firebrick;
            this.WorkersOpenLink.AutoSize = true;
            this.WorkersOpenLink.BackColor = System.Drawing.SystemColors.MenuBar;
            this.WorkersOpenLink.Font = new System.Drawing.Font("Calibri", 18F);
            this.WorkersOpenLink.LinkColor = System.Drawing.SystemColors.InfoText;
            this.WorkersOpenLink.Location = new System.Drawing.Point(5, 89);
            this.WorkersOpenLink.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.WorkersOpenLink.Name = "WorkersOpenLink";
            this.WorkersOpenLink.Size = new System.Drawing.Size(197, 29);
            this.WorkersOpenLink.TabIndex = 9;
            this.WorkersOpenLink.TabStop = true;
            this.WorkersOpenLink.Text = "Работники склада";
            this.WorkersOpenLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.WorkersOpenLink_LinkClicked);
            // 
            // SellLink
            // 
            this.SellLink.ActiveLinkColor = System.Drawing.Color.Firebrick;
            this.SellLink.AutoSize = true;
            this.SellLink.BackColor = System.Drawing.SystemColors.Menu;
            this.SellLink.Font = new System.Drawing.Font("Calibri", 18F);
            this.SellLink.LinkColor = System.Drawing.SystemColors.InfoText;
            this.SellLink.Location = new System.Drawing.Point(5, 127);
            this.SellLink.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.SellLink.Name = "SellLink";
            this.SellLink.Size = new System.Drawing.Size(196, 29);
            this.SellLink.TabIndex = 10;
            this.SellLink.TabStop = true;
            this.SellLink.Text = "Продажа товаров";
            this.SellLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.SellLink_LinkClicked);
            // 
            // MainMenuGropBox
            // 
            this.MainMenuGropBox.BackColor = System.Drawing.SystemColors.MenuBar;
            this.MainMenuGropBox.Controls.Add(this.WorkersOpenLink);
            this.MainMenuGropBox.Controls.Add(this.SellLink);
            this.MainMenuGropBox.Controls.Add(this.GoodsOpenLink);
            this.MainMenuGropBox.Font = new System.Drawing.Font("Calibri", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MainMenuGropBox.Location = new System.Drawing.Point(21, 40);
            this.MainMenuGropBox.Name = "MainMenuGropBox";
            this.MainMenuGropBox.Size = new System.Drawing.Size(261, 379);
            this.MainMenuGropBox.TabIndex = 11;
            this.MainMenuGropBox.TabStop = false;
            this.MainMenuGropBox.Text = "Главное меню";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(-23, -46);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(834, 461);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.MainMenuGropBox);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(850, 500);
            this.Name = "MainForm";
            this.Text = "Inventory Manager";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.MainMenuGropBox.ResumeLayout(false);
            this.MainMenuGropBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem менюToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem инофрмацияОПрограммеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.LinkLabel GoodsOpenLink;
        private System.Windows.Forms.LinkLabel WorkersOpenLink;
        private System.Windows.Forms.LinkLabel SellLink;
        private System.Windows.Forms.GroupBox MainMenuGropBox;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

