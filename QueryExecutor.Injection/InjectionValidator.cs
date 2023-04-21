using Nethereum.Util;
using Nethereum.Hex.HexTypes;
using System.Text.RegularExpressions;

namespace QueryExecutor.Injection;

public class InjectionValidator
{
    public bool CheckForSqlInjection(Dictionary<ParameterType, string> parameters)
    {
        if (parameters == null || parameters?.Count == 0)
            return false;

        foreach (var parameter in parameters)
        {
            if (parameter.Key == ParameterType.Number)
            {
                if (!IsValidNumberParameter(parameter.Value))
                    return true;
            }
            else if (parameter.Key == ParameterType.EthereumAddress)
            {
                if (!IsValidEthereumAddressParameter(parameter.Value))
                    return true;
            }
            else if (parameter.Key == ParameterType.Boolean)
            {
                if (!IsValidBooleanParameter(parameter.Value))
                    return true;
            }
            else if (parameter.Key == ParameterType.DateTime)
            {
                if (!IsValidDateTimeParameter(parameter.Value))
                    return true;
            }
            else if (parameter.Key == ParameterType.TxHash)
            {
                if (!IsValidTxHashParameter(parameter.Value))
                    return true;
            }
            else
            {
                return true;
            }
        }

        return false;
    }

    public static bool IsValidNumberParameter(string param) =>
        int.TryParse(param, out _);

    public static bool IsValidEthereumAddressParameter(string param) =>
       AddressUtil.Current.IsValidEthereumAddressHexFormat(param);

    public static bool IsValidBooleanParameter(string param) =>
        bool.TryParse(param, out _);

    public static bool IsValidDateTimeParameter(string param) =>
        DateTime.TryParse(param, out _);

    public static bool IsValidTxHashParameter(string param)
    {
        try
        {
            var number = new HexBigInteger(param).Value;
        }
        catch (Exception)
        {
            Regex rg = new(@"^[0-9a-z]*$");
            if (!param.StartsWith("0x")
                || param.Contains(' ')
                || param.Length != 66
                || !rg.IsMatch(param))
                return false;
        }

        return true;
    }
}