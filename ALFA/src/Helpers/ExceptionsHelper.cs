using System.Diagnostics.CodeAnalysis;

namespace ALFA;

[ExcludeFromCodeCoverage]
public class AlfaException : Exception
{
    public AlfaException(string message) : base(message){}

    public override string StackTrace
    {
        get { return ""; } // remove stacktrace
    }
}

[ExcludeFromCodeCoverage]
public class TypeException : AlfaException
{
    public TypeException(string message)
        : base(message){}
}

[ExcludeFromCodeCoverage]
public class ArgumentTypeException : AlfaException
{
    public ArgumentTypeException(string message)
        : base(message){}
}

[ExcludeFromCodeCoverage]
public class InvalidNumberOfArgumentsException : AlfaException
{
    public InvalidNumberOfArgumentsException(string message)
        : base(message){}
}

[ExcludeFromCodeCoverage]
public class UnknownBuiltinException : AlfaException
{
    public UnknownBuiltinException(string message)
        : base(message){}
}

[ExcludeFromCodeCoverage]
public class UndeclaredVariableException : AlfaException
{
    public UndeclaredVariableException(string message)
        : base(message){}
}

[ExcludeFromCodeCoverage]
public class VariableAlreadyDeclaredException : AlfaException
{
    public VariableAlreadyDeclaredException(string message) : base(message){}
}

[ExcludeFromCodeCoverage]
public class SyntacticException : AlfaException
{
    public SyntacticException(string message) : base(message){
    }
}

[ExcludeFromCodeCoverage]
public class NonPositiveAnimationDurationException : AlfaException
{
    public NonPositiveAnimationDurationException(string message) : base(message){}
}

[ExcludeFromCodeCoverage]
public class AttemptingToChangePropertyOfSameShapeInParalException: AlfaException
{
    public AttemptingToChangePropertyOfSameShapeInParalException(string message) : base(message){}
}

