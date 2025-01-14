using Application.Common.Interfaces.Queries;
using Domain.Authentications.Roles;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Infrastructure.Persistence.Repositories;

public class RoleRepository : IRoleQueries
{
    private readonly ApplicationDbContext _context;

    public RoleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Role>> GetAll(CancellationToken cancellationToken)
    {
        return await _context.Roles
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Option<Role>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.Roles
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        
        return entity == null ? Option.None<Role>() : Option.Some(entity);
    }

    public async Task<Option<Role>> GetByName(string name, CancellationToken cancellationToken)
    {
        var entity = await _context.Roles
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
        
        return entity == null ? Option.None<Role>() : Option.Some(entity);
    }
}