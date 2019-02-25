using Autoentity.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Autoentity
{
    public partial class ToClassForm : Form
    {
        public ToClassForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            var resut = richTextBox1.Text.Split(',');
            foreach (string str in resut)
            {
                var level2 = str.Split('.');
                if (level2.Length > 1)
                {
                    sb.AppendFormat(@"
        /// <summary>
        /// 
        /// </summary>
        public string {0}  {{ get; set; }}

                ", level2[1]);
                }
                else
                {
                    sb.AppendFormat(@"
        /// <summary>
        /// 
        /// </summary>
        public string {0}  {{ get; set; }}

                ", level2[0]);
                }
            }

            richTextBox2.Text = sb.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable tableInfo = SQLServerHelper.QueryTableInfo(Program.ConStr, richTextBox1.Text);
            string table = string.Empty;
            var sqlTable = textBox1.Text.Split(',').ToArray();
            for (int i = 0; i < sqlTable.Length; i++)
            {
                table += $"'{sqlTable[i]}',";
            }
            table = table.Substring(0, table.Length - 1);
            string sql2 = $@"
                                        SELECT
                                        B.name AS OBJNAME,
                                        C.value AS VALUE
                                        FROM sys.tables A
                                        INNER JOIN sys.columns B ON B.object_id = A.object_id
                                        LEFT JOIN sys.extended_properties C 
                                        ON C.major_id = B.object_id AND C.minor_id = B.column_id
                                        WHERE A.name in ({table})";
            DataTable colsInfos = SQLServerHelper.QueryDataTable(Program.ConStr, sql2);
            StringBuilder codeString = new StringBuilder();
            foreach (DataRow dr in tableInfo.Rows)
            {
                var zhuShi = string.Empty;//列名注释
                foreach (DataRow uu in colsInfos.Rows)
                {
                    if (uu["OBJNAME"].ToString().ToUpper() == dr["ColumnName"].ToString().ToUpper())
                        zhuShi = uu["VALUE"].ToString();
                }
                if ((bool)dr["IsKey"] && !(bool)dr["IsIdentity"])
                {


                    {
                        codeString.Append($@"       
                                                                    /// <summary>
                                                                    /// -zhuShi-
                                                                    /// </summary>
                                                                    public -dbType- -colName- {{ get; set; }}
                                                            ");
                    }
                }
                else if ((bool)dr["IsKey"] && (bool)dr["IsIdentity"])
                {

                    {
                        codeString.Append($@"       
                                                                        /// <summary>
                                                                        /// -zhuShi-
                                                                        /// </summary>
                                                                        public -dbType- -colName- {{ get; set; }}
                                                                ");
                    }
                }
                else if (!(bool)dr["IsKey"] && (bool)dr["IsIdentity"])
                {

                    {
                        codeString.Append($@"       
                                                                        /// <summary>
                                                                        /// -zhuShi-
                                                                        /// </summary>
                                                                        public -dbType- -colName- {{ get; set; }}
                                                                ");
                    }
                }
                else
                {
                    codeString.Append($@"       
                                                                /// <summary>
                                                                /// -zhuShi-
                                                                /// </summary>
                                                                public -dbType- -colName- {{ get; set; }}
                                                        ");
                }
                Type ttttt = this.GetTypeByString(dr["DataType"].ToString());
                if (ttttt.IsValueType && dr["AllowDBNull"].ToString() == "True")
                {
                    codeString.Replace("-dbType-", dr["DataType"].ToString() + "?");  //替换数据类型
                 
                    {
                        codeString.Replace("-value-", "value");
                    }
                }
                else
                {
                    if (dr["DataType"].ToString() == "System.String")
                    {
                        codeString.Replace("-dbType-", dr["DataType"].ToString());  //替换数据类型
                       
                        {
                            codeString.Replace("-value-", "value");
                        }
                    }
                    else
                    {
                        codeString.Replace("-dbType-", dr["DataType"].ToString());  //替换数据类型
                        codeString.Replace("-value-", "value");
                    }
                }
                codeString.Replace("-colName-", dr["ColumnName"].ToString());  //替换列名（属性名）
                codeString.Replace("-zhuShi-", zhuShi);
            }
            richTextBox2.Text = codeString.ToString();
        }

        /// <summary>
        /// 获得类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        Type GetTypeByString(string type)
        {
            switch (type.ToLower())
            {
                case "system.boolean":
                    return Type.GetType("System.Boolean", true, true);
                case "system.byte":
                    return Type.GetType("System.Byte", true, true);
                case "system.sbyte":
                    return Type.GetType("System.SByte", true, true);
                case "system.char":
                    return Type.GetType("System.Char", true, true);
                case "system.decimal":
                    return Type.GetType("System.Decimal", true, true);
                case "system.double":
                    return Type.GetType("System.Double", true, true);
                case "system.single":
                    return Type.GetType("System.Single", true, true);
                case "system.int32":
                    return Type.GetType("System.Int32", true, true);
                case "system.uint32":
                    return Type.GetType("System.UInt32", true, true);
                case "system.int64":
                    return Type.GetType("System.Int64", true, true);
                case "system.uint64":
                    return Type.GetType("System.UInt64", true, true);
                case "system.object":
                    return Type.GetType("System.Object", true, true);
                case "system.int16":
                    return Type.GetType("System.Int16", true, true);
                case "system.uint16":
                    return Type.GetType("System.UInt16", true, true);
                case "system.string":
                    return Type.GetType("System.String", true, true);
                case "system.datetime":
                case "datetime":
                    return Type.GetType("System.DateTime", true, true);
                case "system.guid":
                    return Type.GetType("System.Guid", true, true);
                default:
                    return Type.GetType(type, true, true);
            }
        }
    }
}


