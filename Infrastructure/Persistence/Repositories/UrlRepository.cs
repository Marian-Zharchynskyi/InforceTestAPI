using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.Urls;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Infrastructure.Persistence.Repositories;

public class UrlRepository : IUrlRepository, IUrlQueries
{
    private readonly ApplicationDbContext _context;

    public UrlRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Url>> GetAll(CancellationToken cancellationToken)
    {
        return await _context.Urls
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Option<Url>> GetById(UrlId id, CancellationToken cancellationToken)
    {
        var entity = await _context.Urls
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        
        return entity == null ? Option.None<Url>() : Option.Some(entity);
    }

    public async Task<Option<Url>> GetByUrl(string url, CancellationToken cancellationToken)
    {
        var entity = await _context.Urls
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.OriginalUrl == url, cancellationToken);
        
        return entity == null ? Option.None<Url>() : Option.Some(entity);
    }

    public async Task<Url> Add(Url url, CancellationToken cancellationToken)
    {
        await _context.Urls.AddAsync(url, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return url;
    }

    public async Task<Url> Update(Url url, CancellationToken cancellationToken)
    {
        _context.Urls.Update(url);
        await _context.SaveChangesAsync(cancellationToken);
        return url;
    }

    public async Task<Url> Delete(Url url, CancellationToken cancellationToken)
    {
        _context.Urls.Remove(url);
        await _context.SaveChangesAsync(cancellationToken);
        return url;
    }
}