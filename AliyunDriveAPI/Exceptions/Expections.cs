namespace AliyunDriveAPI.Exceptions;

public class NotFoundException : APIException
{
    public string FieldName { get; set; }

    public NotFoundException(APIException ex) : base(ex)
    {
        FieldName = ex.Code.Split('.')[1];
    }
}

public class JsonParseException : APIException
{
    public JsonParseException(APIException ex) : base(ex) {}
}

public class BadRequestException : APIException
{
    public BadRequestException(APIException ex) : base(ex) {}
}

public class InvalidParameterException : APIException
{
    public string FieldName { get; set; }

    public InvalidParameterException(APIException ex) : base(ex) 
    {
        FieldName = ex.Code.Split('.')[1];
    }
}

public class ForbiddenNoPermissionException : APIException
{
    public string ResourceName { get; set; }

    public ForbiddenNoPermissionException(APIException ex) : base(ex)
    {
        ResourceName = ex.Code.Split('.')[1];
    }
}

public class AccessTokenInvalidException : APIException
{
    public AccessTokenInvalidException(APIException ex) : base(ex) { }
}

public class InvalidResourceException : APIException
{
    public string ResourceName { get; set; }

    public InvalidResourceException(APIException ex) : base(ex)
    {
        ResourceName = ex.Code.Split('.')[1];
    }
}

public class AlreadyExistException : APIException
{
    public string ResourceName { get; set; }

    public AlreadyExistException(APIException ex) : base(ex)
    {
        ResourceName = ex.Code.Split('.')[1];
    }
}