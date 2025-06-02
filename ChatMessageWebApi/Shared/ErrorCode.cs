using System.ComponentModel;

namespace posterr_webapi.Shared;

public enum ErrorCode
{
    [Description("Malformed request")]
    BadRequest = 1,
    [Description("Invalid user to use the resource")]
    Unauthorized,
    [Description("The user is authorized to use the service, but not the requested resource")]
    Forbid,
    [Description("Requested resource cloud not be found")]
    NotFound,
    [Description("There is a resource with the same characteristics sent")]
    Conflict,
    [Description("Validation of business rules executed")]
    UnprocessableEntity,
    [Description("An internal server error has occurred")]
    InternalServerError,
    [Description("The service is temporarily unavailable")]
    ServiceUnavailable,
}