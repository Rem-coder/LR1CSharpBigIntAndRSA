using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Vovk_A_A_LR1
{
    class Program
    {
        static void Main()
        {
            var numbers = GetNumbers();
            var longNumbers = new List<LongInt>();
        }

        static List<(char, List<int>)> GetNumbers() //Метод получения чисел из Numbers.xml
        {
            var xDoc = new XmlDocument();
            xDoc.Load(@".\Data\Numbers.xml");
            var xRoot = xDoc.DocumentElement;
            var numbers = new List<(char, List<int>)>(); //Был выбран способ хранения в виде кортежа, чтобы всегда было удобно отследить знак числа
            foreach (XmlNode xNode in xRoot)
            {
                var numbersList = ('+', new List<int>());
                foreach (var discharge in xNode.InnerText)
                {
                    if (discharge == '-')
                    {
                        numbersList.Item1 = '-';
                        continue;
                    }
                    numbersList.Item2.Add(int.Parse(discharge.ToString()));
                }
                numbers.Add(numbersList);
            }
            return numbers;
        }
    }
}
