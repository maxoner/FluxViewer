namespace FluxViewer.App
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.registrarTabPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.zedGraphControl2 = new ZedGraph.ZedGraphControl();
            this.zedGraphControl3 = new ZedGraph.ZedGraphControl();
            this.zedGraphControl4 = new ZedGraph.ZedGraphControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.btn_minus_reg = new System.Windows.Forms.Button();
            this.bpn_plus_reg = new System.Windows.Forms.Button();
            this.cb_graphtype = new System.Windows.Forms.ComboBox();
            this.btn_stop = new System.Windows.Forms.Button();
            this.btn_start = new System.Windows.Forms.Button();
            this.dataArchiveTabPage = new System.Windows.Forms.TabPage();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.btn_achive_down = new System.Windows.Forms.Button();
            this.btn_achive_up = new System.Windows.Forms.Button();
            this.btn_achive_minus = new System.Windows.Forms.Button();
            this.btn_achive_plus = new System.Windows.Forms.Button();
            this.btn_achive_autozoom = new System.Windows.Forms.Button();
            this.zedGraphControl5 = new ZedGraph.ZedGraphControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox17 = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.exportTabPage = new System.Windows.Forms.TabPage();
            this.exportMainGroupBox = new System.Windows.Forms.GroupBox();
            this.exportGroupBox = new System.Windows.Forms.GroupBox();
            this.fillHolesCheckBox = new System.Windows.Forms.CheckBox();
            this.exportProgressBar = new System.Windows.Forms.ProgressBar();
            this.secondsLabel = new System.Windows.Forms.Label();
            this.exportIntervalsTextBox = new System.Windows.Forms.TextBox();
            this.filterApplyingCheckBox = new System.Windows.Forms.CheckBox();
            this.dateFormatLabel = new System.Windows.Forms.Label();
            this.dateFormatComboBox = new System.Windows.Forms.ComboBox();
            this.exportButton = new System.Windows.Forms.Button();
            this.beginExportDateLabel = new System.Windows.Forms.Label();
            this.beginExportDate = new System.Windows.Forms.DateTimePicker();
            this.endExportDateLabel = new System.Windows.Forms.Label();
            this.endExportDate = new System.Windows.Forms.DateTimePicker();
            this.exportTypeLabel = new System.Windows.Forms.Label();
            this.exportTypeComboBox = new System.Windows.Forms.ComboBox();
            this.exportFieldsGroupBox = new System.Windows.Forms.GroupBox();
            this.dateTimeForExportCheckBox = new System.Windows.Forms.CheckBox();
            this.fluxForExportCheckBox = new System.Windows.Forms.CheckBox();
            this.tempForExportCheckBox = new System.Windows.Forms.CheckBox();
            this.hummForExportCheckBox = new System.Windows.Forms.CheckBox();
            this.presForExportCheckBox = new System.Windows.Forms.CheckBox();
            this.exportInfoGroupBox = new System.Windows.Forms.GroupBox();
            this.lastExportDateTextBox = new System.Windows.Forms.TextBox();
            this.firstExportDateTextBox = new System.Windows.Forms.TextBox();
            this.lastExportDateLabel = new System.Windows.Forms.Label();
            this.firstExportDateLabel = new System.Windows.Forms.Label();
            this.exportDataCountTextBox = new System.Windows.Forms.TextBox();
            this.exportDataCountLabel = new System.Windows.Forms.Label();
            this.settingsTabPage = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.rb_isFluxclock = new System.Windows.Forms.RadioButton();
            this.rb_isPCclock = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btn_testconnect = new System.Windows.Forms.Button();
            this.rb_485_con = new System.Windows.Forms.RadioButton();
            this.rb_wifi_con = new System.Windows.Forms.RadioButton();
            this.com_name = new System.Windows.Forms.ComboBox();
            this.btn_updateport = new System.Windows.Forms.Button();
            this.com_speed = new System.Windows.Forms.ComboBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.gb_settings = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.cb_st_speed = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cb_st_protocol = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cb_st_pga = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cb_st_freq = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_st_format = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.tb_st_B = new System.Windows.Forms.TextBox();
            this.tb_st_K = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_st_mode = new System.Windows.Forms.ComboBox();
            this.btn_writesettings = new System.Windows.Forms.Button();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.groupBox21 = new System.Windows.Forms.GroupBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.textBox15 = new System.Windows.Forms.TextBox();
            this.textBox16 = new System.Windows.Forms.TextBox();
            this.textBox17 = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox20 = new System.Windows.Forms.GroupBox();
            this.num_linewidth = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rb_templot_2 = new System.Windows.Forms.RadioButton();
            this.rb_templot_1 = new System.Windows.Forms.RadioButton();
            this.btn_graph_settings = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.check_grid = new System.Windows.Forms.CheckBox();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button15 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this.groupBox18 = new System.Windows.Forms.GroupBox();
            this.groupBox19 = new System.Windows.Forms.GroupBox();
            this.button17 = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.button16 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btn_flash = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_flash_edit = new System.Windows.Forms.Button();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.linkLabel5 = new System.Windows.Forms.LinkLabel();
            this.linkLabel4 = new System.Windows.Forms.LinkLabel();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.label34 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip1.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.registrarTabPage.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.dataArchiveTabPage.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox17.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.exportTabPage.SuspendLayout();
            this.exportMainGroupBox.SuspendLayout();
            this.exportGroupBox.SuspendLayout();
            this.exportFieldsGroupBox.SuspendLayout();
            this.exportInfoGroupBox.SuspendLayout();
            this.settingsTabPage.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.gb_settings.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.groupBox21.SuspendLayout();
            this.groupBox20.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_linewidth)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tabPage10.SuspendLayout();
            this.groupBox18.SuspendLayout();
            this.groupBox19.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage8.SuspendLayout();
            this.groupBox16.SuspendLayout();
            this.tabPage9.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel4});
            this.statusStrip1.Location = new System.Drawing.Point(0, 589);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1284, 26);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(133, 21);
            this.toolStripStatusLabel1.Text = "Статус подключения: ";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.AutoSize = false;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(120, 21);
            this.toolStripStatusLabel2.Text = "Не подключено";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(117, 21);
            this.toolStripStatusLabel3.Text = "Версия прошивки:";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(12, 21);
            this.toolStripStatusLabel4.Text = "-";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "disconect.png");
            this.imageList1.Images.SetKeyName(1, "connect.png");
            // 
            // mainTabControl
            // 
            this.mainTabControl.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.mainTabControl.CausesValidation = false;
            this.mainTabControl.Controls.Add(this.registrarTabPage);
            this.mainTabControl.Controls.Add(this.dataArchiveTabPage);
            this.mainTabControl.Controls.Add(this.exportTabPage);
            this.mainTabControl.Controls.Add(this.settingsTabPage);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.mainTabControl.ItemSize = new System.Drawing.Size(200, 50);
            this.mainTabControl.Location = new System.Drawing.Point(0, 0);
            this.mainTabControl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(1284, 589);
            this.mainTabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.mainTabControl.TabIndex = 3;
            // 
            // registrarTabPage
            // 
            this.registrarTabPage.Controls.Add(this.tableLayoutPanel1);
            this.registrarTabPage.Controls.Add(this.panel1);
            this.registrarTabPage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.registrarTabPage.Location = new System.Drawing.Point(4, 54);
            this.registrarTabPage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.registrarTabPage.Name = "registrarTabPage";
            this.registrarTabPage.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.registrarTabPage.Size = new System.Drawing.Size(1276, 531);
            this.registrarTabPage.TabIndex = 0;
            this.registrarTabPage.Text = "РЕГИСТРАТОР";
            this.registrarTabPage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.zedGraphControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.zedGraphControl2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.zedGraphControl3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.zedGraphControl4, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 55);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1270, 474);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.AutoSize = true;
            this.zedGraphControl1.BackColor = System.Drawing.SystemColors.Control;
            this.zedGraphControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedGraphControl1.Location = new System.Drawing.Point(4, 4);
            this.zedGraphControl1.Margin = new System.Windows.Forms.Padding(4);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(627, 229);
            this.zedGraphControl1.TabIndex = 0;
            this.zedGraphControl1.UseExtendedPrintDialog = true;
            this.zedGraphControl1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.zedGraphControl1_MouseClick);
            this.zedGraphControl1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.zedGraphControl1_MouseDoubleClick);
            // 
            // zedGraphControl2
            // 
            this.zedGraphControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedGraphControl2.Location = new System.Drawing.Point(639, 4);
            this.zedGraphControl2.Margin = new System.Windows.Forms.Padding(4);
            this.zedGraphControl2.Name = "zedGraphControl2";
            this.zedGraphControl2.ScrollGrace = 0D;
            this.zedGraphControl2.ScrollMaxX = 0D;
            this.zedGraphControl2.ScrollMaxY = 0D;
            this.zedGraphControl2.ScrollMaxY2 = 0D;
            this.zedGraphControl2.ScrollMinX = 0D;
            this.zedGraphControl2.ScrollMinY = 0D;
            this.zedGraphControl2.ScrollMinY2 = 0D;
            this.zedGraphControl2.Size = new System.Drawing.Size(627, 229);
            this.zedGraphControl2.TabIndex = 1;
            this.zedGraphControl2.UseExtendedPrintDialog = true;
            this.zedGraphControl2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.zedGraphControl2_MouseClick);
            this.zedGraphControl2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.zedGraphControl2_MouseDoubleClick);
            // 
            // zedGraphControl3
            // 
            this.zedGraphControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedGraphControl3.Location = new System.Drawing.Point(4, 241);
            this.zedGraphControl3.Margin = new System.Windows.Forms.Padding(4);
            this.zedGraphControl3.Name = "zedGraphControl3";
            this.zedGraphControl3.ScrollGrace = 0D;
            this.zedGraphControl3.ScrollMaxX = 0D;
            this.zedGraphControl3.ScrollMaxY = 0D;
            this.zedGraphControl3.ScrollMaxY2 = 0D;
            this.zedGraphControl3.ScrollMinX = 0D;
            this.zedGraphControl3.ScrollMinY = 0D;
            this.zedGraphControl3.ScrollMinY2 = 0D;
            this.zedGraphControl3.Size = new System.Drawing.Size(627, 229);
            this.zedGraphControl3.TabIndex = 2;
            this.zedGraphControl3.UseExtendedPrintDialog = true;
            this.zedGraphControl3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.zedGraphControl3_MouseClick);
            this.zedGraphControl3.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.zedGraphControl3_MouseDoubleClick);
            // 
            // zedGraphControl4
            // 
            this.zedGraphControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedGraphControl4.Location = new System.Drawing.Point(639, 241);
            this.zedGraphControl4.Margin = new System.Windows.Forms.Padding(4);
            this.zedGraphControl4.Name = "zedGraphControl4";
            this.zedGraphControl4.ScrollGrace = 0D;
            this.zedGraphControl4.ScrollMaxX = 0D;
            this.zedGraphControl4.ScrollMaxY = 0D;
            this.zedGraphControl4.ScrollMaxY2 = 0D;
            this.zedGraphControl4.ScrollMinX = 0D;
            this.zedGraphControl4.ScrollMinY = 0D;
            this.zedGraphControl4.ScrollMinY2 = 0D;
            this.zedGraphControl4.Size = new System.Drawing.Size(627, 229);
            this.zedGraphControl4.TabIndex = 3;
            this.zedGraphControl4.UseExtendedPrintDialog = true;
            this.zedGraphControl4.MouseClick += new System.Windows.Forms.MouseEventHandler(this.zedGraphControl4_MouseClick);
            this.zedGraphControl4.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.zedGraphControl4_MouseDoubleClick);
            // 
            // panel1
            // 
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.groupBox13);
            this.panel1.Controls.Add(this.btn_stop);
            this.panel1.Controls.Add(this.btn_start);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1270, 53);
            this.panel1.TabIndex = 4;
            // 
            // groupBox13
            // 
            this.groupBox13.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.groupBox13.Controls.Add(this.checkBox2);
            this.groupBox13.Controls.Add(this.btn_minus_reg);
            this.groupBox13.Controls.Add(this.bpn_plus_reg);
            this.groupBox13.Controls.Add(this.cb_graphtype);
            this.groupBox13.Location = new System.Drawing.Point(750, -4);
            this.groupBox13.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox13.Size = new System.Drawing.Size(502, 52);
            this.groupBox13.TabIndex = 15;
            this.groupBox13.TabStop = false;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(390, 21);
            this.checkBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(102, 19);
            this.checkBox2.TabIndex = 21;
            this.checkBox2.Text = "Автомасштаб";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // btn_minus_reg
            // 
            this.btn_minus_reg.Location = new System.Drawing.Point(335, 10);
            this.btn_minus_reg.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_minus_reg.Name = "btn_minus_reg";
            this.btn_minus_reg.Size = new System.Drawing.Size(45, 38);
            this.btn_minus_reg.TabIndex = 19;
            this.btn_minus_reg.Text = "-";
            this.btn_minus_reg.UseVisualStyleBackColor = true;
            this.btn_minus_reg.Click += new System.EventHandler(this.btn_minus_reg_Click);
            // 
            // bpn_plus_reg
            // 
            this.bpn_plus_reg.Location = new System.Drawing.Point(282, 10);
            this.bpn_plus_reg.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bpn_plus_reg.Name = "bpn_plus_reg";
            this.bpn_plus_reg.Size = new System.Drawing.Size(45, 38);
            this.bpn_plus_reg.TabIndex = 18;
            this.bpn_plus_reg.Text = "+";
            this.bpn_plus_reg.UseVisualStyleBackColor = true;
            this.bpn_plus_reg.Click += new System.EventHandler(this.bpn_plus_reg_Click);
            // 
            // cb_graphtype
            // 
            this.cb_graphtype.FormattingEnabled = true;
            this.cb_graphtype.Items.AddRange(new object[] {
            "все графики",
            "электр. поле",
            "температура",
            "давление",
            "влажность"});
            this.cb_graphtype.Location = new System.Drawing.Point(12, 19);
            this.cb_graphtype.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cb_graphtype.Name = "cb_graphtype";
            this.cb_graphtype.Size = new System.Drawing.Size(260, 23);
            this.cb_graphtype.TabIndex = 17;
            this.cb_graphtype.Text = "все графики";
            this.cb_graphtype.SelectedIndexChanged += new System.EventHandler(this.cb_graphtype_SelectedIndexChanged);
            // 
            // btn_stop
            // 
            this.btn_stop.Enabled = false;
            this.btn_stop.Location = new System.Drawing.Point(129, 13);
            this.btn_stop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(96, 26);
            this.btn_stop.TabIndex = 13;
            this.btn_stop.Text = "Остановить";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(21, 13);
            this.btn_start.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(96, 26);
            this.btn_start.TabIndex = 12;
            this.btn_start.Text = "Запустить";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // dataArchiveTabPage
            // 
            this.dataArchiveTabPage.Controls.Add(this.checkBox3);
            this.dataArchiveTabPage.Controls.Add(this.btn_achive_down);
            this.dataArchiveTabPage.Controls.Add(this.btn_achive_up);
            this.dataArchiveTabPage.Controls.Add(this.btn_achive_minus);
            this.dataArchiveTabPage.Controls.Add(this.btn_achive_plus);
            this.dataArchiveTabPage.Controls.Add(this.btn_achive_autozoom);
            this.dataArchiveTabPage.Controls.Add(this.zedGraphControl5);
            this.dataArchiveTabPage.Controls.Add(this.panel2);
            this.dataArchiveTabPage.Location = new System.Drawing.Point(4, 54);
            this.dataArchiveTabPage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataArchiveTabPage.Name = "dataArchiveTabPage";
            this.dataArchiveTabPage.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataArchiveTabPage.Size = new System.Drawing.Size(1276, 531);
            this.dataArchiveTabPage.TabIndex = 1;
            this.dataArchiveTabPage.Text = "АРХИВ ДАННЫХ";
            this.dataArchiveTabPage.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(195, 84);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(157, 23);
            this.checkBox3.TabIndex = 1;
            this.checkBox3.Text = "Автомасштаб по X";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // btn_achive_down
            // 
            this.btn_achive_down.Location = new System.Drawing.Point(158, 81);
            this.btn_achive_down.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_achive_down.Name = "btn_achive_down";
            this.btn_achive_down.Size = new System.Drawing.Size(31, 26);
            this.btn_achive_down.TabIndex = 8;
            this.btn_achive_down.Text = "▼";
            this.btn_achive_down.UseVisualStyleBackColor = true;
            this.btn_achive_down.Click += new System.EventHandler(this.btn_achive_down_Click);
            // 
            // btn_achive_up
            // 
            this.btn_achive_up.Location = new System.Drawing.Point(122, 81);
            this.btn_achive_up.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_achive_up.Name = "btn_achive_up";
            this.btn_achive_up.Size = new System.Drawing.Size(31, 26);
            this.btn_achive_up.TabIndex = 7;
            this.btn_achive_up.Text = "▲";
            this.btn_achive_up.UseVisualStyleBackColor = true;
            this.btn_achive_up.Click += new System.EventHandler(this.btn_achive_up_Click);
            // 
            // btn_achive_minus
            // 
            this.btn_achive_minus.Location = new System.Drawing.Point(86, 81);
            this.btn_achive_minus.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_achive_minus.Name = "btn_achive_minus";
            this.btn_achive_minus.Size = new System.Drawing.Size(31, 26);
            this.btn_achive_minus.TabIndex = 6;
            this.btn_achive_minus.Text = "-";
            this.btn_achive_minus.UseVisualStyleBackColor = true;
            this.btn_achive_minus.Click += new System.EventHandler(this.btn_achive_minus_Click);
            // 
            // btn_achive_plus
            // 
            this.btn_achive_plus.Location = new System.Drawing.Point(50, 81);
            this.btn_achive_plus.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_achive_plus.Name = "btn_achive_plus";
            this.btn_achive_plus.Size = new System.Drawing.Size(31, 26);
            this.btn_achive_plus.TabIndex = 5;
            this.btn_achive_plus.Text = "+";
            this.btn_achive_plus.UseVisualStyleBackColor = true;
            this.btn_achive_plus.Click += new System.EventHandler(this.btn_achive_plus_Click);
            // 
            // btn_achive_autozoom
            // 
            this.btn_achive_autozoom.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_achive_autozoom.Location = new System.Drawing.Point(14, 81);
            this.btn_achive_autozoom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_achive_autozoom.Name = "btn_achive_autozoom";
            this.btn_achive_autozoom.Size = new System.Drawing.Size(31, 26);
            this.btn_achive_autozoom.TabIndex = 4;
            this.btn_achive_autozoom.Text = "az";
            this.btn_achive_autozoom.UseVisualStyleBackColor = true;
            this.btn_achive_autozoom.Click += new System.EventHandler(this.btn_achive_autozoom_Click);
            // 
            // zedGraphControl5
            // 
            this.zedGraphControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedGraphControl5.Location = new System.Drawing.Point(3, 77);
            this.zedGraphControl5.Margin = new System.Windows.Forms.Padding(5);
            this.zedGraphControl5.Name = "zedGraphControl5";
            this.zedGraphControl5.ScrollGrace = 0D;
            this.zedGraphControl5.ScrollMaxX = 0D;
            this.zedGraphControl5.ScrollMaxY = 0D;
            this.zedGraphControl5.ScrollMaxY2 = 0D;
            this.zedGraphControl5.ScrollMinX = 0D;
            this.zedGraphControl5.ScrollMinY = 0D;
            this.zedGraphControl5.ScrollMinY2 = 0D;
            this.zedGraphControl5.Size = new System.Drawing.Size(1270, 452);
            this.zedGraphControl5.TabIndex = 1;
            this.zedGraphControl5.UseExtendedPrintDialog = true;
            this.zedGraphControl5.MouseClick += new System.Windows.Forms.MouseEventHandler(this.zedGraphControl5_MouseClick);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.groupBox17);
            this.panel2.Controls.Add(this.groupBox11);
            this.panel2.Controls.Add(this.groupBox10);
            this.panel2.Controls.Add(this.groupBox9);
            this.panel2.Controls.Add(this.groupBox8);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 2);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1270, 75);
            this.panel2.TabIndex = 0;
            // 
            // groupBox17
            // 
            this.groupBox17.Controls.Add(this.button5);
            this.groupBox17.Controls.Add(this.button4);
            this.groupBox17.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox17.Location = new System.Drawing.Point(841, 4);
            this.groupBox17.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox17.Size = new System.Drawing.Size(284, 59);
            this.groupBox17.TabIndex = 5;
            this.groupBox17.TabStop = false;
            this.groupBox17.Text = "Математический анализ";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(143, 24);
            this.button5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(117, 28);
            this.button5.TabIndex = 1;
            this.button5.Text = "Спектры";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(11, 24);
            this.button4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(117, 28);
            this.button4.TabIndex = 0;
            this.button4.Text = "Гистограммы";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.comboBox2);
            this.groupBox11.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox11.Location = new System.Drawing.Point(586, 4);
            this.groupBox11.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox11.Size = new System.Drawing.Size(249, 59);
            this.groupBox11.TabIndex = 3;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "База данных";
            // 
            // comboBox2
            // 
            this.comboBox2.Enabled = false;
            this.comboBox2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(5, 26);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(238, 25);
            this.comboBox2.TabIndex = 0;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.comboBox1);
            this.groupBox10.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox10.Location = new System.Drawing.Point(332, 4);
            this.groupBox10.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox10.Size = new System.Drawing.Size(248, 59);
            this.groupBox10.TabIndex = 2;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Название канала";
            // 
            // comboBox1
            // 
            this.comboBox1.Enabled = false;
            this.comboBox1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "электростатическое поле",
            "температура",
            "давление",
            "влажность"});
            this.comboBox1.Location = new System.Drawing.Point(5, 26);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(238, 25);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged_1);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.dateTimePicker2);
            this.groupBox9.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox9.Location = new System.Drawing.Point(169, 4);
            this.groupBox9.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox9.Size = new System.Drawing.Size(158, 59);
            this.groupBox9.TabIndex = 1;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Дата окончания";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(7, 26);
            this.dateTimePicker2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(146, 25);
            this.dateTimePicker2.TabIndex = 0;
            this.dateTimePicker2.Value = new System.DateTime(2022, 11, 1, 0, 0, 0, 0);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.dateTimePicker1);
            this.groupBox8.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox8.Location = new System.Drawing.Point(5, 4);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox8.Size = new System.Drawing.Size(158, 59);
            this.groupBox8.TabIndex = 0;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Дата начала";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(7, 26);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(145, 25);
            this.dateTimePicker1.TabIndex = 0;
            this.dateTimePicker1.Value = new System.DateTime(2022, 11, 1, 0, 0, 0, 0);
            // 
            // exportTabPage
            // 
            this.exportTabPage.Controls.Add(this.exportMainGroupBox);
            this.exportTabPage.Location = new System.Drawing.Point(4, 54);
            this.exportTabPage.Name = "exportTabPage";
            this.exportTabPage.Size = new System.Drawing.Size(1276, 531);
            this.exportTabPage.TabIndex = 3;
            this.exportTabPage.Text = "ЭКСПОРТ";
            this.exportTabPage.UseVisualStyleBackColor = true;
            this.exportTabPage.Enter += new System.EventHandler(this.exportTabPage_Enter);
            // 
            // exportMainGroupBox
            // 
            this.exportMainGroupBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.exportMainGroupBox.Controls.Add(this.exportGroupBox);
            this.exportMainGroupBox.Controls.Add(this.exportInfoGroupBox);
            this.exportMainGroupBox.Location = new System.Drawing.Point(21, 17);
            this.exportMainGroupBox.Name = "exportMainGroupBox";
            this.exportMainGroupBox.Size = new System.Drawing.Size(1231, 511);
            this.exportMainGroupBox.TabIndex = 2;
            this.exportMainGroupBox.TabStop = false;
            // 
            // exportGroupBox
            // 
            this.exportGroupBox.Controls.Add(this.fillHolesCheckBox);
            this.exportGroupBox.Controls.Add(this.exportProgressBar);
            this.exportGroupBox.Controls.Add(this.secondsLabel);
            this.exportGroupBox.Controls.Add(this.exportIntervalsTextBox);
            this.exportGroupBox.Controls.Add(this.filterApplyingCheckBox);
            this.exportGroupBox.Controls.Add(this.dateFormatLabel);
            this.exportGroupBox.Controls.Add(this.dateFormatComboBox);
            this.exportGroupBox.Controls.Add(this.exportButton);
            this.exportGroupBox.Controls.Add(this.beginExportDateLabel);
            this.exportGroupBox.Controls.Add(this.beginExportDate);
            this.exportGroupBox.Controls.Add(this.endExportDateLabel);
            this.exportGroupBox.Controls.Add(this.endExportDate);
            this.exportGroupBox.Controls.Add(this.exportTypeLabel);
            this.exportGroupBox.Controls.Add(this.exportTypeComboBox);
            this.exportGroupBox.Controls.Add(this.exportFieldsGroupBox);
            this.exportGroupBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.exportGroupBox.Location = new System.Drawing.Point(21, 19);
            this.exportGroupBox.Name = "exportGroupBox";
            this.exportGroupBox.Size = new System.Drawing.Size(623, 498);
            this.exportGroupBox.TabIndex = 0;
            this.exportGroupBox.TabStop = false;
            this.exportGroupBox.Text = "Настройки экспорта";
            // 
            // fillHolesCheckBox
            // 
            this.fillHolesCheckBox.AutoSize = true;
            this.fillHolesCheckBox.Checked = true;
            this.fillHolesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.fillHolesCheckBox.Location = new System.Drawing.Point(33, 202);
            this.fillHolesCheckBox.Name = "fillHolesCheckBox";
            this.fillHolesCheckBox.Size = new System.Drawing.Size(213, 23);
            this.fillHolesCheckBox.TabIndex = 12;
            this.fillHolesCheckBox.Text = "Заполнять пробелы средним";
            this.fillHolesCheckBox.UseVisualStyleBackColor = true;
            // 
            // exportProgressBar
            // 
            this.exportProgressBar.Location = new System.Drawing.Point(33, 469);
            this.exportProgressBar.Name = "exportProgressBar";
            this.exportProgressBar.Size = new System.Drawing.Size(550, 23);
            this.exportProgressBar.TabIndex = 2;
            // 
            // secondsLabel
            // 
            this.secondsLabel.AutoSize = true;
            this.secondsLabel.Location = new System.Drawing.Point(551, 242);
            this.secondsLabel.Name = "secondsLabel";
            this.secondsLabel.Size = new System.Drawing.Size(32, 19);
            this.secondsLabel.TabIndex = 11;
            this.secondsLabel.Text = "сек.";
            // 
            // exportIntervalsTextBox
            // 
            this.exportIntervalsTextBox.Enabled = false;
            this.exportIntervalsTextBox.Location = new System.Drawing.Point(383, 236);
            this.exportIntervalsTextBox.Name = "exportIntervalsTextBox";
            this.exportIntervalsTextBox.Size = new System.Drawing.Size(162, 25);
            this.exportIntervalsTextBox.TabIndex = 10;
            // 
            // filterApplyingCheckBox
            // 
            this.filterApplyingCheckBox.AutoSize = true;
            this.filterApplyingCheckBox.Enabled = false;
            this.filterApplyingCheckBox.Location = new System.Drawing.Point(33, 238);
            this.filterApplyingCheckBox.Name = "filterApplyingCheckBox";
            this.filterApplyingCheckBox.Size = new System.Drawing.Size(293, 23);
            this.filterApplyingCheckBox.TabIndex = 5;
            this.filterApplyingCheckBox.Text = "Экспортировать равными промежутками";
            this.filterApplyingCheckBox.UseVisualStyleBackColor = true;
            // 
            // dateFormatLabel
            // 
            this.dateFormatLabel.AutoSize = true;
            this.dateFormatLabel.Location = new System.Drawing.Point(33, 163);
            this.dateFormatLabel.Name = "dateFormatLabel";
            this.dateFormatLabel.Size = new System.Drawing.Size(93, 19);
            this.dateFormatLabel.TabIndex = 8;
            this.dateFormatLabel.Text = "Формат даты";
            // 
            // dateFormatComboBox
            // 
            this.dateFormatComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dateFormatComboBox.FormattingEnabled = true;
            this.dateFormatComboBox.Items.AddRange(new object[] {
            "MM/dd/yyyy HH:mm:ss",
            "yyyy’-‘MM’-‘dd’T’HH’:’mm’:’ss"});
            this.dateFormatComboBox.Location = new System.Drawing.Point(383, 163);
            this.dateFormatComboBox.Name = "dateFormatComboBox";
            this.dateFormatComboBox.Size = new System.Drawing.Size(200, 25);
            this.dateFormatComboBox.TabIndex = 9;
            // 
            // exportButton
            // 
            this.exportButton.Enabled = false;
            this.exportButton.Location = new System.Drawing.Point(406, 421);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(177, 42);
            this.exportButton.TabIndex = 7;
            this.exportButton.Text = "Экспорт";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // beginExportDateLabel
            // 
            this.beginExportDateLabel.AutoSize = true;
            this.beginExportDateLabel.Location = new System.Drawing.Point(33, 42);
            this.beginExportDateLabel.Name = "beginExportDateLabel";
            this.beginExportDateLabel.Size = new System.Drawing.Size(87, 19);
            this.beginExportDateLabel.TabIndex = 3;
            this.beginExportDateLabel.Text = "Дата начала";
            // 
            // beginExportDate
            // 
            this.beginExportDate.Location = new System.Drawing.Point(383, 42);
            this.beginExportDate.Name = "beginExportDate";
            this.beginExportDate.Size = new System.Drawing.Size(200, 25);
            this.beginExportDate.TabIndex = 5;
            this.beginExportDate.ValueChanged += new System.EventHandler(this.beginExportDate_ValueChanged);
            // 
            // endExportDateLabel
            // 
            this.endExportDateLabel.AutoSize = true;
            this.endExportDateLabel.Location = new System.Drawing.Point(33, 79);
            this.endExportDateLabel.Name = "endExportDateLabel";
            this.endExportDateLabel.Size = new System.Drawing.Size(81, 19);
            this.endExportDateLabel.TabIndex = 4;
            this.endExportDateLabel.Text = "Дата конца";
            // 
            // endExportDate
            // 
            this.endExportDate.Location = new System.Drawing.Point(383, 82);
            this.endExportDate.Name = "endExportDate";
            this.endExportDate.Size = new System.Drawing.Size(200, 25);
            this.endExportDate.TabIndex = 6;
            this.endExportDate.ValueChanged += new System.EventHandler(this.endExportDate_ValueChanged);
            // 
            // exportTypeLabel
            // 
            this.exportTypeLabel.AutoSize = true;
            this.exportTypeLabel.Location = new System.Drawing.Point(33, 120);
            this.exportTypeLabel.Name = "exportTypeLabel";
            this.exportTypeLabel.Size = new System.Drawing.Size(126, 19);
            this.exportTypeLabel.TabIndex = 0;
            this.exportTypeLabel.Text = "Выходной формат";
            // 
            // exportTypeComboBox
            // 
            this.exportTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.exportTypeComboBox.FormattingEnabled = true;
            this.exportTypeComboBox.Location = new System.Drawing.Point(383, 120);
            this.exportTypeComboBox.Name = "exportTypeComboBox";
            this.exportTypeComboBox.Size = new System.Drawing.Size(200, 25);
            this.exportTypeComboBox.TabIndex = 1;
            // 
            // exportFieldsGroupBox
            // 
            this.exportFieldsGroupBox.Controls.Add(this.dateTimeForExportCheckBox);
            this.exportFieldsGroupBox.Controls.Add(this.fluxForExportCheckBox);
            this.exportFieldsGroupBox.Controls.Add(this.tempForExportCheckBox);
            this.exportFieldsGroupBox.Controls.Add(this.hummForExportCheckBox);
            this.exportFieldsGroupBox.Controls.Add(this.presForExportCheckBox);
            this.exportFieldsGroupBox.Location = new System.Drawing.Point(33, 270);
            this.exportFieldsGroupBox.Name = "exportFieldsGroupBox";
            this.exportFieldsGroupBox.Size = new System.Drawing.Size(253, 193);
            this.exportFieldsGroupBox.TabIndex = 2;
            this.exportFieldsGroupBox.TabStop = false;
            this.exportFieldsGroupBox.Text = "Поля для экспорта";
            // 
            // dateTimeForExportCheckBox
            // 
            this.dateTimeForExportCheckBox.AutoSize = true;
            this.dateTimeForExportCheckBox.Checked = true;
            this.dateTimeForExportCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.dateTimeForExportCheckBox.Location = new System.Drawing.Point(34, 33);
            this.dateTimeForExportCheckBox.Name = "dateTimeForExportCheckBox";
            this.dateTimeForExportCheckBox.Size = new System.Drawing.Size(113, 23);
            this.dateTimeForExportCheckBox.TabIndex = 0;
            this.dateTimeForExportCheckBox.Text = "Дата и время";
            this.dateTimeForExportCheckBox.UseVisualStyleBackColor = true;
            this.dateTimeForExportCheckBox.CheckedChanged += new System.EventHandler(this.dateTimeForExportCheckBox_CheckedChanged);
            // 
            // fluxForExportCheckBox
            // 
            this.fluxForExportCheckBox.AutoSize = true;
            this.fluxForExportCheckBox.Checked = true;
            this.fluxForExportCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.fluxForExportCheckBox.Location = new System.Drawing.Point(34, 62);
            this.fluxForExportCheckBox.Name = "fluxForExportCheckBox";
            this.fluxForExportCheckBox.Size = new System.Drawing.Size(190, 23);
            this.fluxForExportCheckBox.TabIndex = 1;
            this.fluxForExportCheckBox.Text = "Электростатическое поле";
            this.fluxForExportCheckBox.UseVisualStyleBackColor = true;
            this.fluxForExportCheckBox.CheckedChanged += new System.EventHandler(this.fluxForExportCheckBox_CheckedChanged);
            // 
            // tempForExportCheckBox
            // 
            this.tempForExportCheckBox.AutoSize = true;
            this.tempForExportCheckBox.Checked = true;
            this.tempForExportCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tempForExportCheckBox.Location = new System.Drawing.Point(34, 91);
            this.tempForExportCheckBox.Name = "tempForExportCheckBox";
            this.tempForExportCheckBox.Size = new System.Drawing.Size(110, 23);
            this.tempForExportCheckBox.TabIndex = 2;
            this.tempForExportCheckBox.Text = "Температура";
            this.tempForExportCheckBox.UseVisualStyleBackColor = true;
            this.tempForExportCheckBox.CheckedChanged += new System.EventHandler(this.tempForExportCheckBox_CheckedChanged);
            // 
            // hummForExportCheckBox
            // 
            this.hummForExportCheckBox.AutoSize = true;
            this.hummForExportCheckBox.Checked = true;
            this.hummForExportCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.hummForExportCheckBox.Location = new System.Drawing.Point(34, 149);
            this.hummForExportCheckBox.Name = "hummForExportCheckBox";
            this.hummForExportCheckBox.Size = new System.Drawing.Size(95, 23);
            this.hummForExportCheckBox.TabIndex = 4;
            this.hummForExportCheckBox.Text = "Влажность";
            this.hummForExportCheckBox.UseVisualStyleBackColor = true;
            this.hummForExportCheckBox.CheckedChanged += new System.EventHandler(this.hummForExportCheckBox_CheckedChanged);
            // 
            // presForExportCheckBox
            // 
            this.presForExportCheckBox.AutoSize = true;
            this.presForExportCheckBox.Checked = true;
            this.presForExportCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.presForExportCheckBox.Location = new System.Drawing.Point(34, 120);
            this.presForExportCheckBox.Name = "presForExportCheckBox";
            this.presForExportCheckBox.Size = new System.Drawing.Size(89, 23);
            this.presForExportCheckBox.TabIndex = 3;
            this.presForExportCheckBox.Text = "Давление";
            this.presForExportCheckBox.UseVisualStyleBackColor = true;
            this.presForExportCheckBox.CheckedChanged += new System.EventHandler(this.presForExportCheckBox_CheckedChanged);
            // 
            // exportInfoGroupBox
            // 
            this.exportInfoGroupBox.Controls.Add(this.lastExportDateTextBox);
            this.exportInfoGroupBox.Controls.Add(this.firstExportDateTextBox);
            this.exportInfoGroupBox.Controls.Add(this.lastExportDateLabel);
            this.exportInfoGroupBox.Controls.Add(this.firstExportDateLabel);
            this.exportInfoGroupBox.Controls.Add(this.exportDataCountTextBox);
            this.exportInfoGroupBox.Controls.Add(this.exportDataCountLabel);
            this.exportInfoGroupBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.exportInfoGroupBox.Location = new System.Drawing.Point(697, 19);
            this.exportInfoGroupBox.Name = "exportInfoGroupBox";
            this.exportInfoGroupBox.Size = new System.Drawing.Size(555, 182);
            this.exportInfoGroupBox.TabIndex = 1;
            this.exportInfoGroupBox.TabStop = false;
            this.exportInfoGroupBox.Text = "Данные экспорта";
            // 
            // lastExportDateTextBox
            // 
            this.lastExportDateTextBox.Location = new System.Drawing.Point(332, 123);
            this.lastExportDateTextBox.Name = "lastExportDateTextBox";
            this.lastExportDateTextBox.ReadOnly = true;
            this.lastExportDateTextBox.Size = new System.Drawing.Size(183, 25);
            this.lastExportDateTextBox.TabIndex = 5;
            // 
            // firstExportDateTextBox
            // 
            this.firstExportDateTextBox.Location = new System.Drawing.Point(332, 82);
            this.firstExportDateTextBox.Name = "firstExportDateTextBox";
            this.firstExportDateTextBox.ReadOnly = true;
            this.firstExportDateTextBox.Size = new System.Drawing.Size(183, 25);
            this.firstExportDateTextBox.TabIndex = 4;
            // 
            // lastExportDateLabel
            // 
            this.lastExportDateLabel.AutoSize = true;
            this.lastExportDateLabel.Location = new System.Drawing.Point(49, 126);
            this.lastExportDateLabel.Name = "lastExportDateLabel";
            this.lastExportDateLabel.Size = new System.Drawing.Size(219, 19);
            this.lastExportDateLabel.TabIndex = 3;
            this.lastExportDateLabel.Text = "Дата с последними показаниями";
            // 
            // firstExportDateLabel
            // 
            this.firstExportDateLabel.AutoSize = true;
            this.firstExportDateLabel.Location = new System.Drawing.Point(49, 85);
            this.firstExportDateLabel.Name = "firstExportDateLabel";
            this.firstExportDateLabel.Size = new System.Drawing.Size(199, 19);
            this.firstExportDateLabel.TabIndex = 2;
            this.firstExportDateLabel.Text = "Дата с первыми показаниями";
            // 
            // exportDataCountTextBox
            // 
            this.exportDataCountTextBox.Location = new System.Drawing.Point(332, 42);
            this.exportDataCountTextBox.Name = "exportDataCountTextBox";
            this.exportDataCountTextBox.ReadOnly = true;
            this.exportDataCountTextBox.Size = new System.Drawing.Size(183, 25);
            this.exportDataCountTextBox.TabIndex = 1;
            // 
            // exportDataCountLabel
            // 
            this.exportDataCountLabel.AutoSize = true;
            this.exportDataCountLabel.Location = new System.Drawing.Point(49, 51);
            this.exportDataCountLabel.Name = "exportDataCountLabel";
            this.exportDataCountLabel.Size = new System.Drawing.Size(153, 19);
            this.exportDataCountLabel.TabIndex = 0;
            this.exportDataCountLabel.Text = "Количество показаний";
            // 
            // settingsTabPage
            // 
            this.settingsTabPage.Controls.Add(this.tabControl2);
            this.settingsTabPage.Location = new System.Drawing.Point(4, 54);
            this.settingsTabPage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.settingsTabPage.Name = "settingsTabPage";
            this.settingsTabPage.Size = new System.Drawing.Size(1276, 531);
            this.settingsTabPage.TabIndex = 2;
            this.settingsTabPage.Text = "НАСТРОЙКИ";
            this.settingsTabPage.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Controls.Add(this.tabPage6);
            this.tabControl2.Controls.Add(this.tabPage7);
            this.tabControl2.Controls.Add(this.tabPage10);
            this.tabControl2.Controls.Add(this.tabPage8);
            this.tabControl2.Controls.Add(this.tabPage9);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tabControl2.ItemSize = new System.Drawing.Size(50, 300);
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl2.Multiline = true;
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.RightToLeftLayout = true;
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(1276, 531);
            this.tabControl2.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl2.TabIndex = 1;
            this.tabControl2.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl2_DrawItem);
            this.tabControl2.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl2_Selecting);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.groupBox15);
            this.tabPage5.Location = new System.Drawing.Point(304, 4);
            this.tabPage5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage5.Size = new System.Drawing.Size(968, 523);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "Настройка программы";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // groupBox15
            // 
            this.groupBox15.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox15.Controls.Add(this.groupBox7);
            this.groupBox15.Controls.Add(this.groupBox5);
            this.groupBox15.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox15.Location = new System.Drawing.Point(188, 29);
            this.groupBox15.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox15.Size = new System.Drawing.Size(648, 271);
            this.groupBox15.TabIndex = 18;
            this.groupBox15.TabStop = false;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.rb_isFluxclock);
            this.groupBox7.Controls.Add(this.rb_isPCclock);
            this.groupBox7.Enabled = false;
            this.groupBox7.Location = new System.Drawing.Point(32, 170);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(585, 81);
            this.groupBox7.TabIndex = 19;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Синхронизация данных";
            // 
            // rb_isFluxclock
            // 
            this.rb_isFluxclock.AutoSize = true;
            this.rb_isFluxclock.Location = new System.Drawing.Point(37, 47);
            this.rb_isFluxclock.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rb_isFluxclock.Name = "rb_isFluxclock";
            this.rb_isFluxclock.Size = new System.Drawing.Size(232, 23);
            this.rb_isFluxclock.TabIndex = 5;
            this.rb_isFluxclock.Text = "Использовать часы флюксметра";
            this.rb_isFluxclock.UseVisualStyleBackColor = true;
            // 
            // rb_isPCclock
            // 
            this.rb_isPCclock.AutoSize = true;
            this.rb_isPCclock.Checked = true;
            this.rb_isPCclock.Location = new System.Drawing.Point(37, 23);
            this.rb_isPCclock.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rb_isPCclock.Name = "rb_isPCclock";
            this.rb_isPCclock.Size = new System.Drawing.Size(232, 23);
            this.rb_isPCclock.TabIndex = 4;
            this.rb_isPCclock.TabStop = true;
            this.rb_isPCclock.Text = "Использовать часы компьютера";
            this.rb_isPCclock.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btn_testconnect);
            this.groupBox5.Controls.Add(this.rb_485_con);
            this.groupBox5.Controls.Add(this.rb_wifi_con);
            this.groupBox5.Controls.Add(this.com_name);
            this.groupBox5.Controls.Add(this.btn_updateport);
            this.groupBox5.Controls.Add(this.com_speed);
            this.groupBox5.Location = new System.Drawing.Point(32, 26);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox5.Size = new System.Drawing.Size(585, 130);
            this.groupBox5.TabIndex = 17;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Подключение";
            // 
            // btn_testconnect
            // 
            this.btn_testconnect.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_testconnect.Location = new System.Drawing.Point(452, 46);
            this.btn_testconnect.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.btn_testconnect.Name = "btn_testconnect";
            this.btn_testconnect.Size = new System.Drawing.Size(122, 40);
            this.btn_testconnect.TabIndex = 15;
            this.btn_testconnect.Text = "Проверить подключение";
            this.btn_testconnect.UseVisualStyleBackColor = true;
            this.btn_testconnect.Click += new System.EventHandler(this.btn_testconnect_Click);
            // 
            // rb_485_con
            // 
            this.rb_485_con.AutoSize = true;
            this.rb_485_con.Checked = true;
            this.rb_485_con.Location = new System.Drawing.Point(9, 22);
            this.rb_485_con.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rb_485_con.Name = "rb_485_con";
            this.rb_485_con.Size = new System.Drawing.Size(179, 23);
            this.rb_485_con.TabIndex = 14;
            this.rb_485_con.TabStop = true;
            this.rb_485_con.Text = "Подключение по RS485";
            this.rb_485_con.UseVisualStyleBackColor = true;
            // 
            // rb_wifi_con
            // 
            this.rb_wifi_con.AutoSize = true;
            this.rb_wifi_con.Enabled = false;
            this.rb_wifi_con.Location = new System.Drawing.Point(9, 97);
            this.rb_wifi_con.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rb_wifi_con.Name = "rb_wifi_con";
            this.rb_wifi_con.Size = new System.Drawing.Size(172, 23);
            this.rb_wifi_con.TabIndex = 12;
            this.rb_wifi_con.Text = "Подключение по Wi-Fi";
            this.rb_wifi_con.UseVisualStyleBackColor = true;
            // 
            // com_name
            // 
            this.com_name.Location = new System.Drawing.Point(28, 55);
            this.com_name.Name = "com_name";
            this.com_name.Size = new System.Drawing.Size(164, 25);
            this.com_name.TabIndex = 8;
            // 
            // btn_updateport
            // 
            this.btn_updateport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_updateport.Location = new System.Drawing.Point(385, 46);
            this.btn_updateport.Name = "btn_updateport";
            this.btn_updateport.Size = new System.Drawing.Size(44, 40);
            this.btn_updateport.TabIndex = 11;
            this.btn_updateport.Text = "upd";
            this.btn_updateport.UseVisualStyleBackColor = true;
            this.btn_updateport.Click += new System.EventHandler(this.btn_updateport_Click);
            // 
            // com_speed
            // 
            this.com_speed.FormattingEnabled = true;
            this.com_speed.Items.AddRange(new object[] {
            "4800",
            "9600",
            "38400",
            "57600",
            "115200"});
            this.com_speed.Location = new System.Drawing.Point(220, 55);
            this.com_speed.Name = "com_speed";
            this.com_speed.Size = new System.Drawing.Size(147, 25);
            this.com_speed.TabIndex = 10;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.gb_settings);
            this.tabPage4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tabPage4.Location = new System.Drawing.Point(304, 4);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage4.Size = new System.Drawing.Size(968, 523);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "Настройка устройства";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // gb_settings
            // 
            this.gb_settings.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.gb_settings.Controls.Add(this.panel3);
            this.gb_settings.Controls.Add(this.btn_writesettings);
            this.gb_settings.Enabled = false;
            this.gb_settings.Location = new System.Drawing.Point(239, 30);
            this.gb_settings.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gb_settings.Name = "gb_settings";
            this.gb_settings.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gb_settings.Size = new System.Drawing.Size(546, 512);
            this.gb_settings.TabIndex = 24;
            this.gb_settings.TabStop = false;
            this.gb_settings.Text = "Настройки флюксметра";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.cb_st_speed);
            this.panel3.Controls.Add(this.groupBox3);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.cb_st_protocol);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.cb_st_pga);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.cb_st_freq);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.cb_st_format);
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.cb_st_mode);
            this.panel3.Location = new System.Drawing.Point(38, 21);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(476, 443);
            this.panel3.TabIndex = 37;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(60, 301);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 15);
            this.label8.TabIndex = 49;
            this.label8.Text = "Скорость RS485";
            // 
            // cb_st_speed
            // 
            this.cb_st_speed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_st_speed.FormattingEnabled = true;
            this.cb_st_speed.Items.AddRange(new object[] {
            "4800",
            "9600",
            "38400",
            "57600",
            "115200"});
            this.cb_st_speed.Location = new System.Drawing.Point(246, 298);
            this.cb_st_speed.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cb_st_speed.Name = "cb_st_speed";
            this.cb_st_speed.Size = new System.Drawing.Size(170, 23);
            this.cb_st_speed.TabIndex = 48;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Enabled = false;
            this.groupBox3.Location = new System.Drawing.Point(60, 325);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Size = new System.Drawing.Size(355, 108);
            this.groupBox3.TabIndex = 47;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Дата/Время";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(47, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(242, 15);
            this.label7.TabIndex = 1;
            this.label7.Text = "синхронизировать время устройства с ПК ";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(138, 61);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 34);
            this.button1.TabIndex = 0;
            this.button1.Text = "Выполнить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(60, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(161, 15);
            this.label6.TabIndex = 46;
            this.label6.Text = "Протокол передачи данных";
            // 
            // cb_st_protocol
            // 
            this.cb_st_protocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_st_protocol.FormattingEnabled = true;
            this.cb_st_protocol.Items.AddRange(new object[] {
            "ModBus RTU Slave",
            "Master Direct"});
            this.cb_st_protocol.Location = new System.Drawing.Point(246, 92);
            this.cb_st_protocol.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cb_st_protocol.Name = "cb_st_protocol";
            this.cb_st_protocol.Size = new System.Drawing.Size(170, 23);
            this.cb_st_protocol.TabIndex = 45;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(60, 265);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 15);
            this.label5.TabIndex = 44;
            this.label5.Text = "Диапазон измерения";
            // 
            // cb_st_pga
            // 
            this.cb_st_pga.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_st_pga.FormattingEnabled = true;
            this.cb_st_pga.Items.AddRange(new object[] {
            "[±112000В/м]",
            "[±56000В/м]",
            "[±28000В/м]",
            "[±14000В/м]",
            "[±7000В/м]",
            "[±3500В/м]",
            "",
            "",
            ""});
            this.cb_st_pga.Location = new System.Drawing.Point(246, 261);
            this.cb_st_pga.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cb_st_pga.Name = "cb_st_pga";
            this.cb_st_pga.Size = new System.Drawing.Size(170, 23);
            this.cb_st_pga.TabIndex = 43;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(60, 225);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 15);
            this.label4.TabIndex = 42;
            this.label4.Text = "Скорость измерения";
            // 
            // cb_st_freq
            // 
            this.cb_st_freq.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_st_freq.FormattingEnabled = true;
            this.cb_st_freq.Items.AddRange(new object[] {
            "2.5 выб/сек (Fср=1,2 Гц)",
            "5 выб/сек (Fср=2,4 Гц)",
            "10 выб/сек (Fср=4,7 Гц)",
            "16.6 выб/сек (Fср=9 Гц)",
            "20 выб/сек (Fср=13 Гц)"});
            this.cb_st_freq.Location = new System.Drawing.Point(246, 223);
            this.cb_st_freq.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cb_st_freq.Name = "cb_st_freq";
            this.cb_st_freq.Size = new System.Drawing.Size(170, 23);
            this.cb_st_freq.TabIndex = 41;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(60, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 15);
            this.label3.TabIndex = 40;
            this.label3.Text = "Формат данных";
            // 
            // cb_st_format
            // 
            this.cb_st_format.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_st_format.FormattingEnabled = true;
            this.cb_st_format.Items.AddRange(new object[] {
            "HEX",
            "ASCII"});
            this.cb_st_format.Location = new System.Drawing.Point(246, 55);
            this.cb_st_format.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cb_st_format.Name = "cb_st_format";
            this.cb_st_format.Size = new System.Drawing.Size(170, 23);
            this.cb_st_format.TabIndex = 39;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Controls.Add(this.tb_st_B);
            this.groupBox2.Controls.Add(this.tb_st_K);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(60, 119);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(355, 87);
            this.groupBox2.TabIndex = 38;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Уравнение калибровки";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(22, 25);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(146, 19);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "Вывод в миливольтах";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // tb_st_B
            // 
            this.tb_st_B.Location = new System.Drawing.Point(209, 52);
            this.tb_st_B.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tb_st_B.Name = "tb_st_B";
            this.tb_st_B.Size = new System.Drawing.Size(91, 23);
            this.tb_st_B.TabIndex = 2;
            this.tb_st_B.Text = "0";
            // 
            // tb_st_K
            // 
            this.tb_st_K.Location = new System.Drawing.Point(74, 52);
            this.tb_st_K.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tb_st_K.Name = "tb_st_K";
            this.tb_st_K.Size = new System.Drawing.Size(84, 23);
            this.tb_st_K.TabIndex = 1;
            this.tb_st_K.Text = "1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(188, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Y[В/м] =                               *X[В] + ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 15);
            this.label1.TabIndex = 37;
            this.label1.Text = "Режим передачи данных";
            // 
            // cb_st_mode
            // 
            this.cb_st_mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_st_mode.FormattingEnabled = true;
            this.cb_st_mode.Items.AddRange(new object[] {
            "RS485",
            "Wi-Fi",
            "microSD",
            "Аналог\t"});
            this.cb_st_mode.Location = new System.Drawing.Point(246, 19);
            this.cb_st_mode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cb_st_mode.Name = "cb_st_mode";
            this.cb_st_mode.Size = new System.Drawing.Size(170, 23);
            this.cb_st_mode.TabIndex = 36;
            // 
            // btn_writesettings
            // 
            this.btn_writesettings.Location = new System.Drawing.Point(185, 469);
            this.btn_writesettings.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_writesettings.Name = "btn_writesettings";
            this.btn_writesettings.Size = new System.Drawing.Size(174, 32);
            this.btn_writesettings.TabIndex = 36;
            this.btn_writesettings.Text = "Записать в устройство";
            this.btn_writesettings.UseVisualStyleBackColor = true;
            this.btn_writesettings.Click += new System.EventHandler(this.btn_writesettings_Click);
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.groupBox14);
            this.tabPage6.Location = new System.Drawing.Point(304, 4);
            this.tabPage6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(968, 523);
            this.tabPage6.TabIndex = 2;
            this.tabPage6.Text = "Настройка вывода графиков";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // groupBox14
            // 
            this.groupBox14.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox14.Controls.Add(this.groupBox21);
            this.groupBox14.Controls.Add(this.groupBox20);
            this.groupBox14.Controls.Add(this.groupBox1);
            this.groupBox14.Controls.Add(this.btn_graph_settings);
            this.groupBox14.Controls.Add(this.groupBox4);
            this.groupBox14.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox14.Location = new System.Drawing.Point(-40, 24);
            this.groupBox14.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox14.Size = new System.Drawing.Size(824, 302);
            this.groupBox14.TabIndex = 4;
            this.groupBox14.TabStop = false;
            // 
            // groupBox21
            // 
            this.groupBox21.Controls.Add(this.label24);
            this.groupBox21.Controls.Add(this.label25);
            this.groupBox21.Controls.Add(this.textBox15);
            this.groupBox21.Controls.Add(this.textBox16);
            this.groupBox21.Controls.Add(this.textBox17);
            this.groupBox21.Controls.Add(this.label26);
            this.groupBox21.Controls.Add(this.label21);
            this.groupBox21.Controls.Add(this.label22);
            this.groupBox21.Controls.Add(this.textBox12);
            this.groupBox21.Controls.Add(this.textBox13);
            this.groupBox21.Controls.Add(this.textBox14);
            this.groupBox21.Controls.Add(this.label23);
            this.groupBox21.Controls.Add(this.label18);
            this.groupBox21.Controls.Add(this.label19);
            this.groupBox21.Controls.Add(this.textBox9);
            this.groupBox21.Controls.Add(this.textBox10);
            this.groupBox21.Controls.Add(this.textBox11);
            this.groupBox21.Controls.Add(this.label20);
            this.groupBox21.Controls.Add(this.label17);
            this.groupBox21.Controls.Add(this.label16);
            this.groupBox21.Controls.Add(this.textBox8);
            this.groupBox21.Controls.Add(this.textBox7);
            this.groupBox21.Controls.Add(this.textBox6);
            this.groupBox21.Controls.Add(this.label15);
            this.groupBox21.Location = new System.Drawing.Point(18, 17);
            this.groupBox21.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox21.Name = "groupBox21";
            this.groupBox21.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox21.Size = new System.Drawing.Size(532, 269);
            this.groupBox21.TabIndex = 8;
            this.groupBox21.TabStop = false;
            this.groupBox21.Text = "Настройка графиков";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(52, 228);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(31, 19);
            this.label24.TabIndex = 23;
            this.label24.Text = "Y =";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(205, 228);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(41, 19);
            this.label25.TabIndex = 22;
            this.label25.Text = "* X +";
            // 
            // textBox15
            // 
            this.textBox15.Location = new System.Drawing.Point(250, 226);
            this.textBox15.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new System.Drawing.Size(110, 25);
            this.textBox15.TabIndex = 21;
            this.textBox15.Text = "0";
            // 
            // textBox16
            // 
            this.textBox16.Location = new System.Drawing.Point(93, 226);
            this.textBox16.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new System.Drawing.Size(110, 25);
            this.textBox16.TabIndex = 20;
            this.textBox16.Text = "1";
            // 
            // textBox17
            // 
            this.textBox17.Location = new System.Drawing.Point(93, 197);
            this.textBox17.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new System.Drawing.Size(419, 25);
            this.textBox17.TabIndex = 19;
            this.textBox17.Text = "Влажность, %";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(18, 200);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(66, 19);
            this.label26.TabIndex = 18;
            this.label26.Text = "график 4";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(52, 171);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(31, 19);
            this.label21.TabIndex = 17;
            this.label21.Text = "Y =";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(205, 171);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(41, 19);
            this.label22.TabIndex = 16;
            this.label22.Text = "* X +";
            // 
            // textBox12
            // 
            this.textBox12.Location = new System.Drawing.Point(250, 169);
            this.textBox12.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(110, 25);
            this.textBox12.TabIndex = 15;
            this.textBox12.Text = "0";
            // 
            // textBox13
            // 
            this.textBox13.Location = new System.Drawing.Point(93, 169);
            this.textBox13.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(110, 25);
            this.textBox13.TabIndex = 14;
            this.textBox13.Text = " 0,7500637";
            // 
            // textBox14
            // 
            this.textBox14.Location = new System.Drawing.Point(93, 140);
            this.textBox14.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new System.Drawing.Size(419, 25);
            this.textBox14.TabIndex = 13;
            this.textBox14.Text = "Давление, мм. рт. ст.";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(18, 142);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(66, 19);
            this.label23.TabIndex = 12;
            this.label23.Text = "график 3";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(52, 114);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(31, 19);
            this.label18.TabIndex = 11;
            this.label18.Text = "Y =";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(205, 114);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(41, 19);
            this.label19.TabIndex = 10;
            this.label19.Text = "* X +";
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(250, 112);
            this.textBox9.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(110, 25);
            this.textBox9.TabIndex = 9;
            this.textBox9.Text = "0";
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(93, 112);
            this.textBox10.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(110, 25);
            this.textBox10.TabIndex = 8;
            this.textBox10.Text = "1";
            // 
            // textBox11
            // 
            this.textBox11.Location = new System.Drawing.Point(93, 83);
            this.textBox11.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(419, 25);
            this.textBox11.TabIndex = 7;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(18, 86);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(66, 19);
            this.label20.TabIndex = 6;
            this.label20.Text = "график 2";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(52, 57);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(31, 19);
            this.label17.TabIndex = 5;
            this.label17.Text = "Y =";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(205, 57);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(41, 19);
            this.label16.TabIndex = 4;
            this.label16.Text = "* X +";
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(250, 55);
            this.textBox8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(110, 25);
            this.textBox8.TabIndex = 3;
            this.textBox8.Text = "0";
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(93, 55);
            this.textBox7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(110, 25);
            this.textBox7.TabIndex = 2;
            this.textBox7.Text = "1";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(93, 26);
            this.textBox6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(419, 25);
            this.textBox6.TabIndex = 1;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(18, 28);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(66, 19);
            this.label15.TabIndex = 0;
            this.label15.Text = "график 1";
            // 
            // groupBox20
            // 
            this.groupBox20.Controls.Add(this.num_linewidth);
            this.groupBox20.Location = new System.Drawing.Point(564, 154);
            this.groupBox20.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox20.Name = "groupBox20";
            this.groupBox20.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox20.Size = new System.Drawing.Size(242, 53);
            this.groupBox20.TabIndex = 7;
            this.groupBox20.TabStop = false;
            this.groupBox20.Text = "Толщина линии";
            // 
            // num_linewidth
            // 
            this.num_linewidth.Location = new System.Drawing.Point(57, 22);
            this.num_linewidth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.num_linewidth.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.num_linewidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_linewidth.Name = "num_linewidth";
            this.num_linewidth.Size = new System.Drawing.Size(131, 25);
            this.num_linewidth.TabIndex = 0;
            this.num_linewidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rb_templot_2);
            this.groupBox1.Controls.Add(this.rb_templot_1);
            this.groupBox1.Location = new System.Drawing.Point(564, 73);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(242, 77);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Выбрать тему";
            // 
            // rb_templot_2
            // 
            this.rb_templot_2.AutoSize = true;
            this.rb_templot_2.Location = new System.Drawing.Point(15, 46);
            this.rb_templot_2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rb_templot_2.Name = "rb_templot_2";
            this.rb_templot_2.Size = new System.Drawing.Size(107, 23);
            this.rb_templot_2.TabIndex = 1;
            this.rb_templot_2.Text = "Темная тема";
            this.rb_templot_2.UseVisualStyleBackColor = true;
            // 
            // rb_templot_1
            // 
            this.rb_templot_1.AutoSize = true;
            this.rb_templot_1.Checked = true;
            this.rb_templot_1.Location = new System.Drawing.Point(15, 22);
            this.rb_templot_1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rb_templot_1.Name = "rb_templot_1";
            this.rb_templot_1.Size = new System.Drawing.Size(111, 23);
            this.rb_templot_1.TabIndex = 0;
            this.rb_templot_1.TabStop = true;
            this.rb_templot_1.Text = "Светлая тема";
            this.rb_templot_1.UseVisualStyleBackColor = true;
            // 
            // btn_graph_settings
            // 
            this.btn_graph_settings.Location = new System.Drawing.Point(632, 234);
            this.btn_graph_settings.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_graph_settings.Name = "btn_graph_settings";
            this.btn_graph_settings.Size = new System.Drawing.Size(121, 40);
            this.btn_graph_settings.TabIndex = 5;
            this.btn_graph_settings.Text = "Принять";
            this.btn_graph_settings.UseVisualStyleBackColor = true;
            this.btn_graph_settings.Click += new System.EventHandler(this.btn_graph_settings_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.check_grid);
            this.groupBox4.Location = new System.Drawing.Point(564, 16);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Size = new System.Drawing.Size(242, 51);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Сетка";
            // 
            // check_grid
            // 
            this.check_grid.AutoSize = true;
            this.check_grid.Location = new System.Drawing.Point(20, 22);
            this.check_grid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.check_grid.Name = "check_grid";
            this.check_grid.Size = new System.Drawing.Size(126, 23);
            this.check_grid.TabIndex = 0;
            this.check_grid.Text = "включить сетку";
            this.check_grid.UseVisualStyleBackColor = true;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.panel4);
            this.tabPage7.Controls.Add(this.richTextBox1);
            this.tabPage7.Location = new System.Drawing.Point(304, 4);
            this.tabPage7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(968, 523);
            this.tabPage7.TabIndex = 3;
            this.tabPage7.Text = "Терминал";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.button15);
            this.panel4.Controls.Add(this.button14);
            this.panel4.Controls.Add(this.button6);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 471);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(968, 52);
            this.panel4.TabIndex = 3;
            // 
            // button15
            // 
            this.button15.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button15.Location = new System.Drawing.Point(595, 10);
            this.button15.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(262, 33);
            this.button15.TabIndex = 2;
            this.button15.Text = "Запустить вывод данных в ASCII";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // button14
            // 
            this.button14.Enabled = false;
            this.button14.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button14.Location = new System.Drawing.Point(308, 10);
            this.button14.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(262, 33);
            this.button14.TabIndex = 1;
            this.button14.Text = "Журнал работы флюксметра";
            this.button14.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Enabled = false;
            this.button6.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button6.Location = new System.Drawing.Point(20, 10);
            this.button6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(262, 33);
            this.button6.TabIndex = 0;
            this.button6.Text = "Журнал регистрации";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.InfoText;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.richTextBox1.ForeColor = System.Drawing.SystemColors.Menu;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox1.Size = new System.Drawing.Size(968, 523);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // tabPage10
            // 
            this.tabPage10.Controls.Add(this.groupBox18);
            this.tabPage10.Location = new System.Drawing.Point(304, 4);
            this.tabPage10.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Size = new System.Drawing.Size(968, 523);
            this.tabPage10.TabIndex = 6;
            this.tabPage10.Text = "Градуировка";
            this.tabPage10.UseVisualStyleBackColor = true;
            // 
            // groupBox18
            // 
            this.groupBox18.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox18.Controls.Add(this.groupBox19);
            this.groupBox18.Controls.Add(this.radioButton5);
            this.groupBox18.Controls.Add(this.radioButton2);
            this.groupBox18.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox18.Location = new System.Drawing.Point(143, 29);
            this.groupBox18.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox18.Name = "groupBox18";
            this.groupBox18.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox18.Size = new System.Drawing.Size(642, 354);
            this.groupBox18.TabIndex = 0;
            this.groupBox18.TabStop = false;
            this.groupBox18.Text = "Градуировка";
            // 
            // groupBox19
            // 
            this.groupBox19.Controls.Add(this.button17);
            this.groupBox19.Controls.Add(this.label14);
            this.groupBox19.Controls.Add(this.numericUpDown1);
            this.groupBox19.Controls.Add(this.textBox5);
            this.groupBox19.Controls.Add(this.textBox4);
            this.groupBox19.Controls.Add(this.label13);
            this.groupBox19.Controls.Add(this.button16);
            this.groupBox19.Controls.Add(this.dataGridView1);
            this.groupBox19.Location = new System.Drawing.Point(11, 50);
            this.groupBox19.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox19.Name = "groupBox19";
            this.groupBox19.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox19.Size = new System.Drawing.Size(620, 235);
            this.groupBox19.TabIndex = 2;
            this.groupBox19.TabStop = false;
            // 
            // button17
            // 
            this.button17.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button17.Location = new System.Drawing.Point(373, 64);
            this.button17.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(213, 54);
            this.button17.TabIndex = 16;
            this.button17.Text = "Записать измерение в таблицу";
            this.button17.UseVisualStyleBackColor = true;
            this.button17.Click += new System.EventHandler(this.button17_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label14.Location = new System.Drawing.Point(340, 24);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(156, 19);
            this.label14.TabIndex = 15;
            this.label14.Text = "Количество измерений";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(515, 21);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(83, 25);
            this.numericUpDown1.TabIndex = 14;
            this.numericUpDown1.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // textBox5
            // 
            this.textBox5.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox5.Location = new System.Drawing.Point(502, 149);
            this.textBox5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(97, 25);
            this.textBox5.TabIndex = 13;
            // 
            // textBox4
            // 
            this.textBox4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox4.Location = new System.Drawing.Point(364, 149);
            this.textBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(97, 25);
            this.textBox4.TabIndex = 12;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(331, 152);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(155, 19);
            this.label13.TabIndex = 11;
            this.label13.Text = " Y=                         *X+";
            // 
            // button16
            // 
            this.button16.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button16.Location = new System.Drawing.Point(373, 183);
            this.button16.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(213, 28);
            this.button16.TabIndex = 10;
            this.button16.Text = "Вычислить уравнение";
            this.button16.UseVisualStyleBackColor = true;
            this.button16.Click += new System.EventHandler(this.button16_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeight = 40;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dataGridView1.Location = new System.Drawing.Point(9, 16);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 29;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView1.Size = new System.Drawing.Size(299, 206);
            this.dataGridView1.TabIndex = 9;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "X, мВ";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 130;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Y, В/м";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.Width = 130;
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Enabled = false;
            this.radioButton5.Location = new System.Drawing.Point(58, 299);
            this.radioButton5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(162, 23);
            this.radioButton5.TabIndex = 1;
            this.radioButton5.Text = "Автоматизированная";
            this.radioButton5.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(58, 26);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(72, 23);
            this.radioButton2.TabIndex = 0;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Ручная";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.groupBox16);
            this.tabPage8.Location = new System.Drawing.Point(304, 4);
            this.tabPage8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Size = new System.Drawing.Size(968, 523);
            this.tabPage8.TabIndex = 4;
            this.tabPage8.Text = "Обновление прошивки";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // groupBox16
            // 
            this.groupBox16.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox16.Controls.Add(this.label29);
            this.groupBox16.Controls.Add(this.label28);
            this.groupBox16.Controls.Add(this.label27);
            this.groupBox16.Controls.Add(this.progressBar1);
            this.groupBox16.Controls.Add(this.btn_flash);
            this.groupBox16.Controls.Add(this.label12);
            this.groupBox16.Controls.Add(this.label11);
            this.groupBox16.Controls.Add(this.textBox1);
            this.groupBox16.Controls.Add(this.btn_flash_edit);
            this.groupBox16.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox16.Location = new System.Drawing.Point(269, 126);
            this.groupBox16.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox16.Size = new System.Drawing.Size(491, 240);
            this.groupBox16.TabIndex = 0;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "Обновление прошивки";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label29.Location = new System.Drawing.Point(101, 166);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(195, 19);
            this.label29.TabIndex = 8;
            this.label29.Text = "Выберите файл прошивки";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label28.Location = new System.Drawing.Point(41, 165);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(61, 19);
            this.label28.TabIndex = 7;
            this.label28.Text = "Статус: ";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(41, 64);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(139, 19);
            this.label27.TabIndex = 6;
            this.label27.Text = "Версия прошивки: - ";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(18, 133);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(457, 22);
            this.progressBar1.TabIndex = 5;
            // 
            // btn_flash
            // 
            this.btn_flash.Location = new System.Drawing.Point(195, 193);
            this.btn_flash.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_flash.Name = "btn_flash";
            this.btn_flash.Size = new System.Drawing.Size(116, 33);
            this.btn_flash.TabIndex = 4;
            this.btn_flash.Text = "Выполнить";
            this.btn_flash.UseVisualStyleBackColor = true;
            this.btn_flash.Click += new System.EventHandler(this.btn_flash_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(41, 105);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(118, 19);
            this.label12.TabIndex = 3;
            this.label12.Text = "Размер памяти: -";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(41, 86);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(131, 19);
            this.label11.TabIndex = 2;
            this.label11.Text = "UIN контроллера: -";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox1.Location = new System.Drawing.Point(18, 38);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(346, 22);
            this.textBox1.TabIndex = 1;
            // 
            // btn_flash_edit
            // 
            this.btn_flash_edit.Location = new System.Drawing.Point(393, 36);
            this.btn_flash_edit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_flash_edit.Name = "btn_flash_edit";
            this.btn_flash_edit.Size = new System.Drawing.Size(82, 26);
            this.btn_flash_edit.TabIndex = 0;
            this.btn_flash_edit.Text = "Обзор";
            this.btn_flash_edit.UseVisualStyleBackColor = true;
            this.btn_flash_edit.Click += new System.EventHandler(this.btn_flash_edit_Click);
            // 
            // tabPage9
            // 
            this.tabPage9.Controls.Add(this.panel5);
            this.tabPage9.Location = new System.Drawing.Point(304, 4);
            this.tabPage9.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage9.Size = new System.Drawing.Size(968, 523);
            this.tabPage9.TabIndex = 5;
            this.tabPage9.Text = "О программе";
            this.tabPage9.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.linkLabel5);
            this.panel5.Controls.Add(this.linkLabel4);
            this.panel5.Controls.Add(this.linkLabel3);
            this.panel5.Controls.Add(this.linkLabel2);
            this.panel5.Controls.Add(this.label34);
            this.panel5.Controls.Add(this.linkLabel1);
            this.panel5.Controls.Add(this.label33);
            this.panel5.Controls.Add(this.label32);
            this.panel5.Controls.Add(this.label31);
            this.panel5.Controls.Add(this.label30);
            this.panel5.Location = new System.Drawing.Point(97, 48);
            this.panel5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(634, 333);
            this.panel5.TabIndex = 0;
            // 
            // linkLabel5
            // 
            this.linkLabel5.AutoSize = true;
            this.linkLabel5.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.linkLabel5.Location = new System.Drawing.Point(142, 304);
            this.linkLabel5.Name = "linkLabel5";
            this.linkLabel5.Size = new System.Drawing.Size(350, 19);
            this.linkLabel5.TabIndex = 9;
            this.linkLabel5.TabStop = true;
            this.linkLabel5.Text = "PDF Руководство по сборки градуировочной камеры";
            this.linkLabel5.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel5_LinkClicked);
            // 
            // linkLabel4
            // 
            this.linkLabel4.AutoSize = true;
            this.linkLabel4.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.linkLabel4.Location = new System.Drawing.Point(143, 280);
            this.linkLabel4.Name = "linkLabel4";
            this.linkLabel4.Size = new System.Drawing.Size(258, 19);
            this.linkLabel4.TabIndex = 8;
            this.linkLabel4.TabStop = true;
            this.linkLabel4.Text = "PDF Руководство флюксметр ПЧЕЛА-Д";
            this.linkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel4_LinkClicked);
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.linkLabel3.Location = new System.Drawing.Point(144, 257);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(276, 19);
            this.linkLabel3.TabIndex = 7;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "PDF Руководство пользователя FluxViewer";
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(284, 176);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(134, 19);
            this.linkLabel2.TabIndex = 6;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "http://fluxmeter.ru";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label34.Location = new System.Drawing.Point(241, 176);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(39, 19);
            this.label34.TabIndex = 5;
            this.label34.Text = "сайт:";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(284, 201);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(118, 19);
            this.linkLabel1.TabIndex = 4;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "az@azmotors.ru";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label33.Location = new System.Drawing.Point(233, 201);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(44, 19);
            this.label33.TabIndex = 3;
            this.label33.Text = "email:";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label32.Location = new System.Drawing.Point(142, 144);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(334, 19);
            this.label32.TabIndex = 2;
            this.label32.Text = "Авторские права 2022 ООО \"НПП\" Электростатик\"";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label31.Location = new System.Drawing.Point(214, 18);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(205, 37);
            this.label31.TabIndex = 1;
            this.label31.Text = "FluxViewer 1.0";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label30.Location = new System.Drawing.Point(108, 66);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(393, 21);
            this.label30.TabIndex = 0;
            this.label30.Text = "для электростатического флюксметра ПЧЕЛА-Д";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Текстовый файл (*.txt)|*.txt|Таблица|*.csv";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 615);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.statusStrip1);
            this.Name = "MainForm";
            this.Text = "FluxViewer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosing);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.mainTabControl.ResumeLayout(false);
            this.registrarTabPage.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            this.dataArchiveTabPage.ResumeLayout(false);
            this.dataArchiveTabPage.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.groupBox17.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.exportTabPage.ResumeLayout(false);
            this.exportMainGroupBox.ResumeLayout(false);
            this.exportGroupBox.ResumeLayout(false);
            this.exportGroupBox.PerformLayout();
            this.exportFieldsGroupBox.ResumeLayout(false);
            this.exportFieldsGroupBox.PerformLayout();
            this.exportInfoGroupBox.ResumeLayout(false);
            this.exportInfoGroupBox.PerformLayout();
            this.settingsTabPage.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.groupBox15.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.gb_settings.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.groupBox14.ResumeLayout(false);
            this.groupBox21.ResumeLayout(false);
            this.groupBox21.PerformLayout();
            this.groupBox20.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.num_linewidth)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabPage7.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.tabPage10.ResumeLayout(false);
            this.groupBox18.ResumeLayout(false);
            this.groupBox18.PerformLayout();
            this.groupBox19.ResumeLayout(false);
            this.groupBox19.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage8.ResumeLayout(false);
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            this.tabPage9.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private ImageList imageList1;
        private ToolStripStatusLabel toolStripStatusLabel3;
        private ToolStripStatusLabel toolStripStatusLabel4;
        private TabControl mainTabControl;
        private TabPage registrarTabPage;
        private TableLayoutPanel tableLayoutPanel1;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private ZedGraph.ZedGraphControl zedGraphControl2;
        private ZedGraph.ZedGraphControl zedGraphControl3;
        private ZedGraph.ZedGraphControl zedGraphControl4;
        private Panel panel1;
        private TabPage dataArchiveTabPage;
        private TabPage settingsTabPage;
        public TabControl tabControl2;
        private TabPage tabPage4;
        private TabPage tabPage5;
        private TabPage tabPage6;
        private TabPage tabPage7;
        private TabPage tabPage8;
        private GroupBox gb_settings;
        private Button btn_writesettings;
        public RichTextBox richTextBox1;
        private ZedGraph.ZedGraphControl zedGraphControl5;
        private Panel panel2;
        private Panel panel3;
        private GroupBox groupBox3;
        private Label label7;
        private Button button1;
        private Label label6;
        private ComboBox cb_st_protocol;
        private Label label5;
        private ComboBox cb_st_pga;
        private Label label4;
        private ComboBox cb_st_freq;
        private Label label3;
        private ComboBox cb_st_format;
        private GroupBox groupBox2;
        private CheckBox checkBox1;
        private TextBox tb_st_B;
        private TextBox tb_st_K;
        private Label label2;
        private Label label1;
        private ComboBox cb_st_mode;
        private Button btn_stop;
        private Button btn_start;
        private Label label8;
        private ComboBox cb_st_speed;
        private GroupBox groupBox11;
        private GroupBox groupBox10;
        private ComboBox comboBox1;
        private GroupBox groupBox9;
        private GroupBox groupBox8;
        private Button btn_achive_down;
        private Button btn_achive_up;
        private Button btn_achive_minus;
        private Button btn_achive_plus;
        private Button btn_achive_autozoom;
        private ComboBox comboBox2;
        private TabPage tabPage9;
        private OpenFileDialog openFileDialog1;
        private GroupBox groupBox13;
        private CheckBox checkBox2;
        private Button btn_minus_reg;
        private Button bpn_plus_reg;
        private ComboBox cb_graphtype;
        private GroupBox groupBox15;
        private GroupBox groupBox7;
        private RadioButton rb_isFluxclock;
        private RadioButton rb_isPCclock;
        private GroupBox groupBox5;
        private Button btn_testconnect;
        private RadioButton rb_485_con;
        private RadioButton rb_wifi_con;
        private ComboBox com_name;
        private Button btn_updateport;
        private ComboBox com_speed;
        private GroupBox groupBox14;
        private GroupBox groupBox1;
        private RadioButton rb_templot_2;
        private RadioButton rb_templot_1;
        private Button btn_graph_settings;
        private GroupBox groupBox4;
        private CheckBox check_grid;
        private GroupBox groupBox16;
        private Button btn_flash;
        private Label label12;
        private Label label11;
        private TextBox textBox1;
        private Button btn_flash_edit;
        private ProgressBar progressBar1;
        private GroupBox groupBox17;
        private Button button5;
        private Button button4;
        private Panel panel4;
        private Button button14;
        private Button button6;
        private Button button15;
        private TabPage tabPage10;
        private GroupBox groupBox18;
        private RadioButton radioButton5;
        private RadioButton radioButton2;
        private GroupBox groupBox19;
        private Button button17;
        private Label label14;
        private NumericUpDown numericUpDown1;
        private TextBox textBox5;
        private TextBox textBox4;
        private Label label13;
        private Button button16;
        private DataGridView dataGridView1;
        private DateTimePicker dateTimePicker1;
        private DateTimePicker dateTimePicker2;
        private GroupBox groupBox20;
        private NumericUpDown num_linewidth;
        private GroupBox groupBox21;
        private Label label24;
        private Label label25;
        private TextBox textBox15;
        private TextBox textBox16;
        private TextBox textBox17;
        private Label label26;
        private Label label21;
        private Label label22;
        private TextBox textBox12;
        private TextBox textBox13;
        private TextBox textBox14;
        private Label label23;
        private Label label18;
        private Label label19;
        private TextBox textBox9;
        private TextBox textBox10;
        private TextBox textBox11;
        private Label label20;
        private Label label17;
        private Label label16;
        private TextBox textBox8;
        private TextBox textBox7;
        private TextBox textBox6;
        private Label label15;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private Label label27;
        private Label label28;
        private Label label29;
        private CheckBox checkBox3;
        private System.Windows.Forms.Timer timer1;
        private SaveFileDialog saveFileDialog1;
        private Panel panel5;
        private LinkLabel linkLabel1;
        private Label label33;
        private Label label32;
        private Label label31;
        private Label label30;
        private LinkLabel linkLabel2;
        private Label label34;
        private LinkLabel linkLabel3;
        private LinkLabel linkLabel5;
        private LinkLabel linkLabel4;
        private TabPage exportTabPage;
        private GroupBox exportGroupBox;
        private GroupBox exportFieldsGroupBox;
        private CheckBox fluxForExportCheckBox;
        private CheckBox dateTimeForExportCheckBox;
        private ComboBox exportTypeComboBox;
        private Label exportTypeLabel;
        private Label endExportDateLabel;
        private Label beginExportDateLabel;
        private DateTimePicker endExportDate;
        private DateTimePicker beginExportDate;
        private CheckBox presForExportCheckBox;
        private CheckBox tempForExportCheckBox;
        private CheckBox hummForExportCheckBox;
        private Button exportButton;
        private GroupBox exportInfoGroupBox;
        private Label exportDataCountLabel;
        private TextBox exportDataCountTextBox;
        private Label dateFormatLabel;
        private ComboBox dateFormatComboBox;
        private Label secondsLabel;
        private TextBox exportIntervalsTextBox;
        private CheckBox filterApplyingCheckBox;
        private ProgressBar exportProgressBar;
        private CheckBox fillHolesCheckBox;
        private TextBox lastExportDateTextBox;
        private TextBox firstExportDateTextBox;
        private Label lastExportDateLabel;
        private Label firstExportDateLabel;
        private GroupBox exportMainGroupBox;
    }
}