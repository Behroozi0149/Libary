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
#region Books
// Book Add
app.MapPost("/books/add", (FirstDB db, Book book) =>
{
    db.Books.Add(book);
    db.SaveChanges();
});
// Book List
app.MapGet("/books/list", (FirstDB db) =>
{
    Thread.Sleep(1000);
    return db.Books.ToList();
});
// Book Edit
app.MapPost("/books/edit", (FirstDB db, Book book) =>
{
    db.Books.Update(book);
    db.SaveChanges();
});
// Book Remove
app.MapPost("/books/remove/{id}", (FirstDB db, int id) =>
{
    var book = db.Books.Find(id);
    if (book != null)
    {
        db.Books.Remove(book);
        db.SaveChanges();
    }
});
#endregion
#region Members
// Member Add
app.MapPost("/members/add", (FirstDB db, Member member) =>
{
    db.Members.Add(member);
    db.SaveChanges();
});
// Member List
app.MapGet("/members/list", (FirstDB db) =>
{
    Thread.Sleep(1000);
    return db.Members.ToList();
});
// Member Edit
app.MapPost("/members/edit", (FirstDB db, Member member) =>
{
    db.Members.Update(member);
    db.SaveChanges();
});
// Member Remove
app.MapPost("/members/remove/{id}", (FirstDB db, int id) =>
{
    var member = db.Members.Find(id);
    if (member != null)
    {
        db.Members.Remove(member);
        db.SaveChanges();
    }
});
#endregion
#region Borrows
// Borrow Add
app.MapPost("/borrows/add", (FirstDB db, Borrow borrow) =>
{
    db.Borrows.Add(borrow);
    db.SaveChanges();
});
// Borrow List
app.MapGet("/borrow/list", (FirstDB db) =>
{
    Thread.Sleep(1000);
    return db.Borrows.Include(m => m.Book).Include(m => m.Member).ToList();
});
// Borrow Edit
app.MapPost("/borrows/edit", (FirstDB db, Borrow borrow) =>
{
    db.Borrows.Update(borrow);
    db.SaveChanges();
});
// Borrow Remove
app.MapPost("/borrows/remove/{id}", (FirstDB db, int id) =>
{
    var borrow = db.Borrows.Find(id);
    if (borrow != null)
    {
        db.Borrows.Remove(borrow);
        db.SaveChanges();
    }
});
#endregion
app.Run();