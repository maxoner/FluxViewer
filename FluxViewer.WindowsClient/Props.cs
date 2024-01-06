using System.Xml.Serialization;

namespace FluxViewer.WindowsClient
{
    /// <summary>
    /// Класс, определяющий настройки программы.
    /// Каждое поле класса - соответствующая настройка.
    /// Чтобы добавить настройку в программу просто добавьте в класс строку вида:
    ///     public ТИП_ПЕРЕМЕННОЙ ИМЯ_ПЕРЕМЕННОЙ = значение_переменной_по_умолчанию; 
    /// </summary>
    public class PropsFields
    {
        public bool IsPcTime = true;
        public string ComNum = @"";
        public string ComSpeed = @"115200";

        // Параметры, связанные с графиками
        public decimal LineWidth = 1;
        public bool IsBlackTheme = true;
        public bool IsGrid = true;

        public string GOneTitle = @"Электростатическое поле, В/м";
        public string GTwoTitle = @"Температура, гр. С";
        public string GThreeTitle = @"Давление, мм. рт. ст.";
        public string GFourTitle = @"Влажность, %";
        public string G1K = @"1";
        public string G1B = @"0";
        public string G2K = @"0,7500637";
        public string G2B = @"0";
        public string G3K = @"1";
        public string G3B = @"0";
        public string G4K = @"1";
        public string G4B = @"0";
    }

    /// <summary>
    /// Класс, позволяющий читать и сохранять настройки в XML файл
    /// </summary>
    public class Props
    {
        private readonly string _pathToXmlFile;
        public PropsFields Fields;

        public Props(string pathToXmlFile)
        {
            _pathToXmlFile = pathToXmlFile;
            Fields = new PropsFields();
        }

        /// <summary>
        /// Запись настроек в файл.
        /// Настройки записываются (сохраняются)  в папку с приложением в файл "settings.xml".
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void WriteXml()
        {
            TextWriter writer;
            try
            {
                writer = new StreamWriter(_pathToXmlFile);
            }
            catch (IOException e)
            {
                throw new Exception(""); // TODO: надо написать уникальное исключение
            }

            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(PropsFields));
                ser.Serialize(writer, Fields);
            }
            catch
            {
                throw new Exception("fsda"); // TODO: надо написать уникальное исключение
            }
            finally
            {
                writer.Close();
            }
        }

        /// <summary>
        /// Чтение настроек из файла.
        /// Настройки ищутся в папке с приложением в файле "settings.xml".
        /// </summary>
        public void ReadXml()
        {
            var ser = new XmlSerializer(typeof(PropsFields));
            TextReader reader;
            try
            {
                reader = new StreamReader(_pathToXmlFile);
            }
            catch (Exception e)
            {
                throw new Exception("TODO"); // TODO: надо написать уникальное исключение
            }

            try
            {
                Fields = ser.Deserialize(reader) as PropsFields;
            }
            catch (Exception e)
            {
                throw new Exception("TODO"); // TODO: надо написать уникальное исключение
            }
            finally
            {
                reader.Close();
            }
        }
    }
}