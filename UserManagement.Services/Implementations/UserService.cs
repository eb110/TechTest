﻿using System;
using System.Collections.Generic;
using UserManagement.Data;
using UserManagement.Data.Entities;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Services.Domain.Implementations;

public class UserService : IUserService
{
    private readonly IDataContext _dataAccess;
    public UserService(IDataContext dataAccess) => _dataAccess = dataAccess;

    public void Add(User user)
    {
        user.IsActive = false;
        _dataAccess.Create(user);
    }

    public void DeleteUser(User user) => _dataAccess.Delete(user);

    /// <summary>
    /// Return users by active state
    /// </summary>
    /// <param name="isActive"></param>
    /// <returns></returns>
    public IEnumerable<User> FilterByActive(bool isActive)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<User> GetAll() => _dataAccess.GetAll<User>();
    public User? GetById(long id) => _dataAccess.GetById<User>(id);
    public IEnumerable<Log> GetLogs() => _dataAccess.GetAll<Log>();
    public void UpdateUser(User user) => _dataAccess.Update(user);
}
