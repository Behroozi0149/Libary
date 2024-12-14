using Microsoft.EntityFrameworkCore;
using Novin.Libary.Backend.API.DB;
using Novin.Libary.Backend.API.Entities;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FirstDB>(Options =>
{
    Options.UseSqlServer(builder.Configuration.GetConnectionString("MainDB"));
});
builder.Services.AddCors(Options =>
{
    Options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
    });
});
var app = builder.Build();
app.UseCors();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
// Book list
app.MapGet("/books/list", (FirstDB db) =>
{
    return db.Books.ToList();
});
// Book add
app.MapPost("/books/add", (FirstDB db, Book book) =>
{
    db.Books.Add(book);
    db.SaveChanges();
});
// Member list
app.MapGet("/members/list", (FirstDB db) =>
{
    return db.Members.ToList();
});
// Member add
app.MapPost("/members/add", (FirstDB db, Member member) =>
{
    db.Members.Add(member);
    db.SaveChanges();
});
// Borrow list
app.MapGet("/borrow/list", (FirstDB db) =>
{
    return db.Borrows.Include(m => m.Book).Include(m => m.Member).ToList();
});
// Borrow add
app.MapPost("/borrows/add", (FirstDB db, Borrow borrow) =>
{
    db.Borrows.Add(borrow);
    db.SaveChanges();
});
app.Run();