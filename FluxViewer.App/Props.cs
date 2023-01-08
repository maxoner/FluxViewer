using System.Reflection;
using System.Xml.Serialization;

namespace XMLFileSettings
{
    //Класс определяющий какие настройки есть в программе
    public class PropsFields
    {
        //Чтобы добавить настройку в программу просто добавьте туда строку вида -
        //public ТИП ИМЯ_ПЕРЕМЕННОЙ = значение_переменной_по_умолчанию;
        public String TextValue = @"File Settings";
        public DateTime DateValue = new DateTime(2011, 1, 1);
        public Decimal DecimalValue = 555;
        public Boolean BoolValue = true;

        public String db_path = @"d:\Project\Ya\YandexDisk\!Project\Fluxmeter\!Digital\ver1.1\FluxViewer\FluxViewer\FluxViewer.App\bin\Debug\net6.0-windows\defaultDataBase.db";

        public Boolean is_pc_time = true;
        public String com_num = @"";
        public String com_speed = @"115200";
        public double maxGbRAM = 2.5;
        
        //вкладка графифики
        public Decimal line_width = 1;
        public Boolean is_black_theme = true;
        public Boolean is_grid = true;

        public String g_one_title = @"Электростатическое поле, В/м";
        public String g_two_title = @"Температура, гр. С";
        public String g_three_title = @"Давление, мм. рт. ст.";
        public String g_four_title = @"Влажность, %";
        public String g1_K = @"1";
        public String g1_B = @"0";
        public String g2_K = @"0,7500637";
        public String g2_B = @"0";
        public String g3_K = @"1";
        public String g3_B = @"0";
        public String g4_K = @"1";
        public String g4_B = @"0";
    }

    //Класс работы с настройками
    public class Props
    {
        public PropsFields Fields;

        public Props()
        {
            Fields = new PropsFields();
        }

        //Запись настроек в файл
        public void WriteXml()
        {
            String CurrentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            String XMLFileName = Environment.CurrentDirectory + "\\settings.xml";
            XmlSerializer ser = new XmlSerializer(typeof(PropsFields));
            TextWriter writer = new StreamWriter(XMLFileName);
            ser.Serialize(writer, Fields);
            writer.Close();
        }

        //Чтение настроек из файла
        public void ReadXml()
        {
            String CurrentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            String XMLFileName = Environment.CurrentDirectory + "\\settings.xml";
            if (File.Exists(XMLFileName))
            {
                XmlSerializer ser = new XmlSerializer(typeof(PropsFields));
                TextReader reader = new StreamReader(XMLFileName);
                Fields = ser.Deserialize(reader) as PropsFields;
                reader.Close();
            }
            else
            {
                //можно написать вывод сообщения если файла не существует
            }
        }
    }
}