using System.Diagnostics;
using System.IO.Ports;
using System.Text;
using FluxViewer.DataAccess.GraphThemes;
using FluxViewer.DataAccess.Log;
using FluxViewer.DataAccess.Storage;
using ZedGraph;

namespace FluxViewer.App
{
    public partial class MainForm : Form
    {
        private static readonly SerialPort SerialPort = new();

        private const int Capacity = 50;
        private const int PosAverageMax = 10;
        private const int MaxOutsizeGraph = 600000;
        private const int MaxReadsizeBase = 600000; // TODO: ����� ��� ����???
        private const double Step = 1;

        private double _currentX;
        
        // ���������� ���������� ������ �������
        private float _flux;
        private float _temp;
        private float _pres;
        private float _humm;
        
        private readonly RollingPointPairList[] _daGraphData = new RollingPointPairList[4]; // ���� �������
        private readonly GraphPane[] _daGraphPanels = new GraphPane[4];
        private readonly LineItem[] _daGraphCurves = new LineItem[4];

        private bool _isGraduateMode; // ������� �����������, ����� ������ � �������
        private float _averageDataflux; // ������� �������� ��������� � ������ ����������� TODO: ����� ��� ����???
        private int _posAverage; // ���������� �������

        private bool _isDataStartFlux;
        private bool _isTestButton; // ������ ���������� � ������ ������������ �����
        private bool _isAsciiConsole; // ������ ������ ������� ������ � �������?

        private byte _selectPane = 255; // ������� ��������� ������

        private int _baseSize; // ������ ����� � ���� ����� ���������� ������ TODO: �������� ������ ����

        // ���� ������
        private IStorage _storage;

        private Props _props;
        private readonly string[] _graphTitle = new string[4];
        private readonly float[] _graphK = new float[4];
        private readonly float[] _graphB = new float[4];

        private byte[] _wirmvareData;
        private long _positionFirmware = 0;
        private long _sizeWirmware = 0;

        public MainForm()
        {
            FileSystemLogger.WriteLog(LogLevel.Info, LogInitiator.Application, "���������� ��������!");
            _daGraphData[0] = new RollingPointPairList(Capacity);
            _daGraphData[1] = new RollingPointPairList(Capacity);
            _daGraphData[2] = new RollingPointPairList(Capacity);
            _daGraphData[3] = new RollingPointPairList(Capacity);

            InitializeComponent();
            OpenStorage();
            PrepareSettings();
            SetSettings();
            InitDataArchiveGraphs();
            
            InitDataArchiveTab();
            InitExportTab();
            InitSettingsTab();
            
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();

            save_settings_graph();

            groupBox14.Left = (tabPage6.Width - groupBox14.Width) / 2;
            groupBox14.Top = (tabPage6.Height - groupBox14.Height) / 2;

            groupBox15.Left = (tabPage6.Width - groupBox15.Width) / 2;
            groupBox15.Top = (tabPage6.Height - groupBox15.Height) / 2;

            groupBox16.Left = (tabPage6.Width - groupBox16.Width) / 2;
            groupBox16.Top = (tabPage6.Height - groupBox16.Height) / 2;

            groupBox18.Left = (tabPage10.Width - groupBox18.Width) / 2;
            groupBox18.Top = (tabPage10.Height - groupBox18.Height) / 2;

            gb_settings.Left = (tabPage4.Width - gb_settings.Width) / 2;
            gb_settings.Top = (tabPage4.Height - gb_settings.Height) / 2;

            panel5.Left = (tabPage4.Width - panel5.Width) / 2;
            panel5.Top = (tabPage4.Height - panel5.Height) / 2;

            tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Percent;
            tableLayoutPanel1.RowStyles[1].SizeType = SizeType.Percent;
            tableLayoutPanel1.ColumnStyles[0].SizeType = SizeType.Percent;
            tableLayoutPanel1.ColumnStyles[1].SizeType = SizeType.Percent;
        }
        
        /// <summary>
        /// ������ � ��������� ���������, ���� ����� ������������ ������ ������� 
        /// </summary>
        private void OpenStorage()
        {
            _storage = new FileSystemStorage();
            _storage.Open();
            FileSystemLogger.WriteLog(LogLevel.Info, LogInitiator.Application,
                "������� ������� ��������� ��� ������ ��������� �������");
        }
        
        /// <summary>
        /// �������������� ��������� ���������� � ���� �� �� ����������, �� ������ ���� � ������������ �����������
        /// </summary>
        private void PrepareSettings()
        {
            var pathToXmlSettingsFile = Environment.CurrentDirectory + "\\settings.xml";
            _props = new Props(pathToXmlSettingsFile);
            // ���� ����� � ����������� �� ����������, �� ��������� ��������� � ������ ���� ����
            if (!File.Exists(pathToXmlSettingsFile))
            {
                _props.WriteXml();
                FileSystemLogger.WriteLog(LogLevel.Info, LogInitiator.Application,
                    $"������ ���� � ����������� �� ����: `{pathToXmlSettingsFile}`");
            }
        }

        private void InitDataArchiveGraphs()
        {
            // ������� ������ ��� ���������
            _daGraphPanels[0] = zedGraphControl1.GraphPane;
            _daGraphPanels[1] = zedGraphControl2.GraphPane;
            _daGraphPanels[2] = zedGraphControl3.GraphPane;
            _daGraphPanels[3] = zedGraphControl4.GraphPane;
            
            // ������� ������ ���� ��� ��� �����-���� �����
            for (var i = 0; i < 4; i++)
            {
                _daGraphCurves[i] = _daGraphPanels[i].AddCurve("", _daGraphData[i], Color.Blue, SymbolType.None);
                _daGraphCurves[i].Line.Width = (float)num_linewidth.Value; //2.0F;
                _daGraphPanels[i].XAxis.Title.Text = "����� ��:cc";
            }
            _daGraphPanels[0].Title.Text = _graphTitle[0];
            _daGraphPanels[1].Title.Text = _graphTitle[1];
            _daGraphPanels[2].Title.Text = _graphTitle[2];
            _daGraphPanels[3].Title.Text = _graphTitle[3];

            _daGraphPanels[0].YAxis.Scale.Min = -5000;
            _daGraphPanels[0].YAxis.Scale.Max = 5000;
            _daGraphPanels[1].YAxis.Scale.Min = -40;
            _daGraphPanels[1].YAxis.Scale.Max = 60;
            _daGraphPanels[2].YAxis.Scale.Min = -10;
            _daGraphPanels[2].YAxis.Scale.Max = 1200;
            _daGraphPanels[3].YAxis.Scale.Min = 0;
            _daGraphPanels[3].YAxis.Scale.Max = 100;

            DrawUpdate();
        }
        
        /// <summary>
        /// ���������� �������� �����������
        /// </summary>
        private void DrawUpdate()
        {
            // �������� ����� AxisChange (), ����� �������� ������ �� ����.
            zedGraphControl1.AxisChange();
            zedGraphControl2.AxisChange();
            zedGraphControl3.AxisChange();
            zedGraphControl4.AxisChange();

            // ��������� ������
            zedGraphControl1.Invalidate();
            zedGraphControl2.Invalidate();
            zedGraphControl3.Invalidate();
            zedGraphControl4.Invalidate();
        }
        
        private void com_connect_Click(object sender, EventArgs e)
        {

        }
        
        /// <summary>
        /// ����������� ����� ��������� ������
        /// </summary>
        /// <param name="data"></param>
        /// <param name="lenght"></param>
        /// <returns></returns>
        private byte checksum(byte[] data, byte lenght)
        {
            int sum = 0;
            for (byte i = 2; i < lenght + 2; i++) // ���������� ���������
            {
                sum ^= (byte)data[i];
            }
            return (byte)sum;
        }

        /// <summary>
        /// ������� ������ ������ �� COM �����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            if (e.EventType == SerialData.Eof)
                return;
            if(_isAsciiConsole)
            {
                string str = sp.ReadExisting();
                this.BeginInvoke((MethodInvoker)delegate
                {
                    richTextBox1.AppendText(str);
                    richTextBox1.ScrollToCaret();
                });
                return;
            }
            byte[] rx_buf = new byte[30];
            try
            {
                rx_buf[0] = (byte)sp.ReadByte();
                rx_buf[1] = (byte)sp.ReadByte();
                if (rx_buf[0] != 0xFF && rx_buf[1] != 0xFF) //��������� 2 ��������� ���� //� CRC �� ��������
                { return; }
                rx_buf[2] = (byte)sp.ReadByte(); //ID
                rx_buf[3] = (byte)sp.ReadByte(); //Size

                for (int i = 4; i < rx_buf[3] + 5; i++)
                {
                    rx_buf[i] = (byte)sp.ReadByte();
                }
            }
            catch (Exception ex)
            {
                //this.BeginInvoke((MethodInvoker)delegate { MessageBox.Show(this, ex.Message); });
                return;
            }
            byte crc = rx_buf[rx_buf[3] + 4];
            if (crc != checksum(rx_buf, (byte)(rx_buf[3] + 2)))
            {
                return;
            }
            if (rx_buf[2] == 0x1A)// ����� ������
            {
                if (!_isDataStartFlux)
                {
                    _isDataStartFlux = true;//�������������� ��������
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        toolStripStatusLabel2.Text = "����������";
                        toolStripStatusLabel2.BackColor = Color.Green;
                        btn_start.Enabled = false;
                        btn_stop.Enabled = true;

                    });
                }
                DateTime date1;
                if (rb_isFluxclock.Checked == true)//�����
                {
                    date1 = new DateTime(rx_buf[4] + 2000, rx_buf[5], rx_buf[6], rx_buf[7], rx_buf[8], rx_buf[9], rx_buf[10] << 8 | rx_buf[11]); // ��� - ����� - ���� - ��� - ������ - ������� - ��������
                }
                else
                {
                    date1 = DateTime.Now;
                }
                _flux = BitConverter.ToSingle(rx_buf, 12) * float.Parse(textBox7.Text) + float.Parse(textBox8.Text);
                _temp = BitConverter.ToSingle(rx_buf, 16) * float.Parse(textBox10.Text) + float.Parse(textBox9.Text);
                _pres = BitConverter.ToSingle(rx_buf, 20) * float.Parse(textBox13.Text) + float.Parse(textBox12.Text);
                _humm = BitConverter.ToSingle(rx_buf, 24) * float.Parse(textBox16.Text) + float.Parse(textBox15.Text);

                //����� �����������
                if (_isGraduateMode)
                {
                    _averageDataflux += _flux / 2;
                    if (_posAverage > PosAverageMax)
                    {
                        this.BeginInvoke((MethodInvoker)delegate
                        {
                            dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value = _flux.ToString();
                            btn_stop_Click(sender, e);// ��������� ��������������
                        });
                        _posAverage = 0;
                        _isGraduateMode = false;
                        return;
                    }
                    _posAverage++;
                    return;
                }
                //XDate valueX = new XDate(date1);
                _daGraphData[0].Add(_currentX, _flux);
                _daGraphData[1].Add(_currentX, _temp);
                _daGraphData[2].Add(_currentX, _pres);
                _daGraphData[3].Add(_currentX, _humm);
                _currentX += Step;

                // ���������� �������� �� ��� X, ������� ����� ���������� �� �������
                double xmin = _currentX - Capacity * Step;
                double xmax = _currentX;

                for (int j = 0; j < 4; j++)
                {
                    _daGraphPanels[j].XAxis.Scale.Min = xmin;
                    _daGraphPanels[j].XAxis.Scale.Max = xmax;
                }

                //��������� ������ � ����
                // TODO: ������� ID, �.�. �� ����� ��� ���������������!
                _storage.WriteData(new Data(DateTime.Now, _flux, _temp, _pres, _humm));


                this.BeginInvoke((MethodInvoker)delegate { DrawUpdate(); });
            }
            else if (rx_buf[2] == 0x1B)// ����� ����������� ��������                    is_TestButton = false;
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    cb_st_mode.SelectedIndex = rx_buf[4];
                    float temp;
                    cb_st_speed.SelectedIndex = rx_buf[5];
                    temp = BitConverter.ToSingle(rx_buf, 6);
                    tb_st_K.Text = temp.ToString();
                    temp = BitConverter.ToSingle(rx_buf, 10);
                    tb_st_B.Text = temp.ToString();
                    cb_st_pga.SelectedIndex = rx_buf[14];
                    cb_st_freq.SelectedIndex = rx_buf[16];
                    cb_st_protocol.SelectedIndex = rx_buf[17];
                    cb_st_format.SelectedIndex = rx_buf[18];
                    if (rx_buf[19] == 1)
                        checkBox1.Checked = true;
                    else if (rx_buf[19] == 0)
                        checkBox1.Checked = false;
                });
            }
            /*          else if (rx_buf[2] == 0x1C)// ������ ��������
                      {
                          string mas = Encoding.UTF8.GetString(rx_buf, 2, 14);
                      }*/
            else if (rx_buf[2] == 0x2e)// ������ ��������
            {
                String info = "������ ��������\n" + Encoding.UTF8.GetString(rx_buf, 18, 10);


                StringBuilder hex = new StringBuilder(10 * 2);
                for (int i = 4; i <= 15; i++)
                {
                    hex.AppendFormat("{0:x2}", rx_buf[i]);
                }
                this.BeginInvoke((MethodInvoker)delegate
                {
                    label27.Text = "������ ��������: " + Encoding.UTF8.GetString(rx_buf, 18, 10);
                    label11.Text = "UIN �����������: 0x" + hex.ToString();
                    label12.Text = "������ ������: " + (rx_buf[16] | (rx_buf[17] << 8)).ToString();
                    toolStripStatusLabel2.Text = "����������";
                    toolStripStatusLabel2.BackColor = Color.Green;
                    toolStripStatusLabel4.Text = Encoding.UTF8.GetString(rx_buf, 18, 10);
                    if (_isTestButton)
                    {
                        timer1.Enabled = false;                        
                        MessageBox.Show(info, "������� ����������", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        _isTestButton = false;

                    }
                });
            }
            else if (rx_buf[2] == 0x5b)//������������
            {
                if (_positionFirmware == 0)
                {                   
                    _sizeWirmware = _wirmvareData.Length;
                    _positionFirmware = 200;
                    this.BeginInvoke((MethodInvoker)delegate { 
                        progressBar1.Maximum = (int)_sizeWirmware; 
                        label29.Text = "�������� ������ � ����������";
                        progressBar1.Value = (int)_positionFirmware;
                    });
                    com_send(0x5b, _wirmvareData, 200);
                }
                else if (_positionFirmware <= _sizeWirmware)
                {
                    if (_sizeWirmware - _positionFirmware >= 200)
                    {
                        byte[] arr = new byte[200];
                        Array.Copy(_wirmvareData, (int)_positionFirmware, arr, 0, 200);
                        com_send(0x5b, arr, 200);
                        _positionFirmware += 200;
                        this.BeginInvoke((MethodInvoker)delegate { progressBar1.Value = (int)_positionFirmware; });
                    }
                    else
                    {
                        byte[] arr = new byte[_sizeWirmware - _positionFirmware];
                        Array.Copy(_wirmvareData, (int)_positionFirmware, arr, 0, _sizeWirmware - _positionFirmware);
                        com_send(0x5b, arr, (int)(_sizeWirmware - _positionFirmware));
                        _positionFirmware += _sizeWirmware - _positionFirmware;
                        this.BeginInvoke((MethodInvoker)delegate { 
                        label29.Text = "�������������������� ����������";
                            Thread.Sleep(3000);
                        btn_stop_Click(sender, e);
                         });
                        SerialPort.Close();
                    //com_send_cmd(0x5a); //������������� ����������
                }
                }
/*                else
                {
                    position_firmware = 0;
                    this.BeginInvoke((MethodInvoker)delegate { progressBar1.Value = (int)size_wirmware; });
                    MessageBox.Show("�������� ��������� �������", "���������", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }*/
            }
            else if (rx_buf[2] == 0x5c)//������������ ������� ���������
            {
                this.BeginInvoke((MethodInvoker)delegate { progressBar1.Value = 0; });
                MessageBox.Show("�������� ��������� �������", "���������", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
            }
        }

        /// <summary>
        /// ��������� ��������� COM �����
        /// </summary>
        /// <returns></returns>
        public string SetPortName()
        {
            string portName = "";
            foreach (string portname in SerialPort.GetPortNames())
            {
                comNameComboBox.Items.Add(portname); //�������� ���� � ������    
            }
            if (comNameComboBox.Items.Count > 0)
            {
                comNameComboBox.SelectedIndex = 0;
                portName = comNameComboBox.Text;
            }
            else
            {
                portName = "��� ������";
                comNameComboBox.Text = "��� ������";
            }
            //     string[] myPort; //������� ������ �����
            //    myPort = System.IO.Ports.SerialPort.GetPortNames(); // � ������ �������� ��������� �����
            //     comboBox1.Items.AddRange(myPort); //������ ���� ������ ������� � ������(comboBox) 
            return portName; //���������� ���� �� ���������
        }
        
        /// <summary>
        /// ��������� ��������� �� ����� � ��������� ��
        /// </summary>
        public void SetSettings()
        {
            _props.ReadXml();
         
            rb_isPCclock.Checked = _props.Fields.IsPcTime;
            comNameComboBox.Text = _props.Fields.ComNum.ToString();
            com_speed.Text = _props.Fields.ComSpeed.ToString();


            textBox6.Text = _props.Fields.GOneTitle.ToString();
            textBox11.Text = _props.Fields.GTwoTitle.ToString();
            textBox14.Text = _props.Fields.GThreeTitle.ToString();
            textBox17.Text = _props.Fields.GFourTitle.ToString();
            textBox7.Text = _props.Fields.G1K.ToString();
            textBox8.Text = _props.Fields.G1B.ToString();
            textBox10.Text = _props.Fields.G2K.ToString();
            textBox9.Text = _props.Fields.G2B.ToString();
            textBox13.Text = _props.Fields.G3K.ToString();
            textBox12.Text = _props.Fields.G3B.ToString();
            textBox16.Text = _props.Fields.G4K.ToString();
            textBox15.Text = _props.Fields.G4B.ToString();

            _graphTitle[0] = _props.Fields.GOneTitle.ToString();
            _graphTitle[1] = _props.Fields.GTwoTitle.ToString();
            _graphTitle[2] = _props.Fields.GThreeTitle.ToString();
            _graphTitle[3] = _props.Fields.GFourTitle.ToString();

            _graphK[0] = float.Parse(_props.Fields.G1K);
            _graphB[0] = float.Parse(_props.Fields.G1B);
            _graphK[1] = float.Parse(_props.Fields.G2K);
            _graphB[1] = float.Parse(_props.Fields.G2B);
            _graphK[2] = float.Parse(_props.Fields.G3K);
            _graphB[2] = float.Parse(_props.Fields.G3B);
            _graphK[3] = float.Parse(_props.Fields.G4K);
            _graphB[3] = float.Parse(_props.Fields.G4B);
            
            cb_graphtype.Items.Clear();
            cb_graphtype.Items.Add("��� �������");
            cb_graphtype.Items.Add(_graphTitle[0]);
            cb_graphtype.Items.Add(_graphTitle[1]);
            cb_graphtype.Items.Add(_graphTitle[2]);
            cb_graphtype.Items.Add(_graphTitle[3]);
            cb_graphtype.SelectedIndex = 0;

            //������� ���������
            num_linewidth.Value = _props.Fields.LineWidth;
            rb_templot_2.Checked = _props.Fields.IsBlackTheme;
            check_grid.Checked = _props.Fields.IsGrid;
            FileSystemLogger.WriteLog(LogLevel.Info, LogInitiator.Application,
                "��� ���������� �� ����� ������� ����������");
        }

        /// <summary>
        /// ��������� ��������� ����������
        /// </summary>
        public void SaveSettings()
        {
            //������� ���������
            _props.Fields.IsPcTime = rb_isPCclock.Checked;
            _props.Fields.ComNum = comNameComboBox.Text;
            _props.Fields.ComSpeed = com_speed.Text;

            //������� ���������
            _props.Fields.LineWidth = num_linewidth.Value;
            _props.Fields.IsBlackTheme = rb_templot_2.Checked;
            _props.Fields.IsGrid = check_grid.Checked;

            _props.Fields.GOneTitle = textBox6.Text;
            _props.Fields.GTwoTitle = textBox11.Text;
            _props.Fields.GThreeTitle = textBox14.Text;
            _props.Fields.GFourTitle = textBox17.Text;
            _props.Fields.G1K = textBox7.Text;
            _props.Fields.G1B = textBox8.Text;
            _props.Fields.G2K = textBox10.Text;
            _props.Fields.G2B = textBox9.Text;
            _props.Fields.G3K = textBox13.Text;
            _props.Fields.G3B = textBox12.Text;
            _props.Fields.G4K = textBox16.Text;
            _props.Fields.G4B = textBox15.Text;
            _props.WriteXml();
        }
        
        /// <summary>
        /// ������������� �����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Shown(object sender, EventArgs e)
        {
            //_serialPort.PortName = SetPortName();
            //com_speed.SelectedIndex = 4;
        }

        /// <summary>
        /// ����� ����� ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void leftMenuTabPage_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush _textBrush;

            // Get the item from the collection.
            TabPage _tabPage = leftMenuTabPage.TabPages[e.Index];

            // Get the real bounds for the tab rectangle.
            Rectangle _tabBounds = leftMenuTabPage.GetTabRect(e.Index);

            if (e.State == DrawItemState.Selected)
            {

                // Draw a different background color, and don't paint a focus rectangle.
                _textBrush = new SolidBrush(Color.Yellow);
                g.FillRectangle(Brushes.Gray, e.Bounds);
            }
            else
            {
                _textBrush = new SolidBrush(e.ForeColor);
                e.DrawBackground();
            }

            // Use our own font.
            Font _tabFont = new Font("Arial", (float)14.0, FontStyle.Bold, GraphicsUnit.Pixel);

            // Draw string. Center the text.
            StringFormat _stringFlags = new StringFormat();
            _stringFlags.Alignment = StringAlignment.Center;
            _stringFlags.LineAlignment = StringAlignment.Center;
            g.DrawString(_tabPage.Text, _tabFont, _textBrush, _tabBounds, new StringFormat(_stringFlags));
        }

        /// <summary>
        /// �������������� ������� �������� �����������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                for (int j = 0; j < 4; j++)
                {
                    _daGraphPanels[j].YAxis.Scale.MinAuto = true;
                    _daGraphPanels[j].YAxis.Scale.MaxAuto = true;
                    _daGraphPanels[j].IsBoundedRanges = true;
                }
            }
            else
            {
                for (int j = 0; j < 4; j++)
                {
                    _daGraphPanels[j].YAxis.Scale.MinAuto = false;
                    _daGraphPanels[j].YAxis.Scale.MaxAuto = false;
                    _daGraphPanels[j].IsBoundedRanges = false;
                }
            }
            DrawUpdate();
        }
        
        /// <summary>
        /// �������� � COM ���� �������
        /// </summary>
        /// <param name="cmd"></param>
        private void send_command(byte cmd)
        {
            byte[] data = new byte[5];
            data[0] = 0xFF;
            data[1] = 0xFF;
            data[2] = cmd;
            data[3] = 0x00;
            data[4] = cmd;
            if (SerialPort.IsOpen)
            {
                SerialPort.Write(data, 0, 5);
            }
        }
        
        /// <summary>
        /// �������� ����� � COM ����
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data_send"></param>
        /// <param name="lenght"></param>
        private void com_send(byte id, byte[] data_send, int lenght)
        {
            byte[] buf = new byte[lenght + 5];
            buf[0] = 0xFF;
            buf[1] = 0xFF;
            buf[2] = id;
            buf[3] = (byte)lenght;
            int i;
            for (i = 0; i < lenght; i++)
            {
                buf[i + 4] = data_send[i];
            }
            buf[i + 4] = checksum(buf, (byte)(lenght + 2));
            SerialPort.Write(buf, 0, lenght + 5);
        }
        
        //�������� �������
        private void com_send_cmd(byte id)
        {
            byte[] temp = new byte[1];
            com_send(id, temp, 0);
        }
        
        /// <summary>
        /// �������� � COM ����� ����� ��������� �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_writesettings_Click(object sender, EventArgs e)
        {
            byte[] buf = new byte[21];
            buf[0] = 0xFF;
            buf[1] = 0xFF;
            buf[2] = 0x1d;
            buf[3] = 16;
            buf[4] = (byte)(cb_st_mode.SelectedIndex);
            buf[5] = (byte)(cb_st_speed.SelectedIndex);
            float temp = (float)Convert.ToDouble(tb_st_K.Text);
            byte[] temp_mas = BitConverter.GetBytes(temp);
            buf[6] = temp_mas[0];
            buf[7] = temp_mas[1];
            buf[8] = temp_mas[2];
            buf[9] = temp_mas[3];
            temp = (float)Convert.ToDouble(tb_st_B.Text);
            temp_mas = BitConverter.GetBytes(temp);
            buf[10] = temp_mas[0];
            buf[11] = temp_mas[1];
            buf[12] = temp_mas[2];
            buf[13] = temp_mas[3];
            buf[14] = (byte)(cb_st_pga.SelectedIndex);
            buf[15] = 0;
            buf[16] = (byte)(cb_st_freq.SelectedIndex);
            buf[17] = (byte)(cb_st_protocol.SelectedIndex);
            buf[18] = (byte)(cb_st_format.SelectedIndex);
            buf[19] = checkBox1.Checked ? (byte)1 : (byte)0;
            buf[20] = checksum(buf, (byte)(buf.Length - 3));
            SerialPort.Write(buf, 0, buf.Length);

            DialogResult result = MessageBox.Show("��������� �������� � ����������\n��� ���������� ���������� ������������� ����������\n�������������?", "���������", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.Yes)
            {
                Thread.Sleep(500);
                btn_stop_Click(sender,e);
                //������� ������������
                Thread.Sleep(500);
                com_send_cmd(0x5a);
                SerialPort.Close();

            }
        }
       
        /// <summary>
        /// ������ �������� �����������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
    
        
        private void btn_start_Click(object sender, EventArgs e)
        {
            if (!_isDataStartFlux)
            {
                connect_flux();
            }
            if (SerialPort.IsOpen)
            {
                Thread.Sleep(400);
                com_send_cmd(0x2a);// ����� ��������������
            }
        }
        /*
          ������ ��������� ������������� �������
         */

        private void button1_Click(object sender, EventArgs e)
        {
            com_send_cmd(0x1b);// ����� ��������������
        }

        private void leftMenuTabPage_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPageIndex == 1) //��������� ����������
            {
                if (SerialPort.IsOpen)
                {
                    com_send_cmd(0x1b);// ��������� ����������
                }
                else
                {
                    gb_settings.Enabled = false;
                }
            }
            else if (e.TabPageIndex == 0)//��������� ���������
            { }
            else if (e.TabPageIndex == 2)
            { }
            else if (e.TabPageIndex == 3)
            { }
            else if (e.TabPageIndex == 4)
            { }
        }

        /// <summary>
        /// ���������� ����� �� ������� ������ �� �����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void zedGraphControl5_MouseClick(object sender, MouseEventArgs e)
        {
            // ���� ����� ��������� ������, ����� � ������� ��� ���������� ����
            CurveItem curve;

            // ���� ����� �������� ����� ����� ������, ��������� � ����� �����
            int index;

            GraphPane pane_dot = daMainZedGraphControl.GraphPane;

            // ������������ ���������� �� ����� ����� �� ������ � ��������,
            // ��� ������� ��� ���������, ��� ���� ����� � ����������� ������.
            GraphPane.Default.NearestTol = 10;

            bool result = pane_dot.FindNearestPoint(e.Location, out curve, out index);

            if (result)
            {
                // ����������� ���������� �� ����� ����� �� ������ �� ��������� NearestTol

                // ������� ����� �� ������, ������ ������� ��������� ����
                PointPairList point = new PointPairList();

                point.Add(curve[index]);

                // ������, ��������� �� ����� �����. ����� ����� �������� ����� ������
                LineItem curvePount = pane_dot.AddCurve("",
                    new double[] { curve[index].X },
                    new double[] { curve[index].Y },
                    Color.Blue,
                    SymbolType.Circle);

                //
                curvePount.Line.IsVisible = false;

                // ���� ���������� ����� - �������
                curvePount.Symbol.Fill.Color = Color.Blue;

                // ��� ���������� - �������� �������
                curvePount.Symbol.Fill.Type = FillType.Solid;

                // ������ �����
                curvePount.Symbol.Size = 6;
            }
        }

        /// <summary>
        /// �������� ������ ����������� ��� �����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void zedGraphControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (_selectPane == 0)
            {
                _daGraphPanels[0].Border.Color = Color.Black;// ��������� ���� ����� ��� ����� ����������
                _selectPane = 255;
            }
            else
            {
                _daGraphPanels[0].Border.Color = Color.Red;// ��������� ���� ����� ��� ����� ����������
                _daGraphPanels[1].Border.Color = Color.Black;// ��������� ���� ����� ��� ����� ����������
                _daGraphPanels[2].Border.Color = Color.Black;// ��������� ���� ����� ��� ����� ����������
                _daGraphPanels[3].Border.Color = Color.Black;// ��������� ���� ����� ��� ����� ����������
                _selectPane = 0;
            }
            DrawUpdate();
        }
        private void zedGraphControl2_MouseClick(object sender, MouseEventArgs e)
        {
            if (_selectPane == 1)
            {
                _daGraphPanels[1].Border.Color = Color.Black;// ��������� ���� ����� ��� ����� ����������
                _selectPane = 255;
            }
            else
            {
                _daGraphPanels[0].Border.Color = Color.Black;// ��������� ���� ����� ��� ����� ����������
                _daGraphPanels[1].Border.Color = Color.Red;// ��������� ���� ����� ��� ����� ����������
                _daGraphPanels[2].Border.Color = Color.Black;// ��������� ���� ����� ��� ����� ����������
                _daGraphPanels[3].Border.Color = Color.Black;// ��������� ���� ����� ��� ����� ����������
                _selectPane = 1;
            }
            DrawUpdate();
        }
        private void zedGraphControl3_MouseClick(object sender, MouseEventArgs e)
        {
            if (_selectPane == 2)
            {
                _daGraphPanels[2].Border.Color = Color.Black;// ��������� ���� ����� ��� ����� ����������
                _selectPane = 255;
            }
            else
            {
                _daGraphPanels[0].Border.Color = Color.Black;// ��������� ���� ����� ��� ����� ����������
                _daGraphPanels[1].Border.Color = Color.Black;// ��������� ���� ����� ��� ����� ����������
                _daGraphPanels[2].Border.Color = Color.Red;// ��������� ���� ����� ��� ����� ����������
                _daGraphPanels[3].Border.Color = Color.Black;// ��������� ���� ����� ��� ����� ����������
                _selectPane = 2;
            }
            DrawUpdate();
        }
        private void zedGraphControl4_MouseClick(object sender, MouseEventArgs e)
        {
            if (_selectPane == 3)
            {
                _daGraphPanels[3].Border.Color = Color.Black;// ��������� ���� ����� ��� ����� ����������
                _selectPane = 255;
            }
            else
            {
                _daGraphPanels[0].Border.Color = Color.Black;// ��������� ���� ����� ��� ����� ����������
                _daGraphPanels[1].Border.Color = Color.Black;// ��������� ���� ����� ��� ����� ����������
                _daGraphPanels[2].Border.Color = Color.Black;// ��������� ���� ����� ��� ����� ����������
                _daGraphPanels[3].Border.Color = Color.Red;// ��������� ���� ����� ��� ����� ����������
                _selectPane = 3;
            }
            DrawUpdate();
        }

        /// <summary>
        /// ���������� �������� �������� �����������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bpn_plus_reg_Click(object sender, EventArgs e)
        {
            if (_selectPane == 255)
                return;
            double amp = (_daGraphPanels[_selectPane].YAxis.Scale.Max - _daGraphPanels[_selectPane].YAxis.Scale.Min) * 0.1;
            _daGraphPanels[_selectPane].YAxis.Scale.Min = _daGraphPanels[_selectPane].YAxis.Scale.Min + amp;
            _daGraphPanels[_selectPane].YAxis.Scale.Max = _daGraphPanels[_selectPane].YAxis.Scale.Max - amp;
            DrawUpdate();
        }

        /// <summary>
        /// ���������� �������� �������� �����������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_minus_reg_Click(object sender, EventArgs e)
        {
            if (_selectPane == 255)
                return;
            double amp = (_daGraphPanels[_selectPane].YAxis.Scale.Max - _daGraphPanels[_selectPane].YAxis.Scale.Min) * 0.1;
            _daGraphPanels[_selectPane].YAxis.Scale.Min = _daGraphPanels[_selectPane].YAxis.Scale.Min - amp;
            _daGraphPanels[_selectPane].YAxis.Scale.Max = _daGraphPanels[_selectPane].YAxis.Scale.Max + amp;
            DrawUpdate();
        }

        /// <summary>
        /// ������ �������������������� ����������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_flash_edit_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Binary files (*.bin)|*.bin";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filename = openFileDialog1.FileName;
                textBox1.Text = filename;
                progressBar1.Value = 0;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }

        /// <summary>
        /// ����� ������������� ������� ����������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_graphtype_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cb_graphtype.SelectedIndex == 0)
            {
                tableLayoutPanel1.RowStyles[1].Height = 50;
                tableLayoutPanel1.ColumnStyles[1].Width = 50;
                tableLayoutPanel1.RowStyles[0].Height = 50;
                tableLayoutPanel1.ColumnStyles[0].Width = 50;
            }
            else if (cb_graphtype.SelectedIndex == 1)
            {
                tableLayoutPanel1.RowStyles[0].Height = 100;
                tableLayoutPanel1.ColumnStyles[0].Width = 100;
                tableLayoutPanel1.RowStyles[1].Height = 0;
                tableLayoutPanel1.ColumnStyles[1].Width = 0;
            }
            else if (cb_graphtype.SelectedIndex == 2)
            {
                tableLayoutPanel1.RowStyles[0].Height = 100;
                tableLayoutPanel1.ColumnStyles[0].Width = 0;
                tableLayoutPanel1.RowStyles[1].Height = 0;
                tableLayoutPanel1.ColumnStyles[1].Width = 100;
            }
            else if (cb_graphtype.SelectedIndex == 3)
            {
                tableLayoutPanel1.RowStyles[0].Height = 0;
                tableLayoutPanel1.ColumnStyles[0].Width = 100;
                tableLayoutPanel1.RowStyles[1].Height = 100;
                tableLayoutPanel1.ColumnStyles[1].Width = 0;
            }
            else if (cb_graphtype.SelectedIndex == 4)
            {
                tableLayoutPanel1.RowStyles[0].Height = 0;
                tableLayoutPanel1.ColumnStyles[0].Width = 0;
                tableLayoutPanel1.RowStyles[1].Height = 100;
                tableLayoutPanel1.ColumnStyles[1].Width = 100;
            }
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "�����������";
            toolStripStatusLabel2.BackColor = SystemColors.Control;
            com_send_cmd(0x2b); // ���� ��������������
            //_serialPort.Close();
            btn_stop.Enabled = false;
            btn_start.Enabled = true;
            _isDataStartFlux = false;
            gb_settings.Enabled = false;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > numericUpDown1.Value)
                dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 1);
            else
                dataGridView1.Rows.Add();
        }
        /// <summary>
        /// ������ �������� � �������� �������� � �������������� ������� �������������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button17_Click(object sender, EventArgs e)
        {
            _isGraduateMode = true;
            if (!_isDataStartFlux) btn_start_Click(sender, e); // ������ ���������
        }
        /// <summary>
        /// ������ ������������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button16_Click(object sender, EventArgs e)
        {
            double[,] table = new double[2, dataGridView1.Rows.Count];
            for (int i = 0; i < 2; i++)//��������� ������ �� �������
            {
                for (int j = 0; j < dataGridView1.Rows.Count; j++)
                {
                    table[i, j] = double.Parse((string)dataGridView1.Rows[j].Cells[i].Value);
                }
            }
            double[,] aprox = MakeSystem(table, 2);
            //����� ����� ��������� ��������
        }

        /// <summary>
        /// ���������� �������� �������� � �� ���������
        /// </summary>
        private void save_settings_graph()
        {
            if (check_grid.Checked)
            {
                _daGraphController.ShowGrid();
                for (int j = 0; j < 4; j++)
                {

                    _daGraphPanels[j].XAxis.MajorGrid.IsVisible = true;
                    // ����� ������� ����� 10 ��������, ...
                    _daGraphPanels[j].XAxis.MajorGrid.DashOn = 10;
                    // ����� 5 �������� - �������
                    _daGraphPanels[j].XAxis.MajorGrid.DashOff = 5;
                    // �������� ����������� ����� �������� ������� ����� �� ��� Y
                    _daGraphPanels[j].YAxis.MajorGrid.IsVisible = true;
                    // ���������� ������ ��� ���������� ����� ��� ������� ����� �� ��� Y
                    _daGraphPanels[j].YAxis.MajorGrid.DashOn = 10;
                    _daGraphPanels[j].YAxis.MajorGrid.DashOff = 5;
                    // �������� ����������� ����� �������� ������ ����� �� ��� X
                    _daGraphPanels[j].YAxis.MinorGrid.IsVisible = true;
                    // ������ ��� ���������� ����� ��� ������� ����� �� ��� Y:
                    // ����� ������� ����� ������ �������, ...
                    _daGraphPanels[j].YAxis.MinorGrid.DashOn = 1;
                    // ����� 2 ������� - �������
                    _daGraphPanels[j].YAxis.MinorGrid.DashOff = 2;
                    // �������� ����������� ����� �������� ������ ����� �� ��� Y
                    _daGraphPanels[j].XAxis.MinorGrid.IsVisible = true;
                    // ���������� ������ ��� ���������� ����� ��� ������� ����� �� ��� Y
                    _daGraphPanels[j].XAxis.MinorGrid.DashOn = 1;
                    _daGraphPanels[j].XAxis.MinorGrid.DashOff = 2;
                }
            }
            else
            {
                _daGraphController.HideGrid();
                for (int j = 0; j < 4; j++)
                {
                    _daGraphPanels[j].XAxis.MajorGrid.IsVisible = false;
                    _daGraphPanels[j].YAxis.MajorGrid.IsVisible = false;
                    _daGraphPanels[j].YAxis.MinorGrid.IsVisible = false;
                    _daGraphPanels[j].XAxis.MinorGrid.IsVisible = false;
                }
            }
            if (rb_templot_1.Checked == true)
            {
                _daGraphController.SetGraphTheme(new WhiteGraphTheme());
                for (int j = 0; j < 4; j++)
                {
                    _daGraphPanels[j].CurveList.RemoveAt(0);
                    _daGraphCurves[j] = _daGraphPanels[j].AddCurve("", _daGraphData[j], Color.Blue, SymbolType.None);

                    _daGraphCurves[j].Line.Width = (float)num_linewidth.Value;
                    _daGraphPanels[j].Border.Color = Color.Black;// ��������� ���� ����� ��� ����� ����������                 
                    _daGraphPanels[j].Chart.Border.Color = Color.Black; // ��������� ���� ����� ������ �������                                      
                    _daGraphPanels[j].Fill.Type = FillType.Solid;// �������� ��� ����� ���������� ZedGraph
                    _daGraphPanels[j].Fill.Color = Color.White;// ������� ����� ��������                   
                    _daGraphPanels[j].Chart.Fill.Type = FillType.Solid; // �������� ������� ������� (��� ���) � ������ ����
                    _daGraphPanels[j].Chart.Fill.Color = Color.White;
                    _daGraphPanels[j].XAxis.MajorGrid.IsZeroLine = true;// ������� ����� ��� �� ������ X = 0 � Y = 0, ����� ������ ���� ����
                    _daGraphPanels[j].YAxis.MajorGrid.IsZeroLine = true;
                    _daGraphPanels[j].XAxis.Color = Color.Black; // ��������� ���� ����
                    _daGraphPanels[j].YAxis.Color = Color.Black;
                    _daGraphPanels[j].XAxis.MajorGrid.Color = Color.Black;// ��������� ���� ��� �����
                    _daGraphPanels[j].YAxis.MajorGrid.Color = Color.Black;
                    _daGraphPanels[j].XAxis.Title.FontSpec.FontColor = Color.Black;// ��������� ���� ��� �������� ����� � �����
                    _daGraphPanels[j].YAxis.Title.FontSpec.FontColor = Color.Black;
                    _daGraphPanels[j].XAxis.Scale.FontSpec.FontColor = Color.Black;// ��������� ���� �������� ��� �������
                    _daGraphPanels[j].YAxis.Scale.FontSpec.FontColor = Color.Black;
                    _daGraphPanels[j].Title.FontSpec.FontColor = Color.Black; // ��������� ���� ��������� ��� ��������                    
                }
            }
            else if (rb_templot_2.Checked == true)
            {
                _daGraphController.SetGraphTheme(new BlackGraphTheme());
                for (int j = 0; j < 4; j++)
                {
                    _daGraphPanels[j].CurveList.RemoveAt(0);
                    _daGraphCurves[j] = _daGraphPanels[j].AddCurve("", _daGraphData[j], Color.Yellow, SymbolType.None);

                    _daGraphCurves[j].Line.Width = (float)num_linewidth.Value;
                    _daGraphPanels[j].Border.Color = Color.Black;// ��������� ���� ����� ��� ����� ����������                 
                    _daGraphPanels[j].Chart.Border.Color = Color.Green; // ��������� ���� ����� ������ �������                                      
                    _daGraphPanels[j].Fill.Type = FillType.Solid;// �������� ��� ����� ���������� ZedGraph
                    _daGraphPanels[j].Fill.Color = Color.Silver;// ������� ����� ��������                   
                    _daGraphPanels[j].Chart.Fill.Type = FillType.Solid; // �������� ������� ������� (��� ���) � ������ ����
                    _daGraphPanels[j].Chart.Fill.Color = Color.Black;
                    _daGraphPanels[j].XAxis.MajorGrid.IsZeroLine = true;// ������� ����� ��� �� ������ X = 0 � Y = 0, ����� ������ ���� ����
                    _daGraphPanels[j].YAxis.MajorGrid.IsZeroLine = true;
                    _daGraphPanels[j].XAxis.Color = Color.Gray; // ��������� ���� ����
                    _daGraphPanels[j].YAxis.Color = Color.Gray;
                    _daGraphPanels[j].XAxis.MajorGrid.Color = Color.Cyan;// ��������� ���� ��� �����
                    _daGraphPanels[j].YAxis.MajorGrid.Color = Color.Cyan;
                    _daGraphPanels[j].XAxis.Title.FontSpec.FontColor = Color.Teal;// ��������� ���� ��� �������� ����� � �����
                    _daGraphPanels[j].YAxis.Title.FontSpec.FontColor = Color.Teal;
                    _daGraphPanels[j].XAxis.Scale.FontSpec.FontColor = Color.Black;// ��������� ���� �������� ��� �������
                    _daGraphPanels[j].YAxis.Scale.FontSpec.FontColor = Color.Black;
                    _daGraphPanels[j].Title.FontSpec.FontColor = Color.Teal; // ��������� ���� ��������� ��� ��������
                }
            }
            _daGraphController.SetLineWidth((int) num_linewidth.Value);
            mainTabControl.SelectedIndex = 0;
            DrawUpdate();
        }

        private void btn_flash_Click(object sender, EventArgs e)
        {
            string url = textBox1.Text;
            if (url == null) return;

            _wirmvareData = File.ReadAllBytes(url);
            long size = _wirmvareData.Length;

            byte[] data = new byte[2];
            data[0] = (byte)size;
            data[1] = (byte)(size>>8);
            com_send(0x5b, data, 2);//���� ������� ������� + � ���������� ������ �����
        }

        /// <summary>
        /// ������ ��������� ��������� ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_graph_settings_Click(object sender, EventArgs e)
        {
            save_settings_graph();
        }

        private void Form1_FormClosing(object sender, FormClosedEventArgs e)
        {
           if(SerialPort.IsOpen)
            {
                SerialPort.Close();
            }
            SaveSettings();
        }

        private void zedGraphControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (cb_graphtype.SelectedIndex == 0)
            {
                tableLayoutPanel1.RowStyles[0].Height = 100;
                tableLayoutPanel1.ColumnStyles[0].Width = 100;
                tableLayoutPanel1.RowStyles[1].Height = 0;
                tableLayoutPanel1.ColumnStyles[1].Width = 0;
                cb_graphtype.SelectedIndex = 1;
            }
            else
            {
                tableLayoutPanel1.ColumnStyles[1].Width = 50;
                tableLayoutPanel1.RowStyles[0].Height = 50;
                tableLayoutPanel1.ColumnStyles[0].Width = 50;
                cb_graphtype.SelectedIndex = 0;
            }
        }

        private void zedGraphControl2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (cb_graphtype.SelectedIndex == 0)
            {
                tableLayoutPanel1.RowStyles[0].Height = 100;
                tableLayoutPanel1.ColumnStyles[0].Width = 0;
                tableLayoutPanel1.RowStyles[1].Height = 0;
                tableLayoutPanel1.ColumnStyles[1].Width = 100;
                cb_graphtype.SelectedIndex = 2;
            }
            else
            {
                tableLayoutPanel1.ColumnStyles[1].Width = 50;
                tableLayoutPanel1.RowStyles[0].Height = 50;
                tableLayoutPanel1.ColumnStyles[0].Width = 50;
                cb_graphtype.SelectedIndex = 0;
            }
        }

        private void zedGraphControl3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (cb_graphtype.SelectedIndex == 0)
            {
                tableLayoutPanel1.RowStyles[0].Height = 0;
                tableLayoutPanel1.ColumnStyles[0].Width = 100;
                tableLayoutPanel1.RowStyles[1].Height = 100;
                tableLayoutPanel1.ColumnStyles[1].Width = 0;
                cb_graphtype.SelectedIndex = 3;
            }
            else
            {
                tableLayoutPanel1.ColumnStyles[1].Width = 50;
                tableLayoutPanel1.RowStyles[0].Height = 50;
                tableLayoutPanel1.ColumnStyles[0].Width = 50;
                cb_graphtype.SelectedIndex = 0;
            }
        }

        private void zedGraphControl4_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (cb_graphtype.SelectedIndex == 0)
            {
                tableLayoutPanel1.RowStyles[0].Height = 0;
                tableLayoutPanel1.ColumnStyles[0].Width = 0;
                tableLayoutPanel1.RowStyles[1].Height = 100;
                tableLayoutPanel1.ColumnStyles[1].Width = 100;
                cb_graphtype.SelectedIndex = 4;
            }
            else
            {
                tableLayoutPanel1.ColumnStyles[1].Width = 50;
                tableLayoutPanel1.RowStyles[0].Height = 50;
                tableLayoutPanel1.ColumnStyles[0].Width = 50;
                cb_graphtype.SelectedIndex = 0;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_isTestButton)//��������� �� ������� ������ ������������
            {
                _isTestButton = false;
                SerialPort.Close();
                MessageBox.Show("�� ������� ������������ � ����������.\n���������� ������������� ����������\n��� �������� ��������", "������", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);           
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if(button15.Text=="��������� ����� ������ � ASCII")
            {
                button15.Text = "���������� ����� ������ � ASCII";
                _isAsciiConsole = true;
                com_send_cmd(0xc1);
                if(!_isDataStartFlux)
                    btn_start_Click(sender, e);
            }
            else
            {
                button15.Text = "��������� ����� ������ � ASCII";
                com_send_cmd(0xc2);
                btn_stop_Click(sender, e);
                _isAsciiConsole = false;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string mailto = string.Format("mailto:{0}?Subject={1}&Body={2}", "az@azmotors.ru", "������ �� FluxViewer", "������ ����!");
            //            mailto = Uri.EscapeUriString(mailto);
            Process.Start(new ProcessStartInfo(mailto) { UseShellExecute = true });
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.linkLabel1.LinkVisited = true;          
            Process.Start(new ProcessStartInfo { FileName = @"http://fluxmeter.ru", UseShellExecute = true });
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = Environment.CurrentDirectory + @"\doc\����������� ������������ FluxViewer.pdf",  // ���� � ����������
                UseShellExecute = true,
                CreateNoWindow = true
            };
            Process.Start(startInfo);
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = Environment.CurrentDirectory + @"\doc\����������� ������������ ��������� �����-�.pdf",  // ���� � ����������
                UseShellExecute = true,
                CreateNoWindow = true
            };
            Process.Start(startInfo);
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void connect_flux()
        {
            if (!SerialPort.IsOpen)
            {
                SerialPort.ReadTimeout = 500;
                SerialPort.WriteTimeout = 500;
                if (comNameComboBox.Text == "")
                {
                    MessageBox.Show("�������� COM ����", "���������", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return;
                }
                if (comNameComboBox.Text == "��� ������")
                {
                    MessageBox.Show("��� ��������� COM ������", "���������", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return;
                }
                SerialPort.PortName = (string)comNameComboBox.Text;
                SerialPort.BaudRate = int.Parse((string)com_speed.Text);
                SerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                try
                {
                    SerialPort.Open();
                }
                catch (/*InvalidOperationException*/Exception ex)
                {
                    MessageBox.Show(ex.Message, "������ �����������", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return;
                }
            }
            gb_settings.Enabled = true;
            com_send_cmd(0x2e);// ��������� ����

        }
        /// <summary>
        /// ������ �������������� ����������� � ��� �����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>								
        private void btn_testconnect_Click(object sender, EventArgs e)
        {
            if(SerialPort.IsOpen)
            {
                SerialPort.Close();
            }
            _isTestButton = true;
            timer1.Interval = 1000;
            timer1.Enabled = true;
            connect_flux();
        }
        
        /// <summary>
        /// ����������� �������� ��������� �� ������ ���������� ���������
        /// </summary>
        /// <param name="xyTable"></param>
        /// <param name="basis"></param>
        /// <returns></returns>
        private double[,] MakeSystem(double[,] xyTable, int basis)
        {
            double[,] matrix = new double[basis, basis + 1];
            for (int i = 0; i < basis; i++)
            {
                for (int j = 0; j < basis; j++)
                {
                    matrix[i, j] = 0;
                }
            }
            for (int i = 0; i < basis; i++)
            {
                for (int j = 0; j < basis; j++)
                {
                    double sumA = 0, sumB = 0;
                    for (int k = 0; k < xyTable.Length / 2; k++)
                    {
                        sumA += Math.Pow(xyTable[0, k], i) * Math.Pow(xyTable[0, k], j);
                        sumB += xyTable[1, k] * Math.Pow(xyTable[0, k], i);
                    }
                    matrix[i, j] = sumA;
                    matrix[i, basis] = sumB;
                }
            }
            return matrix;
        }
        
    }
}