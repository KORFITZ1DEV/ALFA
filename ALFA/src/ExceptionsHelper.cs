namespace ALFA;

public class TypeException : Exception
{
    public TypeException(string message)
        : base(message){}
}
public class ArgumentTypeException : Exception
{
    public ArgumentTypeException(string message)
        : base(message){}
}
public class NumArgumentException : Exception
{
    public NumArgumentException(string message)
        : base(message){}
}

public class UnknownBuiltinException : Exception
{
    public UnknownBuiltinException(string message)
        : base(message){}
}

public class UndeclaredVariable : Exception
{
    public UndeclaredVariable(string message)
        : base(message){}
}
