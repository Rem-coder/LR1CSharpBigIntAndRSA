using NUnit.Framework;
using System.Linq;

namespace Vovk_A_A_LR1
{
    [TestFixture]
    public class GetStrTest
    {
        [TestCase('+', new int[] { 1, 2, 3, 4 }, TestName = "1234", ExpectedResult = "1234")]
        [TestCase('+', new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
            TestName = "999999999999999999999", ExpectedResult = "999999999999999999999")]
        [TestCase('-', new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
            TestName = "-999999999999999999999", ExpectedResult = "-999999999999999999999")]
        [TestCase('-', new int[] { 1, 0, 0 }, TestName = "-100", ExpectedResult = "-100")]
        [TestCase('+', new int[] { 0 }, TestName = "0", ExpectedResult = "0")]
        public string GetStrTests(char sign, int[] number)
        {
            var longInt = new LongInt((sign, number.ToList<int>()));
            return longInt.StrNumber;
        }
    }

    public class ComparisonTest
    {
        [TestCase('+', new int[] { 1, 2, 3 }, '+', new int[] { 1, 2, 3 }, TestName = "shortPlus", ExpectedResult = 0)]
        [TestCase('+', new int[] { 1 }, '+', new int[] { 1, 2, 3 }, TestName = "shortPlusSecondB", ExpectedResult = -1)]
        [TestCase('+', new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
            '+', new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, TestName = "longPlus", ExpectedResult = 1)]
        [TestCase('+', new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
            '+', new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 8, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
            TestName = "longPlus", ExpectedResult = 1)]
        [TestCase('-', new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
            '+', new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 8, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
            TestName = "longMinusPlus", ExpectedResult = -1)]
        [TestCase('-', new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
            '-', new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 8, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
            TestName = "longMinus", ExpectedResult = -1)]
        public int ComparsionTests(char firstSign, int[] firstNumber, char secondSign, int[] secondNumber)
        {
            var firstLongInt = new LongInt((firstSign, firstNumber.ToList<int>()));
            var secondLongInt = new LongInt((secondSign, secondNumber.ToList<int>()));
            return LongInt.Comparison(firstLongInt, secondLongInt, false);
        }
    }

    public class OperationTest
    {
        //B==big
        //Сравнение идёт по модулю чисел, а не по знаку.
        public class MoreTest
        {
            [TestCase('+', new int[] { 1, 2, 3 }, '+', new int[] { 2, 0 }, TestName = "shortPlus", ExpectedResult = true)]
            [TestCase('-', new int[] { 1, 2, 3 }, '-', new int[] { 2, 0 }, TestName = "shortMinus", ExpectedResult = false)]
            [TestCase('+', new int[] { 1, 2, 3 }, '-', new int[] { 2, 0 }, TestName = "shortPlusMinus", ExpectedResult = true)]
            [TestCase('-', new int[] { 1, 2, 3 }, '+', new int[] { 2, 0 }, TestName = "shortMinusPlus", ExpectedResult = false)]
            [TestCase('+', new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 9, 9, 9, 9, 9, 9 },
                '+', new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
                TestName = "longPlus", ExpectedResult = false)]
            [TestCase('-', new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 9, 9, 9, 9, 9, 9 },
                '-', new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
                TestName = "longMinus", ExpectedResult = true)]
            [TestCase('+', new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 9, 9, 9, 9, 9, 9 },
                '-', new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
                TestName = "longPlusMinus", ExpectedResult = true)]
            [TestCase('-', new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 9, 9, 9, 9, 9, 9 },
                '+', new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
                TestName = "longMinusPlus", ExpectedResult = false)]
            public bool MoreTests(char firstSign, int[] firstNumber, char secondSign, int[] secondNumber)
            {
                var firstLongInt = new LongInt((firstSign, firstNumber.ToList<int>()));
                var secondLongInt = new LongInt((secondSign, secondNumber.ToList<int>()));
                return firstLongInt > secondLongInt;
            }
        }

        public class LessTest
        {
            [TestCase('+', new int[] { 1, 2, 3 }, '+', new int[] { 2, 0 }, TestName = "shortPlus", ExpectedResult = false)]
            [TestCase('-', new int[] { 1, 2, 3 }, '-', new int[] { 2, 0 }, TestName = "shortMinus", ExpectedResult = true)]
            [TestCase('+', new int[] { 1, 2, 3 }, '-', new int[] { 2, 0 }, TestName = "shortPlusMinus", ExpectedResult = false)]
            [TestCase('-', new int[] { 1, 2, 3 }, '+', new int[] { 2, 0 }, TestName = "shortMinusPlus", ExpectedResult = true)]
            [TestCase('+', new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 9, 9, 9, 9, 9, 9 },
                '+', new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
                TestName = "longPlus", ExpectedResult = true)]
            [TestCase('-', new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 9, 9, 9, 9, 9, 9 },
                '-', new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
                TestName = "longMinus", ExpectedResult = false)]
            [TestCase('+', new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 9, 9, 9, 9, 9, 9 },
                '-', new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
                TestName = "longPlusMinus", ExpectedResult = false)]
            [TestCase('-', new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
                '+', new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
                TestName = "longMinusPlus", ExpectedResult = true)]
            public bool LessTests(char firstSign, int[] firstNumber, char secondSign, int[] secondNumber)
            {
                var firstLongInt = new LongInt((firstSign, firstNumber.ToList<int>()));
                var secondLongInt = new LongInt((secondSign, secondNumber.ToList<int>()));
                return firstLongInt < secondLongInt;
            }
        }

        public class EquallyTest
        {
            [TestCase('+', new int[] { 1, 2, 3 }, '+', new int[] { 2, 0 }, TestName = "shortPlus", ExpectedResult = false)]
            [TestCase('-', new int[] { 1, 2, 3 }, '-', new int[] { 2, 0 }, TestName = "shortMinus", ExpectedResult = false)]
            [TestCase('-', new int[] { 1, 2, 3 }, '-', new int[] { 1, 2, 3 }, TestName = "shortMinusEqual", ExpectedResult = true)]
            [TestCase('+', new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 9, 9, 9, 9, 9, 9 },
                '+', new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 }, TestName = "longPlus", ExpectedResult = false)]
            [TestCase('-', new int[] { 1, 2, 3 }, '-', new int[] { 1, 2, 4 }, TestName = "shortMinusSameLength", ExpectedResult = false)]
            public bool EquallyTests(char firstSign, int[] firstNumber, char secondSign, int[] secondNumber)
            {
                var firstLongInt = new LongInt((firstSign, firstNumber.ToList<int>()));
                var secondLongInt = new LongInt((secondSign, secondNumber.ToList<int>()));
                return firstLongInt == secondLongInt;
            }
        }

        public class UnEquallyTest
        {
            [TestCase('+', new int[] { 1, 2, 3 }, '+', new int[] { 2, 0 }, TestName = "shortPlusFirstB", ExpectedResult = true)]
            [TestCase('-', new int[] { 1, 2, 3 }, '-', new int[] { 2, 0 }, TestName = "shortMinus", ExpectedResult = true)]
            [TestCase('-', new int[] { 1, 2, 3 }, '-', new int[] { 1, 2, 3 }, TestName = "equal", ExpectedResult = false)]
            [TestCase('+', new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 9, 9, 9, 9, 9, 9 },
                '+', new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 }, TestName = "longPlus", ExpectedResult = true)]
            [TestCase('-', new int[] { 1, 2, 3 }, '-', new int[] { 1, 2, 3 }, TestName = "longMinus", ExpectedResult = false)]
            public bool UnEquallyTests(char firstSign, int[] firstNumber, char secondSign, int[] secondNumber)
            {
                var firstLongInt = new LongInt((firstSign, firstNumber.ToList<int>()));
                var secondLongInt = new LongInt((secondSign, secondNumber.ToList<int>()));
                return firstLongInt != secondLongInt;
            }
        }

        public class PlusTest
        {
            [TestCase('+', new int[] { 1, 0, 0 }, '+', new int[] { 1, 0, 0 }, new int[] { 2, 0, 0 }, '+',
            TestName = "shortPlusPlusEqual")]
            [TestCase('+', new int[] { 1, 0, 0, 0, 5 }, '+', new int[] { 1, 0, 0 }, new int[] { 1, 0, 1, 0, 5 }, '+',
            TestName = "shortPlusPlusFirstB")]
            [TestCase('+', new int[] { 1, 0, 5 }, '+', new int[] { 1, 0, 0, 0, 5 }, new int[] { 1, 0, 1, 1, 0 }, '+',
            TestName = "shortPlusPlusSecondB")]
            [TestCase('+', new int[] { 1, 0, 0, 1, 0, 9 }, '-', new int[] { 1, 0, 0, 1, 0, 9 }, new int[] { 0 }, '-',
            TestName = "shortPlusMinusEqual")]
            [TestCase('-', new int[] { 1, 0, 0, 1, 0, 9 }, '+', new int[] { 1, 0, 0, 1, 0, 9 }, new int[] { 0 }, '+',
            TestName = "shortMinusPlusEqual")]
            [TestCase('+', new int[] { 1, 0, 0, 1, 0, 9 }, '-', new int[] { 1, 0, 0 }, new int[] { 1, 0, 0, 0, 0, 9 }, '+',
            TestName = "shortPlusMinusFirstB")]
            [TestCase('+', new int[] { 1, 0, 0 }, '-', new int[] { 9, 9, 9 }, new int[] { 8, 9, 9 }, '-',
            TestName = "shortPlusMinusSecondB")]
            [TestCase('-', new int[] { 7, 7, 7 }, '+', new int[] { 2, 0, 2 }, new int[] { 5, 7, 5 }, '-',
            TestName = "shortMinusPlusFirstB")]
            [TestCase('-', new int[] { 2, 2, 2 }, '+', new int[] { 7, 6, 7 }, new int[] { 5, 4, 5 }, '+',
            TestName = "shortMinusPlusSecondB")]
            [TestCase('-', new int[] { 7, 7, 7 }, '-', new int[] { 7, 7, 7 }, new int[] { 1, 5, 5, 4 }, '-',
            TestName = "shortMinusMinusEqual")]
            [TestCase('-', new int[] { 7, 0, 0 }, '-', new int[] { 7 }, new int[] { 7, 0, 7, }, '-',
            TestName = "shortMinusMinusFirstB")]
            [TestCase('-', new int[] { 2 }, '-', new int[] { 9, 8, 7, 6 }, new int[] { 9, 8, 7, 8 }, '-',
            TestName = "shortMinusMinusSecondB")]
            //А вот ту начинается веселье
            [TestCase('+', new int[] { 1, 3, 6, 5, 7, 3, 1, 4, 8, 5, 1, 3, 7, 4, 8, 9, 5, 1, 3, 4, 1, 5, 9, 3, 8, 5, 9, 1, 3, 5, 1, 3, 9, 5, 1, 3, 8, 5, 1, 3, 4, 1, 3, 6, 8, 7 },
                '+', new int[] { 1, 3, 6, 5, 7, 3, 1, 4, 8, 5, 1, 3, 7, 4, 8, 9, 5, 1, 3, 4, 1, 5, 9, 3, 8, 5, 9, 1, 3, 5, 1, 3, 9, 5, 1, 3, 8, 5, 1, 3, 4, 1, 3, 6, 8, 7 },
                new int[] { 2, 7, 3, 1, 4, 6, 2, 9, 7, 0, 2, 7, 4, 9, 7, 9, 0, 2, 6, 8, 3, 1, 8, 7, 7, 1, 8, 2, 7, 0, 2, 7, 9, 0, 2, 7, 7, 0, 2, 6, 8, 2, 7, 3, 7, 4 }, '+',
            TestName = "longtPlusPlusEqual")]
            [TestCase('+', new int[] { 1, 3, 6, 5, 7, 3, 1, 4, 8, 5, 1, 3, 7, 4, 8, 9, 5, 1, 3, 4, 1, 5, 9, 3, 8, 5, 9, 1, 3, 5, 1, 3, 9, 5, 1, 3, 8, 5, 1, 3, 4, 1, 3, 6, 8, 7, },
                '+', new int[] { 5, 1, 8, 3, 4, 7, 5, 1, 0, 3, 9, 5, 0, 1, 3, 9, 6, 5, 9, 1, 3, 6, 5, 7, 1, 8, 3, 4, 1, 3, 7, 4, 1, 8, 4 },
                new int[] { 1, 3, 6, 5, 7, 3, 1, 4, 8, 5, 1, 8, 9, 3, 2, 4, 2, 6, 4, 4, 5, 5, 4, 3, 9, 9, 8, 7, 9, 4, 2, 7, 6, 0, 8, 5, 6, 8, 5, 4, 7, 8, 7, 8, 7, 1 }, '+',
            TestName = "longPlusPlusFirstB")]
            [TestCase('+', new int[] { 1, 2, 3, 4, 1, 0, 3, 5, 0, 1, 3, 0, 5, 0, 1, 3, 5, 0, 1, 3, 9, 5, 3, 1, 0, 7, 4, 5, 1, 3, 7, 5, 0, 1, 3, 7, 5, 1, 0, 3, 5, 7, 0, 3 },
                '+', new int[] { 1, 0, 8, 9, 4, 3, 1, 2, 4, 0, 3, 1, 2, 0, 8, 3, 2, 4, 0, 1, 4, 3, 0, 2, 0, 1, 4, 2, 3, 5, 3, 2, 6, 5, 8, 1, 3, 4, 9, 6, 5, 7, 1, 3, 4, 5, 7, 1, 8, 5, 9, 1, 8, 3, 6, 5, 6, 1, 3, 9, 8, 5, 1, 3, 5, 6 },
                new int[] { 1, 0, 8, 9, 4, 3, 1, 2, 4, 0, 3, 1, 2, 0, 8, 3, 2, 4, 0, 1, 4, 3, 1, 4, 3, 5, 5, 2, 7, 0, 3, 3, 9, 6, 3, 1, 4, 8, 4, 6, 7, 1, 0, 8, 7, 6, 7, 9, 3, 1, 0, 5, 5, 8, 6, 6, 9, 8, 9, 0, 8, 8, 7, 0, 5, 9 }, '+',
            TestName = "longPlusPlusSecondB")]
            [TestCase('+', new int[] { 3, 6, 5, 4, 6, 4, 3, 6, 0, 3, 5, 9, 1, 3, 0, 5, 9, 3, 1, 4, 5, 7, 1, 3, 5, 0, 9, 1, 3, 8, 5, 0, 1, 3, 5, 7, 0, 1, 3, 4, 5, 1, 3, 4, 5, 7, 1, 3, 0, 5, 1, 3, 4, 7, 5, 0, 1, 3, 5, 7, 0, 1, 3, 4, 6, 0, 1, 7 },
                '-', new int[] { 3, 6, 5, 4, 6, 4, 3, 6, 0, 3, 5, 9, 1, 3, 0, 5, 9, 3, 1, 4, 5, 7, 1, 3, 5, 0, 9, 1, 3, 8, 5, 0, 1, 3, 5, 7, 0, 1, 3, 4, 5, 1, 3, 4, 5, 7, 1, 3, 0, 5, 1, 3, 4, 7, 5, 0, 1, 3, 5, 7, 0, 1, 3, 4, 6, 0, 1, 7 },
                new int[] { 0 }, '-',
            TestName = "longPlusMinusEqual")]
            [TestCase('-', new int[] { 5, 7, 5, 3, 7, 0, 3, 5, 9, 7, 5, 3, 6, 7, 0, 3, 5, 6, 9, 7, 3, 5, 0, 8, 7, 0, 3, 5, 8, 6, 0, 7, 8, 3, 5, 0, 6, 7, 3, 0, 5, 7, 8, 0, 3, 5, 6, 7, 8, 9, 0, 3, 5, 8, 7, 0, 3, 5, 8, 6, 9, 0, 7, 3, 5, 8, 0, 6, 7, 8, 3, 5, 0, 8, 7, 3, 5, 8, 7, 3, 0 },
                '+', new int[] { 5, 7, 5, 3, 7, 0, 3, 5, 9, 7, 5, 3, 6, 7, 0, 3, 5, 6, 9, 7, 3, 5, 0, 8, 7, 0, 3, 5, 8, 6, 0, 7, 8, 3, 5, 0, 6, 7, 3, 0, 5, 7, 8, 0, 3, 5, 6, 7, 8, 9, 0, 3, 5, 8, 7, 0, 3, 5, 8, 6, 9, 0, 7, 3, 5, 8, 0, 6, 7, 8, 3, 5, 0, 8, 7, 3, 5, 8, 7, 3, 0 },
                new int[] { 0 }, '+',
            TestName = "longMinusPlusEqual")]
            [TestCase('+', new int[] { 5, 6, 7, 3, 4, 6, 2, 9, 8, 6, 7, 9, 2, 7, 6, 6, 0, 2, 8, 5, 3, 4, 0, 5, 2, 3, 5, 2, 3, 5, 3, 2, 9, 4, 7, 5, 8, 2, 4, 3, 7, 5, 0, 2, 8, 9, 5, 3, 2, 0, 9, 5, 7, 3, 2, 0, 9, 4, 5, 8, 3, 2, 7, 5, 0, 9, 3, 2, 8, 5, 7, 2, 3, 0, 5, 7, 3, 2, 7, 5, 8, 3, 2, 7, 5 },
                '-', new int[] { 1, 7, 8, 4, 5, 1, 3, 5, 1, 3, 4, 1, 3, 5, 0, 3, 1, 4, 5, 8, 3, 5, 0, 5, 8, 1, 4, 5, 3, 1, 5, 8, 3, 0, 4, 5, 1, 3, 0, 7, 4, 5, 0, 1, 7, 3, 5, 0, 1, 3, 7, 5, 1, 3, 7, 5, 7, 1, 3, 5, 0, 7, 3, 1, 5, 6, 9 },
                new int[] { 5, 6, 7, 3, 4, 6, 2, 9, 8, 6, 7, 9, 2, 7, 6, 6, 0, 2, 6, 7, 4, 9, 5, 3, 8, 8, 3, 8, 9, 3, 9, 7, 9, 1, 6, 1, 2, 4, 0, 8, 6, 9, 2, 1, 4, 4, 2, 1, 6, 2, 6, 5, 2, 8, 0, 7, 8, 7, 1, 3, 3, 1, 0, 1, 5, 9, 1, 9, 1, 0, 5, 8, 5, 4, 8, 5, 9, 7, 6, 8, 5, 1, 7, 0, 6, }, '+',
            TestName = "longPlusMinusFirstB")]
            [TestCase('+', new int[] { 4, 6, 3, 4, 6, 3, 4, 6, 4, 9, 0, 6, 0, 8, 4, 3, 6, 0, 4, 3, 6, 8, 0, 4, 6, 4, 3, 8, 6, 4, 3, 8, 6, 2, 9, 4, 5, 0, 1, 2, 5, 8, 1, 3, 4, 6, 4, 3, 7, 1, 6, 4, 2, 0, 6, 2, 4, 7, 8, 6 },
                '-', new int[] { 1, 3, 7, 4, 5, 1, 9, 3, 8, 4, 5, 1, 5, 8, 7, 8, 7, 0, 1, 4, 3, 0, 5, 1, 3, 0, 5, 1, 3, 4, 7, 5, 1, 3, 9, 4, 5, 8, 7, 1, 3, 4, 5, 9, 1, 7, 3, 4, 9, 5, 1, 3, 5, 1, 3, 9, 5, 1, 3, 4, 5, 1, 3, 4, 5, 1, 3, 4, 1, 3, 5, 9, 9, 1, 5, 1, 3, 8, 7, 5 },
                new int[] { 1, 3, 7, 4, 5, 1, 9, 3, 8, 4, 5, 1, 5, 8, 7, 8, 7, 0, 1, 3, 8, 4, 1, 6, 6, 7, 0, 4, 8, 5, 6, 9, 0, 5, 5, 0, 9, 8, 2, 7, 6, 6, 5, 4, 5, 2, 9, 6, 3, 0, 7, 4, 8, 8, 4, 5, 0, 1, 2, 1, 9, 3, 2, 1, 0, 4, 9, 0, 4, 1, 9, 5, 7, 0, 8, 8, 9, 0, 8, 9, }, '-',
            TestName = "longPlusMinusSecondB")]
            [TestCase('-', new int[] { 5, 2, 1, 3, 4, 6, 3, 5, 0, 8, 9, 5, 4, 0, 3, 5, 1, 0, 3, 9, 4, 5, 0, 1, 3, 7, 4, 5, 9, 1, 3, 5, 7, 1, 3, 0, 5, 9, 3, 1, 5, 7, 1, 3, 5, 7, 0, 1, 3, 5, 3, 1, 8, 5, 7, 1, 3, 5, 7, 1, 3, 7, 5, 0, 3, 5, 1, 3, 5, 8, 1, 3 },
                '+', new int[] { 1, 9, 7, 8, 5, 3, 1, 4, 5, 3, 1, 4, 5, 1, 0, 3, 1, 7, 5, 9, 3, 4, 5, 8, 9, 1, 3, 5, 1, 3, 4, 5, 9, 1, 3, 5, 7, 3, 4, 5, 1, 7, 3, 4, 5, 9, 8, 1, 3, 7, 5, 6, 1, 9, 9, 5, 1, 6, },
                new int[] { 5, 2, 1, 3, 4, 6, 3, 5, 0, 8, 9, 5, 4, 0, 1, 5, 3, 1, 8, 6, 3, 0, 4, 8, 2, 2, 9, 4, 8, 8, 1, 8, 1, 1, 9, 6, 0, 0, 4, 0, 2, 2, 0, 0, 1, 1, 0, 9, 9, 9, 5, 8, 4, 0, 5, 4, 0, 1, 1, 1, 5, 6, 1, 2, 7, 8, 9, 3, 6, 2, 9, 7 }, '-',
            TestName = "longMinusPlusFirstB")]
            [TestCase('-', new int[] { 5, 8, 9, 1, 5, 8, 1, 8, 5, 9, 3, 6, 4, 5, 1, 3, 7, 5, 9, 7, 3, 0, 7, 7, 5, 0, 0, 1, 0, 5, 7, 8, 3, 0, 7, 5, 3, 7, 5, 1, 3, 8, 7, 4, 5, 1, 3, 7, 5, 7, 3, 5, 8, 1, 0, 7 },
                '+', new int[] { 1, 8, 0, 5, 1, 0, 8, 3, 4, 9, 5, 1, 3, 9, 0, 5, 7, 3, 9, 1, 8, 7, 5, 9, 3, 1, 7, 5, 1, 3, 4, 5, 7, 0, 3, 5, 1, 3, 7, 8, 5, 0, 3, 1, 4, 5, 3, 1, 7, 5, 8, 7, 1, 3, 9, 5, 7, 1, 3, 5, 7, 1, 3, 5, 7, 8, 0, 7, 3, 0, 1, 5, 8 },
                new int[] { 1, 8, 0, 5, 1, 0, 8, 3, 4, 9, 5, 1, 3, 9, 0, 5, 6, 8, 0, 2, 7, 1, 7, 7, 4, 5, 8, 1, 4, 8, 9, 4, 3, 2, 7, 5, 4, 0, 7, 0, 7, 5, 3, 1, 3, 4, 7, 3, 9, 2, 7, 9, 6, 0, 2, 0, 5, 7, 4, 8, 2, 6, 2, 2, 0, 2, 3, 3, 7, 2, 0, 5, 1 }, '+',
            TestName = "longMinusPlusSecondB")]
            [TestCase('-', new int[] { 5, 7, 8, 3, 5, 3, 6, 5, 7, 8, 9, 1, 9, 3, 4, 1, 3, 5, 1, 3, 5, 1, 3, 4, 5, 9, 1, 3, 5, 7, 1, 5, 1, 3, 5, 1, 3, 6, 4, 5, 7, 8, 5, 4, 8 }, '-',
                new int[] { 5, 7, 8, 3, 5, 3, 6, 5, 7, 8, 9, 1, 9, 3, 4, 1, 3, 5, 1, 3, 5, 1, 3, 4, 5, 9, 1, 3, 5, 7, 1, 5, 1, 3, 5, 1, 3, 6, 4, 5, 7, 8, 5, 4, 8 },
                new int[] { 1, 1, 5, 6, 7, 0, 7, 3, 1, 5, 7, 8, 3, 8, 6, 8, 2, 7, 0, 2, 7, 0, 2, 6, 9, 1, 8, 2, 7, 1, 4, 3, 0, 2, 7, 0, 2, 7, 2, 9, 1, 5, 7, 0, 9, 6 }, '-',
            TestName = "longMinusMinusEqual")]
            [TestCase('-', new int[] { 3, 6, 8, 6, 9, 7, 4, 0, 7, 8, 9, 3, 7, 0, 2, 4, 8, 9, 5, 6, 0, 2, 9, 4, 8, 6, 0, 2, 4, 5, 9, 2, 4, 7, 5, 9, 0, 2, 4, 6, 2, 4, 6, 8, 2, 4, 0, 6 },
                '-', new int[] { 1, 3, 7, 4, 1, 3, 4, 5, 1, 3, 9, 5, 1, 3, 9, 5, 3, 7, 6, 2, 7, 4, 5, 1, 4, 5, 6, 1, 9, 3, 8, 7, 4, 5, 6, 7 },
                new int[] { 3, 6, 8, 6, 9, 7, 4, 0, 7, 8, 9, 3, 8, 3, 9, 9, 0, 3, 0, 1, 1, 6, 8, 9, 9, 9, 9, 7, 8, 3, 5, 5, 2, 2, 1, 0, 4, 8, 0, 8, 1, 8, 5, 5, 6, 9, 7, 3 }, '-',
            TestName = "longMinusMinusFirstB")]
            // и контрольный в голову
            [TestCase('-', new int[] { 5, 6, 2, 7, 8, 5, 9, 3, 3, 1, 5, 1, 3, 6, 3, 6, 2, 5, 3, 4, 5, 8, 3, 5, 3, 9, 6, 6, 4, 2, 6, 2, 4, 8, 6, 7, 8, 7, 6, 7, 6, 8, 6, 7, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 5, 8, 5, 9, 6, 6, 4, 0, 0, 0, 0, 0, 0, 0, 4, 6, 8, 1, 7, 5, 1, 3, 4, 5, 0, 7, 3, 7, 3, 1, 4, 5, 1, 3, 5, 7, 1, 3, 0 },
                '-', new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
                new int[] { 1, 0, 5, 6, 2, 7, 8, 5, 9, 3, 3, 1, 5, 1, 3, 6, 3, 6, 2, 5, 3, 4, 5, 8, 3, 5, 3, 9, 6, 6, 4, 2, 6, 2, 4, 8, 6, 7, 8, 7, 6, 7, 6, 8, 6, 7, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 5, 8, 5, 9, 6, 6, 4, 0, 0, 0, 0, 0, 0, 0, 4, 6, 8, 1, 7, 5, 1, 3, 4, 5, 0, 7, 3, 7, 3, 1, 4, 5, 1, 3, 5, 7, 1, 2, 9 }, '-',
            TestName = "longMinusMinusSecondB")]
            public void PlusTests(char firstSign, int[] firstNumber, char secondSign, int[] secondNumber, int[] res, char signRes)
            {
                var resultAdd = new LongInt((firstSign, firstNumber.ToList<int>())) + new LongInt((secondSign, secondNumber.ToList<int>()));
                for (var i = 0; i < res.Length; i++)
                    Assert.AreEqual(res[i], resultAdd[i]);
                Assert.AreEqual(signRes, resultAdd.Sign);
            }
        }

        public class MinusTest
        {

        }
    }

    public class IndexTest
    {
        [TestCase('+', new int[] { 1, 2, 3 }, 0, TestName = "zeroIndexInShort", ExpectedResult = 1)]
        [TestCase('+', new int[] { 1, 2, 3, 4 }, 3, TestName = "thirdIndexInShort", ExpectedResult = 4)]
        [TestCase('+', new int[] { 1, 2, 3 }, 6, TestName = "outOfRangeInShort", ExpectedResult = 0)]
        [TestCase('+', new int[] { 1, 2, 3, 3, 4, 5, 6, 7, 8, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
            0, TestName = "zeroIndexInLong", ExpectedResult = 1)]
        [TestCase('+', new int[] { 1, 2, 3, 3, 4, 5, 6, 7, 8, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
            20, TestName = "twentyIndexInLong", ExpectedResult = 9)]
        [TestCase('+', new int[] { 1, 2, 3, 3, 4, 5, 6, 7, 8, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
            120, TestName = "outOfRangeInLong", ExpectedResult = 0)]
        public int IndexTests(char firstSign, int[] firstNumber, int index)
        {
            var longInt = new LongInt((firstSign, firstNumber.ToList<int>()));
            return longInt[index];
        }
    }

    public class AddTest
    {
        [TestCase('+', new int[] { 9, 2, 3, 4, 5 }, '+', new int[] { 3, 7, 8, 9, 1 }, new int[] { 1, 3, 0, 2, 3, 6 }, '+',
            TestName = "shortPlusPlusSameLen")]
        [TestCase('+', new int[] { 9, 2, 3, 4, 5, 9, 1 }, '+', new int[] { 1, 2, 3, 4, 5, 6 }, new int[] { 9, 3, 5, 8, 0, 4, 7 }, '+',
            TestName = "shortPlusPlusFirstB")]
        [TestCase('+', new int[] { 1, 2, 3, 4, 5, 6 }, '+', new int[] { 9, 2, 3, 4, 5, 9, 1 }, new int[] { 9, 3, 5, 8, 0, 4, 7 }, '+',
            TestName = "shortPlusPlusSecondB")]
        [TestCase('+', new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
            '+', new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
            new int[] { 1, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 8 }, '+',
            TestName = "longPlusPlusSameLen")]
        [TestCase('+', new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
            '+', new int[] { 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8 },
            new int[] { 1, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 7 }, '+',
            TestName = "longPlusPlusFirstB")]
        [TestCase('+', new int[] { 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8 },
            '+', new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
            new int[] { 1, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 7 }, '+',
            TestName = "longPlusPlusSecondB")]
        [TestCase('-', new int[] { 9, 2, 3, 4, 5 }, '-', new int[] { 3, 7, 8, 9, 1 }, new int[] { 1, 3, 0, 2, 3, 6 }, '-',
            TestName = "shortMinuMinusSameLen")]
        [TestCase('-', new int[] { 9, 2, 3, 4, 5, 9, 1 }, '-', new int[] { 1, 2, 3, 4, 5, 6 }, new int[] { 9, 3, 5, 8, 0, 4, 7 }, '-',
            TestName = "shortMinuMinusFirstB")]
        [TestCase('-', new int[] { 1, 2, 3, 4, 5, 6 }, '-', new int[] { 9, 2, 3, 4, 5, 9, 1 }, new int[] { 9, 3, 5, 8, 0, 4, 7 }, '-',
            TestName = "shortMinuMinusSecondB")]
        [TestCase('-', new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
            '-', new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
            new int[] { 1, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 8 }, '-',
            TestName = "longMinuMinusSameLen")]
        [TestCase('-', new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
            '-', new int[] { 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8 },
            new int[] { 1, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 7 }, '-',
            TestName = "longMinuMinusFirstB")]
        [TestCase('-', new int[] { 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8 },
            '-', new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
            new int[] { 1, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 7 }, '-',
            TestName = "longMinuMinusSecondB")]
        public void AddTests(char firstSign, int[] firstNumber, char secondSign, int[] secondNumber, int[] res, char sign)
        {
            var resultAdd = LongInt.Add(new LongInt((firstSign, firstNumber.ToList<int>())),
                new LongInt((secondSign, secondNumber.ToList<int>())));
            for (var i = 0; i < res.Length; i++)
                Assert.AreEqual(res[i], resultAdd[i]);
            Assert.AreEqual(sign, resultAdd.Sign);
        }
    }

    public class SubstractTest
    {
        [TestCase('-', new int[] { 9, 2, 3, 4, 5 }, '+', new int[] { 3, 7, 8, 9, 1 }, new int[] { 5, 4, 4, 5, 4 }, '-',
            TestName = "shortMinusPlusSameLen")]
        [TestCase('+', new int[] { 9, 2, 3, 4, 5 }, '-', new int[] { 3, 7, 8, 9, 1 }, new int[] { 5, 4, 4, 5, 4 }, '+',
            TestName = "shortPlusMinusSameLen")]
        [TestCase('+', new int[] { 2, 3, 4, 5, 6, 7 }, '-', new int[] { 1, 0, 0, 0, 3, 0, 2, 1, 3 }, new int[] { 9, 9, 7, 9, 5, 6, 4, 6 }, '-',
            TestName = "shortPlusMinusSecondB")]
        [TestCase('+', new int[] { 0 }, '-', new int[] { 0 }, new int[] { 0 }, '-',
            TestName = "zero")]
        [TestCase('+', new int[] { 1, 0, 0, 0, 0, 0, 0, 0 }, '-', new int[] { 1, 0, 0, 0, 0, 0, 0, 0 }, new int[] { 0 }, '-',
            TestName = "shortEqualValues")]
        [TestCase('-', new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            '+', new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
            new int[] { 9, 9, 9, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 }, '-',
            TestName = "longMinusPlusFirstB")]
        [TestCase('+', new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            '-', new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
            new int[] { 9, 9, 9, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 }, '+',
            TestName = "longPlusMinusFirstB")]
        [TestCase('+', new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
            '-', new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            new int[] { 9, 9, 9, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 }, '-',
            TestName = "longPlusMinusSecondB")]
        [TestCase('-', new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
            '+', new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            new int[] { 9, 9, 9, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 }, '+',
            TestName = "longMinusPlusSecondB")]

        public void SubstractTests(char firstSign, int[] firstNumber, char secondSign, int[] secondNumber, int[] res, char signRes)
        {
            var resultAdd = LongInt.Substract(new LongInt((firstSign, firstNumber.ToList<int>())),
                new LongInt((secondSign, secondNumber.ToList<int>())));
            for (var i = 0; i < res.Length; i++)
                Assert.AreEqual(res[i], resultAdd[i]);
            Assert.AreEqual(signRes, resultAdd.Sign);
        }
    }
}