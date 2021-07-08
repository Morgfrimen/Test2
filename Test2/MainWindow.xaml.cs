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
            txtDelet.Text = i.ToString();
            XDocument xNewDoc = XDocument.Load(xmlFile);
            //Запись
            xNewDoc.Root.Add(new XElement("User",
                new XElement("ID", txtDelet.Text),
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
            XmlDocument xNewDoc = new XmlDocument();
            xNewDoc.Load(xmlFile);
            
            //Удалить хотя бы 3

            XmlNodeList nodes = xNewDoc.SelectNodes("Users/User[@ID='3']");
            for (int i = nodes.Count - 1; i >= 0; i--)
            {
                nodes[i].ParentNode.RemoveChild(nodes[i]);
            }

            xNewDoc.Save("XmlFile1.xml");

            //Обновление
            obnovleniewpf();

        }

        //Запись юзера по номеру в лейбл и пересохранение его
        private void Button_Click4(object sender, RoutedEventArgs e)
        {
            XmlDocument xNewDoc = new XmlDocument();
            xNewDoc.Load(xmlFile);



        }

        //Пересоздание документа, полностью чистый
        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            //Очистка
            XDocument sourceDoc = XDocument.Parse(@"<Users>
            </Users>");
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
            var result = xNewDoc.Descendants("User").Select(x => new
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
