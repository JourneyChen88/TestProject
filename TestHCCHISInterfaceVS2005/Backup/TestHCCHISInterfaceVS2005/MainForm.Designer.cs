namespace TestHCCHISInterfaceVS2005
{
  partial class MainForm
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
      this.lblttt = new System.Windows.Forms.Label();
      this.txtLog = new System.Windows.Forms.TextBox();
      this.cmdClearLog = new System.Windows.Forms.Button();
      this.cmdOpenCloseDevice = new System.Windows.Forms.Button();
      this.cmdGetCardInfo = new System.Windows.Forms.Button();
      this.cmdGetPersonInfo = new System.Windows.Forms.Button();
      this.cmdDivide = new System.Windows.Forms.Button();
      this.cmdTrade = new System.Windows.Forms.Button();
      this.cmdRePrintInvoice = new System.Windows.Forms.Button();
      this.cmdRefundmentDivide = new System.Windows.Forms.Button();
      this.cmdMedicareQuery = new System.Windows.Forms.Button();
      this.cndCommitTradeState = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // lblttt
      // 
      this.lblttt.AutoSize = true;
      this.lblttt.Location = new System.Drawing.Point(12, 9);
      this.lblttt.Name = "lblttt";
      this.lblttt.Size = new System.Drawing.Size(55, 15);
      this.lblttt.TabIndex = 0;
      this.lblttt.Text = "日志信息";
      // 
      // txtLog
      // 
      this.txtLog.Location = new System.Drawing.Point(12, 27);
      this.txtLog.Multiline = true;
      this.txtLog.Name = "txtLog";
      this.txtLog.ReadOnly = true;
      this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtLog.Size = new System.Drawing.Size(396, 403);
      this.txtLog.TabIndex = 1;
      this.txtLog.WordWrap = false;
      // 
      // cmdClearLog
      // 
      this.cmdClearLog.Location = new System.Drawing.Point(414, 33);
      this.cmdClearLog.Name = "cmdClearLog";
      this.cmdClearLog.Size = new System.Drawing.Size(108, 29);
      this.cmdClearLog.TabIndex = 2;
      this.cmdClearLog.Text = "清除日志信息";
      this.cmdClearLog.UseVisualStyleBackColor = true;
      this.cmdClearLog.Click += new System.EventHandler(this.cmdClearLog_Click);
      // 
      // cmdOpenCloseDevice
      // 
      this.cmdOpenCloseDevice.Location = new System.Drawing.Point(414, 68);
      this.cmdOpenCloseDevice.Name = "cmdOpenCloseDevice";
      this.cmdOpenCloseDevice.Size = new System.Drawing.Size(108, 29);
      this.cmdOpenCloseDevice.TabIndex = 3;
      this.cmdOpenCloseDevice.Text = "初始化/关闭设备";
      this.cmdOpenCloseDevice.UseVisualStyleBackColor = true;
      this.cmdOpenCloseDevice.Click += new System.EventHandler(this.cmdOpenCloseDevice_Click);
      // 
      // cmdGetCardInfo
      // 
      this.cmdGetCardInfo.Location = new System.Drawing.Point(414, 103);
      this.cmdGetCardInfo.Name = "cmdGetCardInfo";
      this.cmdGetCardInfo.Size = new System.Drawing.Size(108, 29);
      this.cmdGetCardInfo.TabIndex = 4;
      this.cmdGetCardInfo.Text = "获取卡片信息";
      this.cmdGetCardInfo.UseVisualStyleBackColor = true;
      this.cmdGetCardInfo.Click += new System.EventHandler(this.cmdGetCardInfo_Click);
      // 
      // cmdGetPersonInfo
      // 
      this.cmdGetPersonInfo.Location = new System.Drawing.Point(414, 138);
      this.cmdGetPersonInfo.Name = "cmdGetPersonInfo";
      this.cmdGetPersonInfo.Size = new System.Drawing.Size(108, 29);
      this.cmdGetPersonInfo.TabIndex = 5;
      this.cmdGetPersonInfo.Text = "获取个人信息";
      this.cmdGetPersonInfo.UseVisualStyleBackColor = true;
      this.cmdGetPersonInfo.Click += new System.EventHandler(this.cmdGetPersonInfo_Click);
      // 
      // cmdDivide
      // 
      this.cmdDivide.Location = new System.Drawing.Point(414, 173);
      this.cmdDivide.Name = "cmdDivide";
      this.cmdDivide.Size = new System.Drawing.Size(108, 29);
      this.cmdDivide.TabIndex = 6;
      this.cmdDivide.Text = "费用分解";
      this.cmdDivide.UseVisualStyleBackColor = true;
      this.cmdDivide.Click += new System.EventHandler(this.cmdDivide_Click);
      // 
      // cmdTrade
      // 
      this.cmdTrade.Location = new System.Drawing.Point(414, 208);
      this.cmdTrade.Name = "cmdTrade";
      this.cmdTrade.Size = new System.Drawing.Size(108, 29);
      this.cmdTrade.TabIndex = 7;
      this.cmdTrade.Text = "门诊交易确认";
      this.cmdTrade.UseVisualStyleBackColor = true;
      this.cmdTrade.Click += new System.EventHandler(this.cmdTrade_Click);
      // 
      // cmdRePrintInvoice
      // 
      this.cmdRePrintInvoice.Location = new System.Drawing.Point(414, 243);
      this.cmdRePrintInvoice.Name = "cmdRePrintInvoice";
      this.cmdRePrintInvoice.Size = new System.Drawing.Size(108, 29);
      this.cmdRePrintInvoice.TabIndex = 8;
      this.cmdRePrintInvoice.Text = "收据重打";
      this.cmdRePrintInvoice.UseVisualStyleBackColor = true;
      this.cmdRePrintInvoice.Click += new System.EventHandler(this.cmdRePrintInvoice_Click);
      // 
      // cmdRefundmentDivide
      // 
      this.cmdRefundmentDivide.Location = new System.Drawing.Point(414, 278);
      this.cmdRefundmentDivide.Name = "cmdRefundmentDivide";
      this.cmdRefundmentDivide.Size = new System.Drawing.Size(108, 29);
      this.cmdRefundmentDivide.TabIndex = 9;
      this.cmdRefundmentDivide.Text = "退费分解";
      this.cmdRefundmentDivide.UseVisualStyleBackColor = true;
      this.cmdRefundmentDivide.Click += new System.EventHandler(this.cmdRefundmentDivide_Click);
      // 
      // cmdMedicareQuery
      // 
      this.cmdMedicareQuery.Location = new System.Drawing.Point(414, 348);
      this.cmdMedicareQuery.Name = "cmdMedicareQuery";
      this.cmdMedicareQuery.Size = new System.Drawing.Size(108, 29);
      this.cmdMedicareQuery.TabIndex = 11;
      this.cmdMedicareQuery.Text = "医保查询";
      this.cmdMedicareQuery.UseVisualStyleBackColor = true;
      this.cmdMedicareQuery.Click += new System.EventHandler(this.cmdMedicareQuery_Click);
      // 
      // cndCommitTradeState
      // 
      this.cndCommitTradeState.Location = new System.Drawing.Point(414, 313);
      this.cndCommitTradeState.Name = "cndCommitTradeState";
      this.cndCommitTradeState.Size = new System.Drawing.Size(108, 29);
      this.cndCommitTradeState.TabIndex = 10;
      this.cndCommitTradeState.Text = "交易查询/回退";
      this.cndCommitTradeState.UseVisualStyleBackColor = true;
      this.cndCommitTradeState.Click += new System.EventHandler(this.cndCommitTradeState_Click);
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(534, 442);
      this.Controls.Add(this.cndCommitTradeState);
      this.Controls.Add(this.cmdMedicareQuery);
      this.Controls.Add(this.cmdRefundmentDivide);
      this.Controls.Add(this.cmdRePrintInvoice);
      this.Controls.Add(this.cmdTrade);
      this.Controls.Add(this.cmdDivide);
      this.Controls.Add(this.cmdGetPersonInfo);
      this.Controls.Add(this.cmdGetCardInfo);
      this.Controls.Add(this.cmdOpenCloseDevice);
      this.Controls.Add(this.cmdClearLog);
      this.Controls.Add(this.txtLog);
      this.Controls.Add(this.lblttt);
      this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "MainForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "门诊实时结算HIS调用接口示例";
      this.Load += new System.EventHandler(this.MainForm_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblttt;
    private System.Windows.Forms.TextBox txtLog;
    private System.Windows.Forms.Button cmdClearLog;
    private System.Windows.Forms.Button cmdOpenCloseDevice;
    private System.Windows.Forms.Button cmdGetCardInfo;
    private System.Windows.Forms.Button cmdGetPersonInfo;
    private System.Windows.Forms.Button cmdDivide;
    private System.Windows.Forms.Button cmdTrade;
    private System.Windows.Forms.Button cmdRePrintInvoice;
    private System.Windows.Forms.Button cmdRefundmentDivide;
    private System.Windows.Forms.Button cmdMedicareQuery;
    private System.Windows.Forms.Button cndCommitTradeState;
  }
}

