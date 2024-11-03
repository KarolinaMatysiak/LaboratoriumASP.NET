using System.Diagnostics;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Models;

public class Calculator
{
    public Operator? Operators { get; set; }
    public double? X { get; set; }
    public double? Y { get; set; }

    public String Op
    {
        get
        {
            switch (Operators)
            {
                case Operator.Add:
                    return "+";
                case Operator.Div:
                    return ":";
                case Operator.Mul:
                    return "*";
                case Operator.Sub:
                    return "-";
                case Operator.Unknown:
                    return "";
                
                default:
                    return "";
            }
        }
    }

    public bool IsValid()
    {
        return Operators != null && X != null && Y != null;
    }

    public double Calculate() {
        switch (Operators)
        {
            case Operator.Add:
                return (double) (X + Y);
            case Operator.Div:
                return (double) ( X/ Y);
            case Operator.Mul:
                return (double) (X * Y);
            case Operator.Sub:
                return (double) (X - Y);
            
            
            default: return double.NaN;
        }
    }
}