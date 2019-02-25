using HisService.DrSchedule;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace HisProxy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strGuid = "12";

            var res = Convert.ToDecimal(string.Format("{0:F2}", strGuid));
            VisitScheduleDt entity = new VisitScheduleDt();
            //entity.ScheduleNo
            //var ScheduleNo = context.Insert<VisitScheduleMt>("VisitScheduleMt", entity)
            //                                .AutoMap(x => x.ScheduleNo)
            //                                .ExecuteReturnLastId<Int32>("ScheduleNo");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //richTextBox1.Text = GetMacAddress();
        }
    }
}
