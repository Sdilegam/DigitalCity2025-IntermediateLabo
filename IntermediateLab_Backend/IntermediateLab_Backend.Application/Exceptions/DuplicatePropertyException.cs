namespace IntermediateLab_Backend.Application.Exceptions;

public class DuplicatePropertyException(in string propertyName):Exception($"The '{propertyName}' property is already registered and must be unique.")
{
}