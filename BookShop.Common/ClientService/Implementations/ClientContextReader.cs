using BookShop.Common.ClientService.Abstractions;

namespace BookShop.Common.ClientService.Implementations;

public class ClientContextReader : IClientContextReader
{
    private readonly ClientContext _clientContext;

    public ClientContextReader(ClientContext clientContext)
    {
        _clientContext = clientContext;
    }

    public long GetClientContextId()
    {
        return _clientContext.Id;
    }
}
