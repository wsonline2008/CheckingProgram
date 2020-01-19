using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Odbc;
using System.Data.OleDb;
using IniFile;
using System.Collections;
using System.IO;

namespace CheckingProgram
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }
        private string connetStr;
        private string appDir;
        private string configDir;
        Hashtable hasInfo = new Hashtable();
        private void FormMain_Load(object sender, EventArgs e)
        {
            string[] layerName = {"CM", "INV_PMS", "INV_PG", "CM_PMS", "CM_PG"
                                    , "SP", "INV_HRS", "INV_HUS","INV_ELE_BEARING"
                                    , "INV_ELE_JOINT", "INV_ELE_FD", "INV_ELE_INVERT", "INV_ELE_ABUT"
                                    , "INV_ELE_COL", "INV_ELE_GIRD", "INV_ELE_DIAP", "INV_ELE_SOFFIT"
                                    , "INV_ELE_DECK", "INV_ELE_FLOOR", "INV_ELE_RAMPS", "INV_ELE_TOWER"
                                    , "INV_ELE_DRAIN", "INV_ELE_ROOF", "INV_ELE_FINISH", "ST"
                                    , "INV_RAIL", "INV_BF", "INV_CC", "INV_EG"
                                    , "INV_NB", "RB", "RAIL_STR", "PARAPET"
                                    , "INV_GR", "INV_PIPE", "INV_GS", "INV_MH"
                                    , "INV_CP", "RD","INV_TS", "TS", "MGID_TP"
                                    , "INV_VEG", "VEG","INV_SNP", "INV_VS"
                                    , "SNP_VS","INV_TP", "TP","INV_DK"
                                    , "DK","INV_EMPIT", "EMPIT","INV_PL"
                                    , "INV_MC", "MC","INV_T_ELE_DR", "INV_T_ELE_JT"
                                    , "INV_T_ELE_CO", "INV_T_ELE_CP", "INV_T_ELE_CR", "INV_T_ELE_CU"
                                    , "INV_T_ELE_FC", "INV_T_ELE_KB", "INV_T_ELE_PS", "INV_T_ELE_PW"
                                    , "INV_T_ELE_VP", "TU","INV_RPW", "RPW"
                                    , "INV_TREE"};
            string[] idName = {"CM_ID", "SECTION_LA", "PG_ID", "CM_ID", "CM_ID"
                                    , "SP_ID", "SLOPEID", "HYD_NO","STR_ELE_NO"
                                    , "STR_ELE_NO", "STR_ELE_NO", "STR_ELE_NO", "STR_ELE_NO"
                                    , "STR_ELE_NO", "STR_ELE_NO", "STR_ELE_NO", "STR_ELE_NO"
                                    , "STR_ELE_NO", "STR_ELE_NO", "STR_ELE_NO", "STR_ELE_NO"
                                    , "STR_ELE_NO", "STR_ELE_NO", "STR_ELE_NO", "STR_ELE_NO"
                                    , "RAIL_ID", "BARRIER_ID", "CC_ID", "EG_ID"
                                    , "NB_ID", "RB_ID", "STR_ELE_NO", "STR_ELE_NO"
                                    , "DF_ID", "DF_ID", "DF_ID", "DF_ID"
                                    , "DF_ID", "RD_ID","PLATE_ID", "PLATE_ID", "PLATE_ID"
                                    , "VEG_ID", "VEG_ID","SNP_ID", "VS_ID"
                                    , "SNPVS_ID","TP_ID", "TP_ID","DK_ID"
                                    , "DK_ID","EM_ID", "EM_ID","PL_ID"
                                    , "MC_ID", "MC_ID","TS_ELE_NO", "TS_ELE_NO"
                                    , "TS_ELE_NO", "TS_ELE_NO", "TS_ELE_NO", "TS_ELE_NO"
                                    , "TS_ELE_NO", "TS_ELE_NO", "TS_ELE_NO", "TS_ELE_NO"
                                    , "TS_ELE_NO", "TS_ELE_NO","RPW_ID", "RPW_ID"
                                    , "TREE_ID"};

            for (int i = 0; i < layerName.GetLength(0); i++)
            {
                if (hasInfo[layerName[i]] == null && layerName[i] != "")
                    hasInfo.Add(layerName[i], idName[i]);
            }


            initCProject();
            appDir = System.AppDomain.CurrentDomain.BaseDirectory;
            ClsIniFile ini = new ClsIniFile(appDir + "\\Config.ini");
            string tempStr = ini.IniReadValue("Normal", "Project");
            if (!string.IsNullOrEmpty(tempStr))
                cProject.SelectedIndex = cProject.Items.IndexOf(tempStr);

            initCDriver();
            this.Text = cProject.SelectedItem.ToString() + " Checking Program";
            configDir = appDir + "\\" + cProject.SelectedItem.ToString() + "Config.ini";


            initCCheckItem();
            readConfig();
        }
        private void initCCheckItem()
        {
            cCheckItem.Items.Clear();
            switch (cProject.SelectedItem.ToString()) //获取选择的内容
            {
                case "01":
                    cCheckItem.Items.Add("(01) Check Contract No for ALL");//选择项1
                    cCheckItem.Items.Add("(02) Check WO no for ALL");
                    cCheckItem.Items.Add("(03) Check Coord for ALL");
                    cCheckItem.Items.Add("(04) Check UserID for ALL");
                    cCheckItem.SelectedIndex = 0;
                    break;
                case "09":

                    break;
                case "115":
                    cCheckItem.Items.Add("(01) Check Contract No for ALL");//选择项1
                    cCheckItem.Items.Add("(03) Check Coord for ALL");
                    cCheckItem.Items.Add("(07) Check Delete for ALL");
                    cCheckItem.Items.Add("(08) Check Delete for ALL");
                    cCheckItem.Items.Add("(09) Check Duplication for ALL");
                    cCheckItem.Items.Add("(10) Check Missing Inventory for ALL");
                    cCheckItem.Items.Add("(11) Check Version Problem for ALL1");
                    cCheckItem.Items.Add("(12) Check for max version and work date for ALL");
                    cCheckItem.Items.Add("(13) Check Street Code_name spelling");
                    cCheckItem.Items.Add("(14) Check feature type consistency for ALL");
                    cCheckItem.Items.Add("(15) Check work date for consistency for ALL");
                    cCheckItem.Items.Add("(16) Check street code consistency for ALL");
                    cCheckItem.Items.Add("(17) Check INV_GID Duplication");
                    cCheckItem.Items.Add("(18) Check MH GID Duplication");
                    cCheckItem.Items.Add("(19) Check P_User_ID ALL");
                    cCheckItem.Items.Add("(20) Check P_User_ID Delete ALL");
                    cCheckItem.Items.Add("(22) Check replace with remove for ALL");
                    cCheckItem.Items.Add("(23) Check MT_dt NO_His ALL");
                    cCheckItem.Items.Add("(24) Check Missing MGID in TS/MGID_TP");
                    cCheckItem.Items.Add("(25) Check Delete Indicator_ALL");
                    cCheckItem.Items.Add("(26) Check delete consistency for ALL");
                    cCheckItem.Items.Add("(27) Check install consistency for ALL");
                    cCheckItem.Items.Add("(28) Check Version Problem for ALL2");
                    cCheckItem.Items.Add("(29) Check Value");
                    cCheckItem.Items.Add("(30) Check Null Value 2");
                    cCheckItem.Items.Add("(31) Check SNP Old Plate");
                    cCheckItem.Items.Add("(32) Check CM_PMS CM_PG");

                    cCheckItem.SelectedIndex = 0;
                    break;
                case "116":

                    break;
            }
            
        }
        private void initCDriver()
        {
            cDriver.Items.Clear();
            cDriver.Items.Add("DBase");//选择项1
            cDriver.Items.Add("VfpDriver");
            cDriver.Items.Add("VfpOledb");
            //cDriver.SelectedIndex = 0;
        }
        private void initCProject()
        {
            cProject.Items.Clear();
            cProject.Items.Add("01");//选择项1
            cProject.Items.Add("09");
            cProject.Items.Add("115");
            //cProject.Items.Add("116");
            cProject.SelectedIndex = 0;
        }
        private void getData(string sql, DataTable dt)
        {
            DataTable dt1 = new DataTable();
            setConnetStr();
            switch (cDriver.SelectedItem.ToString()) //获取选择的内容
            {
                case "DBase":
                case "VfpDriver":
                    try
                    {
                        //需要 using System.Data.Odbc;
                        OdbcConnection ocConn = new OdbcConnection();

                        ocConn.ConnectionString = connetStr;
                        ocConn.Open();
                        OdbcDataAdapter oda = new OdbcDataAdapter(sql, ocConn);
                        oda.Fill(dt1);
                        if(dt1.Rows.Count>0)
                            oda.Fill(dt);
                        ocConn.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    break;
                case "VfpOledb":
                    try
                    {
                        //需要 using System.Data.OleDb 安装 VFPOLEDBSetup.msi
                        OleDbConnection oledbConn = new OleDbConnection();

                        oledbConn.ConnectionString = connetStr;
                        oledbConn.Open();
                        OleDbDataAdapter Odda = new OleDbDataAdapter(sql, oledbConn);
                        Odda.Fill(dt1);
                        if (dt1.Rows.Count > 0)
                            Odda.Fill(dt);
                        oledbConn.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    break;
            }
            
        }
        private void addNull(DataTable dt)
        {
            if (!dt.Columns.Contains("id"))
            {
                dt.Columns.Add("id");
            }
            //dt.PrimaryKey = new DataColumn[] { dt.Columns["ID"] };
            DataRow dr = dt.NewRow();
            dr["id"] = "null";
            dt.Rows.Add(dr);
        }
        private void setConnetStr(string sourceType = "dbf")
        {
            switch (cDriver.SelectedItem.ToString()) //获取选择的内容
            {
                case "DBase": setConnetStrDBase(appDir, sourceType); break;
                case "VfpDriver": setConnetStrVfpDriver(appDir, sourceType); break;
                case "VfpOledb": setConnetStrVfpOledb(appDir); break;
            }
        }
        //通过 Microsoft dBASE Driver 访问
        //需要 using System.Data.Odbc;
        private void setConnetStrDBase(string path, string sourceType= "dbf")
        {
            connetStr =
                @"Driver={Microsoft dBASE Driver (*."+ sourceType + ")}; SourceType=" + sourceType + "; " +
                @"Data Source=" + path +
                @"; Exclusive=No; NULL=NO; " +
                @"Collate=Machine; BACKGROUNDFETCH=NO; DELETE=NO";
        }

        //通过 Microsoft Visual FoxPro Driver 访问
        //需要 using System.Data.Odbc;
        private void setConnetStrVfpDriver(string path, string sourceType = "dbf")
        {
            connetStr =
                @"Driver={Microsoft Visual FoxPro Driver}; SourceType=" + sourceType + "; " +
                @"sourcedb=" + path +
                @"; BACKGROUNDFETCH=NO; DELETE=NO";
        }

        //通过 vfpoledb 访问
        //需要 using System.Data.OleDb 安装 VFPOLEDBSetup.msi
        private void setConnetStrVfpOledb(string path)
        {
            connetStr =
                @"Provider=vfpoledb; Data Source=" + path +
                @"; Collating Sequence=machine;";
        }

        private void CDriver_SelectedIndexChanged(object sender, EventArgs e)
        {
            setConnetStr();
            configDir = appDir + "\\" + cProject.SelectedItem.ToString() + "Config.ini";
            ClsIniFile ini = new ClsIniFile(configDir);
            ini.IniWriteValue("Normal", "cDriver", cDriver.SelectedItem.ToString());
        }

        private void Btn01_Click(object sender, EventArgs e)
        {

        }

        private void CProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            configDir = appDir + "\\" + cProject.SelectedItem.ToString() + "Config.ini";
            ClsIniFile ini = new ClsIniFile(appDir + "\\Config.ini");
            ini.IniWriteValue("Normal", "Project", cProject.SelectedItem.ToString());
            initCCheckItem();
        }
        private void readConfig()
        {
            ClsIniFile ini = new ClsIniFile(configDir);
            string tempStr = ini.IniReadValue("Normal", "cDriver");
            if(!string.IsNullOrEmpty(tempStr))
                cDriver.SelectedIndex = cDriver.Items.IndexOf(tempStr);

        }
        private void saveConfig()
        {

            ClsIniFile ini = new ClsIniFile(configDir);
            ini.IniWriteValue("Normal", "cDriver", cDriver.SelectedItem.ToString());

        }
        //筛选日期
        private void dateFilter()
        {
            if (!cDateFilter.Checked)
            {
                return;
            }
            bool hasSUB_DATE = false;
            for (int k = 0; k < dataGV.Columns.Count; k++)//遍历列
            {
                if ("SUB_DATE" == dataGV.Columns[k].Name.ToUpper())
                {
                    hasSUB_DATE = true;
                    break;
                }
            }
            if (hasSUB_DATE)
            {
                CurrencyManager cm = (CurrencyManager)BindingContext[dataGV.DataSource];
                cm.SuspendBinding(); //挂起数据绑定
                foreach (DataGridViewRow row in dataGV.Rows)
                {
                    if (row.Index < dataGV.Rows.Count - 1)
                    {
                        if (Convert.ToDateTime(row.Cells["SUB_DATE"].Value.ToString()) == Convert.ToDateTime(dateTimePicker1.Text))
                        {
                            row.Visible = true;
                        }
                        else
                        {
                            row.Visible = false;
                        }
                    }
                }
                cm.ResumeBinding(); //恢复数据绑定
            }
        }
        private void dateNotFilter()
        {
            CurrencyManager cm = (CurrencyManager)BindingContext[dataGV.DataSource];
            cm.SuspendBinding(); //挂起数据绑定
            foreach (DataGridViewRow row in dataGV.Rows)
            {
                if (row.Index < dataGV.Rows.Count - 1)
                {
                    row.Visible = true;
                }
            }
            cm.ResumeBinding(); //恢复数据绑定
        }

        private void CDateFilter_CheckedChanged(object sender, EventArgs e)
        {
            if (!cDateFilter.Checked)
            {
                dateNotFilter();
            }
            else
            {
                dateFilter();
            }
        }

        private bool CheckOK(string checkItem, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }
            if (name.Equals("RAIL_STR") || name.Equals("PARAPET") || name.Equals("PARAPET_STR") || name.Equals("MGID_TP"))
            {
                return false;
            }
            string tablePath = appDir + "\\" + name + ".dbf";
            if (!File.Exists(tablePath))
            {
                return false;
            }
            if (name.Equals("TS"))
            {
                if (!File.Exists(appDir + "\\" + "MGID_TP.dbf"))
                {
                    return false;
                }
            }



            return true;
        }
        private string getSelect(string checkItem, string field, string id, string name)
        {
            if (name == "TS" && checkItem == "13")
                id = "M_GID";
            string sql = "";
            string table = name + ".dbf";
            string table2 = "MGID_TP.dbf";
            switch (name) //获取选择的内容
            {
                case "TS":
                    field = field.Replace("M_GID", "a.M_GID");
                    sql = @"select " + id + " as id," + field + "'" + name + "' as layer,a.SUB_DATE from " + table + " as a INNER JOIN " + table2 + " as b ON a.M_GID = b.M_GID";
                    break;
                case "CM_PMS":
                case "CM_PG":
                case "INV_PMS":
                case "INV_PG":
                    sql = @"select " + id + " as id," + field + "'" + name + "' as layer,'1900-01-01' as SUB_DATE from " + table;
                    break;
                default:
                    sql = @"select " + id + " as id," + field + "'" + name + "' as layer,SUB_DATE from " + table;
                    break;
            }

            return sql;
        }
        private void BtnStart_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("layer");
            dt.Columns.Add("SUB_DATE");
            tNowLayer.Text = "Start";
            switch (cProject.SelectedItem.ToString()) //获取选择的内容
            {
                case "01":
                    switch (cCheckItem.SelectedIndex) //获取选择的内容
                    {
                        case 0: CheckA("01", dt); break;
                        case 1: CheckA("02", dt); break;
                        case 2: CheckP01("03", dt); break;
                        case 3: CheckP01("04", dt); break;
                        case 4: CheckP01("05", dt); break;
                    }
                    break;
                case "09":

                    break;
                case "115":
                    switch (cCheckItem.SelectedItem.ToString().Substring(1,2)) //获取选择的内容
                    {
                        case "01": CheckA("01", dt); break;
                        case "03": CheckP115("03", dt); break;
                        case "07": CheckA("07", dt); break;
                        case "08": CheckA("08", dt); break;
                        case "09": CheckP115("09", dt); break;
                        case "10": CheckP115("10", dt); break;
                        case "11": CheckP115("11", dt); break;
                        case "12": CheckP115("12", dt); break;
                        case "13": CheckP115("13", dt); break;
                        case "14": CheckP115("14", dt); break;
                        case "15": CheckP115("15", dt); break;
                        case "16": CheckP115("16", dt); break;
                        case "17": CheckP115("17", dt); break;
                        case "18": CheckP115("18", dt); break;
                        case "19": CheckP115("19", dt); break;
                        case "20": CheckP115("20", dt); break;
                        case "22": CheckP115("22", dt); break;
                        case "23": CheckP115("23", dt); break;
                        case "24": CheckP115("24", dt); break;
                        case "25": CheckP115("25", dt); break;
                        case "26": CheckP115("26", dt); break;
                        case "27": CheckP115("27", dt); break;
                        case "28": CheckP115("28", dt); break;
                        case "29": CheckP115("29", dt); break;
                        case "30": CheckP115("30", dt); break;
                        case "31": CheckP115("31", dt); break;
                        case "32": CheckP115("32", dt); break;

                    }
                    break;
                case "116":

                    break;
            }
            tNowLayer.Text = "Done";




            if (dt.Rows.Count > 0)
            {
            }
            else
            {
                addNull(dt);
            }
            this.dataGV.DataSource = null;
            this.dataGV.DataSource = dt.DefaultView;
            dateFilter();
        }
        private void CheckA(string CheckNo, DataTable dt)
        {
            ClsIniFile ini = new ClsIniFile(configDir);
            string dealLayerName = ini.IniReadValue("Check" + CheckNo, "DealLayer");
            string pField = ini.IniReadValue("Check" + CheckNo, "Field");
            string pWhere = ini.IniReadValue("Check" + CheckNo, "Where");
            string pGroupBy = ini.IniReadValue("Check" + CheckNo, "GroupBy");
            Check(CheckNo, dt, dealLayerName, pField, pWhere, pGroupBy);
        }
        private void CheckP01(string CheckNo, DataTable dt)
        {
            string dealLayerName = "";
            string pField = "";
            string pWhere = "";
            string pGroupBy = "";
            switch (CheckNo) //获取选择的内容
            {
                case "03":
                    dealLayerName = "INV_PMS";
                    pField = "FROMX,FROMY,TOX,TOY,";
                    pWhere = "FROMX<800000 or FROMY<800000 or TOX<800000 or TOY<800000 or  FROMX>900000 or FROMY>900000 or TOX>900000 or TOY>900000";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "INV_EMPIT,INV_SNP";
                    pField = "EASTING,NORTHING,";
                    pWhere = "EASTING<800000 or NORTHING<800000 or EASTING>900000 or NORTHING>900000";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "INV_PIPE";
                    pField = "E_US,N_US,E_DS,N_DS,";
                    pWhere = "E_US<800000 or N_US<800000 or E_DS<800000 or N_DS<800000 or E_US>900000 or N_US>900000 or E_DS>900000 or N_DS>900000";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);
                    break;
                case "04":
                    dealLayerName = "INV_TS";
                    pField = "POST_ID,";
                    pWhere = "|id| not like 'ETP%' or POST_ID not like 'ETS%'";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "CM";
                    pWhere = "|id| not like 'E%'";
                    Check(CheckNo, dt, dealLayerName, "", pWhere);

                    dealLayerName = "CM_PMS,CM_PG";
                    pWhere = "|id| not like 'E%'";
                    Check(CheckNo, dt, dealLayerName, "", pWhere);

                    dealLayerName = "INV_PG,INV_BF,INV_CC,INV_EG,INV_NB,INV_GR,INV_GS,INV_MH,INV_CP,MGID_TP,INV_VS,INV_DK,DK,INV_PL,INV_MC,MC";
                    pWhere = "|id| not like 'E|right2|%'";
                    Check(CheckNo, dt, dealLayerName, "", pWhere);

                    dealLayerName = "INV_RAIL";
                    pWhere = "|id| not like 'ERL%'";
                    Check(CheckNo, dt, dealLayerName, "", pWhere);

                    dealLayerName = "RB";
                    pWhere = "|id| not like 'ERL%' and |id| not like 'EBF%' and |id| not like 'ECC%' and |id| not like 'EEG%' and |id| not like 'ENB%'";
                    Check(CheckNo, dt, dealLayerName, "", pWhere);

                    dealLayerName = "INV_PIPE";
                    pWhere = "|id| not like 'EPP%'";
                    Check(CheckNo, dt, dealLayerName, "", pWhere);

                    dealLayerName = "RD";
                    pWhere = "|id| not like 'EGR%' and |id| not like 'EPP%' and |id| not like 'EGS%' and |id| not like 'EMH%' and |id| not like 'ECP%'";
                    Check(CheckNo, dt, dealLayerName, "", pWhere);

                    dealLayerName = "INV_SNP";
                    pWhere = "|id| not like 'KL%'";
                    Check(CheckNo, dt, dealLayerName, "", pWhere);

                    dealLayerName = "SNP_VS";
                    pWhere = "|id| not like 'KL%' and |id| not like 'EVS%'";
                    Check(CheckNo, dt, dealLayerName, "", pWhere);

                    dealLayerName = "INV_TP,TP";
                    pWhere = "|id| not like 'ETT%'";
                    Check(CheckNo, dt, dealLayerName, "", pWhere);

                    dealLayerName = "INV_EMPIT,EMPIT";
                    pWhere = "|id| not like 'EEM%'";
                    Check(CheckNo, dt, dealLayerName, "", pWhere);

                    dealLayerName = "INV_RPW,RPW";
                    pWhere = "|id| not like 'ERP%'";
                    Check(CheckNo, dt, dealLayerName, "", pWhere);

                    dealLayerName = "INV_TREE";
                    pWhere = "|id| not like 'ETR%'";
                    Check(CheckNo, dt, dealLayerName, "", pWhere);
                    break;
                case "05":
                    dealLayerName = "INV_PG,CM_PG,INV_BF,INV_CC,INV_EG,INV_NB,INV_GR,INV_GS,INV_CP,MGID_TP,INV_VS,INV_DK,DK,INV_PL,INV_MC,MC";
                    pField = "|right2|_UID as uid,";
                    pWhere = "|right2|_UID not like '|right2|%E0114' and |right2|_UID not like '|right2|%E0808'";
                    Check(CheckNo, dt, dealLayerName, "", pWhere);

                    dealLayerName = "INV_RAIL";
                    pField = "RL_UID as uid,";
                    pWhere = "RL_UID not like 'RL%E0114' and RL_UID not like 'RL%E0808'";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "RB";
                    pField = "RB_UID as uid,";
                    pWhere = "(RB_UID not like 'RL%' and RB_UID not like 'BF%' and RB_UID not like 'CC%' and RB_UID not like 'EG%' and RB_UID not like 'NB%')  or (RB_UID not like '%E0114' and RB_UID not like '%E0808')";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "INV_PIPE";
                    pField = "PP_UID as uid,";
                    pWhere = "PP_UID not like 'PP%E0114' and PP_UID not like 'PP%E0808'";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "INV_MH";
                    pField = "MH_UID as uid,MC_UID as uid2,";
                    pWhere = "(MH_UID not like 'MH%' and MC_UID not like 'MC%') or (MH_UID not like '%E0114' and MC_UID not like '%E0114' and MH_UID not like '%E0808' and MC_UID not like '%E0808')";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "RD";
                    pField = "RD_UID as uid,";
                    pWhere = "(RD_UID not like 'GR%' and RD_UID not like 'PP%' and RD_UID not like 'GS%' and RD_UID not like 'MH%' and RD_UID not like 'CP%') or (RD_UID not like '%E0114' and RD_UID not like '%E0808')";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "INV_TS";
                    pField = "TP_UID as uid,TS_UID as uid2,";
                    pWhere = "(TP_UID not like 'TP%' and TS_UID not like 'TS%') or (TP_UID not like '%E0114' and TS_UID not like '%E0114' and TP_UID not like '%E0808' and TS_UID not like '%E0808')";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "TS";
                    pField = "b.TP_UID as uid,";
                    pWhere = "b.TP_UID not like 'TP%E0114' and b.TP_UID not like 'TP%E0808'";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "INV_SNP";
                    pField = "SNP_ID as uid,";
                    pWhere = "SNP_ID not like 'KL%E0114' and SNP_ID not like 'KL%E0808'";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "INV_TP,TP";
                    pField = "TT_UID as uid,";
                    pWhere = "TT_UID not like 'TT%E0114' and TT_UID not like 'TT%E0808'";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "INV_EMPIT,EMPIT";
                    pField = "EM_UID as uid,";
                    pWhere = "EM_UID not like 'EM%E0114' and EM_UID not like 'EM%E0808'";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "INV_RPW,RPW";
                    pField = "RPW_UID as uid,";
                    pWhere = "RPW_UID not like 'RP%E0114' and RPW_UID not like 'RP%E0808'";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "INV_TREE";
                    pField = "TREE_UID as uid,";
                    pWhere = "TREE_UID not like 'TR%E0114' and TREE_UID not like 'TR%E0808'";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);
                    break;

            }


        }
        private void CheckP115(string CheckNo, DataTable dt)
        {
            string dealLayerName = "";
            string pField = "";
            string pWhere = "";
            string pGroupBy = "";
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            switch (CheckNo) //获取选择的内容
            {
                case "03":
                    dealLayerName = "CM_PMS";
                    pField = "FROMX,FROMY,TOX,TOY,";
                    pWhere = "FROMX<800000 or FROMY<800000 or TOX<800000 or TOY<800000 or  FROMX>900000 or FROMY>900000 or TOX>900000 or TOY>900000";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "INV_EMPIT,INV_SNP";
                    pField = "EASTING,NORTHING,";
                    pWhere = "EASTING<800000 or NORTHING<800000 or EASTING>900000 or NORTHING>900000";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "INV_PIPE";
                    pField = "E_US,N_US,E_DS,N_DS,";
                    pWhere = "E_US<800000 or N_US<800000 or E_DS<800000 or N_DS<800000 or E_US>900000 or N_US>900000 or E_DS>900000 or N_DS>900000";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);
                    break;
                case "09":
                    dealLayerName = "TS";
                    pField = "b.I_VERSION,a.M_VERSION,COUNT(*) as count,";
                    pGroupBy = "|id|,b.I_VERSION,a.M_VERSION";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere, pGroupBy);

                    dealLayerName = "SP,ST,RB,RD,VEG,SNP_VS,TP,DK,EMPIT,MC,TU,RPW";
                    pField = "I_VERSION,M_VERSION,COUNT(*) as count,";
                    pGroupBy = "|id|,I_VERSION,M_VERSION";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere, pGroupBy);

                    dealLayerName = "INV_PG,INV_HRS,INV_HUS,INV_ELE_BEARING,INV_ELE_JOINT,INV_ELE_FD,INV_ELE_INVERT,INV_ELE_ABUT,INV_ELE_COL,INV_ELE_GIRD,INV_ELE_DIAP,INV_ELE_SOFFIT,INV_ELE_DECK,INV_ELE_FLOOR,INV_ELE_RAMPS,INV_ELE_TOWER,INV_ELE_DRAIN,INV_ELE_ROOF,INV_ELE_FINISH,INV_RAIL,INV_BF,INV_CC,INV_EG,INV_NB,PARAPET,RAIL_STR,INV_GR,INV_PIPE,INV_GS,INV_MH,INV_CP,INV_TS,MGID_TP,INV_VEG,INV_SNP,INV_VS,INV_TP,INV_DK,INV_EMPIT,INV_MC,INV_T_ELE_DR,INV_T_ELE_JT,INV_T_ELE_CO,INV_T_ELE_CP,INV_T_ELE_CR,INV_T_ELE_CU,INV_T_ELE_FC,INV_T_ELE_KB,INV_T_ELE_PS,INV_T_ELE_PW,INV_T_ELE_VP,INV_RPW";
                    pField = "I_VERSION,COUNT(*) as count,";
                    pGroupBy = "|id|,I_VERSION";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere, pGroupBy);
                    if (dt.DefaultView.Count > 0)
                    {
                        dt.DefaultView.RowFilter = "count>1";
                    }
                    break;
                case "10":
                    dealLayerName = "SP";
                    pGroupBy = "|id|";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    dealLayerName = "INV_HRS";
                    Check(CheckNo, dt2, dealLayerName, pField, pWhere, pGroupBy);
                    CheckSubA(dt, dt1, dt2, new DataRowIDComparer());

                    dealLayerName = "ST";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    dealLayerName = "INV_ELE_BEARING,INV_ELE_JOINT,INV_ELE_FD,INV_ELE_INVERT,INV_ELE_ABUT,INV_ELE_COL,INV_ELE_GIRD,INV_ELE_DIAP,INV_ELE_SOFFIT,INV_ELE_DECK,INV_ELE_FLOOR,INV_ELE_RAMPS,INV_ELE_TOWER,INV_ELE_DRAIN,INV_ELE_ROOF,INV_ELE_FINISH";
                    Check(CheckNo, dt2, dealLayerName, pField, pWhere, pGroupBy);
                    CheckSubA(dt, dt1, dt2, new DataRowIDComparer());

                    dealLayerName = "RB";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    dealLayerName = "INV_RAIL,INV_BF,INV_CC,INV_EG,INV_NB";
                    Check(CheckNo, dt2, dealLayerName, pField, pWhere, pGroupBy);
                    CheckSubA(dt, dt1, dt2, new DataRowIDComparer());

                    dealLayerName = "RD";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    dealLayerName = "INV_GR,INV_PIPE,INV_GS,INV_MH,INV_CP";
                    Check(CheckNo, dt2, dealLayerName, pField, pWhere, pGroupBy);
                    CheckSubA(dt, dt1, dt2, new DataRowIDComparer());

                    dealLayerName = "MGID_TP";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    dealLayerName = "INV_TS";
                    Check(CheckNo, dt2, dealLayerName, pField, pWhere, pGroupBy);
                    CheckSubA(dt, dt1, dt2, new DataRowIDComparer());

                    dealLayerName = "VEG";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    dealLayerName = "INV_VEG";
                    Check(CheckNo, dt2, dealLayerName, pField, pWhere, pGroupBy);
                    CheckSubA(dt, dt1, dt2, new DataRowIDComparer());

                    dealLayerName = "SNP_VS";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    dealLayerName = "INV_SNP,INV_VS";
                    Check(CheckNo, dt2, dealLayerName, pField, pWhere, pGroupBy);
                    CheckSubA(dt, dt1, dt2, new DataRowIDComparer());

                    dealLayerName = "TP";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    dealLayerName = "INV_TP";
                    Check(CheckNo, dt2, dealLayerName, pField, pWhere, pGroupBy);
                    CheckSubA(dt, dt1, dt2, new DataRowIDComparer());

                    dealLayerName = "DK";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    dealLayerName = "INV_DK";
                    Check(CheckNo, dt2, dealLayerName, pField, pWhere, pGroupBy);
                    CheckSubA(dt, dt1, dt2, new DataRowIDComparer());

                    dealLayerName = "EMPIT";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    dealLayerName = "INV_EMPIT";
                    Check(CheckNo, dt2, dealLayerName, pField, pWhere, pGroupBy);
                    CheckSubA(dt, dt1, dt2, new DataRowIDComparer());

                    dealLayerName = "MC";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    dealLayerName = "INV_MC";
                    Check(CheckNo, dt2, dealLayerName, pField, pWhere, pGroupBy);
                    CheckSubA(dt, dt1, dt2, new DataRowIDComparer());

                    dealLayerName = "RPW";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    dealLayerName = "INV_RPW";
                    Check(CheckNo, dt2, dealLayerName, pField, pWhere, pGroupBy);
                    CheckSubA(dt, dt1, dt2, new DataRowIDComparer());
                    break;
                case "11":
                    dt.Columns.Add("I_VERSION");

                    dealLayerName = "SP";
                    pField = "max(I_VERSION) as I_VERSION,";
                    pGroupBy = "|id|";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    dealLayerName = "INV_HRS,INV_HUS";
                    Check(CheckNo, dt2, dealLayerName, pField, pWhere, pGroupBy);
                    CheckSubA(dt, dt1, dt2, new DataRowIDIComparer());

                    dealLayerName = "ST";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    dealLayerName = "INV_ELE_BEARING,INV_ELE_JOINT,INV_ELE_FD,INV_ELE_INVERT,INV_ELE_ABUT,INV_ELE_COL,INV_ELE_GIRD,INV_ELE_DIAP,INV_ELE_SOFFIT,INV_ELE_DECK,INV_ELE_FLOOR,INV_ELE_RAMPS,INV_ELE_TOWER,INV_ELE_DRAIN,INV_ELE_ROOF,INV_ELE_FINISH";
                    Check(CheckNo, dt2, dealLayerName, pField, pWhere, pGroupBy);
                    CheckSubA(dt, dt1, dt2, new DataRowIDIComparer());

                    dealLayerName = "RB";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    dealLayerName = "INV_RAIL,INV_BF,INV_CC,INV_EG,INV_NB,PARAPET,RAIL_STR";
                    Check(CheckNo, dt2, dealLayerName, pField, pWhere, pGroupBy);
                    CheckSubA(dt, dt1, dt2, new DataRowIDIComparer());

                    dealLayerName = "RD";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    dealLayerName = "INV_GR,INV_PIPE,INV_GS,INV_MH,INV_CP";
                    Check(CheckNo, dt2, dealLayerName, pField, pWhere, pGroupBy);
                    CheckSubA(dt, dt1, dt2, new DataRowIDIComparer());

                    dealLayerName = "MGID_TP";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    dealLayerName = "INV_TS";
                    Check(CheckNo, dt2, dealLayerName, pField, pWhere, pGroupBy);
                    CheckSubA(dt, dt1, dt2, new DataRowIDIComparer());

                    dealLayerName = "VEG";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    dealLayerName = "INV_VEG";
                    Check(CheckNo, dt2, dealLayerName, pField, pWhere, pGroupBy);
                    CheckSubA(dt, dt1, dt2, new DataRowIDIComparer());

                    dealLayerName = "SNP_VS";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    dealLayerName = "INV_SNP,INV_VS";
                    Check(CheckNo, dt2, dealLayerName, pField, pWhere, pGroupBy);
                    CheckSubA(dt, dt1, dt2, new DataRowIDIComparer());

                    dealLayerName = "TP";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    dealLayerName = "INV_TP";
                    Check(CheckNo, dt2, dealLayerName, pField, pWhere, pGroupBy);
                    CheckSubA(dt, dt1, dt2, new DataRowIDIComparer());

                    dealLayerName = "DK";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    dealLayerName = "INV_DK";
                    Check(CheckNo, dt2, dealLayerName, pField, pWhere, pGroupBy);
                    CheckSubA(dt, dt1, dt2, new DataRowIDIComparer());

                    dealLayerName = "EMPIT";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    dealLayerName = "INV_EMPIT";
                    Check(CheckNo, dt2, dealLayerName, pField, pWhere, pGroupBy);
                    CheckSubA(dt, dt1, dt2, new DataRowIDIComparer());

                    dealLayerName = "MC";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    dealLayerName = "INV_MC";
                    Check(CheckNo, dt2, dealLayerName, pField, pWhere, pGroupBy);
                    CheckSubA(dt, dt1, dt2, new DataRowIDIComparer());

                    dealLayerName = "TU";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    dealLayerName = "INV_T_ELE_DR,INV_T_ELE_JT,INV_T_ELE_CO,INV_T_ELE_CP,INV_T_ELE_CR,INV_T_ELE_CU,INV_T_ELE_FC,INV_T_ELE_KB,INV_T_ELE_PS,INV_T_ELE_PW,INV_T_ELE_VP";
                    Check(CheckNo, dt2, dealLayerName, pField, pWhere, pGroupBy);
                    CheckSubA(dt, dt1, dt2, new DataRowIDIComparer());

                    dealLayerName = "RPW";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    dealLayerName = "INV_RPW";
                    Check(CheckNo, dt2, dealLayerName, pField, pWhere, pGroupBy);
                    CheckSubA(dt, dt1, dt2, new DataRowIDIComparer());

                    break;
                case "12":
                    dt.Columns.Add("M_VERSION");
                    dt.Columns.Add("WORKS_DATE");

                    string name = null;
                    dealLayerName = "SP,ST,RB,RD,VEG,SNP_VS,TP,DK,EMPIT,MC,TU,RPW";
                    string[] arrayLayerName = dealLayerName.Split(',');
                    for (int II = 0; II < arrayLayerName.GetLength(0); II++)
                    {
                        name = arrayLayerName[II].ToString().Trim();
                        pGroupBy = "|id|";
                        pField = "max(M_VERSION) as M_VERSION,max(WORKS_DATE) as WORKS_DATE,";
                        Check(CheckNo, dt1, name, pField, pWhere, pGroupBy);
                        pField = "M_VERSION,WORKS_DATE,";
                        Check(CheckNo, dt2, name, pField, pWhere, "");
                        CheckSubA(dt, dt1, dt2, new DataRowIDMDateComparer());
                    }
                    dt.Columns.Remove("WORKS_DATE");
                    break;
                case "13":
                    string tablePath = appDir + "\\road polygon.xls";
                    if (!File.Exists(tablePath))
                    {
                        break;
                    }
                    dt.Columns.Add("ST_CODE");
                    dt.Columns.Add("ROAD_NAME");

                    setConnetStr("xls");
                    getData("select ST_CODE,ROAD_NAME from [Export_Output$]", dt2);
                    setConnetStr();
                    dealLayerName = "EMPIT,DK,TP,VEG,TS,RD,RB,ST,SP,CM,MC,TU,RPW";
                    pField = "ST_CODE,ROAD_NAME,";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere);
                    CheckSubA(dt, dt1, dt2, new DataRowCodeNameComparer());
                    break;
                case "14":
                    dt.Columns.Add("TYPE");

                    dealLayerName = "RB";
                    pField = "FEAT_TYPE as TYPE,";
                    pGroupBy = "|id|,FEAT_TYPE";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    Check(CheckNo, dt2, "INV_RAIL", "RAIL_TYPE as TYPE,");
                    Check(CheckNo, dt2, "INV_BF", "BARR_TYPE as TYPE,");
                    Check(CheckNo, dt2, "INV_EG", "EG_TYPE as TYPE,");
                    Check(CheckNo, dt2, "INV_NB", "NB_TYPE as TYPE,");
                    Check(CheckNo, dt2, "INV_CC", "19 as TYPE,");
                    CheckSubA(dt, dt1, dt2, new DataRowIDTypeComparer());

                    dealLayerName = "RD";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    Check(CheckNo, dt2, "INV_PIPE", "TYPE,");
                    Check(CheckNo, dt2, "INV_GR", "4 as TYPE,");
                    Check(CheckNo, dt2, "INV_GS", "5 as TYPE,");
                    Check(CheckNo, dt2, "INV_CP", "7 as TYPE,");
                    Check(CheckNo, dt2, "INV_MH", "6 as TYPE,");
                    CheckSubA(dt, dt1, dt2, new DataRowIDTypeComparer());

                    dealLayerName = "TP";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    Check(CheckNo, dt2, "INV_TP", "LOC_TYPE as TYPE,");
                    CheckSubA(dt, dt1, dt2, new DataRowIDTypeComparer());

                    dealLayerName = "DK";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    Check(CheckNo, dt2, "INV_DK", "1 as TYPE,");
                    CheckSubA(dt, dt1, dt2, new DataRowIDTypeComparer());

                    dealLayerName = "RPW";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    Check(CheckNo, dt2, "INV_RPW", "FEAT_TYPE as TYPE,");
                    CheckSubA(dt, dt1, dt2, new DataRowIDTypeComparer());
                    break;
                case "15":
                    dt.Columns.Add("LST_MT_DT");

                    dealLayerName = "RB";
                    pField = "max(WORKS_DATE) as LST_MT_DT,";
                    pGroupBy = "|id|";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    Check(CheckNo, dt2, "INV_RAIL,INV_BF,INV_EG,INV_NB,INV_CC", "LST_MT_DT,");
                    CheckSubA(dt, dt1, dt2, new DataRowIDLDComparer());

                    dealLayerName = "RD";
                    pField = "max(WORKS_DATE) as LST_MT_DT,";
                    pGroupBy = "|id|";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    Check(CheckNo, dt2, "INV_PIPE,INV_GR,INV_GS,INV_CP,INV_MH", "LST_MT_DT,");
                    CheckSubA(dt, dt1, dt2, new DataRowIDLDComparer());

                    dealLayerName = "SNP_VS";
                    pField = "max(WORKS_DATE) as LST_MT_DT,";
                    pGroupBy = "|id|";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    Check(CheckNo, dt2, "INV_VS", "LST_MT_DT,");
                    CheckSubA(dt, dt1, dt2, new DataRowIDLDComparer());

                    dealLayerName = "TP";
                    pField = "max(WORKS_DATE) as LST_MT_DT,";
                    pGroupBy = "|id|";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    Check(CheckNo, dt2, "INV_TP", "LST_MT_DT,");
                    CheckSubA(dt, dt1, dt2, new DataRowIDLDComparer());

                    dealLayerName = "DK";
                    pField = "max(WORKS_DATE) as LST_MT_DT,";
                    pGroupBy = "|id|";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    Check(CheckNo, dt2, "INV_DK", "LST_MT_DT,");
                    CheckSubA(dt, dt1, dt2, new DataRowIDLDComparer());

                    dealLayerName = "EMPIT";
                    pField = "max(WORKS_DATE) as LST_MT_DT,";
                    pGroupBy = "|id|";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    Check(CheckNo, dt2, "INV_EMPIT", "LST_MT_DT,");
                    CheckSubA(dt, dt1, dt2, new DataRowIDLDComparer());
                    break;
                case "16":
                    dt.Columns.Add("ST_CODE");

                    dealLayerName = "RB";
                    pField = "ST_CODE,";
                    pWhere = "ST_CODE<>null";
                    pGroupBy = "|id|,ST_CODE";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    Check(CheckNo, dt2, "INV_EG,INV_CC", pField, pWhere, pGroupBy);
                    Check(CheckNo, dt2, "INV_RAIL,INV_BF,INV_NB", "ST_CODE1,", "ST_CODE1<>null", "|id|,ST_CODE1");
                    CheckSubA(dt, dt1, dt2, new DataRowIDTypeComparer());

                    dealLayerName = "RD";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    Check(CheckNo, dt2, "INV_PIPE,INV_GR,INV_GS,INV_CP,INV_MH", "ST_CODE1,", "ST_CODE1<>null", "|id|,ST_CODE1");
                    CheckSubA(dt, dt1, dt2, new DataRowIDTypeComparer());

                    dealLayerName = "TS";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    Check(CheckNo, dt2, "INV_TS", pField, pWhere, pGroupBy);
                    CheckSubA(dt, dt1, dt2, new DataRowIDTypeComparer());

                    dealLayerName = "VEG";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    Check(CheckNo, dt2, "INV_VEG", pField, pWhere, pGroupBy);
                    CheckSubA(dt, dt1, dt2, new DataRowIDTypeComparer());

                    dealLayerName = "SNP_VS";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    Check(CheckNo, dt2, "INV_SNP,INV_VS", pField, pWhere, pGroupBy);
                    CheckSubA(dt, dt1, dt2, new DataRowIDTypeComparer());

                    dealLayerName = "TP";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    Check(CheckNo, dt2, "INV_TP", pField, pWhere, pGroupBy);
                    CheckSubA(dt, dt1, dt2, new DataRowIDTypeComparer());

                    dealLayerName = "DK";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    Check(CheckNo, dt2, "INV_DK", pField, pWhere, pGroupBy);
                    CheckSubA(dt, dt1, dt2, new DataRowIDTypeComparer());

                    dealLayerName = "EMPIT";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    Check(CheckNo, dt2, "INV_EMPIT", pField, pWhere, pGroupBy);
                    CheckSubA(dt, dt1, dt2, new DataRowIDTypeComparer());

                    dealLayerName = "MC";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    Check(CheckNo, dt2, "INV_MC", "ST_CODE1,", "ST_CODE1<>null", "|id|,ST_CODE1");
                    CheckSubA(dt, dt1, dt2, new DataRowIDTypeComparer());
                    break;
                case "17":
                    dt.Columns.Add("INV_GID");

                    dealLayerName = "INV_PG,INV_HRS,INV_HUS,INV_ELE_BEARING,INV_ELE_JOINT,INV_ELE_FD,INV_ELE_INVERT,INV_ELE_ABUT,INV_ELE_COL,INV_ELE_GIRD,INV_ELE_DIAP,INV_ELE_SOFFIT,INV_ELE_DECK,INV_ELE_FLOOR,INV_ELE_RAMPS,INV_ELE_TOWER,INV_ELE_DRAIN,INV_ELE_ROOF,INV_ELE_FINISH,INV_RAIL,INV_BF,INV_CC,INV_EG,INV_NB,INV_GR,INV_PIPE,INV_GS,INV_MH,INV_CP,INV_TS,INV_VEG,INV_SNP,INV_VS,INV_TP,INV_DK,INV_EMPIT,INV_MC,INV_T_ELE_DR,INV_T_ELE_JT,INV_T_ELE_CO,INV_T_ELE_CP,INV_T_ELE_CR,INV_T_ELE_CU,INV_T_ELE_FC,INV_T_ELE_KB,INV_T_ELE_PS,INV_T_ELE_PW,INV_T_ELE_VP,INV_RPW,INV_TREE";
                    pField = "INV_GID,";
                    Check(CheckNo, dt1, dealLayerName, pField);
                    if (dt1.DefaultView.Count > 0)
                    {
                        //var query = dt.AsEnumerable().GroupBy(t => new { physicalCode = t.Field<string>("体检编号"), empName = t.Field<string>("体检人员") })
                        var query = dt1.AsEnumerable().GroupBy(t => new { GID = t.Field<Decimal>("INV_GID") })
                    .Where(s => s.Count() > 1)
                    .Select(g => new { g.Key.GID, c = g.Count() });
                        foreach (var item in query)
                        {
                            DataRow[] drs = dt1.Select("INV_GID='" + item.GID + "'");
                            foreach (DataRow dr in drs)
                                dt.ImportRow(dr);
                        }
                    }
                    break;
                case "18":
                    dt.Columns.Add("M_GID");

                    dealLayerName = "CM,SP,ST,RB,RD,TS,VEG,SNP_VS,TP,DK,EMPIT,MC,TU,RPW";
                    pField = "M_GID,";
                    Check(CheckNo, dt1, dealLayerName, pField);
                    if (dt1.DefaultView.Count > 0)
                    {
                        var query = dt1.AsEnumerable().GroupBy(t => new { GID = t.Field<Decimal>("M_GID") })
                    .Where(s => s.Count() > 1)
                    .Select(g => new { g.Key.GID, c = g.Count() });
                        foreach (var item in query)
                        {
                            DataRow[] drs = dt1.Select("M_GID='" + item.GID + "'");
                            foreach (DataRow dr in drs)
                                dt.ImportRow(dr);
                        }
                    }
                    break;
                case "19":
                    dt.Columns.Add("P_User_ID");

                    dealLayerName = "INV_PG,INV_HRS,INV_HUS,INV_ELE_BEARING,INV_ELE_JOINT,INV_ELE_FD,INV_ELE_INVERT,INV_ELE_ABUT,INV_ELE_COL,INV_ELE_GIRD,INV_ELE_DIAP,INV_ELE_SOFFIT,INV_ELE_DECK,INV_ELE_FLOOR,INV_ELE_RAMPS,INV_ELE_TOWER,INV_ELE_DRAIN,INV_ELE_ROOF,INV_ELE_FINISH,INV_RAIL,INV_BF,INV_CC,INV_EG,INV_NB,INV_GR,INV_PIPE,INV_GS,INV_MH,INV_CP,INV_TS,INV_VEG,INV_SNP,INV_VS,INV_TP,INV_DK,INV_EMPIT,INV_PL,INV_MC,INV_T_ELE_DR,INV_T_ELE_JT,INV_T_ELE_CO,INV_T_ELE_CP,INV_T_ELE_CR,INV_T_ELE_CU,INV_T_ELE_FC,INV_T_ELE_KB,INV_T_ELE_PS,INV_T_ELE_PW,INV_T_ELE_VP,INV_RPW,INV_TREE";
                    pField = "|id| as P_User_ID,";
                    Check(CheckNo, dt1, dealLayerName, pField);

                    pField = "P_User_ID,";
                    pWhere = "P_User_ID is not null and P_User_ID<>''";
                    Check(CheckNo, dt2, dealLayerName, pField);
                    CheckSubA(dt, dt2, dt1, new DataRowPUIDComparer());
                    break;
                case "20":
                    dt.Columns.Add("P_User_ID");
                    dt.Columns.Add("DELETE");

                    dealLayerName = "INV_PG,INV_HRS,INV_HUS,INV_ELE_BEARING,INV_ELE_JOINT,INV_ELE_FD,INV_ELE_INVERT,INV_ELE_ABUT,INV_ELE_COL,INV_ELE_GIRD,INV_ELE_DIAP,INV_ELE_SOFFIT,INV_ELE_DECK,INV_ELE_FLOOR,INV_ELE_RAMPS,INV_ELE_TOWER,INV_ELE_DRAIN,INV_ELE_ROOF,INV_ELE_FINISH,INV_RAIL,INV_BF,INV_CC,INV_EG,INV_NB,INV_GR,INV_PIPE,INV_GS,INV_MH,INV_CP,INV_TS,INV_VEG,INV_SNP,INV_VS,INV_TP,INV_DK,INV_EMPIT,INV_PL,INV_MC,INV_T_ELE_DR,INV_T_ELE_JT,INV_T_ELE_CO,INV_T_ELE_CP,INV_T_ELE_CR,INV_T_ELE_CU,INV_T_ELE_FC,INV_T_ELE_KB,INV_T_ELE_PS,INV_T_ELE_PW,INV_T_ELE_VP,INV_RPW,INV_TREE";
                    pField = "|id| as P_User_ID,DELETE,";
                    Check(CheckNo, dt1, dealLayerName, pField);

                    pField = "P_User_ID,'Y' as DELETE,";
                    pWhere = "P_User_ID is not null and P_User_ID<>''";
                    Check(CheckNo, dt2, dealLayerName, pField, pWhere);
                    CheckSubA(dt, dt2, dt1, new DataRowPUIDDComparer());
                    dt.Columns.Remove("DELETE");
                    break;
                case "22":
                    dt.Columns.Add("WORKS_TYPE");
                    dt.Columns.Add("M_GID");

                    dealLayerName = "RB,TS,SNP_VS,TP,DK,EMPIT,MC,RPW";
                    pField = "WORKS_TYPE,M_GID,";
                    Check(CheckNo, dt1, dealLayerName, pField);
                    pWhere = "WORKS_TYPE=3";
                    Check(CheckNo, dt2, dealLayerName, pField, pWhere);
                    foreach (DataRow dr in dt2.Rows)
                    {
                        bool f = true;
                        DataRow[] drs = dt1.Select("id='" + dr["id"].ToString().Trim() + "'", "M_GID");
                        if (drs.Length > 1)
                        {
                            for(int i=1; i<drs.Length;i++)
                            {
                                if (drs[i]["WORKS_TYPE"].ToString() == "3")
                                {
                                    if (drs[i-1]["WORKS_TYPE"].ToString() == "2")
                                    {
                                        f = false;
                                    }
                                }
                            }
                        }
                        if(f)
                            dt.ImportRow(dr);
                    }
                    break;
                case "23":
                    dealLayerName = "INV_RAIL,INV_BF,INV_EG,INV_NB,INV_CC";
                    pWhere = "LST_MT_DT<>null";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere);
                    Check(CheckNo, dt2, "RB");
                    CheckSubA(dt, dt1, dt2, new DataRowIDComparer());

                    dealLayerName = "INV_PIPE,INV_GR,INV_GS,INV_CP,INV_MH";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere);
                    Check(CheckNo, dt2, "RD");
                    CheckSubA(dt, dt1, dt2, new DataRowIDComparer());

                    dealLayerName = "INV_VS";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere);
                    Check(CheckNo, dt2, "SNP_VS");
                    CheckSubA(dt, dt1, dt2, new DataRowIDComparer());

                    dealLayerName = "INV_TP";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere);
                    Check(CheckNo, dt2, "TP");
                    CheckSubA(dt, dt1, dt2, new DataRowIDComparer());

                    dealLayerName = "INV_DK";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere);
                    Check(CheckNo, dt2, "DK");
                    CheckSubA(dt, dt1, dt2, new DataRowIDComparer());


                    dealLayerName = "INV_EMPIT";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere);
                    Check(CheckNo, dt2, "EMPIT");
                    CheckSubA(dt, dt1, dt2, new DataRowIDComparer());
                    break;
                case "24":
                    getData("select M_GID as id,'TS' as layer,SUB_DATE from TS.dbf", dt1);
                    getData("select M_GID as id,'MGID_TP' as layer,'1900-01-01' as SUB_DATE from MGID_TP.dbf", dt2);
                    CheckSubA(dt, dt1, dt2, new DataRowIDComparer());
                    CheckSubA(dt, dt2, dt1, new DataRowIDComparer());
                    break;
                case "25":
                    dealLayerName = "INV_HRS,INV_HUS,INV_ELE_BEARING,INV_ELE_JOINT,INV_ELE_FD,INV_ELE_INVERT,INV_ELE_ABUT,INV_ELE_COL,INV_ELE_GIRD,INV_ELE_DIAP,INV_ELE_SOFFIT,INV_ELE_DECK,INV_ELE_FLOOR,INV_ELE_RAMPS,INV_ELE_TOWER,INV_ELE_DRAIN,INV_ELE_ROOF,INV_ELE_FINISH,INV_RAIL,INV_BF,INV_CC,INV_EG,INV_NB,INV_GR,INV_GS,INV_MH,INV_CP,INV_PIPE,INV_TS,INV_VEG,INV_SNP,INV_VS,INV_TP,INV_DK,INV_PG,INV_PL,INV_MC,INV_EMPIT,INV_T_ELE_DR,INV_T_ELE_JT,INV_T_ELE_CO,INV_T_ELE_CP,INV_T_ELE_CR,INV_T_ELE_CU,INV_T_ELE_FC,INV_T_ELE_KB,INV_T_ELE_PS,INV_T_ELE_PW,INV_T_ELE_VP,INV_RPW,INV_TREE";
                    pWhere = "DELETE<>'Y'";
                    Check(CheckNo, dt1, dealLayerName, "I_VERSION,", pWhere);
                    Check(CheckNo, dt2, dealLayerName, "max(I_VERSION) as I_VERSION,", "","|id|");
                    CheckSubA(dt, dt1, dt2, new DataRowIDIComparer());
                    break;
                case "26":
                    DataTable dt3 = new DataTable();
                    dt3.Columns.Add("id");
                    dt3.Columns.Add("layer");
                    dt3.Columns.Add("SUB_DATE");
                    dealLayerName = "INV_RAIL,INV_BF,INV_EG,INV_NB,INV_CC";
                    pWhere = "DELETE<>'Y'";
                    Check(CheckNo, dt1, dealLayerName, "INV_GID,", pWhere);
                    Check(CheckNo, dt2, dealLayerName, "max(INV_GID) as INV_GID,", "", "|id|");
                    CheckSubB(dt3, dt2, dt1, new DataRowGIDComparer());
                    //dt3.Columns.Remove("INV_GID");
                    Check(CheckNo, dt2, "RB", "max(WORKS_TYPE) as WORKS_TYPE,", "", "|id|");
                    dt2 = ToDataTable(dt2.Select("WORKS_TYPE=2"));
                    //dt2.Columns.Remove("WORKS_TYPE");
                    CheckSubB(dt, dt2, dt3, new DataRowIDComparer());

                    dealLayerName = "INV_PIPE,INV_GR,INV_GS,INV_CP,INV_MH";
                    Check(CheckNo, dt1, dealLayerName, "INV_GID,", pWhere);
                    Check(CheckNo, dt2, dealLayerName, "max(INV_GID) as INV_GID,", "", "|id|");
                    CheckSubB(dt3, dt2, dt1, new DataRowGIDComparer());
                    //dt3.Columns.Remove("INV_GID");
                    Check(CheckNo, dt2, "RD", "max(WORKS_TYPE) as WORKS_TYPE", "", "|id|");
                    dt2 = ToDataTable(dt2.Select("WORKS_TYPE=2"));
                    //dt2.Columns.Remove("WORKS_TYPE");
                    CheckSubB(dt, dt2, dt3, new DataRowIDComparer());

                    dealLayerName = "INV_TS";
                    Check(CheckNo, dt1, dealLayerName, "INV_GID", pWhere);
                    Check(CheckNo, dt2, dealLayerName, "max(INV_GID) as INV_GID", "", "|id|");
                    CheckSubB(dt3, dt2, dt1, new DataRowGIDComparer());
                    //dt3.Columns.Remove("INV_GID");
                    Check(CheckNo, dt2, "TS", "max(WORKS_TYPE) as WORKS_TYPE", "", "|id|");
                    dt2 = ToDataTable(dt2.Select("WORKS_TYPE=2"));
                    //dt2.Columns.Remove("WORKS_TYPE");
                    CheckSubB(dt, dt2, dt3, new DataRowIDComparer());

                    dealLayerName = "INV_SNP,INV_VS";
                    Check(CheckNo, dt1, dealLayerName, "INV_GID", pWhere);
                    Check(CheckNo, dt2, dealLayerName, "max(INV_GID) as INV_GID", "", "|id|");
                    CheckSubB(dt3, dt2, dt1, new DataRowGIDComparer());
                    //dt3.Columns.Remove("INV_GID");
                    Check(CheckNo, dt2, "SNP_VS", "max(WORKS_TYPE) as WORKS_TYPE", "", "|id|");
                    dt2 = ToDataTable(dt2.Select("WORKS_TYPE=2"));
                    //dt2.Columns.Remove("WORKS_TYPE");
                    CheckSubB(dt, dt2, dt3, new DataRowIDComparer());

                    dealLayerName = "INV_TP";
                    Check(CheckNo, dt1, dealLayerName, "INV_GID", pWhere);
                    Check(CheckNo, dt2, dealLayerName, "max(INV_GID) as INV_GID", "", "|id|");
                    CheckSubB(dt3, dt2, dt1, new DataRowGIDComparer());
                    //dt3.Columns.Remove("INV_GID");
                    Check(CheckNo, dt2, "TP", "max(WORKS_TYPE) as WORKS_TYPE", "", "|id|");
                    dt2 = ToDataTable(dt2.Select("WORKS_TYPE=2"));
                    //dt2.Columns.Remove("WORKS_TYPE");
                    CheckSubB(dt, dt2, dt3, new DataRowIDComparer());

                    dealLayerName = "INV_DK";
                    Check(CheckNo, dt1, dealLayerName, "INV_GID", pWhere);
                    Check(CheckNo, dt2, dealLayerName, "max(INV_GID) as INV_GID", "", "|id|");
                    CheckSubB(dt3, dt2, dt1, new DataRowGIDComparer());
                    //dt3.Columns.Remove("INV_GID");
                    Check(CheckNo, dt2, "DK", "max(WORKS_TYPE) as WORKS_TYPE", "", "|id|");
                    dt2 = ToDataTable(dt2.Select("WORKS_TYPE=2"));
                    //dt2.Columns.Remove("WORKS_TYPE");
                    CheckSubB(dt, dt2, dt3, new DataRowIDComparer());

                    dealLayerName = "INV_EMPIT";
                    Check(CheckNo, dt1, dealLayerName, "INV_GID", pWhere);
                    Check(CheckNo, dt2, dealLayerName, "max(INV_GID) as INV_GID", "", "|id|");
                    CheckSubB(dt3, dt2, dt1, new DataRowGIDComparer());
                    //dt3.Columns.Remove("INV_GID");
                    Check(CheckNo, dt2, "EMPIT", "max(WORKS_TYPE) as WORKS_TYPE", "", "|id|");
                    dt2 = ToDataTable(dt2.Select("WORKS_TYPE=2"));
                    //dt2.Columns.Remove("WORKS_TYPE");
                    CheckSubB(dt, dt2, dt3, new DataRowIDComparer());

                    dealLayerName = "INV_MC";
                    Check(CheckNo, dt1, dealLayerName, "INV_GID", pWhere);
                    Check(CheckNo, dt2, dealLayerName, "max(INV_GID) as INV_GID", "", "|id|");
                    CheckSubB(dt3, dt2, dt1, new DataRowGIDComparer());
                    //dt3.Columns.Remove("INV_GID");
                    Check(CheckNo, dt2, "MC", "max(WORKS_TYPE) as WORKS_TYPE", "", "|id|");
                    dt2 = ToDataTable(dt2.Select("WORKS_TYPE=2"));
                    //dt2.Columns.Remove("WORKS_TYPE");
                    CheckSubB(dt, dt2, dt3, new DataRowIDComparer());

                    dealLayerName = "INV_RPW";
                    Check(CheckNo, dt1, dealLayerName, "INV_GID", pWhere);
                    Check(CheckNo, dt2, dealLayerName, "max(INV_GID) as INV_GID", "", "|id|");
                    CheckSubB(dt3, dt2, dt1, new DataRowGIDComparer());
                    //dt3.Columns.Remove("INV_GID");
                    Check(CheckNo, dt2, "RPW", "max(WORKS_TYPE) as WORKS_TYPE", "", "|id|");
                    dt2 = ToDataTable(dt2.Select("WORKS_TYPE=2"));
                    //dt2.Columns.Remove("WORKS_TYPE");
                    CheckSubB(dt, dt2, dt3, new DataRowIDComparer());
                    break;
                case "27":
                    dealLayerName = "INV_RAIL,INV_BF,INV_EG,INV_NB,INV_CC";
                    pField = "INSTALL_DT,";
                    pWhere = "INSTALL_DT<>null";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere);
                    Check(CheckNo, dt2, "RB", "WORKS_DATE as INSTALL_DT,", "WORKS_TYPE=1");
                    CheckSubA(dt, dt1, dt2, new DataRowIDIDComparer());

                    dealLayerName = "INV_GR,INV_PIPE,INV_GS,INV_MH,INV_CP";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere);
                    Check(CheckNo, dt2, "RD", "WORKS_DATE as INSTALL_DT,", "WORKS_TYPE=1");
                    CheckSubA(dt, dt1, dt2, new DataRowIDIDComparer());

                    dealLayerName = "INV_TS";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere);
                    Check(CheckNo, dt2, "TS", "WORKS_DATE as INSTALL_DT,", "WORKS_TYPE=1");
                    CheckSubA(dt, dt1, dt2, new DataRowIDIDComparer());

                    dealLayerName = "INV_SNP,INV_VS";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere);
                    Check(CheckNo, dt2, "SNP_VS", "WORKS_DATE as INSTALL_DT,", "WORKS_TYPE=1");
                    CheckSubA(dt, dt1, dt2, new DataRowIDIDComparer());

                    dealLayerName = "INV_TP";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere);
                    Check(CheckNo, dt2, "TP", "WORKS_DATE as INSTALL_DT,", "WORKS_TYPE=1");
                    CheckSubA(dt, dt1, dt2, new DataRowIDIDComparer());

                    dealLayerName = "INV_DK";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere);
                    Check(CheckNo, dt2, "DK", "WORKS_DATE as INSTALL_DT,", "WORKS_TYPE=1");
                    CheckSubA(dt, dt1, dt2, new DataRowIDIDComparer());

                    dealLayerName = "INV_EMPIT";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere);
                    Check(CheckNo, dt2, "EMPIT", "WORKS_DATE as INSTALL_DT,", "WORKS_TYPE=1");
                    CheckSubA(dt, dt1, dt2, new DataRowIDIDComparer());
                    break;
                case "28":
                    dt.Columns.Add("VERSION");

                    dealLayerName = "INV_HRS,INV_HUS,INV_ELE_BEARING,INV_ELE_JOINT,INV_ELE_FD,INV_ELE_INVERT,INV_ELE_ABUT,INV_ELE_COL,INV_ELE_GIRD,INV_ELE_DIAP,INV_ELE_SOFFIT,INV_ELE_DECK,INV_ELE_FLOOR,INV_ELE_RAMPS,INV_ELE_TOWER,INV_ELE_DRAIN,INV_ELE_ROOF,INV_ELE_FINISH,INV_RAIL,INV_BF,INV_CC,INV_EG,INV_NB,INV_GR,INV_PIPE,INV_GS,INV_MH,INV_CP,INV_TS,INV_VEG,INV_SNP,INV_VS,INV_TP,INV_DK,INV_EMPIT,INV_MC,INV_T_ELE_DR,INV_T_ELE_JT,INV_T_ELE_CO,INV_T_ELE_CP,INV_T_ELE_CR,INV_T_ELE_CU,INV_T_ELE_FC,INV_T_ELE_KB,INV_T_ELE_PS,INV_T_ELE_PW,INV_T_ELE_VP,INV_RPW";
                    pField = "MAX(I_VERSION) as VERSION,";
                    pGroupBy = "|id|";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere, pGroupBy);
                    Check(CheckNo, dt2, dealLayerName, "(count(*)-1) as VERSION,", pWhere, pGroupBy);
                    CheckSubA(dt, dt1, dt2, new DataRowIDVComparer());
                    Check(CheckNo, dt1, "SP,ST,RB,RD,TS,VEG,SNP_VS,TP,DK,EMPIT,MC,TU,RPW", "MAX(M_VERSION) as VERSION,", pWhere, pGroupBy);
                    Check(CheckNo, dt2, "SP,ST,RB,RD,TS,VEG,SNP_VS,TP,DK,EMPIT,MC,TU,RPW", "(count(*)-1) as VERSION,", pWhere, pGroupBy);
                    CheckSubA(dt, dt1, dt2, new DataRowIDVComparer());
                    break;
                case "29":
                    dealLayerName = "INV_ELE_BEARING,INV_ELE_JOINT,INV_ELE_FD,INV_ELE_INVERT,INV_ELE_ABUT,INV_ELE_COL,INV_ELE_GIRD,INV_ELE_DIAP,INV_ELE_SOFFIT,INV_ELE_DECK,INV_ELE_FLOOR,INV_ELE_RAMPS,INV_ELE_TOWER,INV_ELE_DRAIN,INV_ELE_ROOF,INV_ELE_FINISH";
                    pField = "FEAT_TYPE,";
                    pWhere = "FEAT_TYPE<1 or FEAT_TYPE=11 or FEAT_TYPE>19";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "INV_RAIL";
                    pField = "RAIL_TYPE,";
                    pWhere = "RAIL_TYPE<1 or (RAIL_TYPE>9 and RAIL_TYPE<26) or RAIL_TYPE>28";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "INV_BF";
                    pField = "BARR_TYPE,";
                    pWhere = "BARR_TYPE<10 or (BARR_TYPE>17 and BARR_TYPE<20) or BARR_TYPE>25";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "INV_EG";
                    pField = "EG_TYPE,";
                    pWhere = "EG_TYPE<18 or (EG_TYPE>18 and EG_TYPE<29) or EG_TYPE>32";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "INV_RPW";
                    pField = "FEAT_TYPE,";
                    pWhere = "FEAT_TYPE>5 and FEAT_TYPE<1";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "INV_PIPE";
                    pField = "TYPE,";
                    pWhere = "TYPE<1 or (TYPE>3 and TYPE<8) or TYPE>9";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "INV_GR";
                    pField = "GRAT_MAT,";
                    pWhere = "GRAT_MAT>2 and GRAT_MAT<1";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "INV_TS";
                    pField = "SIGN_TYPE,";
                    pWhere = "SIGN_TYPE>2 and SIGN_TYPE<1";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "INV_SNP";
                    pField = "MOUNT_TYPE,";
                    pWhere = "MOUNT_TYPE not in ('A1','A2','A3','A4','A5','A6','A7','B1','B2','B3','B4','O')";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "INV_TP";
                    pField = "MATERIAL,";
                    pWhere = "MATERIAL<0 or MATERIAL>3";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);
                    pField = "LOC_TYPE,";
                    pWhere = "LOC_TYPE not in ('1','2','3')";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "INV_MC";
                    pField = "COVERTYPE,";
                    pWhere = "COVERTYPE<1 or COVERTYPE>3";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);
                    pField = "MH_TYPE,";
                    pWhere = "MH_TYPE>2 and MH_TYPE<1";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);
                    pField = "COVERSHAPE,";
                    pWhere = "COVERSHAPE not in ('C','R','S')";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);
                    pField = "MAINT,";
                    pWhere = "MAINT<1 or MAINT>2";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "INV_T_ELE_CR,INV_T_ELE_CU,INV_T_ELE_FC,INV_T_ELE_PW,INV_T_ELE_CO,INV_T_ELE_VP,INV_T_ELE_CP,INV_T_ELE_KB,INV_T_ELE_DR,INV_T_ELE_JT,INV_T_ELE_PS";
                    pField = "ELE_TYPE,";
                    pWhere = "ELE_TYPE not in ('01','02','03','04','05','06','07','08','09','10')";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "DK,EMPIT,RPW";
                    pField = "FEAT_TYPE,";
                    pWhere = "FEAT_TYPE<>1";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "TP,VEG";
                    pField = "FEAT_TYPE,";
                    pWhere = "FEAT_TYPE<1 or FEAT_TYPE>3";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "SNP_VS";
                    pField = "FEAT_TYPE,";
                    pWhere = "FEAT_TYPE<1 or FEAT_TYPE>2";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "SP,TS";
                    pField = "FEAT_TYPE,";
                    pWhere = "FEAT_TYPE<1 or FEAT_TYPE>6";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "RD";
                    pField = "FEAT_TYPE,";
                    pWhere = "FEAT_TYPE<1 or FEAT_TYPE>9";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "CM";
                    pField = "FEAT_TYPE,";
                    pWhere = "FEAT_TYPE<1 or FEAT_TYPE=4 or (FEAT_TYPE>9 and FEAT_TYPE<23) or (FEAT_TYPE>24 and FEAT_TYPE<31) or FEAT_TYPE>32";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "ST";
                    pField = "FEAT_TYPE,";
                    pWhere = "FEAT_TYPE<1 or FEAT_TYPE=11 or FEAT_TYPE>19";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "CM";
                    pField = "FEAT_TYPE,";
                    pWhere = "FEAT_TYPE<1 or FEAT_TYPE=12 or (FEAT_TYPE>16 and FEAT_TYPE<18) or FEAT_TYPE>32";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "TP,DK,RPW";
                    pField = "WORKS_TYPE,";
                    pWhere = "WORKS_TYPE<1 or WORKS_TYPE>4";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "CM";
                    pField = "WORKS_TYPE,";
                    pWhere = "WORKS_TYPE<1 or WORKS_TYPE=2 or (WORKS_TYPE>7 and WORKS_TYPE<34) or (WORKS_TYPE>34 and WORKS_TYPE<45) or WORKS_TYPE>46";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "SP";
                    pField = "WORKS_TYPE,";
                    pWhere = "WORKS_TYPE<14 or WORKS_TYPE>25";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "ST";
                    pField = "WORKS_TYPE,";
                    pWhere = "WORKS_TYPE<26 or WORKS_TYPE>43";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "RB";
                    pField = "WORKS_TYPE,";
                    pWhere = "WORKS_TYPE<1 or (WORKS_TYPE>4 and WORKS_TYPE<8) or (WORKS_TYPE>8 and WORKS_TYPE<26) or (WORKS_TYPE>26 and WORKS_TYPE<40) or WORKS_TYPE>44";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "RD";
                    pField = "WORKS_TYPE,";
                    pWhere = "WORKS_TYPE<1 or (WORKS_TYPE>4 and WORKS_TYPE<8) or WORKS_TYPE>11";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "TS";
                    pField = "WORKS_TYPE,";
                    pWhere = "WORKS_TYPE<1 or (WORKS_TYPE>3 and WORKS_TYPE<6) or WORKS_TYPE>6";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "VEG";
                    pField = "WORKS_TYPE,";
                    pWhere = "WORKS_TYPE<0 or WORKS_TYPE>14";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "SNP_VS";
                    pField = "WORKS_TYPE,";
                    pWhere = "WORKS_TYPE<1 or (WORKS_TYPE>4 and WORKS_TYPE<8) or WORKS_TYPE>8";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "EMPIT";
                    pField = "WORKS_TYPE,";
                    pWhere = "WORKS_TYPE<1 or (WORKS_TYPE>4 and WORKS_TYPE<12) or WORKS_TYPE>12";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "MC";
                    pField = "WORKS_TYPE,";
                    pWhere = "WORKS_TYPE<1 or (WORKS_TYPE>3 and WORKS_TYPE<11) or WORKS_TYPE>11";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "TU";
                    pField = "WORKS_TYPE,";
                    pWhere = "WORKS_TYPE<26 or (WORKS_TYPE>43 and WORKS_TYPE<99) or WORKS_TYPE>99";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "CM";
                    pField = "CON_TYPE,";
                    pWhere = "CON_TYPE<1 or CON_TYPE>3";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "CM";
                    pField = "SUR_TYPE,";
                    pWhere = "SUR_TYPE<1 or (SUR_TYPE>3 and SUR_TYPE<7) or (SUR_TYPE>8 and SUR_TYPE<10) or SUR_TYPE>10";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "ST";
                    pField = "ELE_TYPE,";
                    pWhere = "ELE_TYPE not in ('01','02','03','04','05','06','07','08','09','10','11','14','15','16','17','18')";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);
                    break;
                case "30":
                    dealLayerName = "INV_HRS,INV_HUS,INV_ELE_BEARING,INV_ELE_JOINT,INV_ELE_FD,INV_ELE_INVERT,INV_ELE_ABUT,INV_ELE_COL,INV_ELE_GIRD,INV_ELE_DIAP,INV_ELE_SOFFIT,INV_ELE_DECK,INV_ELE_FLOOR,INV_ELE_RAMPS,INV_ELE_TOWER,INV_ELE_DRAIN,INV_ELE_ROOF,INV_ELE_FINISH,INV_RAIL,INV_BF,INV_CC,INV_EG,INV_NB,INV_GR,INV_GS,INV_MH,INV_CP,INV_PIPE,INV_TS,INV_VEG,INV_SNP,INV_VS,INV_TP,INV_DK,INV_EMPIT,INV_MC,INV_T_ELE_DR,INV_T_ELE_JT,INV_T_ELE_CO,INV_T_ELE_CP,INV_T_ELE_CR,INV_T_ELE_CU,INV_T_ELE_FC,INV_T_ELE_KB,INV_T_ELE_PS,INV_T_ELE_PW,INV_T_ELE_VP,INV_RPW,INV_TREE";
                    pField = "TIMESTAMP,";
                    pWhere = "TIMESTAMP=null or SUB_DATE=null or TIMESTAMP='0:00:00' or SUB_DATE='0:00:00'";
                    pWhere = "TIMESTAMP=null or SUB_DATE=null";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);

                    dealLayerName = "CM,SP,ST,RB,RD,TS,VEG,SNP_VS,TP,DK,EMPIT,MC,TU,RPW";
                    pField = "WORKS_DATE,";
                    pWhere = "WORKS_DATE=null or SUB_DATE=null or WORKS_DATE='0:00:00' or SUB_DATE='0:00:00'";
                    pWhere = "WORKS_DATE=null or SUB_DATE=null";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);
                    break;
                case "31":
                    dealLayerName = "INV_SNP";
                    pField = "OLD_PLATE,P_User_ID,";
                    pWhere = "P_User_ID='' and OLD_PLATE='Y'";
                    Check(CheckNo, dt, dealLayerName, pField, pWhere);
                    break;
                case "32":
                    dealLayerName = "CM";
                    pWhere = "PMS='Y'";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere);
                    Check(CheckNo, dt2, "CM_PMS");
                    CheckSubA(dt, dt1, dt2, new DataRowIDComparer());
                    pWhere = "PG='Y'";
                    Check(CheckNo, dt1, dealLayerName, pField, pWhere);
                    Check(CheckNo, dt2, "CM_PG");
                    CheckSubA(dt, dt1, dt2, new DataRowIDComparer());
                    break;



            }


        }

        private void Check(string CheckNo, DataTable dt, string dealLayerName, string pField ="", string pWhere = "", string pGroupBy = "", string pOrderBy = "")
        {
            string name = null;
            string sql = null;
            string[] arrayLayerName = dealLayerName.Split(',');
            for (int II = 0; II < arrayLayerName.GetLength(0); II++)
            {
                name = arrayLayerName[II].ToString().Trim();
                if (!CheckOK(CheckNo, name))
                {
                    continue;
                }
                if (!string.IsNullOrEmpty(pField))
                {
                    pField = pField + ",";
                    pField = pField.Replace(",,", ",");
                }
                sql = getSelect(CheckNo, pField, hasInfo[name].ToString(), name);
                if(!string.IsNullOrEmpty(pWhere))
                    sql += " where " + pWhere;
                if (!string.IsNullOrEmpty(pOrderBy))
                    sql += " order by " + pOrderBy;
                if (!string.IsNullOrEmpty(pGroupBy))
                {
                    sql += " group by " + pGroupBy;
                    switch (name) //获取选择的内容
                    {
                        case "TS":
                            sql = sql.Replace("a.SUB_DATE", "MAX(a.SUB_DATE)");
                            break;
                        case "CM_PMS":
                        case "CM_PG":
                        case "INV_PMS":
                        case "INV_PG":

                            break;
                        default:
                            sql = sql.Replace("SUB_DATE", "MAX(SUB_DATE)");
                            break;
                    }
                }
                    
                sql = sql.Replace("|id|", hasInfo[name].ToString()).Replace("|right2|", name.Substring(name.Length - 2));
                tNowLayer.Text = name;
                getData(sql, dt);
            }
            
        }
        //ToDataTable
        private DataTable ToDataTable(DataRow[] drs)
        {
            DataTable dt = new DataTable();
            if (drs.Length > 0)
                dt = drs.CopyToDataTable();

            return dt;
        }

        private void CheckSubA(DataTable dt, DataTable dt1, DataTable dt2, IEqualityComparer<DataRow> drc)
        {
            var row1 = dt1.Rows.Cast<DataRow>();
            var row2 = dt2.Rows.Cast<DataRow>();

            if (dt1.Rows.Count < 1){
                dt1 = new DataTable();
                dt2 = new DataTable();
                return;
            }
            if (dt2.Rows.Count < 1)
            {
                foreach (DataRow dr in dt1.Rows)
                {
                    dt.ImportRow(dr);
                }
            }
            else
            {
                var except1 = row1.Except(row2, drc);//1在2中不存在
                foreach (DataRow dr in except1)
                {
                    dt.ImportRow(dr);
                }
            }
            dt1 = new DataTable();
            dt2 = new DataTable();
        }
        private void CheckSubB(DataTable dt, DataTable dt1, DataTable dt2, IEqualityComparer<DataRow> drc)
        {
            var row1 = dt1.Rows.Cast<DataRow>();
            var row2 = dt2.Rows.Cast<DataRow>();
            if (dt1.Rows.Count < 1 || dt2.Rows.Count < 1)
            {
                dt1 = new DataTable();
                dt2 = new DataTable();
                return;
            }
            var except1 = row1.Intersect(row2, drc);//1和2交集
            foreach (DataRow dr in except1)
            {
                dt.ImportRow(dr);
            }
            dt1 = new DataTable();
            dt2 = new DataTable();
        }
        class DataRowIDComparer : IEqualityComparer<DataRow>//只比较ID
        {
            public bool Equals(DataRow x, DataRow y)
            {
                return x["id"].Equals(x["id"]);
            }
            public int GetHashCode(DataRow obj)
            {
                return obj["id"].GetHashCode();
            }
        }
        class DataRowGIDComparer : IEqualityComparer<DataRow>//只比较ID
        {
            public bool Equals(DataRow x, DataRow y)
            {
                return x["INV_GID"].Equals(x["INV_GID"]);
            }
            public int GetHashCode(DataRow obj)
            {
                return obj["INV_GID"].GetHashCode();
            }
        }
        class DataRowPUIDComparer : IEqualityComparer<DataRow>//只比较ID
        {
            public bool Equals(DataRow x, DataRow y)
            {
                return x["P_User_ID"].Equals(x["P_User_ID"]);
            }
            public int GetHashCode(DataRow obj)
            {
                return obj["P_User_ID"].GetHashCode();
            }
        }
        class DataRowPUIDDComparer : IEqualityComparer<DataRow>//比较ID,DELETE
        {
            public bool Equals(DataRow x, DataRow y)
            {
                return (x["P_User_ID"].Equals(x["P_User_ID"]) && x["DELETE"].Equals(x["DELETE"]));
            }
            public int GetHashCode(DataRow obj)
            {
                return (obj["P_User_ID"].ToString() + obj["DELETE"].ToString()).GetHashCode();
            }
        }
        class DataRowIDIComparer : IEqualityComparer<DataRow>//比较ID,I_VERSION
        {
            public bool Equals(DataRow x, DataRow y)
            {
                return (x["id"].Equals(x["id"]) && x["I_VERSION"].Equals(x["I_VERSION"]));
            }
            public int GetHashCode(DataRow obj)
            {
                return (obj["id"].ToString()+obj["I_VERSION"].ToString()).GetHashCode();
            }
        }
        class DataRowIDVComparer : IEqualityComparer<DataRow>//比较ID,I_VERSION
        {
            public bool Equals(DataRow x, DataRow y)
            {
                return (x["id"].Equals(x["id"]) && x["VERSION"].Equals(x["VERSION"]));
            }
            public int GetHashCode(DataRow obj)
            {
                return (obj["id"].ToString() + obj["VERSION"].ToString()).GetHashCode();
            }
        }
        class DataRowIDIDComparer : IEqualityComparer<DataRow>//比较ID,I_VERSION
        {
            public bool Equals(DataRow x, DataRow y)
            {
                return (x["id"].Equals(x["id"]) && x["INSTALL_DT"].Equals(x["INSTALL_DT"]));
            }
            public int GetHashCode(DataRow obj)
            {
                return (obj["id"].ToString() + obj["INSTALL_DT"].ToString()).GetHashCode();
            }
        }
        class DataRowIDMDateComparer : IEqualityComparer<DataRow>//比较ID,M_VERSION,WORKS_DATE
        {
            public bool Equals(DataRow x, DataRow y)
            {
                return (x["id"].Equals(x["id"]) && x["M_VERSION"].Equals(x["M_VERSION"]) && x["WORKS_DATE"].Equals(x["WORKS_DATE"]));
            }
            public int GetHashCode(DataRow obj)
            {
                return (obj["id"].ToString() + obj["M_VERSION"].ToString() + obj["WORKS_DATE"].ToString()).GetHashCode();
            }
        }
        class DataRowCodeNameComparer : IEqualityComparer<DataRow>//
        {
            public bool Equals(DataRow x, DataRow y)
            {
                return (x["ST_CODE"].Equals(x["ST_CODE"]) && x["ROAD_NAME"].Equals(x["ROAD_NAME"]));
            }
            public int GetHashCode(DataRow obj)
            {
                return (obj["ST_CODE"].ToString() + obj["ROAD_NAME"].ToString()).GetHashCode();
            }
        }
        class DataRowIDTypeComparer : IEqualityComparer<DataRow>//比较ID,TYPE
        {
            public bool Equals(DataRow x, DataRow y)
            {
                return (x["id"].Equals(x["id"]) && x["TYPE"].Equals(x["TYPE"]));
            }
            public int GetHashCode(DataRow obj)
            {
                return (obj["id"].ToString() + obj["TYPE"].ToString()).GetHashCode();
            }
        }
        class DataRowIDLDComparer : IEqualityComparer<DataRow>//比较ID,TYPE
        {
            public bool Equals(DataRow x, DataRow y)
            {
                return (x["id"].Equals(x["id"]) && x["LST_MT_DT"].Equals(x["LST_MT_DT"]));
            }
            public int GetHashCode(DataRow obj)
            {
                return (obj["id"].ToString() + obj["LST_MT_DT"].ToString()).GetHashCode();
            }
        }

    }
}
