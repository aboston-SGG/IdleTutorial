using System.Collections.Generic;

public static class Suffixes
{
    // Suffixes for exponents less than 33
    public static string[] suffixes = new string[]
    {
        "", "K", "M", "B", "T", "Qa", "Qi", "Sx", "Sp", "Oc", "No"
    };

    // Suffix parts for larger numbers, each line is 10 times larger than the previous one
    public static List<string> suffixParts = new List<string>
    {
        "", "U", "D", "T", "QU", "QI", "SX", "SP", "O", "N",
        "", "Dc", "Vg", "Tg", "Qug", "Qig", "Sxg", "Spg", "Og", "Ng",
        "", "CE", "DC", "TC", "QUC", "QIC", "SXC", "SPC", "OC", "NC",
        "", "-Mi", "-Du", "-Tr"
    };
}
