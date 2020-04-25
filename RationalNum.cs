using static System.Console;
using System.Text;
using System;

namespace Lab7
{
    class RationalNum
    {
        public int FirstNum { get; set; }
        public int SecondNum { get; set; }

        //реализация перекрытия
        public static RationalNum operator +(RationalNum a, RationalNum b)
        {
            if (a != null && b != null)
            {
                if (a.SecondNum == b.SecondNum)
                {
                    return new RationalNum { FirstNum = a.FirstNum + b.FirstNum, SecondNum = a.SecondNum };
                }
                else
                {
                    return new RationalNum { FirstNum = a.FirstNum * b.SecondNum + b.FirstNum * a.SecondNum,
                        SecondNum = a.SecondNum * b.SecondNum };
                }
            }
            else
            {
                WriteLine("Wrong values entered!");

                return null;
            }
        }

        public static RationalNum operator -(RationalNum a, RationalNum b)
        {
            if (a != null && b != null)
            {
                if (a.SecondNum == b.SecondNum)
                {
                    return new RationalNum { FirstNum = a.FirstNum - b.FirstNum, SecondNum = a.SecondNum };
                }
                else
                {
                    return new RationalNum
                    {
                        FirstNum = a.FirstNum * b.SecondNum - b.FirstNum * a.SecondNum,
                        SecondNum = a.SecondNum * b.SecondNum
                    };
                }
            }
            else
            {
                WriteLine("Wrong values entered!");

                return null;
            }
        }

        public static RationalNum operator *(RationalNum a, RationalNum b)
        {
            if (a != null && b != null)
            {
                return new RationalNum { FirstNum = a.FirstNum * b.FirstNum, SecondNum = a.SecondNum * b.SecondNum };
            }
            else
            {
                WriteLine("Wrong values entered!");

                return null;
            }
        }

        public static RationalNum operator /(RationalNum a, RationalNum b)
        {
            if (a != null && b != null)
            {
                return new RationalNum { FirstNum = a.FirstNum * b.SecondNum, SecondNum = a.SecondNum * b.FirstNum };
            }
            else
            {
                WriteLine("Wrong values entered!");

                return null;
            }
        }

        public static bool operator >(RationalNum a, RationalNum b)
        {
            if (a != null && b != null)
            {
                return a.FirstNum * b.SecondNum > b.FirstNum * a.SecondNum;
            }
            else
            {
                WriteLine("Wrong values entered!");

                return false;
            }
        }

        public static bool operator <(RationalNum a, RationalNum b)
        {
            if (a != null && b != null)
            {
                return a.FirstNum * b.SecondNum < b.FirstNum * a.SecondNum;
            }
            else
            {
                WriteLine("Wrong values entered!");

                return false;
            }
        }


        //------------------------------------------------------------------------------
        //представление в виде строки
        public string ToStringConverter(string password)
        {
            if (!string.IsNullOrEmpty(password))
            {
                if (password == "Rational")
                {
                    return (FirstNum.ToString() + "/" + SecondNum.ToString());
                }
                else if (password == "Real")
                {
                    Write((FirstNum / SecondNum).ToString("0.00"));
                    return "";
                }
                else
                {
                    WriteLine("Wrong command was entered, try again please!");

                    return "";
                }
            }
            else
            {
                WriteLine("Empty string entered!");

                return "";
            }
        }

        //из строки в объект
        public void ObjectCreator(string rationNumStr)
        {
            if (!string.IsNullOrEmpty(rationNumStr))
            {
                if (rationNumStr.IndexOf('/') != -1)
                {
                    string[] numbers = rationNumStr.Split('/');

                    //проверка
                    for (int i = 0; i < numbers.Length; i++)
                    {
                        if(int.Parse(numbers[i]) <= 0 || string.IsNullOrEmpty(numbers[i]))
                        {
                            WriteLine("Wrong numbers entered!");
                            return;
                        }
                    }
                    
                    FirstNum = int.Parse(numbers[0]);
                    SecondNum = int.Parse(numbers[1]);

                    return;
                }
                else 
                {
                    double enteredNum;

                    try
                    {
                        int fractional;
                        int wholeNum;

                        enteredNum = Convert.ToDouble(rationNumStr);

                        wholeNum = Convert.ToInt32(Math.Floor(enteredNum));
                        fractional = Convert.ToInt32(Math.Round((enteredNum - wholeNum), 3) * 1000);

                        FirstNum = (wholeNum * 1000) + fractional;
                        SecondNum = 1000;

                        return;
                    }
                    catch(FormatException)
                    {
                        WriteLine("Wrong number entered!");

                        return;
                    }
                }
            }
            else
            {
                WriteLine("Wrong numbers entered!");

                return;
            }
        }
    }
}
