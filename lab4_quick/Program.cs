using Microsoft.EntityFrameworkCore;
using lab4_quick.Domain.Interfaces;
using lab4_quick.Infrastructure;
using lab4_quick.Domain.Services;
using lab4_quick.Domain.Interfaces.IUser;
using lab4_quick.Domain.Interfaces.IMeeting;
using lab4_quick.Domain.Interfaces.IInvitation;
using lab4_quick.Domain.Interfaces.IReminder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IMeetingService, MeetingService>();
builder.Services.AddTransient<IMeetingRepository, MeetingRepository>();
builder.Services.AddTransient<IInvitationService, InvitationService>();
builder.Services.AddTransient<IInvitationRepository, InvitationRepository>();
builder.Services.AddTransient<IReminderService, ReminderService>();
builder.Services.AddTransient<IReminderRepository, ReminderRepository>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddDbContext<QuickMeetingsContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
