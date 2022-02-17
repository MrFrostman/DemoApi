using Product.Core.Interfaces;
using Product.Infrastructure.Data;

namespace Product.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProductContext _context;
        // private readonly IMemberRepository _memberRepository;
        // private readonly ICountryRepository _countryRepository;

        public UnitOfWork(ProductContext context)
        {
            _context = context;
        }

        // public IMemberRepository MemberRepository => _memberRepository ?? new MemberRepository(_context);

        // public ICountryRepository CountryRepository => _countryRepository ?? new CountryRepository(_context);

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
