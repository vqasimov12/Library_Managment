using Dapper;
using Domain.Entities;
using Domain.Enums;
using Repository.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DAL.SqlServer.Infrustructure;

public class SqlUserRepository(string connectionString) : BaseSqlRepository(connectionString), IUserRepository
{
    public async Task AddUserAsync(User user)
    {
        var sql = @"
        INSERT INTO Users (Name, Username, Fathername, Email, PasswordHash, Address, 
                           MobilePhone, CardNumber, Note, BirthDay, DateOfEmployment, 
                           DateOfDismissal, Gender, UserType, CreatedDate, UpdatedDate, 
                           DeletedDate, IsDeleted, CreatedBy, UpdatedBy, DeletedBy)
        OUTPUT INSERTED.Id
        VALUES (@Name, @Username, @Fathername, @Email, @PasswordHash, @Address, 
                @MobilePhone, @CardNumber, @Note, @BirthDay, @DateOfEmployment, 
                @DateOfDismissal, @Gender, @UserType, @CreatedDate, @UpdatedDate, 
                @DeletedDate, @IsDeleted, @CreatedBy, @UpdatedBy, @DeletedBy); SELECT SCOPE_IDENTITY()";

        using var conn = OpenConnection();

        user.Id = await conn.ExecuteScalarAsync<int>(sql, new
        {
            user.Name,
            user.Username,
            user.Fathername,
            user.Email,
            user.PasswordHash,
            user.Address,
            user.MobilePhone,
            user.CardNumber,
            user.Note,
            user.BirthDay,
            user.DateOfEmployment,
            user.DateOfDismissal,
            Gender = (int)user.Gender,
            UserType = (int)user.UserType,
            user.CreatedDate,
            user.UpdatedDate,
            user.DeletedDate,
            user.IsDeleted,
            user.CreatedBy,
            user.UpdatedBy,
            user.DeletedBy
        });
    }

    public async Task<bool> DeleteUserAsync(int id, int deletedBy)
    {
        var checkSql = "SELECT Id FROM Users WHERE Id=@id AND IsDeleted=0";

        var sql = @"UPDATE Users
                SET IsDeleted=1,
                    DeletedBy=@deletedBy,
                    DeletedDate=GETDATE()
                WHERE Id=@id AND IsDeleted=0";

        using var conn = OpenConnection();
        using var transaction = conn.BeginTransaction();

        var userId = await conn.ExecuteScalarAsync<int?>(checkSql, new { id }, transaction);

        if (!userId.HasValue)
            return false;

        var affectedRow = await conn.ExecuteAsync(sql, new { id, deletedBy }, transaction);

        transaction.Commit();
        return affectedRow > 0;
    }

    public async Task<IQueryable<User>> GetAll()
    {
        var sql = "SELECT * FROM Users WHERE IsDeleted=0";
        using var conn = OpenConnection();
        var users = conn.Query<User>(sql).ToList();
        return users.AsQueryable();
    }

    public async Task<IEnumerable<User>> GetByFathernameAsync(string fathername)
    {
        var sql = "SELECT * FROM Users WHERE Fathername=@fathername AND IsDeleted=0";
        using var conn = OpenConnection();
        return await conn.QueryAsync<User>(sql, new { fathername });
    }

    public async Task<IEnumerable<User>> GetByGenderAsync(Gender gender)
    {
        var sql = "SELECT * FROM Users WHERE Gender = @gender AND IsDeleted = 0";
        using var conn = OpenConnection();
        return await conn.QueryAsync<User>(sql, new { gender = (int)gender });
    }

    public async Task<IEnumerable<User>> GetByNameAsync(string name)
    {
        var sql = "SELECT * FROM Users WHERE Name=@name AND IsDeleted=0";
        using var conn = OpenConnection();
        return await conn.QueryAsync<User>(sql, new { name });
    }

    public async Task<User> GetByUsernameAsync(string username)
    {
        var sql = "SELECT * FROM Users WHERE Username=@username AND IsDeleted=0";
        using var conn = OpenConnection();
        return await conn.QueryFirstOrDefaultAsync<User>(sql, new { username })!;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        var sql = "SELECT * FROM Users WHERE Email=@email AND IsDeleted=0";
        using var conn = OpenConnection();
        return await conn.QueryFirstOrDefaultAsync<User>(sql, new { email })!;
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        var sql = "SELECT * FROM Users WHERE Id=@id AND IsDeleted=0";
        using var conn = OpenConnection();
        return await conn.QueryFirstOrDefaultAsync<User>(sql, new { id })!;
    }

    public async Task<bool> UpdateUserAsync(User user)
    {
        var sql = @"
        UPDATE Users
        SET 
            Name = @Name,
            Username = @Username,
            Fathername = @Fathername,
            Email = @Email,
            PasswordHash = @PasswordHash,
            Address = @Address,
            MobilePhone = @MobilePhone,
            CardNumber = @CardNumber,
            Note = @Note,
            BirthDay = @BirthDay,
            DateOfEmployment = @DateOfEmployment,
            DateOfDismissal = @DateOfDismissal,
            Gender = @Gender,
            UserType = @UserType,
            UpdatedBy = @UpdatedBy,
            UpdatedDate = GETDATE()
        WHERE Id = @Id AND IsDeleted = 0";

        using var conn = OpenConnection();
        var affectedRows = await conn.ExecuteAsync(sql, new
        {
            user.Name,
            user.Username,
            user.Fathername,
            user.Email,
            user.PasswordHash,
            user.Address,
            user.MobilePhone,
            user.CardNumber,
            user.Note,
            user.BirthDay,
            user.DateOfEmployment,
            user.DateOfDismissal,
            Gender = (int)user.Gender,
            UserType = (int)user.UserType,
            user.UpdatedBy,
            user.Id
        });

        return affectedRows > 0;
    }

}
