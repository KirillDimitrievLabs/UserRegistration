using UserRegistration.Models;

namespace UserRegistration.Components.Core
{
    interface ISourceConnection
    {
        UserSourceModel Read();
    }
}
