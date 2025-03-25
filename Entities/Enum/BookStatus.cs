using Microsoft.AspNetCore.Http.Connections;

namespace LibaryApi.Entities.Enum
{

    public enum Status
    {
        Available,
        Reserved,
        Borrowed,
        Unavailable,
    }
}
