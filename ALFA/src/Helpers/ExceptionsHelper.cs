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
public class InvalidNumberOfArgumentsException : Exception
{
    public InvalidNumberOfArgumentsException(string message)
        : base(message){}
}

public class UnknownBuiltinException : Exception
{
    public UnknownBuiltinException(string message)
        : base(message){}
}

public class UndeclaredVariableException : Exception
{
    public UndeclaredVariableException(string message)
        : base(message){}
}

public class RedeclaredVariableException : Exception
{
    public RedeclaredVariableException(string message) : base(message){}
}

public class SyntacticException : Exception
{
    public SyntacticException(string message) : base(message){}
}

public class NonPositiveAnimationDurationException : Exception
{
    public NonPositiveAnimationDurationException(string message) : base(message){}
}
