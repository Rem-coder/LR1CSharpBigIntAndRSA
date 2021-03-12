using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vovk_A_A_LR1
{
    public class LongInt
    {
        public List<int> Number { get; private set; }
        public char Sign { get; private set; }
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

            set
            {
                Number[index] = value;
            }
        }

        public void SetByte(int i, int b)
        {
            while (Number.Count <= i)
            {
                Number.Add(0);
            }

            Number[i] = b;
        }

        public void SetValue(int index, int value)
        {
            while (Number.Count <= index)
            {
                Number.Add(0);
            }
            Number[index] = value;
        }

        public static bool operator >(LongInt a, LongInt b) => Comparison(a, b, false) > 0;
        public static bool operator <(LongInt a, LongInt b) => Comparison(a, b, false) < 0;
        public static bool operator ==(LongInt a, LongInt b) => Comparison(a, b, false) == 0;
        public static bool operator !=(LongInt a, LongInt b) => Comparison(a, b, false) != 0;
        public static LongInt operator -(LongInt a) => new LongInt((a.GetInverseModulo(a.Sign), a.Number));

        public static LongInt operator +(LongInt first, LongInt second) => first.Sign == second.Sign
            ? Add(first, second)
            : Substract(first, second);
        public static LongInt operator -(LongInt first, LongInt second) => first + -second;

        private string GetStr()//Получение строкового представления числа
        {//Проверен тестами.
            string res = "";
            if (Sign == '-')
                res += '-';
            foreach (var num in Number)
                res += num.ToString();
            return res;
        }

        public static LongInt operator *(LongInt a, LongInt b) => Multiply(a, b);
        public static LongInt operator /(LongInt a, LongInt b) => Div(a, b).Item1;
        public static LongInt operator %(LongInt a, LongInt b) => Div(a, b).Item2;

        private char GetInverseModulo(char sign) => sign == '+' ? '-' : '+'; //Меняет знак числа

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
            first.Number.Reverse();
            second.Number.Reverse();
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
            var result = new LongInt((biggerLongInt.Sign, res));
            result.RemoveNulls();
            if (result[0] == 0)
                result.Sign = '+';
            return result;
        }

        private static LongInt Multiply(LongInt first, LongInt second)
        {
            first.Number.Reverse();
            second.Number.Reverse();
            var ten = 0;
            var result = new LongInt(('+', new List<int>()));
            for (var i = 0; i < second.GetSize(); i++)
            {
                var resDiscargeMult = new List<int>();
                for (var k = 0; k < i; k++) //Заполняем нулями пустые разряды
                    resDiscargeMult.Add(0);
                for (var j = 0; j < first.GetSize(); j++)
                {
                    var res = second[i] * first[j] + ten;
                    ten = 0;
                    if (res > 9)
                    {
                        ten = res / 10;
                        res %= 10;
                    }
                    resDiscargeMult.Add(res);
                }
                if (ten != 0)
                {
                    resDiscargeMult.Add(ten); // Добавляем последний разряд
                    ten = 0; //Обнуляем переменную десятков
                }
                resDiscargeMult.Reverse();
                result += new LongInt(('+', resDiscargeMult));
            }
            if (first.Sign == '-' && second.Sign == '+' || first.Sign == '+' && second.Sign == '-')
                result = -result;
            first.Number.Reverse();
            second.Number.Reverse();
            return result;
        }

        private void RemoveNulls()
        {
            var resCount = Number.Count;
            for (var i = 0; i < resCount - 1; i++)
            {
                if (Number[0] == 0) //Удаляем возможные не желательные 0
                    Number.RemoveAt(0);
                else
                    break;
            }
        }

        public static LongInt Exp(int val, int exp)
        {
            var bigInt = new LongInt(('+', new List<int>() { 0 }));
            bigInt.SetByte(exp, val);
            bigInt.RemoveNulls();
            return bigInt;
        }

        public static (LongInt, LongInt) Div(LongInt dividend, LongInt divider)
        {
            var startDividerSign = divider.Sign;
            divider.Sign = '+'; //нужно для корректного умножения в случае отрицательного знака
            var startDividendSign = dividend.Sign;
            divider.Sign = '+';

            if (Comparison(dividend, divider, true) == -1)
            {
                var wholeResSpecial = new LongInt(('+', new List<int>() { 0 }));
                var remainderResSpecial = dividend;
                divider.Sign = startDividerSign;
                dividend.Sign = startDividendSign;
                GetSignRes(wholeResSpecial, remainderResSpecial, dividend, divider);
                return (wholeResSpecial, remainderResSpecial);
            }
            if (Comparison(dividend, divider, true) == 0)
            {
                var wholeResSpecial = new LongInt(('+', new List<int>() { 1 }));
                var remainderResSpecial = new LongInt(('+', new List<int>() { 0 }));
                divider.Sign = startDividerSign;
                dividend.Sign = startDividendSign;
                GetSignRes(wholeResSpecial, remainderResSpecial, dividend, divider);
                return (wholeResSpecial, remainderResSpecial);
            }
            if (divider == new LongInt(('+', new List<int>() { 0 })))
                throw new Exception("Деление на 0");

            var pieceDividend = new LongInt(('+', new List<int>()));
            var wholeRes = new LongInt(('+', new List<int>()));
            var remainderRes = new LongInt(('+', new List<int>()));
            var oneDiscarge = new LongInt(('+', new List<int>() { 0 }));
            var i = 0;
            for (var j = 0; j < divider.GetSize(); j++)
            {
                pieceDividend.Number.Add(dividend[i]);
                i++;
            }
            if (Comparison(pieceDividend, divider, true) == -1)
            {
                pieceDividend.Number.Add(dividend[i]); // если часть числителя с равным кол-вом разрядов меньше, то добавляем ещё 1;
                i++;
            }

            do
            {
                oneDiscarge = GetOneDisc(pieceDividend, oneDiscarge, divider).Item1;
                var flagDischargeShift = true; //Костыль для корректного передвижения i- индекса отслеживаемого разряда делимого;
                wholeRes.Number.Add(oneDiscarge[0]);
                remainderRes = pieceDividend - oneDiscarge * divider;
                pieceDividend -= oneDiscarge * divider;
                oneDiscarge[0] = 0;
                if (pieceDividend[0] == 0)
                {
                    i = ShiftDigits(i, dividend, divider, pieceDividend, wholeRes);
                    if (i == dividend.GetSize() - 1)
                    {
                        var res = GetOneDisc(pieceDividend, oneDiscarge, divider);
                        oneDiscarge = res.Item1;
                        remainderRes = res.Item2;
                        wholeRes.Number.Add(oneDiscarge[0]);
                        divider.Sign = startDividerSign;
                        dividend.Sign = startDividendSign;
                        GetSignRes(wholeRes, remainderRes, dividend, divider);
                        return (wholeRes, remainderRes);
                    }
                }
                var iter = 0; // подсчёт сдвига разрядов;
                while (pieceDividend < divider)
                {
                    flagDischargeShift = false;
                    pieceDividend.Number.Add(dividend[i]);
                    if (iter >= 1)
                        wholeRes.Number.Add(0);
                    i++;
                    iter++;
                }
                if (flagDischargeShift)
                    i++;
            }
            while (i <= dividend.GetSize());
            divider.Sign = startDividerSign;
            dividend.Sign = startDividendSign;
            GetSignRes(wholeRes, remainderRes, dividend, divider);
            return (wholeRes, remainderRes);
        }

        private static int ShiftDigits(int i, LongInt dividend, LongInt divider, LongInt pieceDividend, LongInt wholeRes)
        {
            pieceDividend[0] = dividend[i];
            for (var j = i; j < dividend.GetSize(); j++)
            {
                if (pieceDividend < divider)
                {
                    i++;
                    if (pieceDividend[0] == 0)
                        pieceDividend[0] = dividend[i];
                    else
                        pieceDividend.Number.Add(dividend[i]);
                    wholeRes.Number.Add(0);
                }
                else
                    break;
            }
            return i;
        }

        private static (LongInt, LongInt) GetOneDisc(LongInt pieceDividend, LongInt oneDiscarge, LongInt divider)
        {
            var a = pieceDividend - oneDiscarge * divider;
            while (Comparison(a, divider, true) == 1 || Comparison(a, divider, true) == 0)
            {
                oneDiscarge += new LongInt(('+', new List<int>() { 1 }));
                a = pieceDividend - oneDiscarge * divider;
            }
            return (oneDiscarge, a);
        }

        private static void GetSignRes(LongInt wholeRes, LongInt remainderRes, LongInt dividend, LongInt divider)
        {
            if (dividend.Sign != divider.Sign)
            {
                if (wholeRes[0] != 0)
                    wholeRes.Sign = '-';
                if (remainderRes[0] != 0)
                    remainderRes.Sign = '-';
            }
        }

        private static BigInteger GetNODRecurs(BigInteger val1, BigInteger val2)
        {
            if (val2 == 1)
                return new BigInteger(1);
            else
                return (1 - GetNODRecurs(val2 % val1, val1) * val2) / val1 + val2;
        }

        public static LongInt Converter(LongInt val1, LongInt val2)
        {
            var val1Byte = val1.Number.Select<int, byte>(x => Convert.ToByte(x)).ToArray();
            var val2Byte = val2.Number.Select<int, byte>(x => Convert.ToByte(x)).ToArray();

            var bigIntval1 = new BigInteger(val1Byte);
            var bigIntval2 = new BigInteger(val2Byte);

            var num = GetNODRecurs(bigIntval1, bigIntval2);
            var sign = num.Sign;
            var res = num.ToString();
            var result = new LongInt(('+', new List<int>()));
            for(var i = 0; i < res.Length; i++)
                result.Number.Add(int.Parse(res[i].ToString()));
            if (sign < 0)
                result.Sign = '-';
            return result;
        }
    }
}
