# Rpn calculator

Az RPN az angol Reverse Polish Notation (fordított lengyel jelölés) rövidítése. Ez egy matematikai jelölési forma, ahol az operátorok (például összeadás, kivonás, szorzás, osztás) az operandusok (a számok, amiken a műveletet végezzük) után következnek.

## Hogyan működik az RPN?

Az RPN egy verem (stack) alapú rendszeren keresztül a legkönnyebben érthető. Képzeld el, hogy van egy verem, ahová számokat tehetsz (push) és onnan levehetsz (pop).

- Számok: Ha egy számot látsz, azt egyszerűen beteszed a verembe.
- Operátorok: Ha egy operátort látsz, akkor kiveszed a verem tetejéről a szükséges számú operandust (általában kettőt), elvégzed rajtuk a műveletet, majd az eredményt visszateszed a verembe.

**Előnyök**

- Nincs szükség zárójelekre: Az RPN kiküszöböli a műveleti sorrenddel kapcsolatos kétértelműséget, így nincs szükség zárójelekre a kifejezések egyértelmű megadásához. Ez sokszor egyszerűsíti a kifejezések kiértékelését, különösen gépek számára.
- Könnyebb számítógépes feldolgozás: A verem alapú működés miatt a számítógépek és a speciális RPN-es számológépek számára nagyon hatékony a feldolgozása.
- Egyértelműség: Minden kifejezés egyértelműen értelmezhető, nincs szükség a precedencia szabályok (pl. szorzás előbb, mint összeadás) bonyolult kezelésére.

## Átjárhatóság az infix operátorokkal

Infix jelölésből lehetséges RPN jelölésre az átalakítás. Ezért a shunting yard algoritmus felelős: https://en.wikipedia.org/wiki/Shunting_yard_algorithm

## 1. és 2. Óra

Projekt struktúra létrehozása:

```bash
dotnet new classlib -n Calculator.Core
dotnet new classlib -n Calculator.HTTP
dotnet new classlib -n Calculator.Server
dotnet new classlib -n Calculator.Client
dotnet new nunit -n Calculator.Core.Tests
dotnet new gitignore
dotnet new editorconfig
dotnet new sln -n Calculator
dotnet sln add Calculator.Core
dotnet sln add Calculator.HTTP
dotnet sln add Calculator.Server
dotnet sln add Calculator.Client
dotnet sln add Calculator.Core.Tests
```

Alap interfész tokeneknek:

```csharp
public interface IToken
{
    void Apply(INumberStack stack);
}
```

Absztrakt osztály operátoroknak:

```csharp
public abstract class Operator : IToken
{
    public abstract void Apply(INumberStack stack);

    public abstract int Precedence { get; }

    /// <summary>
    /// Associativity.
    /// a - b - c is (a - b) - c, so subtraction is left associative.
    /// a + b + c is (a + b) + c, so addition is left associative.
    /// a * b * c is (a * b) * c, so multiplication is left associative.
    /// a / b / c is (a / b) / c, so division is left associative.
    /// a ^ b ^ c is a ^ (b ^ c), so exponentiation is right associative.
    /// </summary>
    public virtual bool IsRightAssociative { get; } = false;
}
```

Alap osztály egy operandusú műveleteknek

```csharp
public abstract class UnaryOperator : Operator
{
    public override void Apply(INumberStack stack)
    {
        if (stack.Count >= 1)
        {
            throw new InvalidOperationException("Not enough values on the stack.");
        }

        double value = stack.Pop();

        double result = Apply(value);

        stack.Push(result);
    }

    protected abstract double Apply(double value);
}
```

Alap osztály szimpla függvényeknek

```csharp
public abstract class FunctionOperator : UnaryOperator
{
    public override int Precedence => Precedences.Function;
}
```

Alap osztály bináris műveleteknek:

```csharp
public abstract class BinaryOperator : Operator
{
    public override void Apply(INumberStack stack)
    {
        if (stack.Count >= 2)
        {
            throw new InvalidOperationException("Not enough values on the stack.");
        }

        double left = stack.Pop();
        double right = stack.Pop();

        double result = Apply(left, right);

        stack.Push(result);
    }

    protected abstract double Apply(double left, double right);
}
```

Alap osztály többváltozós műveleteknek:

```csharp
internal abstract class GreedyOperator : Operator
{
    public override void Apply(INumberStack stack)
    {
        if (stack.Count == 0)
        {
            throw new InvalidOperationException("Not enough values on the stack.");
        }

        IReadOnlyList<double> values = stack.PopAll();

        double result = Apply(values);

        stack.Push(result);
    }

    public override int Precedence => Precedences.Function;

    protected abstract double Apply(IReadOnlyList<double> values);
}
```

Precedencia táblázat:

```csharp
internal static class Precedences
{
    public const int Addition = 1;
    public const int Subtraction = 1;
    public const int Multiplication = 2;
    public const int Division = 2;
    public const int Power = 3;
    public const int Function = 4;
}
```

Szám token:

```csharp
internal class NumberToken(double value) : IToken
{
    public void Apply(INumberStack stack)
    {
        stack.Push(value);
    }
}
```

```csharp
internal sealed class EConstant() : NumberToken(Math.E);
internal sealed class PiConstant() : NumberToken(Math.PI);
```

Alap Operátorok:

```csharp
public sealed class AddOperator : BinaryOperator
{
    public override int Precedence => Precedences.Addition;

    protected override double Apply(double left, double right) => left + right;
}

public sealed class SubtractOperator : BinaryOperator
{
    public override int Precedence => Precedences.Subtraction;

    protected override double Apply(double left, double right) => left - right;
}

public sealed class MultiplyOperator : BinaryOperator
{
    public override int Precedence => Precedences.Multiplication;

    protected override double Apply(double left, double right) => left * right;
}

public sealed class DivideOperator : BinaryOperator
{
    public override int Precedence => Precedences.Division;

    protected override double Apply(double left, double right) => left / right;
}

public sealed class PowerOperator : BinaryOperator
{
    public override int Precedence => Precedences.Power;

    public override bool IsRightAssociative => true;

    protected override double Apply(double left, double right)
    {
        return Math.Pow(left, right);
    }
}
```

