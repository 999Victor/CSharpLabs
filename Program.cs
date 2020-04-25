using static System.Console;
using System;
//using System.Object;

namespace Lab7
{
    class Program
    {
        static void Main(string[] args)
        {
            RationalNum[] rationNumbers;

            string numberQuantityStr;
            string numToCompStr;

            int firstNumToComp = 0;
            int secondNumToComp = 0;

            //задание количества рационалных чисел
            WriteLine("How many numbers do you whant to create?");
            numberQuantityStr = ReadLine();
            int.TryParse((( (string.IsNullOrEmpty(numberQuantityStr)) || (int.Parse(numberQuantityStr) <= 0) ) ? 
                "0" : numberQuantityStr), out int numbersQuantity);

            if(numbersQuantity <= 0)
            {
                Write("wrong number entered");
                return;
            }

            rationNumbers = new RationalNum[numbersQuantity];

            //заполнение массива рац. чисел
            for (int i = 0; i < rationNumbers.Length; i++)
            {
                rationNumbers[i] = new RationalNum();

                string entringNumStr;

                for (; ;)
                {
                    //вводим проверяем числитель
                    WriteLine("Enter numerator please");
                    entringNumStr = ReadLine();
                    if ((string.IsNullOrEmpty(entringNumStr)) || (int.Parse(entringNumStr) < 0))
                    {
                        WriteLine("Wrong value was entered, try again please!");
                    }
                    else
                    {
                        //rationNumbers[i] = new RationalNum() { FirstNum = 23 };

                       
                        rationNumbers[i].FirstNum = int.Parse(entringNumStr);
                        break;
                    }
                }

                for (; ;)
                { 
                    //знаменатель
                    WriteLine("Enter denumerator please");
                    entringNumStr = ReadLine();
                    if ((string.IsNullOrEmpty(entringNumStr)) || (int.Parse(entringNumStr) < 0))
                    {
                        WriteLine("Wrong value was entered, try again please!");
                    }
                    else
                    {
                        rationNumbers[i].SecondNum = int.Parse(entringNumStr);
                        break;
                    }
                }
            }

            //--------------------------------------------------------------------------------
            //вывод всех чисел
            for (int i = 0; i < rationNumbers.Length; i++)
            {
                Write($"{i + 1})");

                //вывод как рационального
                WriteLine(rationNumbers[i].ToStringConverter("Rational"));

                //вывод как вещественного
                Write(rationNumbers[i].ToStringConverter("Real"));

                WriteLine("");
            }

            //--------------------------------------------------------------------------------
            //создание объекта по вводимой строке
            RationalNum ratNum = new RationalNum();
            ratNum.ObjectCreator("0,25");
            WriteLine(ratNum.ToStringConverter("Rational"));

            //--------------------------------------------------------------------------------
            //ввод номеров чисел для сравнения
            WriteLine("What numbers you whant to compare? Enter its count numbersat array");

            for (; ;)
            {
                numToCompStr = ReadLine();
                if ((string.IsNullOrEmpty(numToCompStr)) || (int.Parse(numToCompStr) < 0)
                    || int.Parse(numToCompStr) > rationNumbers.Length)
                {
                    WriteLine("Wrong value was entered, try again please!");
                }
                else
                {
                    firstNumToComp = int.Parse(numToCompStr);
                }

                numToCompStr = ReadLine();
                if ((string.IsNullOrEmpty(numToCompStr)) || (int.Parse(numToCompStr) < 0)
                    || int.Parse(numToCompStr) > rationNumbers.Length)
                {
                    WriteLine("Wrong value was entered, try again please!");
                }
                else
                {
                    secondNumToComp = int.Parse(numToCompStr);
                    break;
                }
            }

            
            //--------------------------------------------------------------------------------
            //сам компаратор
            Comparer objComp = new Comparer();
            if (objComp.Compare(rationNumbers[firstNumToComp], rationNumbers[secondNumToComp]) == 1)
            {
                WriteLine("These numbers are equal!");
            }
            else
            {
                WriteLine("The bigger number is: " + 
                objComp.Compare(rationNumbers[firstNumToComp], rationNumbers[secondNumToComp]).ToString());
            }

            RationalNum summNumber = rationNumbers[0] + rationNumbers[1];
            WriteLine($"Result {summNumber.FirstNum} /{summNumber.SecondNum}");
        }
    }
}