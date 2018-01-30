using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize("mypolicy")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class FooController
        : Controller
    {
        private readonly FooContext _context;

        public FooController(FooContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFoo(int id)
        {
            var foo = await _context.Foo
                .FindAsync(id);

            if (foo != null)
            {
                return Ok(foo);
            }

            return NotFound();
        }

        [HttpPost("add")]
        public async Task<IActionResult> PostFoo([FromBody]Foo foo)
        {
            await _context.Foo.AddAsync(foo);

            _context.SaveChanges();

            return Ok();

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFoo(int id, [FromBody]Foo foo)
        {
            var existing = await _context.Foo
               .FindAsync(id);

            if (existing != null)
            {
                existing.Bar = foo.Bar;

                _context.SaveChanges();

                return Ok(existing);
            }

            return NotFound();
        }
    }

    public class FooContext
        : DbContext
    {
        public FooContext(DbContextOptions<FooContext> options) : base(options)
        {
        }

        public DbSet<Foo> Foo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Foo>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<Foo>()
                .Property(p => p.Id)
                .ForSqlServerUseSequenceHiLo();

            modelBuilder.Entity<Foo>()
                .Property(p => p.Bar)
                .IsRequired();

        }
    }

    public class Foo
    {
        public int Id { get; set; }

        public string Bar { get; set; }
    }

}
