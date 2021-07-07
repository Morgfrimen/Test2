using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace Test2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int i = 0;
        string xmlFile = "Users.xml";
        public MainWindow()
        {
            InitializeComponent();
            XDocument xNewDoc = XDocument.Load(xmlFile);
            //Обновление
            obnovleniewpf();
        }

        //Добавление кнопка по Text
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            i++;
            XDocument xNewDoc = XDocument.Load(xmlFile);
            //Запись
            xNewDoc.Root.Add(new XElement("Cont",
                new XElement("ID", i),
                new XElement("Name", txtName.Text),
                new XElement("Surname", txtSurname.Text),
                new XElement("Otches", txtOtches.Text),
                new XElement("Phone", txtName.Text)));
            xNewDoc.Save(xmlFile);

            obnovleniewpf();

        }

        //Кнопка удаления строки из xml но не работает
        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            XDocument sourceDoc = XDocument.Parse(@"<title>
            </title>");
            sourceDoc.Descendants("title").Where(x => (string)x.Element("ID") == (string)txtDelet.Text).Remove();
            sourceDoc.Save(xmlFile);
            //Обновление
            obnovleniewpf();

        }

        //Пересоздание документа, полностью чистый
        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            //Очистка
            XDocument sourceDoc = XDocument.Parse(@"<title>
            </title>");
            var elementList = sourceDoc.Root.DescendantNodes().OfType<XText>();
            foreach (var element in elementList)
            {
                element.Value = element.Value.Replace("<", "LT").Replace(">", "GT");
            }
            sourceDoc.Save(xmlFile);
            i = 0;

            obnovleniewpf();

        }

        //Обновление
        public void obnovleniewpf()
        {
            XDocument xNewDoc = XDocument.Load(xmlFile);
            var result = xNewDoc.Descendants("Cont").Select(x => new
            {
                id = x.Element("ID").Value,
                Name = x.Element("Name").Value,
                Surname = x.Element("Surname").Value,
                Otches = x.Element("Otches").Value,
                Phone = x.Element("Phone").Value,
            });
            UsersGrid.ItemsSource = result;
        }
    }
}
