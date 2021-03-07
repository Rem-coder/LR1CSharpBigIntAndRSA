using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vovk_A_A_LR1
{
    public class LongInt
    {
        public List<int> Number { get; }
        public char Sign { get; }
        public string StrNumber { get; }

        public LongInt((char, List<int>) number)
        {
            this.Number = number.Item2;
            Sign = number.Item1;
            StrNumber = GetStr();
        }

        public int this[int index]
        {
            get
            {
                if (index >= this.GetSize())
                    return 0;
                else
                    return this.Number[index];
            }
        }

        public static bool operator >(LongInt a, LongInt b) => Comparison(a, b, false) > 0;
        public static bool operator <(LongInt a, LongInt b) => Comparison(a, b, false) < 0;
        public static bool operator ==(LongInt a, LongInt b) => Comparison(a, b, false) == 0;
        public static bool operator !=(LongInt a, LongInt b) => Comparison(a, b, false) != 0;

        public static LongInt operator +(LongInt first, LongInt second) => first.Sign == second.Sign
            ? Add(first, second)
            : Substract(first, second);
        public static LongInt operator -(LongInt first, LongInt second)
        {
            char negativeSign = '-';
            if (second.Sign == '-')
                negativeSign = '+';
            return new LongInt((negativeSign, second.Number));
        }

        public int GetSize() => Number.Count(); // Получаем кол-во разрядов(длину числа)

        public static int Comparison(LongInt first, LongInt second, bool ignoreSign)//Сравниваем по первому. больше 1 равно 0 меньше -1
        {//Проверено тестами 
            var res = 0;
            if (first.Sign == '+' && second.Sign == '-' && !ignoreSign) //Сравнение по знаку
                res = 1;
            else if (first.Sign == '-' && second.Sign == '+' && !ignoreSign)
                res = -1;

            else if (first.GetSize() != second.GetSize()) //Сравнение по длине
            {
                if (first.GetSize() > second.GetSize())
                    res = 1;
                else
                    res = -1;
            }

            else if (first.GetSize() == second.GetSize()) //Сравнение по значению старшего разряда
            {
                for (var i = 0; i < first.GetSize(); i++)
                {
                    if (first.Number[i] > second.Number[i])
                    {
                        res = 1;
                        break;
                    }
                    else if (first.Number[i] < second.Number[i])
                    {
                        res = -1;
                        break;
                    }
                }
            }

            if (first.Sign == second.Sign && first.Sign == '-') //Учитываем отрицательные случаи
                res = -res;
            return res;
        }

        public static LongInt Add(LongInt first, LongInt second) //используется при одинаковых знаках числа
        {
            LongInt longLongInt = first;
            LongInt shortLongInt = second;
            if (first.GetSize() != second.GetSize()) //Если кол-во разрядов отличается, то необходимо заполнить меньшее число 0-ми.
            {
                if (first.GetSize() > second.GetSize())
                {
                    longLongInt = first;
                    shortLongInt = FillZero(second, first.GetSize());
                }
                else
                {
                    longLongInt = second;
                    shortLongInt = FillZero(first, second.GetSize()); ;
                }

            }
            var res = new List<int>();
            var ten = 0;
            longLongInt.Number.Reverse(); //Реверсируем чтобы складывать с младших разрядов.
            shortLongInt.Number.Reverse();
            for (var i = 0; i < longLongInt.GetSize(); i++)
            {
                var summ = longLongInt[i] + shortLongInt[i] + ten;
                if (summ >= 10)
                {
                    summ -= 10;
                    ten = 1;
                }
                else
                    ten = 0;
                res.Add(summ);
            }
            if (ten > 0)
                res.Add(ten);
            res.Reverse(); //Реверсируем обратно
            return new LongInt((first.Sign, res));
        }

        private static LongInt FillZero(LongInt longInt, int longIntLength) // Первое заполненное нулями(присмотреться, возможно он и не нужен)
        {
            var newLongInt = new List<int>();
            for (var i = 0; i < longIntLength - longInt.GetSize(); i++)
                newLongInt.Add(0);
            for (var i = 0; i < longInt.GetSize(); i++)
                newLongInt.Add(longInt[i]);
            return new LongInt((longInt.Sign, newLongInt));
        }

        public static LongInt Substract(LongInt first, LongInt second) //Использовать при разных знаках
        {//проверено тестами
            var res = new List<int>();
            var biggerLongInt = first;
            var lesserLongInt = second;
            if (LongInt.Comparison(first, second, true) == 1)
            {
                biggerLongInt = first;
                lesserLongInt = FillZero(second, first.GetSize());
            }
            else
            {
                biggerLongInt = second;
                lesserLongInt = FillZero(first, second.GetSize());
            }
            var ten = 0;
            for (var i = lesserLongInt.GetSize() - 1; i >= 0; i--)
            {
                var sum = biggerLongInt[i] - lesserLongInt[i] - ten;
                if (sum < 0)
                {
                    sum += 10;
                    ten = 1;
                }
                else
                    ten = 0;
                res.Add(sum);
            }
            res.Reverse();
            if (res[0] == 0) //Удаляем возможный не желательный 0
                res.RemoveAt(0);
            return new LongInt((biggerLongInt.Sign, res));
        }

        private string GetStr()//Получение строкового представления числа
        {//Проверен тестами.
            string res = "";
            if (Sign == '-')
                res += '-';
            foreach (var num in Number)
                res += num.ToString();
            return res;
        }
    }
}
