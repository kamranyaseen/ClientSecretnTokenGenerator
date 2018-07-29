﻿using ClientSecretnTokenGenerator.Models;
using System;

namespace ClientSecretnTokenGenerator.Repository
{
    public interface IAuthenticate
    {
        ClientKey GetClientKeysDetailsbyCLientIDandClientSecert(string clientID , string clientSecert);
        bool ValidateKeys(ClientKey ClientKeys);
        bool IsTokenAlreadyExists(int CompanyID);
        int DeleteGenerateToken(int CompanyID);
        int InsertToken(TokensManager token);
        string GenerateToken(ClientKey ClientKeys, DateTime IssuedOn);
    }
}
