using System;

namespace Incapsulation.RationalNumbers {

    public class Rational { // определение класса
        private int numerator; // числитель
        private int denominator; // знаменатель
        private bool isNan; // проверка деления на ноль

        public Rational(int numerator, int denominator = 1) { // конструктор класса
            if (denominator == 0) { // проверка деления на ноль
                IsNan = true; // установка флага
                return;
            }

            int gcd = Gcd(numerator, denominator); //  вычисляем нод числителя и знаменателя 

            numerator /= gcd; // сокращаем числитель на нод
            denominator /= gcd; // сокращаем знаменатель на нод

            if (denominator < 0) { // получившийся знаменатель отрицательный
                denominator = -denominator; // меняем знак числителя и знаменателя местами,
                numerator = -numerator; // чтобы знак дроби был в числителе
            }

            this.numerator = numerator; // присваиваем значение полю класса
            this.denominator = denominator; // присваиваем значение полю класса

        }

        public bool IsNan { // свойство показывает есть ли деление на ноль
            get { return isNan; }
            private set { isNan = value; }
        }

        private int Gcd(int a, int b) { // находим нод по алгоритму Евклида
            a = Math.Abs(a);
            b = Math.Abs(b);
            while (b != 0) {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        public int Numerator { // свойства доступа к числителю
            get { return numerator; }
        }

        public int Denominator { // свойства доступа к знаменателю
            get { return denominator; }
        }

        public static Rational operator +(Rational r1, Rational r2) { // перегрузка оператора сложения
            return new Rational(r1.numerator * r2.denominator + r2.numerator * r1.denominator, r1.denominator * r2.denominator);
        }

        public static Rational operator -(Rational r1, Rational r2) { // перегрузка оператора вычитания
            return new Rational(r1.numerator * r2.denominator - r2.numerator * r1.denominator, r1.denominator * r2.denominator);
        }

        public static Rational operator *(Rational r1, Rational r2) { // перегрузка оператора умножения
            return new Rational(r1.numerator * r2.numerator, r1.denominator * r2.denominator);
        }
        public static Rational operator /(Rational r1, Rational r2) { // перегрузка оператора деления
            return new Rational(r1.numerator * r2.denominator, r1.denominator * r2.numerator);
        }

        public static implicit operator Rational(int value) { // перегрузка оператора преобразования целого числа в рациональное число
            return new Rational(value);
        }
         
        public static explicit operator int(Rational r) { // перегрузка оператора преобразования рационального числа в целое число
            if (r.Denominator != 1) {
                throw new InvalidCastException();
            }
            return r.Numerator;
        }

        public static implicit operator double(Rational r) { // перегрузка оператора преобразования рационального числа в число с плавающей запятой
            if (r.IsNan) {
                return double.NaN;
            }
            return (double)r.Numerator / r.Denominator;
        }
    }
}