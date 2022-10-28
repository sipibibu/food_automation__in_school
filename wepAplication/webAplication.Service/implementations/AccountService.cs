using Microsoft.Extensions.Logging;
using webAplication.DAL;

namespace webAplication.Service;

public class AccountService
{
    AplicationDbContext db;

    private readonly ILogger<AccountService> _logger;

    public AccountService(ILogger<AccountService> logger, AplicationDbContext context)
    {
        db = context;
        _logger = logger;
    }
}