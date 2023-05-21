using System.Diagnostics.CodeAnalysis;

namespace ALFA.Types;

[ExcludeFromCodeCoverage]
public class ALFATypes
{
    public enum TypeEnum
    {
        @int,
        rect,
        @bool
    }

    public enum BuiltInAnimEnum
    {
        move,
        wait
    }
    public enum BuiltInParalAnimEnum
    {
        move
    }

    public enum CreateShapeEnum
    {
        createRect
    }
    public enum OutputEnum
    {
        VarOutput,
        MainOutput,
        SetupOutput,
        DrawOutput,
        Output
    }
}