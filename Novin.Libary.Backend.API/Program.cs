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
// Book List
app.MapGet("/books/list", (FirstDB db) =>
{
    return db.Books.ToList();
});
// Book Add
app.MapPost("/books/add", (FirstDB db, Book book) =>
{
    db.Books.Add(book);
    db.SaveChanges();
});
// Book Edit    Exersice
app.MapPost("/books/edit", (FirstDB db, Book book) =>
{
    db.Books.Update(book);
    db.SaveChanges();
});
// Book Remove    Exersice
app.MapPost("/books/remove/{id}", (FirstDB db, int id) =>
{
    var book = db.Books.Find(id);
    if (book != null)
    {
        db.Books.Remove(book);
        db.SaveChanges();
    }
});
// Member List
app.MapGet("/members/list", (FirstDB db) =>
{
    return db.Members.ToList();
});
// Member Add
app.MapPost("/members/add", (FirstDB db, Member member) =>
{
    db.Members.Add(member);
    db.SaveChanges();
});
// Member Edit    Exersice
app.MapPost("/members/edit", (FirstDB db, Member member) =>
{
    db.Members.Update(member);
    db.SaveChanges();
});
// Member Remove    Exersice
app.MapPost("/members/remove/{id}", (FirstDB db, int id) =>
{
    var member = db.Members.Find(id);
    if (member != null)
    {
        db.Members.Remove(member);
        db.SaveChanges();
    }
});
// Borrow List
app.MapGet("/borrow/list", (FirstDB db) =>
{
    return db.Borrows.Include(m => m.Book).Include(m => m.Member).ToList();
});
// Borrow Add
app.MapPost("/borrows/add", (FirstDB db, Borrow borrow) =>
{
    db.Borrows.Add(borrow);
    db.SaveChanges();
});
// Borrow Edit    Exersice
app.MapPost("/borrows/edit", (FirstDB db, Borrow borrow) =>
{
    db.Borrows.Update(borrow);
    db.SaveChanges();
});
// Borrow Remove    Exersice
app.MapPost("/borrows/remove/{id}", (FirstDB db, int id) =>
{
    var borrow = db.Borrows.Find(id);
    if (borrow != null)
    {
        db.Borrows.Remove(borrow);
        db.SaveChanges();
    }
});
app.Run();