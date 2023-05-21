using System.Diagnostics.CodeAnalysis;

namespace ALFA;

[ExcludeFromCodeCoverage]
public class TypeException : Exception
{
    public TypeException(string message)
        : base(message){}
}

[ExcludeFromCodeCoverage]
public class ArgumentTypeException : Exception
{
    public ArgumentTypeException(string message)
        : base(message){}
}

[ExcludeFromCodeCoverage]
public class InvalidNumberOfArgumentsException : Exception
{
    public InvalidNumberOfArgumentsException(string message)
        : base(message){}
}

[ExcludeFromCodeCoverage]
public class UnknownBuiltinException : Exception
{
    public UnknownBuiltinException(string message)
        : base(message){}
}

[ExcludeFromCodeCoverage]
public class UndeclaredVariableException : Exception
{
    public UndeclaredVariableException(string message)
        : base(message){}
}

[ExcludeFromCodeCoverage]
public class VariableAlreadyDeclaredException : Exception
{
    public VariableAlreadyDeclaredException(string message) : base(message){}
}

[ExcludeFromCodeCoverage]
public class SyntacticException : Exception
{
    public SyntacticException(string message) : base(message){}
}

[ExcludeFromCodeCoverage]
public class NonPositiveAnimationDurationException : Exception
{
    public NonPositiveAnimationDurationException(string message) : base(message){}
}
