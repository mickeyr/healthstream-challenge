using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using HealthStream.Data.Entities;
using HealthStream.Data.Extensions;

namespace HealthStream.Data.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            AutoMapper.Mapper.CreateMap<IDataReader, User>();
        }

        public User Get(int id)
        {
            using (var command = UnitOfWork.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Users WHERE Id = @id";
                command.AddParamaters(new
                {
                    id = id
                });

                using (var reader = command.ExecuteReader())
                {
                    return AutoMapper.Mapper.Map<IDataReader, IList<User>>(reader).SingleOrDefault();
                }
            }
        }

        public IEnumerable<User> GetAll()
        {
            using (var command = UnitOfWork.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Users";
                using (var reader = command.ExecuteReader())
                {
                    return AutoMapper.Mapper.Map<IDataReader, IList<User>>(reader);
                }
            }
        }

        public void Delete(int id)
        {
            using (var command = UnitOfWork.CreateCommand())
            {
                command.CommandText = "DELETE FROM Users WHERE Id = @id";
                command.AddParamaters(new
                {
                    id = id
                });

                command.ExecuteNonQuery();
            }
        }

        public void Udate(User entity)
        {
            using (var command = UnitOfWork.CreateCommand())
            {
                command.CommandText = @"UPDATE Users
                                            SET Username = @Username,
                                            EmailAddress = @EmailAddress, 
                                            PasswordHash = @PasswordHash,
                                            ModifiedOn = GETUTCDATE()  
                                        WHERE Id = @id";

                command.AddParamaters(new
                {
                    Username = entity.Username,
                    EmailAddress = entity.EmailAddress,
                    PasswordHash = entity.PasswordHash,
                    id = entity.Id
                });
              
                command.ExecuteNonQuery();
            }
        }

        public void Insert(User entity)
        {
            using (var command = UnitOfWork.CreateCommand())
            {
                command.CommandText = @"INSERT INTO Users(Username, EmailAddress, PasswordHash)  
                                             VALUES(@Username, @EmailAddress, @PasswordHash); SELECT @@IDENTITY AS Id;";
                command.AddParamaters(new {
                    Username = entity.Username,
                    EmailAddress = entity.EmailAddress,
                    PasswordHash = entity.PasswordHash
                });
             
                var ret = command.ExecuteScalar().ToString();
                int id;
                Int32.TryParse(ret, out id);
                entity.Id = id;
            }
        }

        public void SaveChanges()
        {
            UnitOfWork.SaveChanges();
        }

        public User GetByUsername(string username)
        {
            using (var command = UnitOfWork.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Users WHERE Username = @Username";
                command.AddParamaters(new {
                    Username = username
                });

                using (var reader = command.ExecuteReader())
                {
                    return AutoMapper.Mapper.Map<IDataReader, IList<User>>(reader).SingleOrDefault();
                }
            }
        }
    }
}
