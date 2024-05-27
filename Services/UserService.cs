namespace WebApi.Services;

using AadeshPharmaWeb.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models;

public interface IUserService
{
    Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
    Task<AuthenticateResponse> Register(User user, string password);
    //IEnumerable<User> GetAll();
    //User GetById(int id);
    Task<User> GetById(string id);
}

public class UserService : IUserService

    private readonly AppSettings _appSettings;
    private readonly IMongoCollection<User> _userCollection;
    private readonly PasswordHasher<object> _passwordHasher;
    public UserService(IOptions<AppSettings> appSettings, IOptions<AadeshPharmaDatabaseConfiguration> database)
    {
        _appSettings = appSettings.Value;
        var mongoClient = new MongoClient(database.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(database.Value.DatabaseName);
        _userCollection = mongoDatabase.GetCollection<User>(database.Value.UserCollectionName);

        _passwordHasher = new PasswordHasher<object>();

        _appSettings = appSettings.Value;
    }

    public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
    {
        //var user = await _userCollection.Find(u => u.Id == id).FirstOrDefaultAsync(); ;
        
        var user = await _userCollection.Find(u => u.Username == model.Username).FirstOrDefaultAsync();
        if (user == null || !VerifyHashedPassword( user.PasswordHash, model.Password))
            return null;

        // authentication successful so generate jwt token
        var token = generateJwtToken(user);

        return new AuthenticateResponse(user, token);
    }

    public async Task<AuthenticateResponse> Register(User user, string password)
    {
        if (await _userCollection.Find(u => u.Username == user.Username).AnyAsync())
            throw new Exception("Username already exists");

        //user.PasswordHash = CreatePasswordHash(password);
        user.PasswordHash=HashPassword(password);
        user.Id=Guid.NewGuid().ToString();
        await _userCollection.InsertOneAsync(user);

        var token = generateJwtToken(user);
        var response = new AuthenticateResponse(user, token);

        return response;
        //return user;
    }

    public async Task<User> GetById(string id)
    {
        return await _userCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
    }

    private string generateJwtToken(User user)
    {
        // generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    /// <summary>
    /// Hashes a password.
    /// </summary>
    /// <param name="password">The password to hash.</param>
    /// <returns>The hashed password.</returns>
    public string HashPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
            throw new ArgumentException("Password cannot be null or empty.", nameof(password));

        // Create a dummy user object; it's not used but required by PasswordHasher
        var user = new object();
        return _passwordHasher.HashPassword(user, password);
    }

    /// <summary>
    /// Verifies a password against a hashed password.
    /// </summary>
    /// <param name="hashedPassword">The hashed password.</param>
    /// <param name="password">The password to verify.</param>
    /// <returns>True if the password matches the hashed password; otherwise, false.</returns>
    public bool VerifyHashedPassword(string hashedPassword, string password)
    {
        if (string.IsNullOrEmpty(hashedPassword))
            throw new ArgumentException("Hashed password cannot be null or empty.", nameof(hashedPassword));

        if (string.IsNullOrEmpty(password))
            throw new ArgumentException("Password cannot be null or empty.", nameof(password));

        // Create a dummy user object; it's not used but required by PasswordHasher
        var user = new object();
        var result = _passwordHasher.VerifyHashedPassword(user, hashedPassword, password);
        return result == PasswordVerificationResult.Success;
    }
}