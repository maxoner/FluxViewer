using System.Diagnostics;
using System.IO.Ports;
using System.Text;
using FluxViewer.DataAccess.GraphThemes;
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
        private const int MaxReadsizeBase = 600000; // TODO: зачем это поле???
        private const double Step = 1;

        private double _currentX;
        
        // Переданные измеренные данные прибора
        private float _flux;
        private float _temp;
        private float _pres;
        private float _humm;
        
        private readonly RollingPointPairList[] _daGraphData = new RollingPointPairList[4]; // Сами графики
        private readonly GraphPane[] _daGraphPanels = new GraphPane[4];
        private readonly LineItem[] _daGraphCurves = new LineItem[4];

        private bool _isGraduateMode; // Вкладка градуировка, пишем данные в таблицу
        private float _averageDataflux; // Среднее значение измерений в режиме градуировки TODO: зачем это поле???
        private int _posAverage; // Колическво средних

        private bool _isDataStartFlux;
        private bool _isTestButton; // Запрос информации с кнопки тестирования связи
        private bool _isAsciiConsole; // Нажата кнопка вывести данные в консоль?

        private byte _selectPane = 255; // Текущий выбранный график

        private int _baseSize; // Размер строк в базе между указанными датами TODO: выпилить данное поле

        // База данных
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
        /// Создаём и открываем хранилище, куда будут записываться данные прибора 
        /// </summary>
        private void OpenStorage()
        {
            _storage = new FileSystemStorage();
            _storage.Open();
        }
        
        /// <summary>
        /// Инициализируем настройки приложения и если их не существует, то создаём файл с стандартными настройками
        /// </summary>
        private void PrepareSettings()
        {
            var pathToXmlSettingsFile = Environment.CurrentDirectory + "\\settings.xml";
            _props = new Props(pathToXmlSettingsFile);
            // Если файла с настройками не существует, то дополняем найстроки и создаём этот файл
            if (!File.Exists(pathToXmlSettingsFile))
            {
                _props.WriteXml();
            }
        }

        private void InitDataArchiveGraphs()
        {
            // Получим панель для рисования
            _daGraphPanels[0] = zedGraphControl1.GraphPane;
            _daGraphPanels[1] = zedGraphControl2.GraphPane;
            _daGraphPanels[2] = zedGraphControl3.GraphPane;
            _daGraphPanels[3] = zedGraphControl4.GraphPane;
            
            // Добавим кривую пока еще без каких-либо точек
            for (var i = 0; i < 4; i++)
            {
                _daGraphCurves[i] = _daGraphPanels[i].AddCurve("", _daGraphData[i], Color.Blue, SymbolType.None);
                _daGraphCurves[i].Line.Width = (float)num_linewidth.Value; //2.0F;
                _daGraphPanels[i].XAxis.Title.Text = "Время мм:cc";
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
        /// Обновление графиков регистрации
        /// </summary>
        private void DrawUpdate()
        {
            // Вызываем метод AxisChange (), чтобы обновить данные об осях.
            zedGraphControl1.AxisChange();
            zedGraphControl2.AxisChange();
            zedGraphControl3.AxisChange();
            zedGraphControl4.AxisChange();

            // Обновляем график
            zedGraphControl1.Invalidate();
            zedGraphControl2.Invalidate();
            zedGraphControl3.Invalidate();
            zedGraphControl4.Invalidate();
        }
        
        private void com_connect_Click(object sender, EventArgs e)
        {

        }
        
        /// <summary>
        /// контрольная сумма принятого пакета
        /// </summary>
        /// <param name="data"></param>
        /// <param name="lenght"></param>
        /// <returns></returns>
        private byte checksum(byte[] data, byte lenght)
        {
            int sum = 0;
            for (byte i = 2; i < lenght + 2; i++) // побайтовая обработка
            {
                sum ^= (byte)data[i];
            }
            return (byte)sum;
        }

        /// <summary>
        /// Событие принят данные по COM порту
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
                if (rx_buf[0] != 0xFF && rx_buf[1] != 0xFF) //Проверяем 2 стартовых байт //в CRC не беруться
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
            if (rx_buf[2] == 0x1A)// Режим данных
            {
                if (!_isDataStartFlux)
                {
                    _isDataStartFlux = true;//преобразование запущено
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        toolStripStatusLabel2.Text = "Подключено";
                        toolStripStatusLabel2.BackColor = Color.Green;
                        btn_start.Enabled = false;
                        btn_stop.Enabled = true;

                    });
                }
                DateTime date1;
                if (rb_isFluxclock.Checked == true)//время
                {
                    date1 = new DateTime(rx_buf[4] + 2000, rx_buf[5], rx_buf[6], rx_buf[7], rx_buf[8], rx_buf[9], rx_buf[10] << 8 | rx_buf[11]); // год - месяц - день - час - минута - секунда - миллисек
                }
                else
                {
                    date1 = DateTime.Now;
                }
                _flux = BitConverter.ToSingle(rx_buf, 12) * float.Parse(textBox7.Text) + float.Parse(textBox8.Text);
                _temp = BitConverter.ToSingle(rx_buf, 16) * float.Parse(textBox10.Text) + float.Parse(textBox9.Text);
                _pres = BitConverter.ToSingle(rx_buf, 20) * float.Parse(textBox13.Text) + float.Parse(textBox12.Text);
                _humm = BitConverter.ToSingle(rx_buf, 24) * float.Parse(textBox16.Text) + float.Parse(textBox15.Text);

                //режим градуировки
                if (_isGraduateMode)
                {
                    _averageDataflux += _flux / 2;
                    if (_posAverage > PosAverageMax)
                    {
                        this.BeginInvoke((MethodInvoker)delegate
                        {
                            dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value = _flux.ToString();
                            btn_stop_Click(sender, e);// отключить преобразование
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

                // Рассчитаем интервал по оси X, который нужно отобразить на графике
                double xmin = _currentX - Capacity * Step;
                double xmax = _currentX;

                for (int j = 0; j < 4; j++)
                {
                    _daGraphPanels[j].XAxis.Scale.Min = xmin;
                    _daGraphPanels[j].XAxis.Scale.Max = xmax;
                }

                //Сохранить данные в базу
                // TODO: Убираем ID, т.к. не можем его контроллировать!
                _storage.WriteData(new Data(DateTime.Now, _flux, _temp, _pres, _humm));


                this.BeginInvoke((MethodInvoker)delegate { DrawUpdate(); });
            }
            else if (rx_buf[2] == 0x1B)// Режим считыванием настроек                    is_TestButton = false;
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
            /*          else if (rx_buf[2] == 0x1C)// версия прошивки
                      {
                          string mas = Encoding.UTF8.GetString(rx_buf, 2, 14);
                      }*/
            else if (rx_buf[2] == 0x2e)// версия прошивки
            {
                String info = "Версия прошивки\n" + Encoding.UTF8.GetString(rx_buf, 18, 10);


                StringBuilder hex = new StringBuilder(10 * 2);
                for (int i = 4; i <= 15; i++)
                {
                    hex.AppendFormat("{0:x2}", rx_buf[i]);
                }
                this.BeginInvoke((MethodInvoker)delegate
                {
                    label27.Text = "Версия прошивки: " + Encoding.UTF8.GetString(rx_buf, 18, 10);
                    label11.Text = "UIN контроллера: 0x" + hex.ToString();
                    label12.Text = "Размер памяти: " + (rx_buf[16] | (rx_buf[17] << 8)).ToString();
                    toolStripStatusLabel2.Text = "Подключено";
                    toolStripStatusLabel2.BackColor = Color.Green;
                    toolStripStatusLabel4.Text = Encoding.UTF8.GetString(rx_buf, 18, 10);
                    if (_isTestButton)
                    {
                        timer1.Enabled = false;                        
                        MessageBox.Show(info, "Успешно подключено", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        _isTestButton = false;

                    }
                });
            }
            else if (rx_buf[2] == 0x5b)//Перепрошивка
            {
                if (_positionFirmware == 0)
                {                   
                    _sizeWirmware = _wirmvareData.Length;
                    _positionFirmware = 200;
                    this.BeginInvoke((MethodInvoker)delegate { 
                        progressBar1.Maximum = (int)_sizeWirmware; 
                        label29.Text = "Передача данных в устройство";
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
                        label29.Text = "Перепрограммирование устройства";
                            Thread.Sleep(3000);
                        btn_stop_Click(sender, e);
                         });
                        SerialPort.Close();
                    //com_send_cmd(0x5a); //перезагружаем устроцство
                }
                }
/*                else
                {
                    position_firmware = 0;
                    this.BeginInvoke((MethodInvoker)delegate { progressBar1.Value = (int)size_wirmware; });
                    MessageBox.Show("Прошивка обновлена успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }*/
            }
            else if (rx_buf[2] == 0x5c)//Перепрошивка успешно выполнена
            {
                this.BeginInvoke((MethodInvoker)delegate { progressBar1.Value = 0; });
                MessageBox.Show("Прошивка обновлена успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
            }
        }

        /// <summary>
        /// Проверяем доступные COM Порты
        /// </summary>
        /// <returns></returns>
        public string SetPortName()
        {
            string portName = "";
            foreach (string portname in SerialPort.GetPortNames())
            {
                com_name.Items.Add(portname); //добавить порт в список    
            }
            if (com_name.Items.Count > 0)
            {
                com_name.SelectedIndex = 0;
                portName = com_name.Text;
            }
            else
            {
                portName = "нет портов";
                com_name.Text = "нет портов";
            }
            //     string[] myPort; //создаем массив строк
            //    myPort = System.IO.Ports.SerialPort.GetPortNames(); // в массив помещаем доступные порты
            //     comboBox1.Items.AddRange(myPort); //теперь этот массив заносим в список(comboBox) 
            return portName; //возвращает порт по умолчанию
        }
        
        /// <summary>
        /// Считываем настройки из файла и применяем их
        /// </summary>
        public void SetSettings()
        {
            _props.ReadXml();
         
            rb_isPCclock.Checked = _props.Fields.IsPcTime;
            com_name.Text = _props.Fields.ComNum.ToString();
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
            cb_graphtype.Items.Add("все графики");
            cb_graphtype.Items.Add(_graphTitle[0]);
            cb_graphtype.Items.Add(_graphTitle[1]);
            cb_graphtype.Items.Add(_graphTitle[2]);
            cb_graphtype.Items.Add(_graphTitle[3]);
            cb_graphtype.SelectedIndex = 0;

            //вкладка графифики
            num_linewidth.Value = _props.Fields.LineWidth;
            rb_templot_2.Checked = _props.Fields.IsBlackTheme;
            check_grid.Checked = _props.Fields.IsGrid;
        }

        /// <summary>
        /// Сохранить настройки приложения
        /// </summary>
        public void SaveSettings()
        {
            //вкладка программа
            _props.Fields.IsPcTime = rb_isPCclock.Checked;
            _props.Fields.ComNum = com_name.Text;
            _props.Fields.ComSpeed = com_speed.Text;

            //вкладка графифики
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
        /// Инициализация формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Shown(object sender, EventArgs e)
        {
            //_serialPort.PortName = SetPortName();
            //com_speed.SelectedIndex = 4;
        }

        /// <summary>
        /// Кнопка искать доступные COM порты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_updateport_Click(object sender, EventArgs e)
        {
            com_name.Items.Clear();
            if (!SerialPort.IsOpen)
            {
                SerialPort.PortName = SetPortName();
            }
            else 
            {
                MessageBox.Show("Порт уже открыт", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                //MessageBox.Show("Порт уже открыт", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        /// <summary>
        /// Стили табов настроек
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl2_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush _textBrush;

            // Get the item from the collection.
            TabPage _tabPage = tabControl2.TabPages[e.Index];

            // Get the real bounds for the tab rectangle.
            Rectangle _tabBounds = tabControl2.GetTabRect(e.Index);

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
        /// Автоматический масштаб графиков регистрации
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
        /// Посылаем в COM порт команду
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
        /// Посылаем пакет в COM порт
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
        
        //посылаем команду
        private void com_send_cmd(byte id)
        {
            byte[] temp = new byte[1];
            com_send(id, temp, 0);
        }
        
        /// <summary>
        /// Посылаем в COM Порта новые настройки прибора
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

            DialogResult result = MessageBox.Show("Настройки записаны в устройство\nдля применение необходимо перезагрузить устройство\nПерезагрузить?", "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.Yes)
            {
                Thread.Sleep(500);
                btn_stop_Click(sender,e);
                //команда перезагрузки
                Thread.Sleep(500);
                com_send_cmd(0x5a);
                SerialPort.Close();

            }
        }
       
        /// <summary>
        /// Кнопка запутить регистрацию
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
                com_send_cmd(0x2a);// Старт преобразования
            }
        }
        /*
          Кнопка выполнить синхронизацию времени
         */

        private void button1_Click(object sender, EventArgs e)
        {
            com_send_cmd(0x1b);// Старт преобразования
        }

        private void tabControl2_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPageIndex == 1) //настройка устройства
            {
                if (SerialPort.IsOpen)
                {
                    com_send_cmd(0x1b);// Настройки устройства
                }
                else
                {
                    gb_settings.Enabled = false;
                }
            }
            else if (e.TabPageIndex == 0)//настройка программы
            { }
            else if (e.TabPageIndex == 2)
            { }
            else if (e.TabPageIndex == 3)
            { }
            else if (e.TabPageIndex == 4)
            { }
        }

        /// <summary>
        /// построение точки на графике архива по клику
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void zedGraphControl5_MouseClick(object sender, MouseEventArgs e)
        {
            // Сюда будет сохранена кривая, рядом с которой был произведен клик
            CurveItem curve;

            // Сюда будет сохранен номер точки кривой, ближайшей к точке клика
            int index;

            GraphPane pane_dot = daMainZedGraphControl.GraphPane;

            // Максимальное расстояние от точки клика до кривой в пикселях,
            // при котором еще считается, что клик попал в окрестность кривой.
            GraphPane.Default.NearestTol = 10;

            bool result = pane_dot.FindNearestPoint(e.Location, out curve, out index);

            if (result)
            {
                // Максимально расстояние от точки клика до кривой не превысило NearestTol

                // Добавим точку на график, вблизи которой произошел клик
                PointPairList point = new PointPairList();

                point.Add(curve[index]);

                // Кривая, состоящая из одной точки. Точка будет отмечена синим кругом
                LineItem curvePount = pane_dot.AddCurve("",
                    new double[] { curve[index].X },
                    new double[] { curve[index].Y },
                    Color.Blue,
                    SymbolType.Circle);

                //
                curvePount.Line.IsVisible = false;

                // Цвет заполнения круга - колубой
                curvePount.Symbol.Fill.Color = Color.Blue;

                // Тип заполнения - сплошная заливка
                curvePount.Symbol.Fill.Type = FillType.Solid;

                // Размер круга
                curvePount.Symbol.Size = 6;
            }
        }

        /// <summary>
        /// Выделяем график регистрации при клике
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void zedGraphControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (_selectPane == 0)
            {
                _daGraphPanels[0].Border.Color = Color.Black;// Установим цвет рамки для всего компонента
                _selectPane = 255;
            }
            else
            {
                _daGraphPanels[0].Border.Color = Color.Red;// Установим цвет рамки для всего компонента
                _daGraphPanels[1].Border.Color = Color.Black;// Установим цвет рамки для всего компонента
                _daGraphPanels[2].Border.Color = Color.Black;// Установим цвет рамки для всего компонента
                _daGraphPanels[3].Border.Color = Color.Black;// Установим цвет рамки для всего компонента
                _selectPane = 0;
            }
            DrawUpdate();
        }
        private void zedGraphControl2_MouseClick(object sender, MouseEventArgs e)
        {
            if (_selectPane == 1)
            {
                _daGraphPanels[1].Border.Color = Color.Black;// Установим цвет рамки для всего компонента
                _selectPane = 255;
            }
            else
            {
                _daGraphPanels[0].Border.Color = Color.Black;// Установим цвет рамки для всего компонента
                _daGraphPanels[1].Border.Color = Color.Red;// Установим цвет рамки для всего компонента
                _daGraphPanels[2].Border.Color = Color.Black;// Установим цвет рамки для всего компонента
                _daGraphPanels[3].Border.Color = Color.Black;// Установим цвет рамки для всего компонента
                _selectPane = 1;
            }
            DrawUpdate();
        }
        private void zedGraphControl3_MouseClick(object sender, MouseEventArgs e)
        {
            if (_selectPane == 2)
            {
                _daGraphPanels[2].Border.Color = Color.Black;// Установим цвет рамки для всего компонента
                _selectPane = 255;
            }
            else
            {
                _daGraphPanels[0].Border.Color = Color.Black;// Установим цвет рамки для всего компонента
                _daGraphPanels[1].Border.Color = Color.Black;// Установим цвет рамки для всего компонента
                _daGraphPanels[2].Border.Color = Color.Red;// Установим цвет рамки для всего компонента
                _daGraphPanels[3].Border.Color = Color.Black;// Установим цвет рамки для всего компонента
                _selectPane = 2;
            }
            DrawUpdate();
        }
        private void zedGraphControl4_MouseClick(object sender, MouseEventArgs e)
        {
            if (_selectPane == 3)
            {
                _daGraphPanels[3].Border.Color = Color.Black;// Установим цвет рамки для всего компонента
                _selectPane = 255;
            }
            else
            {
                _daGraphPanels[0].Border.Color = Color.Black;// Установим цвет рамки для всего компонента
                _daGraphPanels[1].Border.Color = Color.Black;// Установим цвет рамки для всего компонента
                _daGraphPanels[2].Border.Color = Color.Black;// Установим цвет рамки для всего компонента
                _daGraphPanels[3].Border.Color = Color.Red;// Установим цвет рамки для всего компонента
                _selectPane = 3;
            }
            DrawUpdate();
        }

        /// <summary>
        /// Увеличение масштаба графиков регистрации
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
        /// Уменьшение масштаба графиков регистрации
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
        /// кнопка перепрограммирование устройства
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
        /// Выбор отображаемого графика регисрации
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
            toolStripStatusLabel2.Text = "Остановлено";
            toolStripStatusLabel2.BackColor = SystemColors.Control;
            com_send_cmd(0x2b); // стоп преобразования
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
        /// Кнопка измерить и записать значение в градуировочную таблицу напряженности
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button17_Click(object sender, EventArgs e)
        {
            _isGraduateMode = true;
            if (!_isDataStartFlux) btn_start_Click(sender, e); // запуск измерения
        }
        /// <summary>
        /// Кнопка апроксимации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button16_Click(object sender, EventArgs e)
        {
            double[,] table = new double[2, dataGridView1.Rows.Count];
            for (int i = 0; i < 2; i++)//заполняем массив из таблицы
            {
                for (int j = 0; j < dataGridView1.Rows.Count; j++)
                {
                    table[i, j] = double.Parse((string)dataGridView1.Rows[j].Cells[i].Value);
                }
            }
            double[,] aprox = MakeSystem(table, 2);
            //метод гауса вычисляем значения
        }

        /// <summary>
        /// Сохранение настроек графиков и их изменение
        /// </summary>
        private void save_settings_graph()
        {
            if (check_grid.Checked)
            {
                _daGraphController.ShowGrid();
                for (int j = 0; j < 4; j++)
                {

                    _daGraphPanels[j].XAxis.MajorGrid.IsVisible = true;
                    // Длина штрихов равна 10 пикселям, ...
                    _daGraphPanels[j].XAxis.MajorGrid.DashOn = 10;
                    // затем 5 пикселей - пропуск
                    _daGraphPanels[j].XAxis.MajorGrid.DashOff = 5;
                    // Включаем отображение сетки напротив крупных рисок по оси Y
                    _daGraphPanels[j].YAxis.MajorGrid.IsVisible = true;
                    // Аналогично задаем вид пунктирной линии для крупных рисок по оси Y
                    _daGraphPanels[j].YAxis.MajorGrid.DashOn = 10;
                    _daGraphPanels[j].YAxis.MajorGrid.DashOff = 5;
                    // Включаем отображение сетки напротив мелких рисок по оси X
                    _daGraphPanels[j].YAxis.MinorGrid.IsVisible = true;
                    // Задаем вид пунктирной линии для крупных рисок по оси Y:
                    // Длина штрихов равна одному пикселю, ...
                    _daGraphPanels[j].YAxis.MinorGrid.DashOn = 1;
                    // затем 2 пикселя - пропуск
                    _daGraphPanels[j].YAxis.MinorGrid.DashOff = 2;
                    // Включаем отображение сетки напротив мелких рисок по оси Y
                    _daGraphPanels[j].XAxis.MinorGrid.IsVisible = true;
                    // Аналогично задаем вид пунктирной линии для крупных рисок по оси Y
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
                    _daGraphPanels[j].Border.Color = Color.Black;// Установим цвет рамки для всего компонента                 
                    _daGraphPanels[j].Chart.Border.Color = Color.Black; // Установим цвет рамки вокруг графика                                      
                    _daGraphPanels[j].Fill.Type = FillType.Solid;// Закрасим фон всего компонента ZedGraph
                    _daGraphPanels[j].Fill.Color = Color.White;// Заливка будет сплошная                   
                    _daGraphPanels[j].Chart.Fill.Type = FillType.Solid; // Закрасим область графика (его фон) в черный цвет
                    _daGraphPanels[j].Chart.Fill.Color = Color.White;
                    _daGraphPanels[j].XAxis.MajorGrid.IsZeroLine = true;// Включим показ оси на уровне X = 0 и Y = 0, чтобы видеть цвет осей
                    _daGraphPanels[j].YAxis.MajorGrid.IsZeroLine = true;
                    _daGraphPanels[j].XAxis.Color = Color.Black; // Установим цвет осей
                    _daGraphPanels[j].YAxis.Color = Color.Black;
                    _daGraphPanels[j].XAxis.MajorGrid.Color = Color.Black;// Установим цвет для сетки
                    _daGraphPanels[j].YAxis.MajorGrid.Color = Color.Black;
                    _daGraphPanels[j].XAxis.Title.FontSpec.FontColor = Color.Black;// Установим цвет для подписей рядом с осями
                    _daGraphPanels[j].YAxis.Title.FontSpec.FontColor = Color.Black;
                    _daGraphPanels[j].XAxis.Scale.FontSpec.FontColor = Color.Black;// Установим цвет подписей под метками
                    _daGraphPanels[j].YAxis.Scale.FontSpec.FontColor = Color.Black;
                    _daGraphPanels[j].Title.FontSpec.FontColor = Color.Black; // Установим цвет заголовка над графиком                    
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
                    _daGraphPanels[j].Border.Color = Color.Black;// Установим цвет рамки для всего компонента                 
                    _daGraphPanels[j].Chart.Border.Color = Color.Green; // Установим цвет рамки вокруг графика                                      
                    _daGraphPanels[j].Fill.Type = FillType.Solid;// Закрасим фон всего компонента ZedGraph
                    _daGraphPanels[j].Fill.Color = Color.Silver;// Заливка будет сплошная                   
                    _daGraphPanels[j].Chart.Fill.Type = FillType.Solid; // Закрасим область графика (его фон) в черный цвет
                    _daGraphPanels[j].Chart.Fill.Color = Color.Black;
                    _daGraphPanels[j].XAxis.MajorGrid.IsZeroLine = true;// Включим показ оси на уровне X = 0 и Y = 0, чтобы видеть цвет осей
                    _daGraphPanels[j].YAxis.MajorGrid.IsZeroLine = true;
                    _daGraphPanels[j].XAxis.Color = Color.Gray; // Установим цвет осей
                    _daGraphPanels[j].YAxis.Color = Color.Gray;
                    _daGraphPanels[j].XAxis.MajorGrid.Color = Color.Cyan;// Установим цвет для сетки
                    _daGraphPanels[j].YAxis.MajorGrid.Color = Color.Cyan;
                    _daGraphPanels[j].XAxis.Title.FontSpec.FontColor = Color.Teal;// Установим цвет для подписей рядом с осями
                    _daGraphPanels[j].YAxis.Title.FontSpec.FontColor = Color.Teal;
                    _daGraphPanels[j].XAxis.Scale.FontSpec.FontColor = Color.Black;// Установим цвет подписей под метками
                    _daGraphPanels[j].YAxis.Scale.FontSpec.FontColor = Color.Black;
                    _daGraphPanels[j].Title.FontSpec.FontColor = Color.Teal; // Установим цвет заголовка над графиком
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
            com_send(0x5b, data, 2);//шлем команду прошить + и отправляем размер файла
        }

        /// <summary>
        /// Кнопка сохранить настройки графиков
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
            if (_isTestButton)//Произошло по нажатию кнопки тестирования
            {
                _isTestButton = false;
                SerialPort.Close();
                MessageBox.Show("Не удалось подключиться к флюксметру.\nПопробуйте перезагрузить устройство\nили изменить настрйки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);           
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if(button15.Text=="Запустить вывод данных в ASCII")
            {
                button15.Text = "Остановить вывод данных в ASCII";
                _isAsciiConsole = true;
                com_send_cmd(0xc1);
                if(!_isDataStartFlux)
                    btn_start_Click(sender, e);
            }
            else
            {
                button15.Text = "Запустить вывод данных в ASCII";
                com_send_cmd(0xc2);
                btn_stop_Click(sender, e);
                _isAsciiConsole = false;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string mailto = string.Format("mailto:{0}?Subject={1}&Body={2}", "az@azmotors.ru", "Запрос по FluxViewer", "Добрый день!");
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
                FileName = Environment.CurrentDirectory + @"\doc\Руководство пользователя FluxViewer.pdf",  // Путь к приложению
                UseShellExecute = true,
                CreateNoWindow = true
            };
            Process.Start(startInfo);
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = Environment.CurrentDirectory + @"\doc\Руководство пользователя флюксметр Пчела-Д.pdf",  // Путь к приложению
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
                if (com_name.Text == "")
                {
                    MessageBox.Show("Выберите COM порт", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return;
                }
                if (com_name.Text == "нет портов")
                {
                    MessageBox.Show("Нет доступных COM портов", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return;
                }
                SerialPort.PortName = (string)com_name.Text;
                SerialPort.BaudRate = int.Parse((string)com_speed.Text);
                SerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                try
                {
                    SerialPort.Open();
                }
                catch (/*InvalidOperationException*/Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка подключения", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return;
                }
            }
            gb_settings.Enabled = true;
            com_send_cmd(0x2e);// запросить инфо

        }
        /// <summary>
        /// Кнопка протестировать подключение к ком порту
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
        /// Составление линейных уравнений из метода наименьших квадратов
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