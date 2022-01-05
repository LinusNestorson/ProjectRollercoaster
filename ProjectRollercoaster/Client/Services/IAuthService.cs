﻿namespace ProjectRollercoaster.Client.Services
{
    using ProjectRollercoaster.Shared;

    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(UserRegister request);
    }
}