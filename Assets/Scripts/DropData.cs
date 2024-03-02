using UnityEngine;

[CreateAssetMenu(fileName = "DropData", menuName = "MathDrop/Create drop operation")]
public class DropData : ScriptableObject
{
    [SerializeField] private OperatorType operatorType;

    [SerializeField] private int firstNumber;

    [SerializeField] private int secondNumber;

    public OperatorType OperatorType => operatorType;
    public int FirstNumber => firstNumber;
    public int SecondNumber => secondNumber;
}

public enum OperatorType
{
    Plus,
    Minus
}

public static class OperatorTypeExtensions
{
    public static string GetSymbol(this OperatorType operatorType)
    {
        return operatorType switch
        {
            OperatorType.Plus => "+",
            OperatorType.Minus => "-",
            _ => ""
        };
    }
}