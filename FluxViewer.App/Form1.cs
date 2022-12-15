using System.IO.Ports;
using ZedGraph;
using System.Globalization;
using System.Text;
using System.Security;
using System.IO;
using System.Windows.Forms;
//using FluxViewer.Properties;
using FluxViewer.DataAccess.LiteDbb;
using FluxViewer.DataAccess.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Reflection;
using System.Collections.Generic;
using XMLFileSettings;
using System.Drawing;
using System.Diagnostics;
//using 

namespace FluxViewer
{
    public partial class Form1 : Form
    {
        static SerialPort _serialPort = new SerialPort();
        public static int tabindex = 0;

        readonly int _capacity = 50;
        double _currentx = 0;
        readonly double _step = 1;

        float flux = 0, temp = 0, pres = 0, humm = 0; //переданные измеренные данные

        RollingPointPairList[] _data = new RollingPointPairList[4]; //данные на графиках
        /*        RollingPointPairList _data2;
                RollingPointPairList _data3;
                RollingPointPairList _data4;   */
        PointPairList list; // Создадим список точек для графика архива

        PointPair[] _data_archive;

        GraphPane[] pane = new GraphPane[5];
        LineItem[] myCurve = new LineItem[5];

        bool is_SettingMode = false; //флаг что тестируем порт из вкладки настройки
        bool is_GraduateMode = false;//вкладка градуировка, пишем данные в таблицу
        float average_dataflux=0; // среднее значение измерений в режиме градуировки
        int pos_average;// колическво средних
        readonly int pos_average_max=10;
        bool is_dataStartFlux = false;
        bool is_TestButton;          //запрос информации с кнопки тестирования связи
        bool is_ASCII_console;//Нажата кнопка вывести данные в консоль

        byte select_pane = 255; //текущий выбранный график

        int baze_size; //размер строк в базе между указанными датами
        readonly int max_outsize_graph = 600000;
        readonly int max_readsize_base = 600000;
        //byte sizetable = 2;

        // База данных
        ILiteDbService _dataBaseContext;

 //       #region Settings action
        Props props = new Props(); //экземпляр класса с настройками 
        string[] graph_title = new string[4];
        float[] graph_k = new float[4];
        float[] graph_b = new float[4];

        //System.IO.FileInfo file;
        byte[] wirmvare_data;
        long position_firmware = 0;
        long size_wirmware = 0;

        public Form1()
        {
            _dataBaseContext = new LiteDbService();

            _data[0] = new RollingPointPairList(_capacity);
            _data[1] = new RollingPointPairList(_capacity);
            _data[2] = new RollingPointPairList(_capacity);
            _data[3] = new RollingPointPairList(_capacity);
            list = new PointPairList();

            InitializeComponent();

            GetSetings();
            DrawGraph();

            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();

            save_settings_graph();

            //_dataBaseContext.LogInformation("Приложение запущено");
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
        private void DrawGraph_dot()
        {
            pane[4] = zedGraphControl5.GraphPane;
        }
        /// <summary>
        /// Инициализация графиков
        /// </summary>
        private void DrawGraph()
        {
            // Получим панель для рисования
            pane[0] = zedGraphControl1.GraphPane;
            pane[1] = zedGraphControl2.GraphPane;
            pane[2] = zedGraphControl3.GraphPane;
            pane[3] = zedGraphControl4.GraphPane;
            pane[4] = zedGraphControl5.GraphPane;

            // Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
            /*        pane1.CurveList.Clear();
                    pane2.CurveList.Clear();
                    pane3.CurveList.Clear();
                    pane4.CurveList.Clear();*/

            // Добавим кривую пока еще без каких-либо точек
            for (int i = 0; i < 5; i++)
            {
                if (i < 4)
                    myCurve[i] = pane[i].AddCurve("", _data[i], Color.Blue, SymbolType.None);
                else
                    myCurve[i] = pane[i].AddCurve("", list, Color.Blue, SymbolType.None);

                myCurve[i].Line.Width = (float)num_linewidth.Value;//2.0F;
                pane[i].XAxis.Title.Text = "Время мм:cc";
            }
            pane[0].Title.Text = graph_title[0];
            pane[1].Title.Text = graph_title[1];
            pane[2].Title.Text = graph_title[2];
            pane[3].Title.Text = graph_title[3];

            pane[0].YAxis.Scale.Min = -5000;
            pane[0].YAxis.Scale.Max = 5000;
            pane[1].YAxis.Scale.Min = -40;
            pane[1].YAxis.Scale.Max = 60;
            pane[2].YAxis.Scale.Min = -10;
            pane[2].YAxis.Scale.Max = 1200;
            pane[3].YAxis.Scale.Min = 0;
            pane[3].YAxis.Scale.Max = 100;

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
            if(is_ASCII_console)
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
                if (!is_dataStartFlux)
                {
                    is_dataStartFlux = true;//преобразование запущено
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        toolStripStatusLabel2.Text = "Подключено";
                        toolStripStatusLabel2.BackColor = System.Drawing.Color.Green;
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
                flux = BitConverter.ToSingle(rx_buf, 12) * float.Parse(textBox7.Text) + float.Parse(textBox8.Text);
                temp = BitConverter.ToSingle(rx_buf, 16) * float.Parse(textBox10.Text) + float.Parse(textBox9.Text);
                pres = BitConverter.ToSingle(rx_buf, 20) * float.Parse(textBox13.Text) + float.Parse(textBox12.Text);
                humm = BitConverter.ToSingle(rx_buf, 24) * float.Parse(textBox16.Text) + float.Parse(textBox15.Text);

                //режим градуировки
                if (is_GraduateMode)
                {
                    average_dataflux += flux / 2;
                    if (pos_average > pos_average_max)
                    {
                        this.BeginInvoke((MethodInvoker)delegate
                        {
                            dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value = flux.ToString();
                            btn_stop_Click(sender, e);// отключить преобразование
                        });
                        pos_average = 0;
                        is_GraduateMode = false;
                        return;
                    }
                    pos_average++;
                    return;
                }
                //XDate valueX = new XDate(date1);
                _data[0].Add(_currentx, flux);
                _data[1].Add(_currentx, temp);
                _data[2].Add(_currentx, pres);
                _data[3].Add(_currentx, humm);
                _currentx += _step;

                // Рассчитаем интервал по оси X, который нужно отобразить на графике
                double xmin = _currentx - _capacity * _step;
                double xmax = _currentx;

                for (int j = 0; j < 4; j++)
                {
                    pane[j].XAxis.Scale.Min = xmin;
                    pane[j].XAxis.Scale.Max = xmax;
                }

                //Сохранить данные в базу
                _dataBaseContext.WriteData(new Data()
                {
                    FluxSensorData = flux,
                    TempSensorData = temp,
                    PressureSensorData = pres,
                    HumiditySensorData = humm
                });


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
                    toolStripStatusLabel2.BackColor = System.Drawing.Color.Green;
                    toolStripStatusLabel4.Text = Encoding.UTF8.GetString(rx_buf, 18, 10);
                    if (is_TestButton)
                    {
                        timer1.Enabled = false;                        
                        MessageBox.Show(info, "Успешно подключено", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        is_TestButton = false;

                    }
                });
            }
            else if (rx_buf[2] == 0x5b)//Перепрошивка
            {
                if (position_firmware == 0)
                {                   
                    size_wirmware = wirmvare_data.Length;
                    position_firmware = 200;
                    this.BeginInvoke((MethodInvoker)delegate { 
                        progressBar1.Maximum = (int)size_wirmware; 
                        label29.Text = "Передача данных в устройство";
                        progressBar1.Value = (int)position_firmware;
                    });
                    com_send(0x5b, wirmvare_data, 200);
                }
                else if (position_firmware <= size_wirmware)
                {
                    if (size_wirmware - position_firmware >= 200)
                    {
                        byte[] arr = new byte[200];
                        Array.Copy(wirmvare_data, (int)position_firmware, arr, 0, 200);
                        com_send(0x5b, arr, 200);
                        position_firmware += 200;
                        this.BeginInvoke((MethodInvoker)delegate { progressBar1.Value = (int)position_firmware; });
                    }
                    else
                    {
                        byte[] arr = new byte[size_wirmware - position_firmware];
                        Array.Copy(wirmvare_data, (int)position_firmware, arr, 0, size_wirmware - position_firmware);
                        com_send(0x5b, arr, (int)(size_wirmware - position_firmware));
                        position_firmware += size_wirmware - position_firmware;
                        this.BeginInvoke((MethodInvoker)delegate { 
                        label29.Text = "Перепрограммирование устройства";
                            System.Threading.Thread.Sleep(3000);
                        btn_stop_Click(sender, e);
                         });
                        _serialPort.Close();
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
        /// Считать настройки из файла .Settings
        /// </summary>
        public void GetSetings()
        {
            props.ReadXml();
            string path = props.Fields.db_path.ToString();
            this.textBox3.Text = path;
            if(_dataBaseContext.ConnectOrCreateDataBase(path)==false)
            {
                MessageBox.Show("Укажите путь к базе данных \nи перезагрузите приложение", "Ошибка подключения к базе", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            //_dataBaseContext.ConnectOrCreateDataBase(path);
            rb_isPCclock.Checked = props.Fields.is_pc_time;
            com_name.Text = props.Fields.com_num.ToString();
            com_speed.Text = props.Fields.com_speed.ToString();


            textBox6.Text = props.Fields.g_one_title.ToString();
            textBox11.Text = props.Fields.g_two_title.ToString();
            textBox14.Text = props.Fields.g_three_title.ToString();
            textBox17.Text = props.Fields.g_four_title.ToString();
            textBox7.Text = props.Fields.g1_K.ToString();
            textBox8.Text = props.Fields.g1_B.ToString();
            textBox10.Text = props.Fields.g2_K.ToString();
            textBox9.Text = props.Fields.g2_B.ToString();
            textBox13.Text = props.Fields.g3_K.ToString();
            textBox12.Text = props.Fields.g3_B.ToString();
            textBox16.Text = props.Fields.g4_K.ToString();
            textBox15.Text = props.Fields.g4_B.ToString();

            graph_title[0] = props.Fields.g_one_title.ToString();
            graph_title[1] = props.Fields.g_two_title.ToString();
            graph_title[2] = props.Fields.g_three_title.ToString();
            graph_title[3] = props.Fields.g_four_title.ToString();

            graph_k[0] = float.Parse(props.Fields.g1_K);
            graph_b[0] = float.Parse(props.Fields.g1_B);
            graph_k[1] = float.Parse(props.Fields.g2_K);
            graph_b[1] = float.Parse(props.Fields.g2_B);
            graph_k[2] = float.Parse(props.Fields.g3_K);
            graph_b[2] = float.Parse(props.Fields.g3_B);
            graph_k[3] = float.Parse(props.Fields.g4_K);
            graph_b[3] = float.Parse(props.Fields.g4_B);

            comboBox1.Items.Clear();
            comboBox1.Items.Add(graph_title[0]);
            comboBox1.Items.Add(graph_title[1]);
            comboBox1.Items.Add(graph_title[2]);
            comboBox1.Items.Add(graph_title[3]);
            cb_graphtype.Items.Clear();
            cb_graphtype.Items.Add("все графики");
            cb_graphtype.Items.Add(graph_title[0]);
            cb_graphtype.Items.Add(graph_title[1]);
            cb_graphtype.Items.Add(graph_title[2]);
            cb_graphtype.Items.Add(graph_title[3]);
            cb_graphtype.SelectedIndex = 0;

            //вкладка графифики
            num_linewidth.Value = props.Fields.line_width;
            rb_templot_2.Checked = props.Fields.is_black_theme;
            check_grid.Checked = props.Fields.is_grid;
        }

        /// <summary>
        /// Сохранить настройки приложения
        /// </summary>
        public void SaveSettings()
        {
            //вкладка программа
            props.Fields.db_path = textBox3.Text;
            props.Fields.is_pc_time = rb_isPCclock.Checked;
            props.Fields.com_num = com_name.Text;
            props.Fields.com_speed = com_speed.Text;

            //вкладка графифики
            props.Fields.line_width = num_linewidth.Value;
            props.Fields.is_black_theme = rb_templot_2.Checked;
            props.Fields.is_grid = check_grid.Checked;

            props.Fields.g_one_title = textBox6.Text;
            props.Fields.g_two_title = textBox11.Text;
            props.Fields.g_three_title = textBox14.Text;
            props.Fields.g_four_title = textBox17.Text;
            props.Fields.g1_K = textBox7.Text;
            props.Fields.g1_B = textBox8.Text;
            props.Fields.g2_K = textBox10.Text;
            props.Fields.g2_B = textBox9.Text;
            props.Fields.g3_K = textBox13.Text;
            props.Fields.g3_B = textBox12.Text;
            props.Fields.g4_K = textBox16.Text;
            props.Fields.g4_B = textBox15.Text;
            props.WriteXml();
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
            if (!_serialPort.IsOpen)
            {
                _serialPort.PortName = SetPortName();
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
                _textBrush = new System.Drawing.SolidBrush(e.ForeColor);
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
                    pane[j].YAxis.Scale.MinAuto = true;
                    pane[j].YAxis.Scale.MaxAuto = true;
                    pane[j].IsBoundedRanges = true;
                }
            }
            else
            {
                for (int j = 0; j < 4; j++)
                {
                    pane[j].YAxis.Scale.MinAuto = false;
                    pane[j].YAxis.Scale.MaxAuto = false;
                    pane[j].IsBoundedRanges = false;
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
            if (_serialPort.IsOpen)
            {
                _serialPort.Write(data, 0, 5);
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
            _serialPort.Write(buf, 0, lenght + 5);
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
            _serialPort.Write(buf, 0, buf.Length);

            DialogResult result = MessageBox.Show("Настройки записаны в устройство\nдля применение необходимо перезагрузить устройство\nПерезагрузить?", "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.Yes)
            {
                System.Threading.Thread.Sleep(500);
                btn_stop_Click(sender,e);
                //команда перезагрузки
                System.Threading.Thread.Sleep(500);
                com_send_cmd(0x5a);
                _serialPort.Close();

            }
        }
       
        /// <summary>
        /// Кнопка запутить регистрацию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
    
        
        private void btn_start_Click(object sender, EventArgs e)
        {
            if (!is_dataStartFlux)
            {
                connect_flux();
            }
            if (_serialPort.IsOpen)
            {
                System.Threading.Thread.Sleep(400);
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
                if (_serialPort.IsOpen)
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

            GraphPane pane_dot = zedGraphControl5.GraphPane;

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
            if (select_pane == 0)
            {
                pane[0].Border.Color = Color.Black;// Установим цвет рамки для всего компонента
                select_pane = 255;
            }
            else
            {
                pane[0].Border.Color = Color.Red;// Установим цвет рамки для всего компонента
                pane[1].Border.Color = Color.Black;// Установим цвет рамки для всего компонента
                pane[2].Border.Color = Color.Black;// Установим цвет рамки для всего компонента
                pane[3].Border.Color = Color.Black;// Установим цвет рамки для всего компонента
                select_pane = 0;
            }
            DrawUpdate();
        }
        private void zedGraphControl2_MouseClick(object sender, MouseEventArgs e)
        {
            if (select_pane == 1)
            {
                pane[1].Border.Color = Color.Black;// Установим цвет рамки для всего компонента
                select_pane = 255;
            }
            else
            {
                pane[0].Border.Color = Color.Black;// Установим цвет рамки для всего компонента
                pane[1].Border.Color = Color.Red;// Установим цвет рамки для всего компонента
                pane[2].Border.Color = Color.Black;// Установим цвет рамки для всего компонента
                pane[3].Border.Color = Color.Black;// Установим цвет рамки для всего компонента
                select_pane = 1;
            }
            DrawUpdate();
        }
        private void zedGraphControl3_MouseClick(object sender, MouseEventArgs e)
        {
            if (select_pane == 2)
            {
                pane[2].Border.Color = Color.Black;// Установим цвет рамки для всего компонента
                select_pane = 255;
            }
            else
            {
                pane[0].Border.Color = Color.Black;// Установим цвет рамки для всего компонента
                pane[1].Border.Color = Color.Black;// Установим цвет рамки для всего компонента
                pane[2].Border.Color = Color.Red;// Установим цвет рамки для всего компонента
                pane[3].Border.Color = Color.Black;// Установим цвет рамки для всего компонента
                select_pane = 2;
            }
            DrawUpdate();
        }
        private void zedGraphControl4_MouseClick(object sender, MouseEventArgs e)
        {
            if (select_pane == 3)
            {
                pane[3].Border.Color = Color.Black;// Установим цвет рамки для всего компонента
                select_pane = 255;
            }
            else
            {
                pane[0].Border.Color = Color.Black;// Установим цвет рамки для всего компонента
                pane[1].Border.Color = Color.Black;// Установим цвет рамки для всего компонента
                pane[2].Border.Color = Color.Black;// Установим цвет рамки для всего компонента
                pane[3].Border.Color = Color.Red;// Установим цвет рамки для всего компонента
                select_pane = 3;
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
            if (select_pane == 255)
                return;
            double amp = (pane[select_pane].YAxis.Scale.Max - pane[select_pane].YAxis.Scale.Min) * 0.1;
            pane[select_pane].YAxis.Scale.Min = pane[select_pane].YAxis.Scale.Min + amp;
            pane[select_pane].YAxis.Scale.Max = pane[select_pane].YAxis.Scale.Max - amp;
            DrawUpdate();
        }

        /// <summary>
        /// Уменьшение масштаба графиков регистрации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_minus_reg_Click(object sender, EventArgs e)
        {
            if (select_pane == 255)
                return;
            double amp = (pane[select_pane].YAxis.Scale.Max - pane[select_pane].YAxis.Scale.Min) * 0.1;
            pane[select_pane].YAxis.Scale.Min = pane[select_pane].YAxis.Scale.Min - amp;
            pane[select_pane].YAxis.Scale.Max = pane[select_pane].YAxis.Scale.Max + amp;
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
            is_dataStartFlux = false;
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
            is_GraduateMode = true;
            if (!is_dataStartFlux) btn_start_Click(sender, e); // запуск измерения
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
        /// изменение даты начала отрезка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            DateTime date1 = new DateTime(dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, dateTimePicker2.Value.Day, 00, 00, 00, 000);
            DateTime date2 = new DateTime(dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, dateTimePicker2.Value.Day, 23, 59, 59, 999);

            comboBox1.Text = "";
           
            if (_dataBaseContext.GetHasDataBetweenTwoDates(date1, date2))
            {
               baze_size = _dataBaseContext.GetDataCountBetweenTwoDates(date1, date2);
               list.Clear();
               comboBox1.Enabled = true;
               //comboBox1.DroppedDown = true;
               btn_export.Enabled = true;
            }
            else
            {
                comboBox1.Enabled = false;
                btn_export.Enabled = false;
            }
        }


        /// <summary>
        /// Построить график архива
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            DateTime date1 = new DateTime(dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, dateTimePicker2.Value.Day, 00, 00, 00, 000);
            DateTime date2 = new DateTime (dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, dateTimePicker2.Value.Day, 23, 59, 59, 999);
           
            list.Clear();
            list.AddRange(_dataBaseContext.GetDataBetweenTwoDatesColumn(date1, date2, comboBox1.SelectedIndex+1, (int)baze_size / max_outsize_graph - 1));
            if (comboBox1.Text == "")
            {
                pane[4].Title.Text ="График";
            }
            else
            {
                pane[4].Title.Text = comboBox1.Text;
            }

            /*           switch (comboBox1.SelectedIndex)
                       {
                           case 0:
                               pane[4].Title.Text = "Электростатическое поле, В/м";
                               break;
                           case 1:
                               pane[4].Title.Text = "Температура, гр. C";
                               break;
                           case 2:
                               pane[4].Title.Text = "Давление, мБар";
                               break;
                           case 3:
                               pane[4].Title.Text = "Влажность, %";
                               break;
                       }*/
            pane[4].XAxis.Type = AxisType.Date;
            pane[4].YAxis.Scale.MinAuto = true;
            pane[4].YAxis.Scale.MaxAuto = true;
            if (checkBox3.Checked)
            {
                pane[4].XAxis.Scale.MinAuto = true;
                pane[4].XAxis.Scale.MaxAuto = true;
            }
            else
            {
                pane[4].XAxis.Scale.MinAuto = false;
                pane[4].XAxis.Scale.MaxAuto = false;
                pane[4].XAxis.Scale.Min = new XDate(date1);
                pane[4].XAxis.Scale.Max = new XDate(date2);
            }
            pane[4].IsBoundedRanges = true;

            zedGraphControl5.AxisChange();
            zedGraphControl5.Invalidate();
        }

        /// <summary>
        /// Сохранение настроек графиков и их изменение
        /// </summary>
        private void save_settings_graph()
        {
            if (check_grid.Checked)
            {
                for (int j = 0; j < 5; j++)
                {

                    pane[j].XAxis.MajorGrid.IsVisible = true;
                    // Длина штрихов равна 10 пикселям, ...
                    pane[j].XAxis.MajorGrid.DashOn = 10;
                    // затем 5 пикселей - пропуск
                    pane[j].XAxis.MajorGrid.DashOff = 5;
                    // Включаем отображение сетки напротив крупных рисок по оси Y
                    pane[j].YAxis.MajorGrid.IsVisible = true;
                    // Аналогично задаем вид пунктирной линии для крупных рисок по оси Y
                    pane[j].YAxis.MajorGrid.DashOn = 10;
                    pane[j].YAxis.MajorGrid.DashOff = 5;
                    // Включаем отображение сетки напротив мелких рисок по оси X
                    pane[j].YAxis.MinorGrid.IsVisible = true;
                    // Задаем вид пунктирной линии для крупных рисок по оси Y:
                    // Длина штрихов равна одному пикселю, ...
                    pane[j].YAxis.MinorGrid.DashOn = 1;
                    // затем 2 пикселя - пропуск
                    pane[j].YAxis.MinorGrid.DashOff = 2;
                    // Включаем отображение сетки напротив мелких рисок по оси Y
                    pane[j].XAxis.MinorGrid.IsVisible = true;
                    // Аналогично задаем вид пунктирной линии для крупных рисок по оси Y
                    pane[j].XAxis.MinorGrid.DashOn = 1;
                    pane[j].XAxis.MinorGrid.DashOff = 2;
                }
            }
            else
            {
                for (int j = 0; j < 5; j++)
                {
                    pane[j].XAxis.MajorGrid.IsVisible = false;
                    pane[j].YAxis.MajorGrid.IsVisible = false;
                    pane[j].YAxis.MinorGrid.IsVisible = false;
                    pane[j].XAxis.MinorGrid.IsVisible = false;
                }
            }
            if (rb_templot_1.Checked == true)
            {
                for (int j = 0; j < 5; j++)
                {
                    pane[j].CurveList.RemoveAt(0);
                    if (j < 4)
                        myCurve[j] = pane[j].AddCurve("", _data[j], Color.Blue, SymbolType.None);
                    else
                        myCurve[j] = pane[j].AddCurve("", list, Color.Blue, SymbolType.None);

                    myCurve[j].Line.Width = (float)num_linewidth.Value;
                    pane[j].Border.Color = Color.Black;// Установим цвет рамки для всего компонента                 
                    pane[j].Chart.Border.Color = Color.Black; // Установим цвет рамки вокруг графика                                      
                    pane[j].Fill.Type = FillType.Solid;// Закрасим фон всего компонента ZedGraph
                    pane[j].Fill.Color = Color.White;// Заливка будет сплошная                   
                    pane[j].Chart.Fill.Type = FillType.Solid; // Закрасим область графика (его фон) в черный цвет
                    pane[j].Chart.Fill.Color = Color.White;
                    pane[j].XAxis.MajorGrid.IsZeroLine = true;// Включим показ оси на уровне X = 0 и Y = 0, чтобы видеть цвет осей
                    pane[j].YAxis.MajorGrid.IsZeroLine = true;
                    pane[j].XAxis.Color = Color.Black; // Установим цвет осей
                    pane[j].YAxis.Color = Color.Black;
                    pane[j].XAxis.MajorGrid.Color = Color.Black;// Установим цвет для сетки
                    pane[j].YAxis.MajorGrid.Color = Color.Black;
                    pane[j].XAxis.Title.FontSpec.FontColor = Color.Black;// Установим цвет для подписей рядом с осями
                    pane[j].YAxis.Title.FontSpec.FontColor = Color.Black;
                    pane[j].XAxis.Scale.FontSpec.FontColor = Color.Black;// Установим цвет подписей под метками
                    pane[j].YAxis.Scale.FontSpec.FontColor = Color.Black;
                    pane[j].Title.FontSpec.FontColor = Color.Black; // Установим цвет заголовка над графиком                    
                }
            }
            else if (rb_templot_2.Checked == true)
            {
                for (int j = 0; j < 5; j++)
                {
                    pane[j].CurveList.RemoveAt(0);
                    if (j < 4)
                        myCurve[j] = pane[j].AddCurve("", _data[j], Color.Yellow, SymbolType.None);
                    else
                        myCurve[j] = pane[j].AddCurve("", list, Color.Yellow, SymbolType.None);

                    myCurve[j].Line.Width = (float)num_linewidth.Value;
                    pane[j].Border.Color = Color.Black;// Установим цвет рамки для всего компонента                 
                    pane[j].Chart.Border.Color = Color.Green; // Установим цвет рамки вокруг графика                                      
                    pane[j].Fill.Type = FillType.Solid;// Закрасим фон всего компонента ZedGraph
                    pane[j].Fill.Color = Color.Silver;// Заливка будет сплошная                   
                    pane[j].Chart.Fill.Type = FillType.Solid; // Закрасим область графика (его фон) в черный цвет
                    pane[j].Chart.Fill.Color = Color.Black;
                    pane[j].XAxis.MajorGrid.IsZeroLine = true;// Включим показ оси на уровне X = 0 и Y = 0, чтобы видеть цвет осей
                    pane[j].YAxis.MajorGrid.IsZeroLine = true;
                    pane[j].XAxis.Color = Color.Gray; // Установим цвет осей
                    pane[j].YAxis.Color = Color.Gray;
                    pane[j].XAxis.MajorGrid.Color = Color.Cyan;// Установим цвет для сетки
                    pane[j].YAxis.MajorGrid.Color = Color.Cyan;
                    pane[j].XAxis.Title.FontSpec.FontColor = Color.Teal;// Установим цвет для подписей рядом с осями
                    pane[j].YAxis.Title.FontSpec.FontColor = Color.Teal;
                    pane[j].XAxis.Scale.FontSpec.FontColor = Color.Black;// Установим цвет подписей под метками
                    pane[j].YAxis.Scale.FontSpec.FontColor = Color.Black;
                    pane[j].Title.FontSpec.FontColor = Color.Teal; // Установим цвет заголовка над графиком
                }
            }
            tabControl1.SelectedIndex = 0;
            DrawUpdate();
        }

        private void btn_achive_plus_Click(object sender, EventArgs e)
        {
            double amp = (pane[4].YAxis.Scale.Max - pane[4].YAxis.Scale.Min) * 0.1;
            pane[4].YAxis.Scale.Min = pane[4].YAxis.Scale.Min + amp;
            pane[4].YAxis.Scale.Max = pane[4].YAxis.Scale.Max - amp;
            zedGraphControl5.AxisChange();
            zedGraphControl5.Invalidate();
        }

        private void btn_achive_minus_Click(object sender, EventArgs e)
        {
            double amp = (pane[4].YAxis.Scale.Max - pane[4].YAxis.Scale.Min) * 0.1;
            pane[4].YAxis.Scale.Min = pane[4].YAxis.Scale.Min - amp;
            pane[4].YAxis.Scale.Max = pane[4].YAxis.Scale.Max + amp;
            zedGraphControl5.AxisChange();
            zedGraphControl5.Invalidate();
        }

        private void btn_achive_up_Click(object sender, EventArgs e)
        {
            double amp = (pane[4].YAxis.Scale.Max - pane[4].YAxis.Scale.Min) * 0.1;
            pane[4].YAxis.Scale.Min = pane[4].YAxis.Scale.Min - amp;
            pane[4].YAxis.Scale.Max = pane[4].YAxis.Scale.Max - amp;
            zedGraphControl5.AxisChange();
            zedGraphControl5.Invalidate();
        }

        private void btn_achive_down_Click(object sender, EventArgs e)
        {
            double amp = (pane[4].YAxis.Scale.Max - pane[4].YAxis.Scale.Min) * 0.1;
            pane[4].YAxis.Scale.Min = pane[4].YAxis.Scale.Min + amp;
            pane[4].YAxis.Scale.Max = pane[4].YAxis.Scale.Max + amp;
            zedGraphControl5.AxisChange();
            zedGraphControl5.Invalidate();
        }

        private void btn_achive_autozoom_Click(object sender, EventArgs e)
        {
            pane[4].YAxis.Scale.MinAuto = true;
            pane[4].YAxis.Scale.MaxAuto = true;
            pane[4].XAxis.Scale.MinAuto = true;
            pane[4].XAxis.Scale.MaxAuto = true;
            pane[4].IsBoundedRanges = true;
            zedGraphControl5.AxisChange();
            zedGraphControl5.Invalidate();
        }

        private void btn_flash_Click(object sender, EventArgs e)
        {
            string url = textBox1.Text;
            if (url == null) return;

            wirmvare_data = File.ReadAllBytes(url);
            long size = wirmvare_data.Length;

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
           if(_serialPort.IsOpen)
            {
                _serialPort.Close();
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

        private void button8_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Base files (*.db)|*.db";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filename = openFileDialog1.FileName;
                textBox3.Text = filename;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (is_TestButton)//Произошло по нажатию кнопки тестирования
            {
                is_TestButton = false;
                _serialPort.Close();
                MessageBox.Show("Не удалось подключиться к флюксметру.\nПопробуйте перезагрузить устройство\nили изменить настрйки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);           
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if(button15.Text=="Запустить вывод данных в ASCII")
            {
                button15.Text = "Остановить вывод данных в ASCII";
                is_ASCII_console = true;
                com_send_cmd(0xc1);
                if(!is_dataStartFlux)
                    btn_start_Click(sender, e);
            }
            else
            {
                button15.Text = "Запустить вывод данных в ASCII";
                com_send_cmd(0xc2);
                btn_stop_Click(sender, e);
                is_ASCII_console = false;
            }


        }

        private void btn_export_Click(object sender, EventArgs e)
        {
            string temp_line;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "Сохранить выбраный отрезок как";
            if (saveFileDialog1.FileName != "")
            {
                System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();
                int time_via = int.Parse(textBox2.Text);

                if (time_via==0)//выводем весь файл
                { 
                 if(checkBox3.Checked)//Выводим только записаный кусок
                    { }
                 else //Выводим кусок с заполнениями
                    { }
                }
                DateTime date1 = new DateTime(dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, dateTimePicker2.Value.Day, 00, 00, 00, 000);
                DateTime date2 = new DateTime(dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, dateTimePicker2.Value.Day, 23, 59, 59, 999);

                //List 
                for (int i = 0; i < (int)(baze_size / max_readsize_base); i++)
                {
                    list.Clear();
                    list.AddRange(_dataBaseContext.GetDataBetweenTwoDatesColumn(date1, date2, comboBox1.SelectedIndex + 1, (int)(baze_size / max_outsize_graph) - 1));
                      }
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1: //txt

                        break;

                    case 2://csv

                        break;
                }

                do
                {
                    temp_line = "1";
                    byte[] output = Encoding.Default.GetBytes(temp_line);
                    fs.Write(output, 0, output.Length);
                }
                while (true);


                fs.Close();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string mailto = string.Format("mailto:{0}?Subject={1}&Body={2}", "az@azmotors.ru", "Запрос по FluxViewer", "Добрый день!");
            //            mailto = Uri.EscapeUriString(mailto);
            System.Diagnostics.Process.Start(new ProcessStartInfo(mailto) { UseShellExecute = true });
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.linkLabel1.LinkVisited = true;          
            System.Diagnostics.Process.Start(new ProcessStartInfo { FileName = @"http://fluxmeter.ru", UseShellExecute = true });
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var startInfo = new System.Diagnostics.ProcessStartInfo
            {
                FileName = Environment.CurrentDirectory + @"\doc\Руководство пользователя FluxViewer.pdf",  // Путь к приложению
                UseShellExecute = true,
                CreateNoWindow = true
            };
            System.Diagnostics.Process.Start(startInfo);
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var startInfo = new System.Diagnostics.ProcessStartInfo
            {
                FileName = Environment.CurrentDirectory + @"\doc\Руководство пользователя флюксметр Пчела-Д.pdf",  // Путь к приложению
                UseShellExecute = true,
                CreateNoWindow = true
            };
            System.Diagnostics.Process.Start(startInfo);
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void connect_flux()
        {
            if (!_serialPort.IsOpen)
            {
                _serialPort.ReadTimeout = 500;
                _serialPort.WriteTimeout = 500;
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
                _serialPort.PortName = (string)com_name.Text;
                _serialPort.BaudRate = int.Parse((string)com_speed.Text);
                _serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                try
                {
                    _serialPort.Open();
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
            if(_serialPort.IsOpen)
            {
                _serialPort.Close();
            }
            is_TestButton = true;
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

  /*      private double[] Gauss(double[,] a, int n)
        {
        double[,] initial_a_matrix;
        double[,] a_matrix;  // матрица A
        double[] x_vector;   // вектор неизвестных x
        double[] initial_b_vector;
        double[] b_vector;   // вектор b
        double eps;          // порядок точности для сравнения вещественных чисел 
        int size;            // размерность задачи

            return x;
        }*/

    }
}