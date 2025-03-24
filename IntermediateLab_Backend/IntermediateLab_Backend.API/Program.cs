using System.Net;
using System.Net.Mail;
using IntermediateLab_Backend.Application.Interfaces;
using IntermediateLab_Backend.Application.Interfaces.Repositories;
using IntermediateLab_Backend.Application.Interfaces.Service;
using IntermediateLab_Backend.Application.Services;
using IntermediateLab_Backend.Infrastructure;
using IntermediateLab_Backend.Infrastructure.Repositories;
using IntermediateLab_Backend.Infrastructure.Smtp;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LaboContext>(
										   options =>
											   options.UseSqlServer(builder.Configuration.GetConnectionString("Main")));


builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<ITournamentService, TournamentService>();
builder.Services.AddScoped<ITournamentRepository, TournamentRepository>();

builder.Services.AddScoped(_ => new SmtpClient
								{
									Host = builder.Configuration["Smtp:Host"] ??
										   throw new Exception("Missing Smtp Configuration"),
									Port = int.Parse(builder.Configuration["Smtp:Port"] ??
													 throw new Exception("Missing Smtp Configuration")),
									Credentials = new NetworkCredential
												  {
													  UserName = builder.Configuration["Smtp:Username"] ??
																 throw new Exception("Missing Smtp Configuration"),
													  Password = builder.Configuration["Smtp:Password"] ??
																 throw new Exception("Missing Smtp Configuration"),
												  }
								});

builder.Services.AddScoped<IMailer, Mailer>();
builder.Services.AddCors(b => b.AddDefaultPolicy(o =>
												 {
													 o.AllowAnyMethod();
													 o.AllowAnyOrigin();
													 o.AllowAnyHeader();
												 }));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();