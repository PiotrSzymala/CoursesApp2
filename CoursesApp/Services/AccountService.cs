using CoursesApp.Data;
using CoursesApp.Models;
using CoursesApp.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CoursesApp.Services
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
        bool AuthenticateUser(string username, string password);
    }

    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;

        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void RegisterUser(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                Email = dto.Email,
                RoleID = dto.RoleId,
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        public bool AuthenticateUser(string username, string password)
        {
            // Tutaj dodaj logikę uwierzytelniania użytkownika
            // Na przykład, możesz użyć LINQ do sprawdzenia, czy istnieje użytkownik o podanym loginie i hasle

            var user = _context.Users
                .SingleOrDefault(u => u.Email == username && u.Password == password);

            return user != null; // Zwróć true, jeśli użytkownik istnieje, w przeciwnym razie false
        }
    }
}
