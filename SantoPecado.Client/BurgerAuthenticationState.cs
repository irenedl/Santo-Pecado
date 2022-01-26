using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace SantoPecado.Client
{
    public class BurgerAuthenticationState : RemoteAuthenticationState
    {
        public Order Order { get; set; }
    }
}
