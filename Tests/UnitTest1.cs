namespace Tests;

using StringCalculator;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        Assert.That(Calculator.Add(""), Is.EqualTo(0));
    }

    [Test]
    public void Test2()
    {
        Assert.That(Calculator.Add("1"), Is.EqualTo(1));
    }

    [Test]
    public void Test3()
    {
        Assert.That(Calculator.Add("1,2"), Is.EqualTo(3));
    }

    [Test]
    public void Test4()
    {
        Assert.That(Calculator.Add("1\n2"), Is.EqualTo(3));
    }

    [Test]
    public void Test5()
    {
        Assert.That(Calculator.Add("1,2,3"), Is.EqualTo(6));
    }

    [Test]
    public void Test6()
    {
        Assert.That(() => Calculator.Add("-1"), Throws.TypeOf<System.ArgumentException>());
    }

    [Test]
    public void Test7()
    {
        Assert.That(Calculator.Add("1001"), Is.EqualTo(0));
    }

    [Test]
    public void Test8()
    {
        Assert.That(Calculator.Add("//;\n1;2"), Is.EqualTo(3));
    }

    [Test]
    public void Test9()
    {
        Assert.That(Calculator.Add("//[###]\n1###2"), Is.EqualTo(3));
    }

    [Test]
    public void Test10()
    {
        Assert.That(Calculator.Add("//[###][***]\n10###20***30"), Is.EqualTo(60));
    }
}