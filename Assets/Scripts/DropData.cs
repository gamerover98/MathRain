using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DropData", menuName = "MathDrop/Create drop operation")]
public class DropData : ScriptableObject
{
    [SerializeField] private OperatorType operatorType;
    [SerializeField] private int firstNumber;
    [SerializeField] private int secondNumber;
    [SerializeField] private int points = 10;
    [SerializeField] private float speed = 0.25F;

    public OperatorType OperatorType => operatorType;
    public int FirstNumber => firstNumber;
    public int SecondNumber => secondNumber;
    public int Result => operatorType.Operation().Invoke(firstNumber, secondNumber);

    public int Points => points;
    public float Speed => speed;
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

    public static Func<int, int, int> Operation(this OperatorType operatorType)
    {
        return operatorType switch
        {
            OperatorType.Plus => Sum,
            OperatorType.Minus => Minus,
            _ => throw new NotSupportedException("This operation is not supported")
        };
    }

    private static int Sum(int a, int b) => a + b;
    private static int Minus(int a, int b) => a - b;
}