﻿using System.Collections.Generic;
using UserManagement.Data.Entities;
using UserManagement.Models;

namespace UserManagement.Services.Domain.Interfaces;

public interface IUserService 
{
    /// <summary>
    /// Return users by active state
    /// </summary>
    /// <param name="isActive"></param>
    /// <returns></returns>
    IEnumerable<User> FilterByActive(bool isActive);
    IEnumerable<User> GetAll();
    void Add(User user);
    User? GetById(long id);
    void DeleteUser(User user);
    void UpdateUser(User user);

    IEnumerable<Log> GetLogs();
}
