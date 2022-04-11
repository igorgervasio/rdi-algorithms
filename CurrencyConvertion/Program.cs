using System;
using System.Linq;
using System.Text;

namespace CurrencyConvertion
{
    public class AmountConverter
    {
        private static readonly string[] unidades =
        {
            "",
            "um",
            "dois",
            "tres",
            "quatro",
            "cinco",
            "seis",
            "sete",
            "oito",
            "nove",
            "dez",
            "onze",
            "doze",
            "treze",
            "quatorze",
            "quinze",
            "dezesseis",
            "dezesete",
            "dezoito",
            "dezenove"
        };

        private static readonly string[] unidades2 =
        {
            "dez",
            "onze",
            "doze",
            "treze",
            "quatorze",
            "quinze",
            "dezesseis",
            "dezesete",
            "dezoito",
            "dezenove"
        };

        private static readonly string[] dezenas =
        {
            "",
            "",
            "vinte",
            "trinta",
            "quarenta",
            "cinquenta",
            "sessenta",
            "setenta",
            "oitenta",
            "noventa"
        };

        private static readonly string[] centenas =
        {
            "",
            "",
            "duzentos",
            "trezentos",
            "quatrocentos",
            "quinhentos",
            "seiscentos",
            "setecentos",
            "oitocentos",
            "novecentos"
        };

        private static string ConvertCents(int cents, bool hasReais)
        {
            if (cents == 0)
                return null;

            char[] centsMask = new char[2];
            Array.Copy(cents.ToString().ToArray(), centsMask, cents.ToString().Length);

            int unit = 0;
            int deze = 0;

            if (cents < 10)
            {
                unit = (centsMask[0] != default) ? int.Parse(centsMask[0].ToString()) : 0;
            }
            else
            {
                deze = (centsMask[0] != default) ? int.Parse(centsMask[0].ToString()) : 0;
                unit = (centsMask[1] != default) ? int.Parse(centsMask[1].ToString()) : 0;
            }

            StringBuilder ret = new StringBuilder();

            if (hasReais && cents > 0)
                ret.Append(" e ");

            if(cents == 1)
                ret.Append($"um centavo");
            else if(cents < 10)
                ret.Append($"{unidades[unit]} centavos");
            else if (cents >= 10 && cents < 20)
                ret.Append($"{unidades2[unit]} centavos");
            else
            {
                ret.Append($"{dezenas[deze]}");
                if(unit > 0)
                {
                    ret.Append($" e ");
                    ret.Append($"{unidades[unit]}");
                }
                ret.Append(" centavos");
            }
            
            return ret.ToString();
        }

        private static string ConvertParcialAmount(char[] amount)
        {
            if (amount[0] == default && amount[1] == default && amount[2] == default)
                return null;

            int cent = 0, deze = 0, unit = 0;

            if (amount[0] != default)
                cent = int.Parse(amount[0].ToString());

            if (amount[1] != default)
                deze = int.Parse(amount[1].ToString());

            if (amount[2] != default)
                unit = int.Parse(amount[2].ToString());

            StringBuilder ret = new StringBuilder();
            if (cent == 1 && deze == 0 && unit == 0)
                return ret.Append("cem ").ToString();

            if (cent == 1 && (deze != 0 || unit != 0))
                ret.Append("cento ");
            else if (cent > 1)
                ret.Append(centenas[cent] + " ");

            if (deze == 0 && unit == 0)
                return ret.ToString();

            if (ret.Length > 0)
                ret.Append("e ");

            if (deze == 1)
                return ret.Append(unidades2[unit]).ToString(); //if is between 10 and 19, the unit defines the index
            else if (deze > 1)
                ret.Append(dezenas[deze]);

            if (ret.Length > 0 && unit > 0)
                ret.Append(" e ");

            if (unit > 0)
                ret.Append(unidades[unit]); //if is between 10 and 19, the unit defines the index

            return ret.ToString();
        }

        public static string convertAmount2Words(int reais, int cents)
        {
            char[] reaisMask = new char[9];
            char[] reaisTemp = reais.ToString().ToArray();

            Array.Copy(reaisTemp, 0, reaisMask, 9 - reaisTemp.Length, reaisTemp.Length);

            //Works int the millions indexes
            StringBuilder total = new StringBuilder();
            if (reais == 1)
                total.Append("Um real");
            else
            {
                StringBuilder milhoes = new StringBuilder();
                milhoes.Append(ConvertParcialAmount(reaisMask.Take(3).ToArray()));

                //Works int the hundreds indexes
                StringBuilder finais = new StringBuilder();
                finais.Append(ConvertParcialAmount(reaisMask.Skip(6).Take(3).ToArray()));

                //Works int the thousands indexes
                StringBuilder milhares = new StringBuilder();
                milhares.Append(ConvertParcialAmount(reaisMask.Skip(3).Take(3).ToArray()));

                //for millions
                if (milhoes.Length > 0)
                {
                    if(reaisMask[0] == default && reaisMask[1] == default && reaisMask[2] == '1')
                        milhoes.Append(" milhão");
                    else
                        milhoes.Append(" milhões");

                    total.Append(milhoes);
                }

                //For thousands
                if (milhares.Length > 0)
                {
                    if (milhoes.Length > 0)
                    {
                        if (finais.Length == 0)
                            total.Append(" e ");
                        else
                            total.Append(" ");
                    }
                    
                    milhares.Append(" mil");
                    total.Append(milhares);
                }

                //for handreds
                if (finais.Length > 0)
                {
                    if(total.Length > 0)
                        finais.Insert(0, " e ");
                    
                    total.Append(finais.ToString());
                }

                if(reais == 1000000)
                    total.Append(" de reais");
                else if (total.Length > 0)
                    total.Append(" reais");

                //Append the result of cents processing
                total.Append(ConvertCents(cents, total.Length > 0));
            }

            return char.ToUpper(total[0]) + total.ToString().Substring(1);
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine( AmountConverter.convertAmount2Words(1000080, 0) );
        }
    }
}
