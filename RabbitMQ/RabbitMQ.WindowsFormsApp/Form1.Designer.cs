namespace RabbitMQ.WindowsFormsApp
{
    partial class FmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btn1 = new System.Windows.Forms.Button();
            this.btn2 = new System.Windows.Forms.Button();
            this.btn3 = new System.Windows.Forms.Button();
            this.txtSysMessage = new System.Windows.Forms.TextBox();
            this.btn4 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lbBindings = new System.Windows.Forms.ListBox();
            this.label37 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbQueues = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbUser = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbPrize = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbPrizeUser = new System.Windows.Forms.Label();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn1
            // 
            this.btn1.Location = new System.Drawing.Point(25, 37);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(142, 33);
            this.btn1.TabIndex = 0;
            this.btn1.Text = "投放奖品";
            this.btn1.UseVisualStyleBackColor = true;
            this.btn1.Click += new System.EventHandler(this.btn1_Click);
            // 
            // btn2
            // 
            this.btn2.Location = new System.Drawing.Point(25, 129);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(142, 33);
            this.btn2.TabIndex = 1;
            this.btn2.Text = "模拟多用户页面请求";
            this.btn2.UseVisualStyleBackColor = true;
            this.btn2.Click += new System.EventHandler(this.btn2_Click);
            // 
            // btn3
            // 
            this.btn3.Location = new System.Drawing.Point(25, 313);
            this.btn3.Name = "btn3";
            this.btn3.Size = new System.Drawing.Size(142, 34);
            this.btn3.TabIndex = 2;
            this.btn3.Text = "模拟多用户抽奖";
            this.btn3.UseVisualStyleBackColor = true;
            this.btn3.Click += new System.EventHandler(this.btn3_Click);
            // 
            // txtSysMessage
            // 
            this.txtSysMessage.Location = new System.Drawing.Point(173, 12);
            this.txtSysMessage.Multiline = true;
            this.txtSysMessage.Name = "txtSysMessage";
            this.txtSysMessage.ReadOnly = true;
            this.txtSysMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSysMessage.Size = new System.Drawing.Size(872, 335);
            this.txtSysMessage.TabIndex = 3;
            this.txtSysMessage.TextChanged += new System.EventHandler(this.txtSysMessage_TextChanged);
            // 
            // btn4
            // 
            this.btn4.Location = new System.Drawing.Point(25, 211);
            this.btn4.Name = "btn4";
            this.btn4.Size = new System.Drawing.Size(142, 37);
            this.btn4.TabIndex = 4;
            this.btn4.Text = "停止所有用户访问";
            this.btn4.UseVisualStyleBackColor = true;
            this.btn4.Click += new System.EventHandler(this.btn4_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 492);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "清空消息";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel5
            // 
            this.panel5.AccessibleName = "";
            this.panel5.AllowDrop = true;
            this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.lbBindings);
            this.panel5.Controls.Add(this.label37);
            this.panel5.Location = new System.Drawing.Point(369, 401);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(408, 166);
            this.panel5.TabIndex = 47;
            // 
            // lbBindings
            // 
            this.lbBindings.FormattingEnabled = true;
            this.lbBindings.ItemHeight = 12;
            this.lbBindings.Location = new System.Drawing.Point(14, 28);
            this.lbBindings.Name = "lbBindings";
            this.lbBindings.Size = new System.Drawing.Size(389, 124);
            this.lbBindings.TabIndex = 39;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(26, 7);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(53, 12);
            this.label37.TabIndex = 21;
            this.label37.Text = "绑定关系";
            // 
            // panel2
            // 
            this.panel2.AccessibleName = "";
            this.panel2.AllowDrop = true;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lbQueues);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Location = new System.Drawing.Point(174, 398);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(189, 169);
            this.panel2.TabIndex = 48;
            // 
            // lbQueues
            // 
            this.lbQueues.FormattingEnabled = true;
            this.lbQueues.ItemHeight = 12;
            this.lbQueues.Location = new System.Drawing.Point(14, 32);
            this.lbQueues.Name = "lbQueues";
            this.lbQueues.Size = new System.Drawing.Size(153, 124);
            this.lbQueues.TabIndex = 39;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 21;
            this.label7.Text = "队列操作";
            // 
            // panel1
            // 
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lbPrizeUser);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lbUser);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lbPrize);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(804, 401);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(241, 166);
            this.panel1.TabIndex = 49;
            // 
            // lbUser
            // 
            this.lbUser.AutoSize = true;
            this.lbUser.ForeColor = System.Drawing.Color.Maroon;
            this.lbUser.Location = new System.Drawing.Point(138, 76);
            this.lbUser.Name = "lbUser";
            this.lbUser.Size = new System.Drawing.Size(11, 12);
            this.lbUser.TabIndex = 3;
            this.lbUser.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(49, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "参与人数：";
            // 
            // lbPrize
            // 
            this.lbPrize.AutoSize = true;
            this.lbPrize.ForeColor = System.Drawing.Color.Maroon;
            this.lbPrize.Location = new System.Drawing.Point(138, 27);
            this.lbPrize.Name = "lbPrize";
            this.lbPrize.Size = new System.Drawing.Size(11, 12);
            this.lbPrize.TabIndex = 1;
            this.lbPrize.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "奖品剩余：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "中奖人数：";
            // 
            // lbPrizeUser
            // 
            this.lbPrizeUser.AutoSize = true;
            this.lbPrizeUser.ForeColor = System.Drawing.Color.Maroon;
            this.lbPrizeUser.Location = new System.Drawing.Point(140, 121);
            this.lbPrizeUser.Name = "lbPrizeUser";
            this.lbPrizeUser.Size = new System.Drawing.Size(11, 12);
            this.lbPrizeUser.TabIndex = 5;
            this.lbPrizeUser.Text = "0";
            // 
            // FmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1057, 572);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn4);
            this.Controls.Add(this.txtSysMessage);
            this.Controls.Add(this.btn3);
            this.Controls.Add(this.btn2);
            this.Controls.Add(this.btn1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "FmMain";
            this.ShowIcon = false;
            this.Text = "RabbitMQ抽奖系统";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FmMain_FormClosing);
            this.Load += new System.EventHandler(this.FmMain_Load);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn1;
        private System.Windows.Forms.Button btn2;
        private System.Windows.Forms.Button btn3;
        private System.Windows.Forms.TextBox txtSysMessage;
        private System.Windows.Forms.Button btn4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ListBox lbBindings;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListBox lbQueues;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbPrize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbUser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbPrizeUser;
    }
}

